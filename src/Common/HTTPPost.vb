Imports System.Net.Http

Public Module HTTPPost_

    Public Function HTTPPost(url As String, content As String, authorisation As String) As HttpResponseMessage

        Dim client As New HttpClient
        'client.BaseAddress = New Uri(url)
        Dim uri As New Uri(url)
        client.DefaultRequestHeaders.Authorization = New System.Net.Http.Headers.AuthenticationHeaderValue("Basic", authorisation)
        Dim response As Task(Of HttpResponseMessage) = client.PostAsync(uri, New StringContent(content))
        Dim result As HttpResponseMessage = response.Result
        Return result

    End Function

End Module
