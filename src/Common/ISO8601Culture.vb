Imports System.Globalization

Public Module ISO8601Culture_
    Public Sub ISO8601Culture()
        Dim iso8601 As New CultureInfo("en-US")
        With iso8601
            .DateTimeFormat = New DateTimeFormatInfo
            With .DateTimeFormat
                .ShortDatePattern = "yyyy-MM-dd"
                .ShortTimePattern = "HH:mm:ss.FFFFFFFK"
                .LongDatePattern = "yyyy-MM-dd"
                .LongTimePattern = "HH:mm:ss.FFFFFFFK"
            End With
            .NumberFormat = New NumberFormatInfo
            With .NumberFormat
                .CurrencyGroupSeparator = "'"
                .NumberGroupSeparator = "'"
            End With
        End With
        System.Threading.Thread.CurrentThread.CurrentCulture = iso8601
        System.Threading.Thread.CurrentThread.CurrentUICulture = iso8601
    End Sub
End Module
