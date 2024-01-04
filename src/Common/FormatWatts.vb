Public Module FormatWatts_

    Public Function FormatWatts(watts As Double) As String

        If watts >= 1000000000.0 Then
            Return $"{watts / 1000000000.0:#,##0.0} GWh"

        ElseIf watts >= 1000000.0 Then
            Return $"{watts / 1000000.0:#,##0.0} MWh"

        ElseIf watts >= 1000.0 Then
            Return $"{watts / 1000.0:#,##0} KWh"

        ElseIf watts >= 1 Then
            Return $"{watts:#,##0} Wh"

        Else
            Return $"{watts * 1000:#,##0} mWh"

        End If

    End Function

End Module
