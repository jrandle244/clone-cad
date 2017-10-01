# bcrp-db
## Installation
##### Notes
Both the client and server require .Net v4.6.1, if you're computer doesn't have it the program will say you need it or will not launch (don't really know myself as only 1 person I've met has had this error). If you want to host on Linux use wine or mono, I personally am more familiar with the latter. I cannot promise this will work with mono, as the server uses paths and I believe that doesn't work on Linux due to the change in file structure.

Another note: The config loader is very flexible. It will automatically trim spaces off, so use spaces wherever you want to keep your config file neat. It also doesnt care which order your values are in, just as long as they're there. If you want to add comments, you must add comments as a whole line and starting with a `;`. If you append to a line it will read that comment as if its part of the line.

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
;IP aliases for easy tracking of known users (e.g. "123.456.678.012:Bobby" w/out quotes will make the server display and log bobby instead of 123.456.678.012). Seperate by comma.
Aliases = 
```

You probably don't need to change the IP, but if you have multiple network adapters change the IP to the IP that you have port forwarded to.

If you want/need to you can change the port. 

The filter will deny specified users if set to blacklist and allow specified user only if set to whitelist, so set the filter desired and then enter the IPs in the menu-specific filter below seperated by a ','. 

The log path is pretty self explanitory. The database path is where the database gets saved/loaded from. I'd recommend changing the paths as right now they are at the root of the C drive, but that's up to you.

The aliases are designed to make it easy to recognize known users i.e. yourself or friends so you dont have to remember their IP. They're entered as `IP:Name` and seperated by a comma. If the server would display or log the IP it'll will grab the first alias the IP has, otherwise it just displays the IP.

## Keeping up-to-date
Just as a side note, I will have `(client)` or `(server)` at the end of my bug fixes in the release notes. If your version is not mentioned in the release notes then you most definately do not have to patch the new update.
