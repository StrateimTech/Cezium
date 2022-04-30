# CEZIUM - External standalone Rust recoil script
External hardware peripheral proxy cheat which provides recoil compensation by hooking into a keyboard and mouse & proxying it to a Host PC.

# Features
- Web interface
- Web API for developers wanting to work with Cezium or make their own interface? ;)
- All attachments & scopes in any configuration are supported and should work as expected.
- Automatic calculations using in game FOV & Sensitivity values (No more need to mess with DPI or use proprietary sensitivity systems other recoil scripts use) 
- Recoil smoothing to make it seem more legit (Will cause loss of accuracy to produce a visually real recoil compensation)
- Recoil smoothing compensation (Will return the bullet accuracy to almost 100% while making the visual experience less smooth with more jumping around)
- Recoil randomization & capable of reversing the randomization after the shot to produce a visually randomized experience.
- Tap firing support by emulating the first shot
- Configuring how much recoil you want compensated on the X/Y Axis so you can do less work while still moving your mouse. (Good for streamers, may take some time to get used)
- Timing randomization's to produce a more subtle X/Y randomization.
- Control timings are used to produce a very accurate spray & visual experience.

## Requirements
- Common sense & Linux knowledge (How to SSH into your pi, navigate through the file system, and downloading/fetching a file or transferring through FileZilla)
- A mouse (Optionally a keyboard) (Wireless keyboard/mouses will depend on if you can connect them to your linux distribution & get a device file to be present in /dev/input/ for them respectively)
- Raspberry Pi 4 Model B (It has support for OTG built into the USB-C Port) (or computer alternative with hardware support for OTG(On-The-GO) as a B-Device, read more https://en.wikipedia.org/wiki/USB_On-The-Go)
- Local ethernet connection to the RaspberryPi to access the settings API or webpage. (Optional if you have access to the RaspberryPi through a GUI/Desktop environment in which case you can access the webpage through the pi's browser.)
- A cable to connect the two computers (Host & Slave) (I you're using a RaspberryPi then you'll need a USB-C male to USB-A female adapter if your computer doesn't directly have USB-C ports)
- Cezium needs at least a single core free in a RaspberryPi to perform at most optimal levels (This should support 1000Hz mouses without a hitch).

## Notes / FAQ
- Do mouse macros/extra mouse keys work? Yes & no, Mouse macros that are built into the mouse, such as sending keystrokes from a mouse button, will work as long as you add the keyboard device file that the mouse provides in /dev/input/by-id/...event-kbd/ (sometimes its if01-event-kbd). Now do extra mouse keys work? The short answer is no only Left/Right/Middle buttons will be sent to the host alongside with X/Y/Wheel data.
- Does this affect latency? No it shouldn't add a substantial amount of latency to the mouse's inputs. It should be unnoticeable to the eye unless you're running Cezium alongside other programs on the RaspberryPi which might bottleneck it and cause hitching / queue lag spikes.

## Cons
- At the moment, only automatic guns are supported by Cezium I do have plans in the future to support semi automatic weapons if they work to a certain degree. (Assault Rifle, LR300, Mp5, M249, Thompson, and Custom)
- Requires hardware for something a simple script on the Host PC could easily perform, although without the insurance of it being "undetectable"
- Due to Cezium being completely external it's basically dumb but tries it's best to interpret the users actions, as an example Cezium doesn't know whether you're focused on the game or doing something completely different on the Host PC

## Pros
- In theory Cezium should be undetectable due to it being completely external and isolated on a separate computer
- Nothing is running on the main Host PC making it easily hideable from screensharing server admins or streamers

## Installation
TBD...

# How it works
![Picture alt](https://media.strateim.tech/img/TgJjo7kzyvt_zAt9.png)
