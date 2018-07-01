# auto-dns-updater
[![Build Status](https://travis-ci.com/Chips100/auto-dns-updater.svg?branch=master)](https://travis-ci.com/Chips100/auto-dns-updater)

Allows automatic updating of a DNS entry to point to the current machine. This is a simplistic implementation to fit my personal needs - it sets the DNS entry in my 1&1 Hosting Account configuration to point to my Raspberry PI.

## Installation on Raspberry PI 3
### Compiling
To compile the source, run the script `publish-win-arm.bat` found in the repository. The output will contain a folder named _publish_ that contains an executable that can be run on the Raspberry PI.

### Configure as scheduled task
When run as a scheduled task, the working directory has to be configured correctly to find the _config.json_ file next to the executable. To do this, a batch can be created that is referenced from the scheduled task. It contains two statements:

```cmd
cd "[FULL PATH TO FOLDER CONTAINING THE EXECUTABLE]"
"AutoDnsUpdater.Console.exe"
```

Next, to create a scheduled task, the previously created batch can be referenced:

```cmd
SCHTASKS /Create /SC HOURLY /TN DnsUpdate /TR "[FULL PATH TO BAT]" /RU SYSTEM
```
