In the installer, don't forget to add folders
runtimes
Assets

install as service:
sc.exe create EnvoyMonitor start=delayed-auto binpath="C:\Program Files (x86)\EnvoyToolkit\EnvoyToolkit\envoymonitor.exe"
sc.exe description EnvoyMonitor "Monitors Envoy power and inverters"