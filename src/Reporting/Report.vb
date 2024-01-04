Imports Persistence.Models
Imports System.IO
Imports ScottPlot
Imports Common
Imports System.Drawing
Imports MimeKit
Imports System.Linq
Imports nlog
Public Class Report

    Dim logger As Logger = LogManager.GetCurrentClassLogger
    Public ReadOnly Property Production As Double
    Public ReadOnly Property Consumption As Double
    Public ReadOnly Property Image As Bitmap
    Public ReadOnly Property Start As DateTimeOffset
    Public ReadOnly Property Finish As DateTimeOffset
    Public ReadOnly Property Grouping As String
    Public ReadOnly Property Dataset As List(Of TPower)
    Public ReadOnly Property Stamp As DateTimeOffset
    Public ReadOnly Property CSVFile As String

    Public Sub New(stamp As DateTimeOffset)

        _Stamp = stamp

    End Sub
    Public Sub Build(config As Configuration, options As ReportOptions)

        _Grouping = options.Grouping

        If options.Start = DateTime.MinValue Then

            If options.Finish = DateTime.MinValue Then ' midnight yesterday
                _Finish = New DateTimeOffset(DateTime.SpecifyKind(New DateTime(Stamp.Year, Stamp.Month, Stamp.Day), DateTimeKind.Local))
            Else
                _Finish = DateTime.SpecifyKind(options.Finish, DateTimeKind.Local)
            End If
            _Start = _Finish.AddDays(-1)

        ElseIf options.Finish = DateTime.MinValue Then

            _Start = DateTime.SpecifyKind(options.Start, DateTimeKind.Local)
            _Finish = _Start.AddDays(1)

        Else
            _Start = DateTime.SpecifyKind(options.Start, DateTimeKind.Local)
            _Finish = DateTime.SpecifyKind(options.Finish, DateTimeKind.Local)
        End If

        If options.Days > 0 Then

            _Start = Finish.AddDays(-options.Days)
            _Grouping = If(_Grouping = "", "hourly", _Grouping)

        ElseIf options.Months > 0 Then

            _Finish = New DateTimeOffset(New DateTime(Finish.Year, Finish.Month, 1)) ' midnight on the last day of the preceding month
            _Start = Finish.AddMonths(-options.Months)
            _Grouping = If(_Grouping = "", "daily", _Grouping)

        ElseIf options.Years > 0 Then

            _Finish = New DateTimeOffset(New DateTime(Finish.Year, 1, 1)) ' midnight New Year's eve
            _Start = Finish.AddYears(-options.Years)
            _Grouping = If(_Grouping = "", "monthly", _Grouping)

        End If

        If String.IsNullOrWhiteSpace(_Grouping) Then
            _Grouping = "hourly"
        End If

        Dim grouper As Func(Of DateTimeOffset, DateTimeOffset)

        Select Case Grouping.ToLower

            Case "hourly"
                grouper = Function(start As DateTimeOffset) As DateTimeOffset
                              Return start.AddHours(1)
                          End Function
            Case "daily"
                grouper = Function(start As DateTimeOffset) As DateTimeOffset
                              Return start.AddDays(1)
                          End Function
            Case "monthly"
                grouper = Function(start As DateTimeOffset) As DateTimeOffset
                              Return start.AddMonths(1)
                          End Function
            Case Else
                Throw New Exception("$Invalid grouping {grouping}")

        End Select

        logger.Info($"Adjusted start {Start} finish {Finish} grouping {Grouping}")

        Using context As New EnphaseContext(config)

            Dim utcstart As DateTimeOffset = Start.ToUniversalTime
            Dim utcfinish As DateTimeOffset = Finish.ToUniversalTime

            _Dataset = context.
                       TPowers.
                       Where(Function(q) q.Start >= utcstart AndAlso q.Finish < utcfinish).
                       OrderBy(Function(q) q.Start).
                       ThenBy(Function(q) q.Finish).
                       ThenBy(Function(q) q.Category).
                       ToList

            If Not Dataset.Any Then
                Throw New Exception("No data in specified period of time")
            End If

            _Production = Dataset.Where(Function(q) q.Category = 1 OrElse q.Category = 3).Sum(Function(q) q.WattHours)
            _Consumption = Dataset.Where(Function(q) q.Category = 2 OrElse q.Category = 4).Sum(Function(q) q.WattHours)

            _Image = CreatePlot(options, grouper)

        End Using

    End Sub
    Function CreatePlot(options As ReportOptions, grouper As Func(Of DateTimeOffset, DateTimeOffset)) As Bitmap

        Dim prodx As New List(Of Double)
        Dim consx As New List(Of Double)
        Dim prody As New List(Of Double)
        Dim consy As New List(Of Double)
        Dim prodtotal As Double = 0
        Dim constotal As Double = 0
        Dim periodstart As DateTimeOffset = Start
        Dim periodfinish As DateTimeOffset = grouper(periodstart)

        For Each power As TPower In Dataset

            Dim start As DateTimeOffset = DateTime.SpecifyKind(power.Start, DateTimeKind.Utc).ToLocalTime

            Dim finish As DateTimeOffset = DateTime.SpecifyKind(power.Finish, DateTimeKind.Utc).ToLocalTime

            If start > periodfinish Then

                prodx.Add(finish.DateTime.ToOADate)
                prody.Add(prodtotal / 1000)

                consx.Add(finish.DateTime.ToOADate)
                consy.Add(constotal / 1000)

                periodstart = periodfinish
                periodfinish = grouper(periodstart)
                prodtotal = 0
                constotal = 0
            End If

            If power.WattHours > 0 Then

                If power.Category = 1 OrElse power.Category = 3 Then
                    prodtotal += power.WattHours
                End If

                If power.Category = 2 OrElse power.Category = 4 Then
                    constotal += power.WattHours
                End If
            End If
        Next

        Dim plot As New Plot(800, 600)

        If prodx.Any Then
            plot.AddScatter(prodx.ToArray, prody.ToArray, label:="Production")

        ElseIf Not consx.Any Then
            Throw New Exception($"No results with grouping {Grouping}")
        End If

        plot.AddScatter(consx.ToArray, consy.ToArray, label:="Consumption")

        Dim prod As String = FormatWatts(Production)
        Dim cons As String = FormatWatts(Consumption)

        plot.Title($"{Start:yyyy-MM-dd} .. {Finish:yyyy-MM-dd} Produced {prod} Consumed {cons}")
        plot.YLabel("KWh " & Grouping)
        plot.Palette = Palette.Category20
        plot.XAxis.DateTimeFormat(True)
        plot.Legend()

        Dim size() As String = options.Plot.Split(","c)
        Dim bmp As New Bitmap(CInt(size(0)), CInt(size(1)))
        bmp = plot.Render(bmp)

        Return bmp

    End Function
    Public Sub WriteCSV()

        _CSVFile = $"{NLogPath()}\{DateTime.Now:yyyyMMdd_HHmmss}.csv"

        Using sw As New StreamWriter(CSVFile)

            sw.WriteLine("Start,Finish,Category,WattHours")

            For Each tpower As TPower In Dataset

                With tpower

                    Dim start As DateTimeOffset = DateTime.SpecifyKind(tpower.Start, DateTimeKind.Utc).ToLocalTime
                    Dim finish As DateTimeOffset = DateTime.SpecifyKind(tpower.Finish, DateTimeKind.Utc).ToLocalTime


                    Dim cat As String = If(.Category Mod 2 = 0, "cons", "prod")
                    sw.WriteLine($"{ start.DateTime.ToOADate},{ finish.DateTime.ToOADate},{ cat},{ .WattHours:0.000}")
                End With
            Next
            logger.Info($"Wrote data {DirectCast(sw.BaseStream, FileStream).Name}")
            sw.Close()
        End Using

    End Sub

End Class
