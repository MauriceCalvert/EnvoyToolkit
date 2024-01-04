Imports System.Reflection

Public Module GetProperties_

    Function GetProperties(obj As Object) As Dictionary(Of String, Object)

        Dim result As New Dictionary(Of String, Object)()

        Dim properties As PropertyInfo() = obj.GetType().GetProperties()

        For Each prop As PropertyInfo In properties
            If prop.CanRead Then
                Dim value As Object = prop.GetValue(obj)
                result.Add(prop.Name, value)
            End If
        Next

        Return result
    End Function
End Module
