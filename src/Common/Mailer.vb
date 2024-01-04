Imports MailKit.Net.Smtp
Imports MailKit.Security
Imports MimeKit
Imports MimeKit.Text

Public Class Mailer

    Sub New(config As Configuration)

        Server = config.SMTPServer
        Port = config.SMTPPort
        User = config.SMTPUser
        Password = config.SMTPPassword

    End Sub
    Public Sub New(server As String, port As Integer, user As String, password As String)

        _Server = server
        _Port = port
        _User = user
        _Password = password

    End Sub
    Public ReadOnly Property Password As String
    Public ReadOnly Property Port As Integer
    Public Sub Send(mailfrom As MailboxAddress, mailto As MailboxAddress, subject As String, body As MimeEntity)

        Dim email As New MimeMessage

        With email
            .From.Add(mailfrom)
            .To.Add(mailto)
            .Subject = subject
            .Body = body
        End With

        Using client As New SmtpClient
            With client
                .Connect(Server, Port, SecureSocketOptions.StartTls)
                .Authenticate(User, Password)
                .Send(email)
                .Disconnect(True)
            End With
        End Using

    End Sub
    Public ReadOnly Property Server As String
    Public ReadOnly Property User As String
End Class
