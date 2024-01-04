Imports Newtonsoft.Json.Linq

Public Module JSONValue_

    Public Function JSONValue(Of T)(json As JObject, section As String, sectionkey As String, sectionvalue As String, valuekey As String) As T

        Dim jsection As JToken = json(section)

        Dim jvalues As JToken = jsection.Where(Function(q) CStr(q(sectionkey)) = sectionvalue).Single

        Dim result As T = jvalues(valuekey).Value(Of T)

        Return result

    End Function

End Module
