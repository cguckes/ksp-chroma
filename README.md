<!-- Readme.md v1.1.0.0
KSP Chroma Control (KCC)
created: 
updated: 21 Jul 2021 -->  
***  

## Download on [Curseforge][MOD:curseforge] or [Github][MOD:github] or [SpaceDock][MOD:spacedock].  Also available on [CKAN][MOD:ckan].  

# KSP Chroma Control (KCC)  
![Mod Version][shield:mod:latest] ![KSP version][shield:ksp] ![KSP-AVC][shield:kspavc] ![License c{LICENSE}][shield:license] ![][LOGO:license]   

![Curseforge][shield:curseforge] ![CKAN][shield:ckan] ![GitHub][shield:github] ![SpaceDock][shield:spacedock]  

![Validate .version files][shield:avcvalid]  
***

[![Join the chat at https://gitter.im/cguckes/ksp-chroma](https://badges.gitter.im/cguckes/ksp-chroma.svg)](https://gitter.im/cguckes/ksp-chroma?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge&utm_content=badge)

Lights up your keyboard to make playing Kerbal Space Program a lot easier. Currently only supports Razer Chroma Keyboards, Mousepads, Mice and Headsets. If you want me to add support for other devices as well, you'll have to send me one. I can send it back after I'm done implementing the code. Enough talk, watch this awesome video by [Game Instructor](https://www.youtube.com/channel/UCeiT-KYAvbos30RKre4W9UQ) to see what this mod really is all about: https://www.youtube.com/watch?v=-nqTzKLMGuU

## Known Issues
Due to a bug in the SDK, some people reported strange flickering. This occurs if you use a custom color scheme on your keyboard. To fix this, switch to the standard color scheme and start the game afterwards.

## Features

- Function keys 1 to 0 are only lit, if the underlying action group actually does anything. The keys are displayed in two different colors, depending on whether the action group is toggled or not.
- The keys for SAS, RCS, Gears, Lights and the Brakes are lit up in different colors, indicating if the respective system was activated or not.
- The amount of resources in the current stage is displayed on your keypad and the keys to the left of it (PrtScr, ScrLk, ..., PageDown)
- The color of W, A, S, D, E and Q varies slightly depending on whether you're in precision or normal steering mode
- The keys for timewarp control are lit either red for physics timewarp or green for normal timewarp

## Full list of game effects

- Stylized Kerbal Space Program logo that fades in on every scene that does not contain any noteworthy keyboard interaction (pressing Escape to go to the menu not being noteworthy enough to light up the key)
- In the vessel editor, different kinds of keysets are lit up according to the current editor mode.
- Control keys and toggleable function keys are lit up in different colors, showing whether the function is switched on or off during flight.
- Reduced keyset lit for EVA mode
- Resource gauges displayed on the keypad and the keys above the UpDownLeftRight keys.
- Power failure animation on vessels that need power to be controllable
- Crash animation that triggers when crashing a vessel's root part
- Splashdown animation that triggers, when landing on water.
- Vessel electricity status displayed on any Razer mouse and mousepad connected
- Vessel heat displayed in three colors (blue = cool, red = warm/hot, yellow = you're in trouble), uses the scrollwheel and logo on mice and the bottom LEDs on the mousepad.
- "Dear GF, please don't disturb me now" feature on the headset. The device is colored red, when you can't quicksave right now. Turns green once quicksave is allowed again.
- Vessel height above ground displayed on F1 to F4 keys (F1 = 10m, F2 = 50m, F3 = 100m, F4 = 1000m). The intensity changes, e.g. if F1 is fully lit and F2 is half lit, you are 30 meters above ground.

## Installation

1. Unzip the release archive and place the KSPChromaControl folder in your KSP GameData directory.
2. Start KSP and witness the awesomeness of highlighted function keys while kerbaling through space
3. (optional) Move the file ChromaAppInfo.xml from the mod folder into the KSP folder, to allow Synapse to recognize the game properly.
 
## Todo

- Make fuel gauge position device dependent (Blackwidow Chroma / Blackwidow Chroma TE / Orbweaver)
- Light up all keys when the player is entering text (vessel name, savegame, etc.)
- Different colors for different altimeter zoom levels.
- Linux version (this might take a bit longer, but I'm planning to do it anyway. What good is a c# interface if it is only ever implemented once...)
***  
  
[MOD:license]:      https://github.com/zer0Kerbal/KSPChromaControl/blob/master/LICENSE  
[MOD:issues]:       https://github.com/zer0Kerbal/KSPChromaControl/issues  
[MOD:wiki]:         https://github.com/zer0Kerbal/KSPChromaControl/  
[MOD:known]:        https://github.com/zer0Kerbal/KSPChromaControl/wiki/Known-Issues  
[MOD:disc]:         https://github.com/zer0Kerbal/KSPChromaControl/discussions "Discussions"  
[MOD:github:repo]:  https://github.com/zer0Kerbal/KSPChromaControl/  
[MOD:clog]:    https://raw.githubusercontent.com/zer0Kerbal/KSPChromaControl/master/Changelog.cfg  
[MOD:forum]:        https://forum.kerbalspaceprogram.com/index.php?/topic/192456-*  
[MOD:contributing]: https://github.com/zer0Kerbal/KSPChromaControl/blob/master/.github/CONTRIBUTING.md  
<!--- original mod stuff -->  
[MOD:original:thread]: https://forum.kerbalspaceprogram.com/index.php?/topic/64520-*   
[MOD:original:download]: https://licensebuttons.net/l/by-sa/4.0/80x15.png  
.  
[KSP:website]: http://kerbalspaceprogram.com/  
[LOGO:license:0]: https://licensebuttons.net/i/l/by-nc-sa/transparent/33/66/99/76x22.png ""  
[LOGO:license:1]: https://creativecommons.org/licenses/by-nc-sa/4.0/ "CC-BY-NC-SA-4.0"  
[LINK:license]: https://creativecommons.org/licenses/by-nc-sa/4.0/ "CC-BY-NC-SA-4.0"  
.  
[MOD:github]: https://github.com/zer0Kerbal/KSPChromaControl/releases/latest "GitHub"  
[MOD:spacedock]: http://spacedock.info/mod/2379  
[MOD:curseforge]: https://www.curseforge.com/kerbal/ksp-mods/KSPChromaControl  
[MOD:ckan]: http://forum.kerbalspaceprogram.com/index.php?/topic/192456-*  
.  
[image:github]:       https://i.imgur.com/RE4Ppr9.png  
[image:spacedock]: https://i.imgur.com/m0a7tn2.png  
[image:curseforge]: https://i.postimg.cc/RZNyB5vP/Download-On-Curse.png  
[image:get-support]:    https://i.postimg.cc/vHP6zmrw/image.png  
.  
[image:ckan]:    https://i.postimg.cc/x8XSVg4R/sj507JC.png  
.  
[shield:mod:latest]: https://img.shields.io/github/v/release/zer0Kerbal/KSPChromaControl?include_prereleases?style=plastic  
[shield:mod]: https://img.shields.io/endpoint?url=https://raw.githubusercontent.com/zer0Kerbal/KSPChromaControl/master/json/mod.json  
[shield:ksp]: https://img.shields.io/endpoint?url=https://raw.githubusercontent.com/zer0Kerbal/KSPChromaControl/master/json/ksp.json  "KSP Version"  
[shield:license]: https://img.shields.io/endpoint?url=https://raw.githubusercontent.com/zer0Kerbal/KSPChromaControl/master/json/license.json  
[shield:kspavc]:     https://img.shields.io/badge/KSP-AVC--supported-brightgreen.svg?style=plastic  
[shield:spacedock]:  https://img.shields.io/badge/SpaceDock-Listed-blue.svg?style=plastic  
[shield:ckan]:       https://img.shields.io/badge/CKAN-Indexed-blue.svg?style=plastic  
[shield:github]:     https://img.shields.io/badge/Github-Indexed-blue.svg?style=plastic&logo=github  
[shield:curseforge]: https://img.shields.io/badge/CurseForge-Listed-blue.svg?style=plastic  
[shield:avcvalid]:   https://github.com/zer0Kerbal/KSPChromaControl/workflows/Validate0AVC0.version0files/badge.svg "thank you to DasSkelett"  

[LINK:zer0Kerbal]:     https://forum.kerbalspaceprogram.com/index.php?/profile/190933-zer0kerbal/ "zer0Kerbal" 
