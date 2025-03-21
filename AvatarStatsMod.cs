﻿using MelonLoader;
using HarmonyLib;
using Il2CppSLZ.VRMK;
using Il2CppSystem.IO;
using System.Text.Json;
using BoneLib;
using System;
using System.Reflection;
using System.Text.Json.Serialization;
using AvatarStatsLoader.BoneMenu;
using MelonLoader.Utils;

namespace AvatarStatsLoader
{
    public class AssemblyInfo
    {
        public const string Name = "Avatar Stats Loader";
        public const string Product = "AvatarStatsLoader";
        public const string Description = "Customized stats loader for BoneLab";
        public const string Version = "1.3.5";
        public const string Author = "FirEmerald";
        public const string Copyright = $"Copyright © {Author} 2024";
        public const string URL = "https://bonelab.thunderstore.io/package/FirEmerald/AvatarStatsLoader/";
    }

    public class AvatarStatsMod : MelonMod
    {
        internal static readonly string STATS_FOLDER = Path.Combine(MelonEnvironment.UserDataDirectory, "AvatarStats");
        internal static readonly string MASS_FOLDER = Path.Combine(MelonEnvironment.UserDataDirectory, "AvatarMass");
        internal static AvatarStatsMod instance;
        internal static MelonPreferences_Category mpCat;
        internal static MelonPreferences_Entry<float> agility, strengthUpper, strengthLower, vitality, speed, intelligence;
        internal static MelonPreferences_Entry<bool> loadStats, saveStats;
        internal static MelonPreferences_Entry<float> massChest, massPelvis, massHead, massArm, massLeg;
        internal static MelonPreferences_Entry<bool> loadMasses, saveMasses;
        internal static Avatar currentAvatar = null; //we cannot use BoneLib's hook for this now
        internal static Boolean isLoadingAvatarValues = false;
        internal static readonly JsonSerializerOptions jsonOpts = new() { WriteIndented = true, AllowTrailingCommas = true };

    public AvatarStatsMod() => instance = this;

        public override void OnInitializeMelon()
        {
            Hooking.OnSwitchAvatarPostfix += (avatar) => {
                if (avatar != null && !avatar.IsEmptyRig())
                {
                    if (avatar != currentAvatar)
                    {
                        Log("Setting avatar to " + avatar.GetName());
                        isLoadingAvatarValues = true;
                        agility.DefaultValue = avatar.GetDefAgility();
                        agility.Value = avatar._agility;
                        strengthUpper.DefaultValue = avatar.GetDefStrengthUpper();
                        strengthUpper.Value = avatar._strengthUpper;
                        strengthLower.DefaultValue = avatar.GetDefStrengthLower();
                        strengthLower.Value = avatar._strengthLower;
                        vitality.DefaultValue = avatar.GetDefVitality();
                        vitality.Value = avatar._vitality;
                        speed.DefaultValue = avatar.GetDefSpeed();
                        speed.Value = avatar._speed;
                        intelligence.DefaultValue = avatar.GetDefIntelligence();
                        intelligence.Value = avatar._intelligence;
                        massChest.DefaultValue = avatar.GetDefMassChest();
                        massChest.Value = avatar._massChest;
                        massPelvis.DefaultValue = avatar.GetDefMassPelvis();
                        massPelvis.Value = avatar._massPelvis;
                        massHead.DefaultValue = avatar.GetDefMassHead();
                        massHead.Value = avatar._massHead;
                        massArm.DefaultValue = avatar.GetDefMassArm();
                        massArm.Value = avatar._massArm;
                        massLeg.DefaultValue = avatar.GetDefMassLeg();
                        massLeg.Value = avatar._massLeg;
                        currentAvatar = avatar;
                        isLoadingAvatarValues = false;
                    }
                }
                else
                {
                    Log("Setting avatar to null");
                    currentAvatar = null;
                }
            };

            static void refreshAvatarAct(float prev, float cur) => RefreshAvatarStats();
            mpCat = MelonPreferences.CreateCategory(nameof(AvatarStatsMod));
            agility = mpCat.CreateEntry("agility", 0f, "Agility", "Determines how fast an avatar can acclerate or decelerate.", dont_save_default:true);
            strengthUpper = mpCat.CreateEntry("strengthUpper", 0f, "Arm strength", "Determines the arm strength, affecting weapon holding and climbing.", dont_save_default: true);
            strengthLower = mpCat.CreateEntry("strengthLower", 0f, "Leg strength", "Determines leg strength, affecting running and jumping.", dont_save_default: true);
            vitality = mpCat.CreateEntry("vitality", 0f, "Vitality", "Determines how much damage an avatar takes.", dont_save_default: true);
            speed = mpCat.CreateEntry("speed", 0f, "Speed", "Determines how fast an avatar can run.", dont_save_default: true);
            intelligence = mpCat.CreateEntry("intelligence", 0f, "Intelligence", "Currently has no effect.", dont_save_default: true);
            agility.OnEntryValueChanged.Subscribe(refreshAvatarAct, int.MaxValue);
            strengthUpper.OnEntryValueChanged.Subscribe(refreshAvatarAct, int.MaxValue);
            strengthLower.OnEntryValueChanged.Subscribe(refreshAvatarAct, int.MaxValue);
            vitality.OnEntryValueChanged.Subscribe(refreshAvatarAct, int.MaxValue);
            speed.OnEntryValueChanged.Subscribe(refreshAvatarAct, int.MaxValue);
            intelligence.OnEntryValueChanged.Subscribe(refreshAvatarAct, int.MaxValue);
            loadStats = mpCat.CreateEntry("loadStats", false, "Reload stats", "loads the previously loaded stats of the current avatar into the preferences.");
            loadStats.OnEntryValueChanged.Subscribe((prev, cur) => {
                if (cur)
                {
                    LoadStatValues();
                    loadStats.ResetToDefault();
                }
            });
            saveStats = mpCat.CreateEntry("saveStats", false, "Save stats", "Saves the current stat preferences into the override file of the current avatar.");
            saveStats.OnEntryValueChanged.Subscribe((prev, cur) => {
                if (cur)
                {
                    SaveStatsToFile();
                    saveStats.ResetToDefault();
                }
            });
            massChest = mpCat.CreateEntry("massChest", 0f, "Chest mass", "Chest mass of the last loaded avatar.", dont_save_default: true);
            massPelvis = mpCat.CreateEntry("massPelvis", 0f, "Pelvis mass", "Pelvis mass of the last loaded avatar.", dont_save_default: true);
            massHead = mpCat.CreateEntry("massHead", 0f, "Head mass", "Head mass of the last loaded avatar.", dont_save_default: true);
            massArm = mpCat.CreateEntry("massArm", 0f, "Arm mass", "Arm mass of the last loaded avatar.", dont_save_default: true);
            massLeg = mpCat.CreateEntry("massLeg", 0f, "Leg mass", "Leg mass of the last loaded avatar.", dont_save_default: true);
            massChest.OnEntryValueChanged.Subscribe(refreshAvatarAct, int.MaxValue);
            massPelvis.OnEntryValueChanged.Subscribe(refreshAvatarAct, int.MaxValue);
            massHead.OnEntryValueChanged.Subscribe(refreshAvatarAct, int.MaxValue);
            massArm.OnEntryValueChanged.Subscribe(refreshAvatarAct, int.MaxValue);
            massLeg.OnEntryValueChanged.Subscribe(refreshAvatarAct, int.MaxValue);
            loadMasses = mpCat.CreateEntry("loadMasses", false, "Reload masses", "Reloads the mass of the current avatar into the preferences.");
            loadMasses.OnEntryValueChanged.Subscribe((prev, cur) => {
                if (cur)
                {
                    LoadMassValues();
                    loadMasses.ResetToDefault();
                }
            });
            saveMasses = mpCat.CreateEntry("saveMasses", false, "Save masses", "Saves the current mass preferences into the override file of the current avatar.");
            saveMasses.OnEntryValueChanged.Subscribe((prev, cur) => {
                if (cur)
                {
                    SaveMassesToFile();
                    saveMasses.ResetToDefault();
                }
            });
            mpCat.SaveToFile(true);
            StatsBoneMenu.Init();
            MassesBoneMenu.Init();
        }

        
        public static void RefreshAvatarStats()
        {
            if (isLoadingAvatarValues) return; //do not refresh when loading
            if (currentAvatar != null)
            {
                Log("Refreshing " + currentAvatar.GetName());
                try
                {
                    //Player.rigManager.ava
                    //Player.rigManager.onAvatarSwapped.Invoke(); //no change
                    //Player.GetPhysicsRig().SetAvatar(currentAvatar); //no change
                    //Player.controllerRig.SetAvatar(currentAvatar);
                    //Player.physicsRig.SetAvatar(currentAvatar);
                    //Player.rigManager._avatarDirty = true; //initially loaded avatar gets reset to polyblank for some reason. switching to a different avatar and back fixes it.
                    //Player.rigManager.SwitchAvatar(currentAvatar); //same effect as above whilst calling more code
                    Player.RigManager.SwapAvatar(currentAvatar); //same effect as above whilst calling more code
                    //Player.rigManager.SwapAvatarCrate(Player.rigManager.AvatarCrate.Barcode, false, null); re-loads entire avatar, preventing overrides from applying
                }
                catch (Exception e)
                {
                    Error("An error occurred attempting to refresh avatar stats.", e);
                }
            }
        }
        

        public static void SaveStatsToFile()
        {
            if (currentAvatar != null)
            {
                if (!Directory.Exists(AvatarStatsMod.STATS_FOLDER))
                {
                    DirectoryInfo info = Directory.CreateDirectory(AvatarStatsMod.STATS_FOLDER);
                    Log("Avatar stats folder did not exist, created at " + info.Name);
                }
                string statsFile = Path.Combine(AvatarStatsMod.STATS_FOLDER, currentAvatar.GetName() + ".json");
                //string statsFile = AvatarStatsMod.STATS_FOLDER + "\\" + Player.rigManager.AvatarCrate.Barcode.ID + ".json";
                Log("Saving stats to " + statsFile);
                File.WriteAllText(statsFile, JsonSerializer.Serialize(new AvatarStats(agility.Value, strengthUpper.Value, strengthLower.Value, vitality.Value, speed.Value, intelligence.Value), jsonOpts));
            }
        }

        internal static void LoadStatValues()
        {
            if (currentAvatar != null)
                LoadStatValues(currentAvatar);
        }

        internal static void LoadStatValues(Avatar avatar)
        {
            agility.Value = avatar._agility;
            strengthUpper.Value = avatar._strengthUpper;
            strengthLower.Value = avatar._strengthLower;
            vitality.Value = avatar._vitality;
            speed.Value = avatar._speed;
            intelligence.Value = avatar._intelligence;
        }

        public static void SaveMassesToFile()
        {
            if (currentAvatar != null)
            {
                if (!Directory.Exists(AvatarStatsMod.MASS_FOLDER))
                {
                    DirectoryInfo info = Directory.CreateDirectory(AvatarStatsMod.MASS_FOLDER);
                    AvatarStatsMod.Log("Avatar masses folder did not exist, created at " + info.Name);
                }
                string massFile = Path.Combine(AvatarStatsMod.MASS_FOLDER, currentAvatar.GetName() + ".json");
                AvatarStatsMod.Log("Saving masses to " + massFile);
                File.WriteAllText(massFile, JsonSerializer.Serialize(new AvatarMass(massChest.Value, massPelvis.Value, massHead.Value, massArm.Value, massLeg.Value), jsonOpts));
            }
        }

        internal static void LoadMassValues()
        {
            if (currentAvatar != null)
                LoadMassValues(currentAvatar);
        }

        internal static void LoadMassValues(Avatar avatar)
        {
            massChest.Value = avatar._massChest;
            massPelvis.Value = avatar._massPelvis;
            massHead.Value = avatar._massHead;
            massArm.Value = avatar._massArm;
            massLeg.Value = avatar._massLeg;
        }

        internal static void Log(string str) => instance.LoggerInstance.Msg(str);

        internal static void Log(string str, Exception ex) => instance.LoggerInstance.Msg(str, ex);

        internal static void Log(object obj) => instance.LoggerInstance.Msg(obj?.ToString() ?? "null");

        internal static void Warn(string str) => instance.LoggerInstance.Warning(str);

        internal static void Warn(string str, Exception ex) => instance.LoggerInstance.Warning(str, ex);

        internal static void Warn(object obj) => instance.LoggerInstance.Warning(obj?.ToString() ?? "null");

        internal static void Error(string str) => instance.LoggerInstance.Error(str);

        internal static void Error(string str, Exception ex) => instance.LoggerInstance.Error(str, ex);

        internal static void Error(object obj) => instance.LoggerInstance.Error(obj?.ToString() ?? "null");
    }

    [HarmonyPatch(typeof(Avatar), "ComputeBaseStats")]
    public static class AvatarComputeStatChange
    {
        public static void Postfix(Avatar __instance)
        {
            if (__instance.IsEmptyRig())
                return;
            string name = __instance.GetName();
            if (__instance == AvatarStatsMod.currentAvatar)
            {
                AvatarStatsMod.Log("Overriding stats for " + name + " with values from preferences.");
                __instance._agility = AvatarStatsMod.agility.Value;
                __instance._strengthUpper = AvatarStatsMod.strengthUpper.Value;
                __instance._strengthLower = AvatarStatsMod.strengthLower.Value;
                __instance._vitality = AvatarStatsMod.vitality.Value;
                __instance._speed = AvatarStatsMod.speed.Value;
                __instance._intelligence = AvatarStatsMod.intelligence.Value;
            }
            else
            {
                __instance.SetDefStats();
                AvatarStatsMod.Log("Load stats: " + name);
                if (Directory.Exists(AvatarStatsMod.STATS_FOLDER))
                {
                    string statsFile = Path.Combine(AvatarStatsMod.STATS_FOLDER, name + ".json");
                    if (File.Exists(statsFile))
                    {
                        AvatarStatsMod.Log("Overriding stats with values from " + statsFile);
                        JsonSerializer.Deserialize<AvatarStats>(File.ReadAllText(statsFile), AvatarStatsMod.jsonOpts).Apply(__instance);
                    }
                }
                __instance.SetLoadStats();
            }
        }
    }
    public class AvatarStats
    {
        [JsonInclude]
        public float agility;
        [JsonInclude]
        public float strengthUpper;
        [JsonInclude]
        public float strengthLower;
        [JsonInclude]
        public float vitality;
        [JsonInclude]
        public float speed;
        [JsonInclude]
        public float intelligence;

        public AvatarStats() {}

        public AvatarStats(float agility, float strengthUpper, float strengthLower, float vitality, float speed, float intelligence)
        {
            this.agility = agility;
            this.strengthUpper = strengthUpper;
            this.strengthLower = strengthLower;
            this.vitality = vitality;
            this.speed = speed;
            this.intelligence = intelligence;
        }

        public AvatarStats(Avatar avatar)
        {
            agility = avatar._agility;
            strengthUpper = avatar._strengthUpper;
            strengthLower = avatar._strengthLower;
            vitality = avatar._vitality;
            speed = avatar._speed;
            intelligence = avatar._intelligence;
        }

        public void Apply(Avatar avatar)
        {
            avatar._agility = agility;
            avatar._strengthUpper = strengthUpper;
            avatar._strengthLower = strengthLower;
            avatar._vitality = vitality;
            avatar._speed = speed;
            avatar._intelligence = intelligence;
        }
    }

    [HarmonyPatch(typeof(Avatar), "ComputeMass")]
    public static class AvatarComputeMassChange
    {
        public static void Postfix(Avatar __instance, float normalizeTo82)
        {
            if (__instance.IsEmptyRig())
                return;
            string name = __instance.GetName();
            if (__instance == AvatarStatsMod.currentAvatar)
            {
                AvatarStatsMod.Log("Overriding mass for " + name + " with values from preferences.");
                __instance._massChest = AvatarStatsMod.massChest.Value;
                __instance._massPelvis = AvatarStatsMod.massPelvis.Value;
                __instance._massHead = AvatarStatsMod.massHead.Value;
                __instance._massArm = AvatarStatsMod.massArm.Value;
                __instance._massLeg = AvatarStatsMod.massLeg.Value;
                __instance.RecalculateTotalMass();
            }
            else
            {
                __instance.SetDefMasses();
                AvatarStatsMod.Log("Load mass: " + name);
                if (Directory.Exists(AvatarStatsMod.MASS_FOLDER))
                {
                    string massFile = Path.Combine(AvatarStatsMod.MASS_FOLDER, ".json");
                    if (File.Exists(massFile))
                    {
                        AvatarStatsMod.Log("Overriding mass with values from " + massFile);
                        JsonSerializer.Deserialize<AvatarMass>(File.ReadAllText(massFile), AvatarStatsMod.jsonOpts).Apply(__instance);
                    }
                }
                __instance.SetLoadMasses();
            }
        }
    }

    public class AvatarMass
    {
        [JsonInclude]
        public float massChest;
        [JsonInclude]
        public float massPelvis;
        [JsonInclude]
        public float massHead;
        [JsonInclude]
        public float massArm;
        [JsonInclude]
        public float massLeg;

        public AvatarMass() {}

        public AvatarMass(float massChest, float massPelvis, float massHead, float massArm, float massLeg)
        {
            this.massChest = massChest;
            this.massPelvis = massPelvis;
            this.massHead = massHead;
            this.massArm = massArm;
            this.massLeg = massLeg;
        }

        public AvatarMass(Avatar avatar)
        {
            massChest = avatar._massChest;
            massPelvis = avatar._massPelvis;
            massHead = avatar._massHead;
            massArm = avatar._massArm;
            massLeg = avatar._massLeg;
        }

        public void Apply(Avatar avatar)
        {
            avatar._massChest = massChest;
            avatar._massPelvis = massPelvis;
            avatar._massHead = massHead;
            avatar._massArm = massArm;
            avatar._massLeg = massLeg;
            avatar.RecalculateTotalMass();
        }
    }
}
