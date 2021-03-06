# Client Center for Configuration Manager

## Project Description
The tool is designed for IT Professionals to troubleshoot ConfigMgr Agent related Issues. The Client Center for Configuration Manager provides a quick and easy overview of client settings, including running services and Agent settings in a good, easy to use user interface.

<a href="https://www.microsoft.com/store/apps/9NBLGGH5127B?ocid=badge"><img src="https://assets.windowsphone.com/f2f77ec7-9ba9-4850-9ebe-77e366d08adc/English_Get_it_Win_10_InvariantCulture_Default.png" alt="Get it on Windows 10" width="300" /></a>

Start UWP from PowerShell:
``` 
Start "shell:AppsFolder\$((Get-StartApps -Name "Client Center for Configuration Manager" | ? { $_.AppId -like "*Manager" }).AppID)" -Args "localhost"
```

![sccmclictr](https://cloud.githubusercontent.com/assets/11909453/24622767/71bcbde4-18a6-11e7-8fcd-5c2b4a3703e7.png)

## ClickOnce setup
http://sccmclictr.azurewebsites.net/ClickOnce/

## Requirements
* Windows Remote Management (WinRM) must be enabled and configured on all target computers. (Run "winrm quickconfig" in a command prompt.)
* Microsoft .NET Framework 4 (on the computer running the Tool)
* Configuration Manager Agent on the target computer (some features will also work with SCCM2007)
* Admin rights on the target computer.
* Windows Management Framework 4.0 (PowerShell 4) is required since Version 1.0.2!

## Tested on:
* Windows7 SP1 x64, Windows 8.1 x64, Windows 10 x64
* Server 2008 R2
* Server 2012 and Server 2012 R2
* Server 2016
