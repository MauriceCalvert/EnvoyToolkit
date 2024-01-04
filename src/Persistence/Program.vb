Imports Common
Imports Config.Net
Imports Microsoft.EntityFrameworkCore.Metadata
Imports Persistence.Models
Module Program
    Sub Main()

        ISO8601Culture()

        Dim builder As New ConfigurationBuilder(Of Configuration)
        Dim config As Configuration = builder.UseIniFile("configuration.ini").Build()

        Using context As New EnphaseContext(config)

            Dim x As IModel = context.Model

            Dim y As Integer = 0 ' breakpoint here!

        End Using

    End Sub
End Module
