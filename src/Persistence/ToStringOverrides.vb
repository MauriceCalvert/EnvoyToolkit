Namespace Models
    Partial Public Class TPower
        Public Overrides Function ToString() As String
            Return $"{Start}-{Finish} {WattHours} {Category}"
        End Function
    End Class
End Namespace
