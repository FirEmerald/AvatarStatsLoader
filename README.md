# BoneLab AvatarStatsLoader
 Allows loading of customized avatar stats in BoneLab

AvatarStatsLoader loads customized avatar stats from "\<Game directory\>\\UserData\\AvatarStats\\\<avatar name\>.json".

To set up custom stats, first enable devMode in the MelonLoader config ("\<Game directory\>\\UserData\\MelonPreferences.cfg").

Switch to the avatar you wish to set up.

There should be a file, "\<Game directory\>\\UserData\\AvatarStats\\dev\\\<avatar name\>.json", which contains the default values for the avatar.

Copy this file into "\<Game directory\>\\UserData\\AvatarStats\\\<avatar name\>.json".

Use your choice of text editor to adjust the values to your liking.

Finally, re-load the avatar by either loading a new level or switching to a different avatar and then back. The avatar should now have the custom stats you gave it!


This mod was inspired by [StatsOverride](https://bonelab.thunderstore.io/package/extraes/StatOverride/) , and I looked at (disassembled) code for it and for [DynamicBones](https://bonelab.thunderstore.io/package/LlamasHere/Dynamic_Bones/) in order to figure out how to make this mod. NO SIGNIFICANT PORTIONS OF CODE WERE COPIED TO MAKE THIS MOD.