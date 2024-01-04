Imports System
Imports System.Collections.Generic

Namespace Models
    Public Class TPower
        Public Property Id As Integer

        Public Property Category As Integer

        Public Property Start As Date

        Public Property Finish As Date

        Public Property WattHours As Integer

        Public Overridable Property CategoryNavigation As TCategory
    End Class
End Namespace
