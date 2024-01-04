Public Class Inverter
    Implements IEquatable(Of Inverter)

    Public Property SerialNumber As String
    Public Property LastReport As DateTime
    Public Property LastEmail As DateTime = DateTime.MinValue

#Region "IEquatable"
    ' 
    Public Function Equals1(other As Inverter) As Boolean Implements IEquatable(Of Inverter).Equals

        Return Me.SerialNumber = other.SerialNumber

    End Function
    ' 
    Public Overrides Function Equals(other As Object) As Boolean

        Return Equals1(DirectCast(other, Inverter))

    End Function
    Public Overrides Function GetHashCode() As Integer

        Return SerialNumber.GetHashCode

    End Function
    Public Shared Operator =(a As Inverter, b As Inverter) As Boolean

        Return a.SerialNumber = b.SerialNumber

    End Operator
    Public Shared Operator <>(a As Inverter, b As Inverter) As Boolean

        Return Not (a = b)

    End Operator
#End Region
End Class
