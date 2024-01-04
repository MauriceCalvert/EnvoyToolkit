Imports System.Drawing
Imports System.IO
Imports Common
Imports MimeKit
Imports ScottPlot
Imports CommandLine
Imports CommandLine.Text
Imports NLog
Imports Persistence.Models
Imports Reporting

Public Module ReporterMain
    Private ReadOnly Property Logger As Logger = LogManager.GetCurrentClassLogger
    Private ReadOnly Property Config As Configuration
    Public Sub Main(args As String())

        Try
            ISO8601Culture()

            'args = "--days 1 -s 2023-04-02 --grouping hourly --output image".Split(" "c)
            'args = "-s 2023-06-01 -f 2023-06-30 -g daily -o image -p 1024,768".Split(" "c)
            'args = "-s 2020-09-01 -f 2023-11-30 -g monthly -o image email csv".Split(" "c)

            Dim result As ParserResult(Of ReportOptions) = Parser.Default.ParseArguments(Of ReportOptions)(args)
            Dim options As ReportOptions = result.Value

            result.WithNotParsed(
                Sub(errors)
                    Dim ht As HelpText = HelpText.AutoBuild(result, Function(h) HelpText.DefaultParsingErrorsHandler(result, h), Function(e) e)
                    Console.WriteLine($"Invalid command line {ht}")
                    Logger.Fatal(ht)
                    Environment.Exit(1)
                End Sub)

            Logger.Info($"EnvoyReporter is starting with {options}")

            _Config = New Configuration(options.Config)

            Dim stamp As New DateTimeOffset(DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Local))

            Dim report As New Report(stamp)

            report.Build(config, options)

            For Each output As String In options.Output

                Select Case output

                    Case "email"
                        SendEmail(config, $"Prod {FormatWatts(report.Production)} Cons {FormatWatts(report.Consumption)}", report.Image)

                    Case "image"
                        Dim file As String = $"{NLogPath()}\{stamp:yyyyMMdd_HHmmss}.jpg"
                        report.Image.Save(file, System.Drawing.Imaging.ImageFormat.Jpeg)
                        Logger.Info($"Wrote chart {Path.GetFullPath(file)}")

                    Case "csv"
                        report.WriteCSV()

                    Case Else
                        Throw New Exception($"Invalid Output {options.Output}")
                End Select

            Next output

        Catch ex As Exception

            Fatal(Logger, Config, ex)

        End Try
    End Sub
End Module

