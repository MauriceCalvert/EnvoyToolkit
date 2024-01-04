Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports NLog
Imports System.Text
Imports System.Xml

Public Class Envoy

    Public ReadOnly Property SerialNumber As String
    Public ReadOnly Property PartNumber As String
    Public ReadOnly Property Software As String
    Public ReadOnly Property Inverters As New List(Of Inverter)

    Private ReadOnly Property Logger As Logger = LogManager.GetCurrentClassLogger
    Private ReadOnly Property Config As Configuration

    Public Sub New(config As Configuration)

        _Config = config

        ReadInfo()

        With config

            If .EnvoyUserID = "" Then
                .EnvoyUserID = "envoy"
                .EnvoyPassword = Right(SerialNumber, 6)
                Logger.Info("Creating default userid and password for Envoy.")
                Logger.Info("See https://enphaseenergy.com/sites/default/files/downloads/support/IQ-Envoy-Manual-EN-US-07-14-2020b.pdf page 29")
            End If

            CheckInverters()

            Logger.Info($"Envoy at { .EnvoyAddress} serial number = {SerialNumber} part number = {PartNumber} software = {Software} inverters = {Inverters.Count}")

        End With

    End Sub
    Private ReadOnly Property OneDay As New TimeSpan(1, 0, 0, 0)
    Public Sub CheckInverters()

        ReadInverters()

        Dim failed As New List(Of Inverter)

        Dim yesterday As DateTime = DateTime.Now - New TimeSpan(1, 0, 0, 0)

        For Each inverter As Inverter In Inverters

            If inverter.LastReport < yesterday Then
                If inverter.LastEmail < yesterday Then
                    failed.Add(inverter)
                End If
            End If
        Next

        If failed.Any Then

            Dim sb As New StringBuilder

            For Each inverter As Inverter In failed
                sb.AppendLine($"Inverter {inverter.SerialNumber} has not reported since {inverter.LastReport:yyyy-MM-dd hh:mm:ss}")
            Next

            SendEmail(Config, $"{failed.Count} of {Inverters.Count} inverters failed", sb.ToString)
        End If

    End Sub
    Function GetNodeValue(xmlDoc As XmlDocument, xpath As String) As String

        Dim node As XmlNode = xmlDoc.SelectSingleNode(xpath)

        If node IsNot Nothing Then
            Return node.InnerText
        Else
            Throw New Exception($"Path {xpath} not found in info.xml")
        End If

    End Function

    Private Sub ReadInfo()

        Dim request As String = Config.EnvoyAddress & "/" & Config.QueryInfo

        Dim response As String = HTTPGet(request, Config.EnvoyUserID, Config.EnvoyPassword)

        Dim xmlDoc As New XmlDocument()
        xmlDoc.LoadXml(response)

        ' Extract sn and pn values from the device node
        _SerialNumber = GetNodeValue(xmlDoc, "envoy_info/device/sn")
        _PartNumber = GetNodeValue(xmlDoc, "envoy_info/device/pn")
        _Software = GetNodeValue(xmlDoc, "envoy_info/device/software")

    End Sub
    Public Sub ReadInverters()

        Dim request As String = Config.EnvoyAddress & "/" & Config.QueryInventory
        Dim response As String = HTTPGet(request, Config.EnvoyUserID, Config.EnvoyPassword)

        ' inventory.json is an array:
        '  [
        '    {
        '      "type": "PCU",
        '      "devices": [
        '        {
        '          "part_num": "800-00362-r01",
        '          "installed": "1592924527",
        ' ...

        Dim json As JArray = JArray.Parse(response)

        Dim devices As IEnumerable(Of JToken) =
            json.
            Where(Function(pcu) pcu("type").ToString() = "PCU").
            SelectMany(Function(pcu) pcu("devices"))

        Dim now As DateTime = DateTime.Now

        For Each device As JToken In devices

            Dim inverter As Inverter = Nothing

            If Not Inverters.Contains(inverter) Then

                ' If we start up at nighttime, the inverter obviously won't be producing
                ' so pretend it reported now and we'll catch failed inverters in 24 hours
                inverter = New Inverter With {
                    .SerialNumber = device.SelectToken("serial_num").ToString(),
                    .LastReport = now}
                _Inverters.Add(inverter)
            End If

            Dim producing As Boolean = device.SelectToken("producing").ToString.ToLower = "true"
            If producing Then
                inverter.LastReport = now
            End If
        Next
    End Sub
    Public Overrides Function ToString() As String

        Return SerialNumber

    End Function
End Class
