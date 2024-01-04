Imports System.IO
Public Class Config

    Public Sub New(path As String)

        InitializeComponent()

        Dim fileContent As String = File.ReadAllText(path)

        TextBox1.Text = fileContent

        TextBox1.Select(0, 0)

    End Sub
End Class