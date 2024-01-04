Imports System
Imports System.Collections.Generic

Namespace Models
    Partial Public Class TCategory
        Public Property Id As Integer

        Public Property Name As String

        Public Overridable ReadOnly Property TPowers As ICollection(Of TPower) = New List(Of TPower)()
    End Class
End Namespace
