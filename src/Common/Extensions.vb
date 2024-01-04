Imports System.Runtime.CompilerServices

Public Module Extensions

    <Extension()>
    Public Function Truncate(ByVal stamp As DateTime, ByVal timeSpan As TimeSpan) As DateTime

        If timeSpan = TimeSpan.Zero Then
            Return stamp
        End If

        If stamp = DateTime.MinValue OrElse stamp = DateTime.MaxValue Then
            Return stamp
        End If

        Return stamp.AddTicks(-(stamp.Ticks Mod timeSpan.Ticks))

    End Function

End Module
