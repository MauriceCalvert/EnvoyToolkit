Modify the file C:\Program Files(x86)\Enphase Envoy\configuation.json, it looks like this:

```json
{
  "database": "Enphase",
  "emailfrom": "envoy@mydomain.com",
  "emailto": "user@mydomain.com",
  "envoyaddress": "http://10.0.0.100",
  "envoyuserid": "",
  "envoypassword": "",
  "interval": "01:00:00",
  "namefrom": "Envoy Agent",
  "nameto": "Your Name",
  "queryinfo": "info.xml",
  "queryinventory": "inventory.json",
  "querymeter": "production.json?details=1",
  "smtppassword": "secret",
  "smtpport": "587",
  "smtpserver": "mail.mydomain.com",
  "smtpuser": "envoy@mydomain.com",
  "sqlPassword": "",
  "sqlserver": "server name",
  "sqlUserID": ""
}
```
It is used by both EnvoyMonitor.exe and EnvoyReporter.exe.
You may need administrative privileges to modify it.

**Be careful to only modify the values in quotes after the colons.**

It's a good idea to validate your JSON file when you've finished modifying it, use one of [these][2]  
If you make a mistake the programs will barf and the error messages can be a little cryptic.  
For example, if you omit to enclose an option correctly with "..." and write

>  "namefrom": "Envoy Agent,

with the missing closing quote, which should be

>  "namefrom": "Envoy Agent",

you will get this in the log:
```
2023-12-20 23:13:39 FATAL System.IO.InvalidDataException: Failed to load configuration from file 'configuration.json'. System.IO.InvalidDataException: Failed to load configuration from file 'configuration.json'.
 ---> System.FormatException: Could not parse the JSON file.
 ---> System.Text.Json.JsonReaderException: '0x0D' is invalid within a JSON string. The string should be correctly escaped. LineNumber: 8 | BytePositionInLine: 27.
```
**LineNumber: 8 | BytePositionInLine: 27** (it is in fact the 9th line because the '{' line doesn't count)
should give you a hint as to where you fouled up.

## Options

*	**database** The name of the database on the SQL Server. Remember to restore Enphase.bak from the Assets folder.  
	It contains 3 years of data which you can use to try out the reporting.  
	Before you put it into production, empty the table tPower. In SQL Server Management Studio, new query:
>**DELETE * FROM tPower**
	
*	**emailfrom** The email address of the person who sends emails e.g. Fred Bloggs.  
	The SMTP rules require that this name corresponds to the email user that you setup with your SMTP provider, smtpuser below.

*	**emailto** The email address of the person who receives the emails. e.g. Andrew Smith.

*	**envoyaddress** The IP address of your Envoy. Whilst you are testing, this will probably be 192.168.1.?.  
	If you setup the Envoy on an isolated network, this will probably be 10.0.0.?

*	**envoyuserid** Leave blank unless you have changed it. The default Envoy userid is Envoy. The programs fill this in automatically.

*	**envoypassword** Leave blank unless you have changed it. The default Envoy password is the last 6 characters of your Envoy's serial number. The programs fill this in automatically.

*	**interval** The interval between meter readings. The default is *01:00:00* which is an hour.  
	Setting less than 15 minutes will generate a lot of data. Setting more than an hour will prevent you from making hourly charts.

*	**namefrom** The full name of the person who sends emails e.g. Envoy Agent. The SMTP rules require that this name corresponds to the **smtpuser** below.

*	**nameto** The full name of the person who receives the emails. e.g. Andrew Smith.

*	**queryinfo** The HTTP query parameter to read the Envoy's information. Don't change this unless you really know what you're doing

*	**queryinventory** The HTTP query parameter to read the inventory. Don't change this unless you really know what you're doing

*	**querymeter** The HTTP query parameter to read the meters. Don't change this unless you really know what you're doing

*	**smtppassword** The password for *smtpuser*

*	**smtpport** The port number of the SMTP provider's server, typically 587

*	**smtpserver** The SMTP provider's server address

*	**smtpuser** The userid that you setup with your SMTP provider, usually the same as emailfrom above.

*	**sqlPassword**  The password of *sqluserid*  
	Leave this blank for [Windows authentication][3].
	
*	**sqlserver** The name of the Windows PC that is running SQL Server

*	**sqlUserID** The userid to connect to the SQL Server   
	Leave this blank for [Windows authentication][3].

[2]: https://www.google.com/search?q=json+online+validator
[3]: https://learn.microsoft.com/en-us/sql/relational-databases/security/choose-an-authentication-mode?view=sql-server-ver16