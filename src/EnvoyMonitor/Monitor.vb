Imports System.Net.Http
Imports System.Threading
Imports Common
Imports NLog
Imports Persistence.Models
Public Class Monitor

    Public ReadOnly Property Readings As New Dictionary(Of TimeSpan, Reading)
    Private Property Logger As Logger = LogManager.GetCurrentClassLogger
    Public ReadOnly Property Config As Configuration
    Private Property Envoy As Envoy
    Private Property Current As Reading
    Private Property Previous As Reading

    Public Sub New(config As Configuration)

        Logger.Debug("Monitor created")

        _Config = config

    End Sub
    Private ReadOnly Property OneSecond As New TimeSpan(0, 0, 1)
    Public Async Sub Run(ByVal stoppingToken As CancellationToken)

        Try
            Logger.Info("Monitor is starting")

            Envoy = New Envoy(Config)
            Previous = ReadMeters(Config)

            Do
                Dim snooze As TimeSpan = Delay()

                Logger.Debug($"Monitor delay {snooze}")

                Await Task.WhenAny(Task.Delay(snooze, stoppingToken)) ' swallow TaskCancelledException, it doesn't apply to us

                If stoppingToken.IsCancellationRequested Then
                    Logger.Info("Cancel requested")
                    Exit Do
                End If

                Current = ReadMeters(Config)

                Current.Start = Previous.Finish
                Current.Production = Current.ProdWHLifetime - Previous.ProdWHLifetime
                Current.Consumption = Current.ConsWHLifetime - Previous.ConsWHLifetime

                Readings.Add(Current.Finish.TimeOfDay, Current)

                Logger.Info("At {0} Produced {1:#,##0} Consumed {2:#,##0} in {3} seconds", Current.Finish, Current.Production, Current.Consumption, (Current.Finish - Current.Start).TotalSeconds)

                Using context As New EnphaseContext(Config)

                    If context.TPowers.Any(Function(q) q.Start < Current.Finish And q.Finish > Current.Start) Then
                        Exit Sub
                    End If

                    Dim start As DateTime = Current.Start.Truncate(OneSecond)
                    start = DateTime.SpecifyKind(start, DateTimeKind.Local)
                    Dim startutc As DateTime = start.ToUniversalTime

                    Dim finish As DateTime = Current.Finish.Truncate(OneSecond)
                    finish = DateTime.SpecifyKind(finish, DateTimeKind.Local)
                    Dim finishutc As DateTime = finish.ToUniversalTime

                    ' 3=NewProd, 4=NewCons
                    If Current.Production > 0 Then
                        context.TPowers.Add(New TPower With {.Category = 3, .Start = startutc, .Finish = finishutc, .WattHours = CInt(Current.Production)})
                    End If
                    If Current.Consumption > 0 Then
                        context.TPowers.Add(New TPower With {.Category = 4, .Start = startutc, .Finish = finishutc, .WattHours = CInt(Current.Consumption)})
                    End If
                    context.SaveChanges()

                End Using

                Envoy.CheckInverters()

                Previous = Current

            Loop

        Catch ex As HttpRequestException
            Fatal(Logger, Config, ex, "Communication with Envoy failed")

        Catch ex As Exception
            Fatal(Logger, Config, ex)

        End Try

    End Sub
    Private Function Delay() As TimeSpan

        Dim interval As Integer = CInt(Config.Interval.TotalSeconds)
        Dim stamp As DateTime = DateTime.Now
        Dim seconds As Double = (stamp - GENESIS).TotalSeconds
        Dim before As Double = Math.Floor(seconds / interval) * interval
        Dim result As Integer = CInt(interval - (seconds - before))

        Debug.Assert(result <= interval, $"SNAFU in delay calculation {result}")

        Return New TimeSpan(0, 0, result)

    End Function

End Class
