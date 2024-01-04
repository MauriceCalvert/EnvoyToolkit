Imports System.Text
Imports Common

Public Class MonitorOptions
    <CommandLine.Option("c"c, "config", [Default]:="configuration.json",
    HelpText:="Configuration JSON file. Defaults to configuration.json")>
    Public Property Config As String

    Public Overrides Function ToString() As String

        Dim sb As New StringBuilder

        For Each opt As KeyValuePair(Of String, Object) In GetProperties(Me)

            If opt.Value Is Nothing Then
                sb.Append($"{opt.Key} = [omitted] ")

            ElseIf opt.Value.GetType.IsArray Then

                sb.Append($"{opt.Key} = ")
                Dim vals As New List(Of String)(DirectCast(opt.Value, String()))
                For Each val As String In vals
                    sb.Append($"{val} ")
                Next
            Else
                sb.Append($"{opt.Key} = {opt.Value} ")
            End If
        Next

        Return sb.ToString()

    End Function

End Class