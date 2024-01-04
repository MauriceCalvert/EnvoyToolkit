Public Module Base64

    Public Function Base64Encode(ByVal plainText As String) As String
        Dim plainTextBytes As Byte() = System.Text.Encoding.UTF8.GetBytes(plainText)
        Return System.Convert.ToBase64String(plainTextBytes)
    End Function

    Public Function Base64Decode(ByVal base64EncodedData As String) As String
        Dim base64EncodedBytes As Byte() = System.Convert.FromBase64String(base64EncodedData)
        Return System.Text.Encoding.UTF8.GetString(base64EncodedBytes)
    End Function

End Module
