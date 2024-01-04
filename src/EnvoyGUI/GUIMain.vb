Imports NLog
Imports CommandLine
Imports Common
Imports Reporting
Public Class GUIMain

    Private Options As ReportOptions
    Private Config As Configuration
    Private Ready As Boolean
    Private Logger As Logger = LogManager.GetCurrentClassLogger
    Private Sub GUIMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try

            ISO8601Culture()
            ToolStrip1.Renderer = New TSRenderer

            Dim args As String() = Environment.GetCommandLineArgs()
            Dim result As ParserResult(Of ReportOptions) = Parser.Default.ParseArguments(Of ReportOptions)(args)
            Options = result.Value
            Config = New Configuration(Options.Config)

            Logger.Info($"EnvoyGUI is starting with {Options}")

            Dim stamp As DateTime = Date.Now.Date
            dpFinish.Text = stamp.ToString
            dpStart.Text = stamp.AddDays(-1).ToString
            cmbGrouping.SelectedItem = "Hourly"
            Ready = True

        Catch ex As Exception
            MessageBox.Show(Me, ex.Message, "Oops, a SNAFU")

        End Try

    End Sub
    Private Sub btnConfig_Click(sender As Object, e As EventArgs) Handles btnConfig.Click

        Dim configform As New Config(Config.GetFullPath)

        configform.ShowDialog()

    End Sub
    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnPlot.Click

        Try
            Dim report As Report = BuildReport()
            PictureBox1.Image = report.Image

        Catch ex As Exception
            MessageBox.Show(Me, ex.Message, "Oops, a SNAFU", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try

    End Sub
    Private Sub btnCSV_Click(sender As Object, e As EventArgs) Handles btnCSV.Click

        Try
            Dim report As Report = BuildReport()
            report.WriteCSV()
            MessageBox.Show(Me, $"{report.CSVFile}", "File saved", MessageBoxButtons.OK, MessageBoxIcon.Information)


        Catch ex As Exception
            MessageBox.Show(Me, ex.Message, "Oops, a SNAFU", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try

    End Sub
    Private Function BuildReport() As Report

        Dim start As DateTime = DateTime.Parse(dpStart.Text)
        Dim finish As DateTime = DateTime.Parse(dpFinish.Text)
        Dim grouping As String = cmbGrouping.SelectedItem.ToString
        With Options
            .Grouping = grouping
            .Start = start
            .Finish = finish
            .Plot = PictureBox1.Width.ToString & "," & PictureBox1.Height.ToString
        End With
        Dim report As New Report(start)
        report.Build(Config, Options)

        Return report

    End Function
    Private Sub GUIMain_ResizeEnd(sender As Object, e As EventArgs) Handles MyBase.ResizeEnd

        If Ready Then
            btnGo_Click(sender, e)
        End If

    End Sub

End Class
