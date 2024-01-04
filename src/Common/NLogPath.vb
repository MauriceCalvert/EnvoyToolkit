Imports NLog
Imports NLog.Config
Imports NLog.Targets
Imports System.IO
Public Module NLogPath_

    Public Function NLogPath() As String

        Dim result As String = GetExecutingPath()

        Try
            result = LogManager.
                Configuration.
                AllTargets.
                OfType(Of FileTarget).
                Select(Function(q) q.FileName.Render(LogEventInfo.CreateNullEvent)).
                First

            result = Path.GetDirectoryName(result)

        Catch wedontgiveafuck As Exception

        End Try

        Return result

    End Function

End Module
