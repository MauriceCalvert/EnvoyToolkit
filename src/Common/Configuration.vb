Imports Microsoft.Extensions.Configuration
Imports Microsoft.Extensions.Configuration.Json
Imports NLog
Imports Common
Imports System.IO
Public Class Configuration

    Public Setting As IConfiguration
    Private Logger As Logger = LogManager.GetCurrentClassLogger
    Private FullPath As String
    Public Sub New(json As String)

        Dim config As New ConfigurationBuilder
        ' AddJsonFile croaks on relative paths
        FullPath = GetExecutingPath() & "\" & json
        config.AddJsonFile(fullpath)

        Setting = config.Build()

        _LogFolder = Setting("logfolder")
        If String.IsNullOrWhiteSpace(LogFolder) Then
            Dim mydocs As String = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments).TrimEnd("\")
            If String.IsNullOrWhiteSpace(mydocs) Then
                mydocs = "C:"
            End If
            _LogFolder = mydocs & "\Envoy"
        End If
        LogManager.Configuration.Variables("logfolder") = LogFolder

        _OutputFolder = Setting("outputfolder")
        If String.IsNullOrWhiteSpace(OutputFolder) Then
            _OutputFolder = LogFolder
        End If

        _Database = Setting("database")
        _EmailFrom = Setting("emailfrom")
        _EmailTo = Setting("emailto")
        _EnvoyAddress = Setting("envoyaddress")
        _EnvoyPassword = Setting("envoypassword")
        _EnvoyUserID = Setting("envoyuserid")
        _Interval = TimeSpan.Parse(Setting("interval"))
        _NameFrom = Setting("namefrom")
        _NameTo = Setting("nameto")
        _QueryInfo = Setting("queryinfo")
        _QueryMeter = Setting("querymeter")
        _QueryInventory = Setting("queryinventory")
        _SMTPPassword = Setting("smtppassword")
        _SMTPPort = Integer.Parse(Setting("smtpport"))
        _SMTPServer = Setting("smtpserver")
        _SMTPUser = Setting("smtpuser")
        _SQLPassword = Setting("sqlpassword")
        _SQLServer = Setting("sqlserver")
        _SQLUserID = Setting("sqluserid")

        Dim props As Dictionary(Of String, Object) = GetProperties(Me)
        Logger.Info("Configuration:")

        For Each prop As KeyValuePair(Of String, Object) In props

            Dim val As String

            If prop.Value Is Nothing Then
                val = "[unspecified]"
            Else
                val = prop.Value.ToString
            End If

            Logger.Info($"  {prop.Key} = {val}")

        Next

    End Sub
    Public Function GetFullPath() As String
        Return FullPath
    End Function
    Public ReadOnly Property Database As String
    Public ReadOnly Property EmailFrom As String
    Public ReadOnly Property EmailTo As String
    Public ReadOnly Property EnvoyAddress As String
    Public Property EnvoyPassword As String
    Public Property EnvoyUserID As String
    Public ReadOnly Property Interval As TimeSpan
    Public ReadOnly Property LogFolder As String
    Public ReadOnly Property NameFrom As String
    Public ReadOnly Property NameTo As String
    Public ReadOnly Property OutputFolder As String
    Public ReadOnly Property QueryInfo As String
    Public ReadOnly Property QueryInventory As String
    Public ReadOnly Property QueryMeter As String
    Public ReadOnly Property SMTPPort As Integer
    Public ReadOnly Property SMTPServer As String
    Public ReadOnly Property SMTPUser As String
    Public ReadOnly Property SMTPPassword As String
    Public ReadOnly Property SQLPassword As String
    Public ReadOnly Property SQLServer As String
    Public ReadOnly Property SQLUserID As String
End Class
