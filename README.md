# IKEv2-VPN-Client
A Client which allows you to choose and connect to multiple servers from an XML document

![Image of the VPN Client](https://github.com/MxFox555/IKEv2-VPN-Client/blob/master/VPN%20UI.png)

This client makes use of:
        DotRas by winnster: https://github.com/winnster/DotRas

And is designed to connect to:
        IKEv2-setup server by jawj: https://github.com/jawj/IKEv2-setup


## Setup
You will see at the top of the main code there will be two constants, *XMLPATH* and *BACKUPXMLPATH*. These both need a link to an XML path whether that be local or hosted on OneDrive or another file sharing site. This XML file will outline all of the servers and their locations for the VPN client to read. The XML file is laid out as follows.

        <?xml version="1.0" encoding="UTF-8"?>
        <Info>
                <location country="Location name">
                        <PING>127.0.0.1</PING>
                        <IP>198.168.0.1</IP>
                </location>
                <location country="Location name">
                        <PING>127.0.0.1</PING>
                        <IP>198.168.0.1</IP>
                </location>
                <vpninfo>
                        <vpnuser>Username for the VPN</vpnuser>
                        <vpnpass>Password for the VPN</vpnpass>
                        <pbkpath><![CDATA[Path to a .pbk file for windows 7 users]]></pbkpath>
                </vpninfo>
                <updateinfo>
                        <currentversion>2.1.0.0</currentversion>
                        <updateurl><![CDATA[Path to the installer]]></updateurl>
                </updateinfo>
        </Info>

The information you will need to put in yourself is:
- The location names
- The Closest server to the end server for ICMP purposes
- The IP of each server in each location
- The VPN User name
- The VPN Password
- the path to a custom .pbk file for those who are using windows 7
- The current version of the software if you have changed it up yourself
- The update url for an update package if you wish

