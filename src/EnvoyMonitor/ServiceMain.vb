Imports CommandLine
Imports Common
Imports Microsoft.Extensions.DependencyInjection
Imports Microsoft.Extensions.Hosting
Imports NLog
Public Module ServiceMain

    Private Property Logger As Logger = LogManager.GetCurrentClassLogger
    Private Property Config As Configuration

    Public Sub Main()

        Try
            Logger.Info("Monitor service is starting")

            Dim builder As HostApplicationBuilder = Host.CreateApplicationBuilder()
            builder.Services.AddWindowsService(Sub(serviceoptions)
                                                   serviceoptions.ServiceName = "EnvoyMonitor"
                                               End Sub)
            builder.Services.AddSingleton(Of Worker)
            builder.Services.AddHostedService(Of Worker)
            Dim monitorhost As IHost = builder.Build
            monitorhost.Run

            Logger.Info("Monitor service has stopped")

        Catch ex As Exception
            Fatal(Logger, Config, ex)

        End Try
    End Sub
End Module
