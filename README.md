# BoneLab AvatarStatsLoader
Allows loading of customized avatar stats in BoneLab<br/>
<br/>
<br/>
AvatarStatsLoader loads customized avatar stats from "\<Game directory\>\\UserData\\AvatarStats\\\<avatar name\>.json".<br/>
To set up custom stats, you'll first want [MelonPreferencesManager](https://github.com/sinai-dev/MelonPreferencesManager)<br/>
Then, in-game, switch to the avatar you want to set up.<br/>
Open up the preferences manager and navigate to the AvatarStatsMod options.<br/>
The options will have been populated with the current values for the avatar.<br/>
Edit any values to your liking, and then click "Save preferences" again. Use the "default" button to reset any values to the avatar's normally calculated value.<br/>
Finally, check "Save stats" and/or "Save masses", depending on what you edited, and click "Save Preferences".<br/>
Your changes will have been saved to the appropriate file(s). To see them in-game, simply swap to another avatar and back or load a new level.<br/>
<br/>
<br/>
A breakdown of the the different stats and what they do:<br/>
Agility: determines how fast an avatar can change direction and accelerate.<br/>
Speed: determines the maximum running speed of an avatar.<br/>
Strength Upper (Arm strength): determines the force with which an avatar can move things with it's hands. Beware - high values will cause issues when holding objects with two hands or climbing!<br/>
Strength Lower (Leg strength): determines jump height. Higher values have greatly diminishing effects - don't expect to be able to super-jump.<br/>
Vitality: determines how much damage the avatar takes from attacks.<br/>
Intelligence: has no affect on an avatar at the moment.<br/>
<br/>
<br/>
This mod was inspired by [StatsOverride](https://bonelab.thunderstore.io/package/extraes/StatOverride/) , and I looked at (disassembled) code for it and for [DynamicBones](https://bonelab.thunderstore.io/package/LlamasHere/Dynamic_Bones/) in order to figure out how to make this mod. NO SIGNIFICANT PORTIONS OF CODE WERE COPIED TO MAKE THIS MOD.<br/>