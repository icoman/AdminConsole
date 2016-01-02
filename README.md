# AdminConsole

![Admin Console](http://rainbowheart.ro/static/uploads/1/2015/7/AdminConsole.jpg)

I made a small C# application used to connect by VNC or RDP to any PC, server or workstation - [blog announcement](http://rainbowheart.ro/410).

I use this app every day since 2012 to connect to more than 7 servers and 24 workstations.

The application call two external tools (vnc viewer and rdp viwer) with the computer name, user and password stored in a config file.

For RDP I use Remote Desktop Plus:
http://www.donkz.nl/

For VNC I use TightVNC:
http://www.tightvnc.com/

If you start app without config file, a default config will be created.
I write in config file Windows credentials and SQL credentials, to help me to administrate computers.

Feel free to use this software in source code or binary form for any kind of purpose, including both personal and commercial use.

The source can be compiled with [SharpDevelop](http://www.icsharpcode.net/OpenSource/SD/Default.aspx).


