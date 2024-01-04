Imports System.Threading
Imports CommandLine
Imports Common
Imports Microsoft.Extensions.Hosting
Imports NLog

Public NotInheritable Class Worker
    Inherits BackgroundService

    Private ReadOnly Logger As Logger = LogManager.GetCurrentClassLogger
    Private ReadOnly Property Monitor As Monitor
    Public Sub New()
        Dim args As String() = Environment.GetCommandLineArgs()
        Dim result As ParserResult(Of MonitorOptions) = Parser.Default.ParseArguments(Of MonitorOptions)(args)
        Dim mo As MonitorOptions = result.Value
        Dim config As New Configuration(mo.Config)
        Monitor = New Monitor(config)

    End Sub
    Protected Overrides Async Function ExecuteAsync(ByVal stoppingToken As CancellationToken) As Task

        Logger.Info("Worker is starting")

        Try
            Dim monitortask As Task = Task.Run(
                Sub()
                    Monitor.Run(stoppingToken)
                End Sub)

            While Not stoppingToken.IsCancellationRequested

                Await Task.Delay(Timeout.InfiniteTimeSpan, stoppingToken)

            End While

        Catch ex As OperationCanceledException
            Logger.Warn("Worker cancelled")

        Catch ex As Exception
            Fatal(Logger, Monitor.Config, ex)

        End Try
    End Function

End Class
