Imports System.Uri
Imports System.IO.Path

Public Module GetExecutingPath_
    Public Function GetExecutingPath() As String

        Return AppContext.BaseDirectory.TrimEnd("\")
        ' Old style, depreciated
        'Dim codeBase As String = System.Reflection.Assembly.GetCallingAssembly().CodeBase
        'Dim uri As New UriBuilder(codeBase)
        'Dim path As String = UnescapeDataString(uri.Path)
        'Dim result As String = GetDirectoryName(path)
        'Return result

    End Function

End Module
