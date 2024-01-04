Imports MimeKit
Imports NLog
Public Module ErrorHandlers

    Public Sub Fatal(logger As Logger, config As Configuration, ex As Exception, Optional message As String = "")

        logger.Fatal(ex)

        Try
            Console.WriteLine($"Fatal error {message}. {ex.Message}")
        Catch dontcare As Exception
        End Try

        Dim bodyBuilder As New BodyBuilder
        bodyBuilder.TextBody = $"Fatal error {message}.{vbCrLf}{ex.ToString}"

        Dim m As New Mailer(config)

        m.Send(New MailboxAddress(config.SMTPUser, config.EmailFrom),
                       New MailboxAddress(config.NameTo, config.EmailTo),
                       $"Fatal error {message}: {ex.Message}",
                       bodyBuilder.ToMessageBody())

        Threading.Thread.Sleep(10)
        Environment.Exit(1)

    End Sub

End Module
