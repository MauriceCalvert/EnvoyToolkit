Imports System.Net
Imports System.IO
Imports System.Net.Http
Imports System.Text

''' <summary>
''' Functions to fetch data from a webpage
''' </summary>
''' <remarks></remarks>
Public Module HTTPGet_

    ''' <summary>
    ''' Get the data from a webpage
    ''' </summary>
    ''' <param name="url">the url to fetch</param>
    ''' <returns>a string containing the entire page</returns>
    ''' <remarks></remarks>
    Public Function HTTPGet(ByVal url As String, Optional userid As String = "", Optional password As String = "") As String

        Dim answer As String = ""

        Using client As New HttpClient

            Dim request As New HttpRequestMessage(HttpMethod.Get, url)

            If userid <> "" AndAlso password <> "" Then
                request.Headers.Add("Authorization", CreateBasicAuthorization(userid, password))
            End If

            client.Timeout = New TimeSpan(0, 1, 0)

            Dim response As HttpResponseMessage = client.Send(request)

            Using reader As New StreamReader(response.Content.ReadAsStream)

                answer = reader.ReadToEnd
                reader.Close()

            End Using
        End Using

        Return answer

    End Function

    Public Function CreateBasicAuthorization(username As String, password As String) As String
        Dim authInfo As String = $"{username}:{password}"
        Dim encodedAuthInfo As String = Convert.ToBase64String(Encoding.UTF8.GetBytes(authInfo))
        Return $"Basic {encodedAuthInfo}"
    End Function

End Module
