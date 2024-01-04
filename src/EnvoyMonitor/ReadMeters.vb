Imports Newtonsoft.Json.Linq
Imports Common
Imports NLog
Imports System.Net.Http

Public Module ReadMeter_

    Private ReadOnly Property Logger As Logger = LogManager.GetCurrentClassLogger

    Public Function ReadMeters(config As Configuration) As Reading

        Dim reading As New Reading

        Dim request As String = config.EnvoyAddress & "/" & config.QueryMeter

        Dim response As String = HTTPGet(request, config.EnvoyUserID, config.EnvoyPassword)

        Dim readings As New Reading

        Dim json As JObject = JObject.Parse(response)


        With reading

            .Finish = DateTime.Now

            .ProdWHLifetime = JSONValue(Of Double)(json, "production", "measurementType", "production", "whLifetime")

            .ProdWHToday = JSONValue(Of Double)(json, "production", "measurementType", "production", "whToday")

            .ConsWHLifetime = JSONValue(Of Double)(json, "consumption", "measurementType", "total-consumption", "whLifetime")

            .ConsWHToday = JSONValue(Of Double)(json, "consumption", "measurementType", "total-consumption", "whToday")

            Logger.Debug("Production lifetime {0} today {1} Consumption lifetime {2} today {3}",
                     FormatWatts(.ProdWHLifetime), FormatWatts(.ProdWHToday), FormatWatts(.ConsWHLifetime), FormatWatts(.ConsWHToday))

        End With

        Return reading

    End Function

End Module
