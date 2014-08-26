
[//]: # (Router Connection Guide)

# Connecting to the BAX Router

Multiple methods of connecting to the router are supported- these fall into 
two categories: a text-mode command interface, and a web configuration 
interface.


## Serial (USB)

The serial connection is available when the router is directly connected to a 
PC via the USB cable. To access a serial connection, some additional (free)
software is required. We have tested using the following apps on these 
platforms:

 * __Windows__: [PuTTy](http://www.chiark.greenend.org.uk/~sgtatham/putty/), Hyperterminal (no longer available in Windows 7)
 * __Linux__ / __Mac OSX__: [GNU screen](http://www.gnu.org/software/screen/), [CoolTerm](http://freeware.the-meiers.org/)



### Microsoft Windows

The device will appear as a COM port on Windows (can be found in the Device 
Manager, Hyperterminal will also enumerate serial ports). The COM port number 
will usually be COM2 or above (COM1 is now a virtual interface on modern 
Windows).

### Linux, OSX (*nix platforms)

Devices on *nix platforms are listed under the `/dev/` directory. 



# Network Access

When the BAX Router is connected to your network using an Ethernet cable, it 
will request an IP address using DHCP.

On home networks, the IP address assigned will normally be of the form
192.168.x.x. On corporate or academic networks you may need to request an
address from your network administrator, quoting the device's MAC address. 
They may also be able to assign a DNS address for convenient access.

The IP address assigned to the router may be discovered by connecting to the 
serial connection and typing `status`. You may also find it useful to assign
a static IP address to the device for the purposes of port forwarding.



## Telnet

Telnet can be used to access the router command terminal over the network 
without needing to plug the USB cable into a PC. All of the commands listed in 
[Router Commands](commands.md) are supported.

The `telnet` command is used to connect to this console:


````
	telnet 192.168.0.15
````

... or use PuTTy on Windows. The router's telnet service runs on the default 
TCP port 23.


## Web Interface (HTTP)

The BuildAX Router configuration interface is accessed using a web browser, 
providing a convenient way to change settings and view router status without
needing to use the text-mode terminal. 

On Windows, type the NETBIOS name of the device may be typed directly into the
address bar. This will be in the form BAX-xxxxxx (e.g. `bax-434c32`), and can 
be found on the sticker on the rear of the device.

For information on using the web interface, please see the [Web Interface User Guide](user-guide.md).



# USB Mass Storage

Finally, data files logged by the router can be retrieved directly by using 
the router as a USB Mass Storage device. For this option to be available, data 
logging on the router must be disabled. This is because it would be 
unsafe to continue writing data to files which could be read at any time.

To override this, use the `mount` command.

**Warning**: removing data files from the mass storage device may cause the 
data fetch service to become unstable.