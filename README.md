# ksp-chroma

[![Join the chat at https://gitter.im/cguckes/ksp-chroma](https://badges.gitter.im/cguckes/ksp-chroma.svg)](https://gitter.im/cguckes/ksp-chroma?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge&utm_content=badge)

Good News: No messy external tool needed any more, now works out of the box as a normal KSP mod.

Lights up your keyboard to make playing Kerbal Space Program a lot easier. Currently only supports Razer Chroma Keyboards. If you want me to add support for other devices as well, you'll have to send me one. I can send it back after I'm finished implementing the code.

The mod is still very beta, so let me know if you experience any difficulties when using it.

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
- Vessel heat displayed in three colors (blue = cool, red = warm/hot, yellow = you're in trouble)
- Vessel height above ground displayed on F1 to F4 keys (F1 = 10m, F2 = 50m, F3 = 100m, F4 = 1000m). The intensity changes, e.g. if F1 is fully lit and F2 is half lit, you are 30 meters above ground.

## Installation

1. Unzip the release archive and place the KSPChromaControl folder in your KSP GameData directory.
2. Start KSP and witness the awesomeness of highlighted function keys while kerbaling through space
3. (optional) Move the file ChromaAppInfo.xml from the mod folder into the KSP folder, to allow Synapse to recognize the game properly.
 
## Todo

- Make fuel gauge position device dependent (Blackwidow Chroma / Blackwidow Chroma TE / Orbweaver)
- Light up all keys when the player is entering text (vessel name, savegame, etc.)

- Linux version (this might take a bit longer, but I'm planning to do it anyway. What good is a c# interface if it is only ever implemented once...)
