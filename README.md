# BoneLab AvatarStatsLoader
Allows loading of customized avatar stats in BoneLab<br/>
<br/>
<br/>
AvatarStatsLoader loads customized avatar stats from "\<Game directory\>\\UserData\\AvatarStats\\\<avatar name\>.json".<br/>
<br/>
<br/>
# Setting up custom stats using BoneLib 2.0.0 or newer using the BoneMenu
In-game, switch to the avatar you want to set up.<br/>
**IMPORTANT: DO NOT ATTEMPT TO CHANGE STATS AFTER LOADING A LEVEL (INCLUDING LOADING THE GAME) BEFORE SWAPPING AVATARS AT LEAST ONCE. THERE'S A BUG WHERE DOING SO PUTS YOU INTO A POLYBLANK**<br/>
Open up Menu->Preferences/Options->BoneMenu->Avatar Stats or Avatar Mass<br/>
Select which attribute(s) you would like to change - they should hold the current value for the avatar - and edit them to your liking.<br/>
Finally, if you want the changes to persist when you next load this avatar, click "Save stats" or "Save masses", depending on what you editet.<br/>
<br/>
<br/>
# Setting up custom stats using [MelonPreferencesManager](https://github.com/sinai-dev/MelonPreferencesManager)
In-game, switch to the avatar you want to set up.<br/>
**IMPORTANT: DO NOT ATTEMPT TO CHANGE STATS AFTER LOADING A LEVEL (INCLUDING LOADING THE GAME) BEFORE SWAPPING AVATARS AT LEAST ONCE. THERE'S A BUG WHERE DOING SO PUTS YOU INTO A POLYBLANK**<br/>
Open up the preferences manager and navigate to the AvatarStatsMod options.<br/>
The options will have been populated with the current values for the avatar.<br/>
Edit any values to your liking, and then click "Save preferences" again. Use the "default" button to reset any values to the avatar's normally calculated value.<br/>
Finally, if you want the changes to persist when you next load this avatar, check "Save stats" and/or "Save masses", depending on what you edited, and click "Save Preferences".<br/>
<br/>
<br/>
# A breakdown of the the different stats and what they do:<br/>
Agility: determines how fast an avatar can change direction and accelerate.<br/>
Speed: determines the maximum running speed of an avatar.<br/>
Strength Upper (Arm strength): determines the force with which an avatar can move things with it's hands. Beware - high values will cause issues when holding objects with two hands or climbing!<br/>
Strength Lower (Leg strength): determines jump height. Higher values have greatly diminishing effects - don't expect to be able to super-jump.<br/>
Vitality: determines how much damage the avatar takes from attacks.<br/>
Intelligence: has no affect on an avatar at the moment.<br/>
<br/>
<br/>

## Compilation

To compile this mod, create a file named `AvatarStatsLoader.csproj.user` in the
project. Copy this text into the file, replacing the path in `BONELAB_PATH` with
the path to your BONELAB installation.
```
<?xml version="1.0" encoding="utf-8"?>
<Project>
    <PropertyGroup>
        <BONELAB_PATH>C:\Program Files (x86)\Steam\steamapps\common\BONELAB</BONELAB_PATH>
    </PropertyGroup>
</Project>
```
<br/>
<br/>


This mod was inspired by [StatsOverride](https://bonelab.thunderstore.io/package/extraes/StatOverride/) , and I looked at (disassembled) code for it and for [DynamicBones](https://bonelab.thunderstore.io/package/LlamasHere/Dynamic_Bones/) in order to figure out how to make this mod. NO SIGNIFICANT PORTIONS OF CODE WERE COPIED TO MAKE THIS MOD.<br/>