Public Class Reading
    Property Start As DateTime
    Property Finish As DateTime
    Property ProdWHLifetime As Double
    Property ProdWHToday As Double
    Property ConsWHLifetime As Double
    Property ConsWHToday As Double
    Property Production As Double
    Property Consumption As Double
    Public Overrides Function ToString() As String
        Return $"{Start:HH:mm:ss}-{Finish:HH:mm:ss} Production {Production} Consumption{Consumption}"
    End Function
End Class
