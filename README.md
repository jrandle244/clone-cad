# bcrp-db
## Installation
Note: The config loader automatically trims spaces off, so use spaces wherever you want to keep your config file neat.

##### Client
First, download it. The easiest way is to head over to releases and download the latest client release. Optionally, download or clone the project and build it to stay the most up-to-date.

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
Like before, go to the releases and download the latest server release or build it yourself.

Open or create server-settings.ini and make sure it has this in it:
```
;The hosting IP of the server.
IP = 0.0.0.0
;The port of the server.
Port = 30120
;Filter type. Set 0 for none, set 1 for whitelist, set 2 for blacklist.
Filter = 0
;IPs the filter applies to. Keep blank for none.
FilteredIPs = 
```

If you have multiple network adapters, change the IP to the IP that you have port forwarded to, or if you have your computer directly hooked up to WAN, set it to your public IP.

If you want/need to you can change the port. The filter will automatically disconnect users as soon as they connect, so set the filter desired and then enter the IPs in the FilteredIPs value below seperated by a ','.
## About
A custom database for the FiveM roleplay server Blaine County Roleplay.

[Our Discord](http://discord.gg/T7RzwVz).

View and vote on the [roadmap](https://trello.com/b/4UpUhulH/bcrp-database).
