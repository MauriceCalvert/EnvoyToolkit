Imports System.IO
Imports MimeKit
Imports NLog
Public Module SendEmail_
    Private ReadOnly Property Logger As Logger = LogManager.GetCurrentClassLogger
    Public Sub SendEmail(config As Configuration, subject As String, bmp As Drawing.Bitmap)

        Dim bytes() As Byte

        Using stream As New MemoryStream()
            bmp.Save(stream, Drawing.Imaging.ImageFormat.Jpeg)
            bytes = stream.ToArray()
        End Using

        Dim bodyBuilder As New BodyBuilder
        bodyBuilder.TextBody = "Chart attached"

        Dim imageAttachment As MimeEntity = bodyBuilder.Attachments.Add("chart.jpg", bytes, ContentType.Parse("image/jpeg"))
        imageAttachment.ContentDisposition = New ContentDisposition(ContentDisposition.Attachment)

        Dim m As New Mailer(config)
        m.Send(New MailboxAddress(config.NameFrom, config.EmailFrom),
               New MailboxAddress(config.NameTo, config.EmailTo),
               subject,
               bodyBuilder.ToMessageBody())

        Logger.Info($"Sent email to {config.EmailTo}: {subject}")

    End Sub
    Public Sub SendEmail(config As Configuration, subject As String, body As String)

        Dim bodyBuilder As New BodyBuilder
        bodyBuilder.TextBody = body

        Dim m As New Mailer(config)

        m.Send(New MailboxAddress(config.NameFrom, config.EmailFrom),
               New MailboxAddress(config.NameTo, config.EmailTo),
               subject,
               bodyBuilder.ToMessageBody())

        Logger.Info($"Sent email to {config.EmailTo}: {subject}")

    End Sub
End Module
