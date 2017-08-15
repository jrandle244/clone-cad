# bcrp-db
## Installation
Note: The config loader is very flexible. It will automatically trim spaces off, so use spaces wherever you want to keep your config file neat. It also doesnt care which order your values are in, just as long as they're there.

##### Client
First, download it. The easiest way is to head over to [releases](https://github.com/Clone-Commando/bcrp-db/releases) and download the latest client release. Optionally, download or clone the project and build it to stay the most up-to-date.

If you downloaded it, it should have a settings.ini file with it. If you built it, create one.
Open settings.ini and make sure that the file has this in it:
```
;The IP of the server
IP = changeme
;The port of the server
Port = 30120
```
Change the IP to the host's IP, and if he has the port changed change it as well.
##### Server
Like before, go to the [releases](https://github.com/Clone-Commando/bcrp-db/releases) and download the latest server release or build it yourself. Put this in a folder that you know you wont accidentally delete.

Open or create a config file (name and extension dont matter) and make sure it has this in it:
```
;The hosting IP of the server.
IP = 0.0.0.0
;The port of the server.
Port = 30120
;Filter type. Set 0 for none, set 1 for whitelist, set 2 for blacklist.
Filter = 0
;IPs the filter applies to in the civ menu. Keep blank for none.
FilteredCivIPs = 
;IPs the filter applies to in the police menu. Keep blank for none.
FilteredPoliceIPs = 
;IPs the filter applies to in the dispatch menu. Keep blank for none.
FilteredDispatchIPs = 
;Log path
Log = C:\bcrpdb.log
;Database path
Database = C:\civilians.db
;IP aliases for easy tracking of known users (e.g. "123.456.678.012:Bobby" w/out quotes will make the server display and log bobby instead of 123.456.678.012). Seperate by comma.
Aliases = 
```

You probably don't need to change the IP, but if you have multiple network adapters change the IP to the IP that you have port forwarded to.

If you want/need to you can change the port. 

The filter will deny specified users if set to blacklist and allow specified user only if set to whitelist, so set the filter desired and then enter the IPs in the menu-specific filter below seperated by a ','. 

The log path is pretty self explanitory. The database path is where the database gets saved/loaded from. I'd recommend changing the paths as right now they are at the root of the C drive, but that's up to you.

The aliases are designed to make it easy to regonize know users i.e. yourself or friends so you dont have to remember their IP they're entered as `IP:Name` and seperated by a comma. If the server would display or log the IP it'll check it will grab the first alias the IP has, otherwise it just displays the IP.

After all that, open command prompt in administrator mode. Opening in admin is required, as installing a service isn't something a normal user is supposed to do. Type `cd [the folder path you put the server in]`. If it is not on the C drive you will need to do `cd /d [folder]` instead.

In file explorer, navigate to `C:\Windows\Microsoft.NET`. If your computer is 64 bit, open Framework64, otherwise open Framework. Then open the newest version you see. The newest one I see is `v4.0.30319`, but it may differ. Just make sure it starts with `v4`. Then, copy the path of the folder you're in, and paste it in command prompt by right clicking command prompt. Add `\installutil.exe` to the end of the path. Type put a space inbetween, and then type `BCRPDBServer.exe`. Press enter. Here's what my command line would look like (this will not look exactly like yours):
`E:\Users\luke0\Desktop\BCRPDB>C:\Windows\Microsoft.NET\Framework64\v4.0.30319\installutil.exe BCRPDBServer.exe`

If you see it say `rollback` anywhere it means it was unsuccessful and you didn't follow the steps properly.

Then go to Services and double click on BCRP Database. Get the path of your settings file, and copy it. Then go to start parameters and press hyphen and the path. The hyphen before the path is very important. This sets your settings file so the database remembers where it is. If all goes well, the database should start shortly after booting up and stop when shutting down. Just make sure that you have your ethernet plugged in or wifi set to auto connect on startup.

To uninstall follow these steps but use `installutil.exe /u BCRPDBServer.exe`. Here's an example:
`E:\Users\luke0\Desktop\BCRPDB>C:\Windows\Microsoft.NET\Framework64\v4.0.30319\installutil.exe /u BCRPDBServer.exe`
Then delete all the files associated with the database (exes, database file, config, etc).
## Keeping up-to-date
Just as a side note, I will have `(client)` or `(server)` at the end of my bug fixes in the release notes. If your version is not mentioned in the release notes then you most definately do not have to patch the new update.
## About
A custom database for the FiveM roleplay server Blaine County Roleplay.

[Our Discord](http://discord.gg/T7RzwVz).

View and vote on the [roadmap](https://trello.com/b/4UpUhulH/bcrp-database).
