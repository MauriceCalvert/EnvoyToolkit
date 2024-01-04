Public Module UnixDateTimes_

    Public Function DateTime2Unix(stamp As DateTime) As Long

        Return New DateTimeOffset(stamp).ToUnixTimeSeconds

    End Function

    Public Function Unix2DateTime(stamp As Long) As DateTime

        Return DateTimeOffset.FromUnixTimeSeconds(stamp).UtcDateTime

    End Function

End Module
