Imports Common
Imports Microsoft.EntityFrameworkCore
Imports NLog

Namespace Models

    Partial Public Class EnphaseContext
        Inherits DbContext

        Private Property Logger As Logger = LogManager.GetCurrentClassLogger
        Private ReadOnly Property Config As Configuration
        Public Sub New(config As Configuration)

            _Config = config

        End Sub
        Protected Overrides Sub OnConfiguring(optionsBuilder As DbContextOptionsBuilder)

            With optionsBuilder

                .EnableDetailedErrors(True)
                .EnableSensitiveDataLogging(True)

                Dim cs As String = $"Server={Config.SQLServer};Database={Config.Database};TrustServerCertificate=True; "

                If String.IsNullOrWhiteSpace(Config.SQLUserID) Then
                    cs &= "integrated security=True; "
                Else
                    cs &= $"User ID={Config.SQLUserID};Password={Config.SQLPassword}; "
                End If

                Logger.Info($"Connection string {cs}")

                optionsBuilder.UseSqlServer(cs)

            End With

            MyBase.OnConfiguring(optionsBuilder)

        End Sub

    End Class

End Namespace
