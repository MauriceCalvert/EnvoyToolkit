Imports System.Text
Imports Common
Imports System.Linq
Public Class ReportOptions

    <CommandLine.Option("s"c, "start",
    HelpText:="Start date YYYY-MM-DD. Defaults to yesterday")>
    Public Property Start As Date

    <CommandLine.Option("f"c, "finish",
    HelpText:="Start date YYYY-MM-DD. Defaults to midnight yesterday")>
    Public Property Finish As Date

    <CommandLine.Option("c"c, "config", [Default]:="configuration.json",
    HelpText:="Configuration JSON file. Defaults to configuration.json")>
    Public Property Config As String

    <CommandLine.Option("g"c, "grouping",
    HelpText:="Sets how readings are summed: hourly, daily or monthly. Defaults to hourly")>
    Public Property Grouping As String

    <CommandLine.Option("d"c, "days", SetName:="last",
    HelpText:="Plot N previous days")>
    Public Property Days As Integer

    <CommandLine.Option("m"c, "months", SetName:="last",
    HelpText:="Plot N previous months")>
    Public Property Months As Integer

    <CommandLine.Option("y"c, "years", SetName:="last",
    HelpText:="Plot N previous years")>
    Public Property Years As Integer

    <CommandLine.Option("o"c, "output", [Default]:={"email"},
    HelpText:="Any of: email, image, csv. Defaults to email")>
    Public Property Output As IEnumerable(Of String)

    <CommandLine.Option("p"c, "plot", [Default]:="1024,768",
    HelpText:="Image size, defaults to 1024,768")>
    Public Property Plot As String

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