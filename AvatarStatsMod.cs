using MelonLoader;
using HarmonyLib;
using SLZ.VRMK;
using Il2CppSystem.IO;
using Newtonsoft.Json;
using BoneLib;
using System;
using System.Reflection;
using AvatarStatsLoader.BoneMenu;

namespace AvatarStatsLoader
{
    public class AvatarStatsMod : MelonMod
    {
        internal static readonly string STATS_FOLDER = MelonUtils.UserDataDirectory + "\\AvatarStats";
        internal static readonly string MASS_FOLDER = MelonUtils.UserDataDirectory + "\\AvatarMass";
        internal static AvatarStatsMod instance;
        internal static MelonPreferences_Category mpCat;
        internal static MelonPreferences_Entry<float> agility, strengthUpper, strengthLower, vitality, speed, intelligence;
        internal static MelonPreferences_Entry<bool> loadStats, saveStats;
        internal static MelonPreferences_Entry<float> massChest, massPelvis, massHead, massArm, massLeg;
        internal static MelonPreferences_Entry<bool> loadMasses, saveMasses;

        public AvatarStatsMod() => instance = this;

        public override void OnInitializeMelon()
        {
            Hooking.OnSwitchAvatarPostfix += (avatar) => {
                if (avatar != null)
                {
                    if (!avatar.isEmptyRig())
                    {
                        Log("Setting avatar to " + avatar.getName());
                        agility.DefaultValue = avatar.getDefAgility();
                        agility.Value = avatar._agility;
                        strengthUpper.DefaultValue = avatar.getDefStrengthUpper();
                        strengthUpper.Value = avatar._strengthUpper;
                        strengthLower.DefaultValue = avatar.getDefStrengthLower();
                        strengthLower.Value = avatar._strengthLower;
                        vitality.DefaultValue = avatar.getDefVitality();
                        vitality.Value = avatar._vitality;
                        speed.DefaultValue = avatar.getDefSpeed();
                        speed.Value = avatar._speed;
                        intelligence.DefaultValue = avatar.getDefIntelligence();
                        intelligence.Value = avatar._intelligence;
                        massChest.DefaultValue = avatar.getDefMassChest();
                        massChest.Value = avatar._massChest;
                        massPelvis.DefaultValue = avatar.getDefMassPelvis();
                        massPelvis.Value = avatar._massPelvis;
                        massHead.DefaultValue = avatar.getDefMassHead();
                        massHead.Value = avatar._massHead;
                        massArm.DefaultValue = avatar.getDefMassArm();
                        massArm.Value = avatar._massArm;
                        massLeg.DefaultValue = avatar.getDefMassLeg();
                        massLeg.Value = avatar._massLeg;
                    }
                }
                else
                {
                    Log("Setting avatar to null");
                }
            };
            mpCat = MelonPreferences.CreateCategory(nameof(AvatarStatsMod));
            agility = mpCat.CreateEntry("agility", 0f, "Agility", "Determines how fast an avatar can acclerate or decelerate.", dont_save_default:true);
            strengthUpper = mpCat.CreateEntry("strengthUpper", 0f, "Arm strength", "Determines the arm strength, affecting weapon holding and climbing.", dont_save_default: true);
            strengthLower = mpCat.CreateEntry("strengthLower", 0f, "Leg strength", "Determines leg strength, affecting running and jumping.", dont_save_default: true);
            vitality = mpCat.CreateEntry("vitality", 0f, "Vitality", "Determines how much damage an avatar takes.", dont_save_default: true);
            speed = mpCat.CreateEntry("speed", 0f, "Speed", "Determines how fast an avatar can run.", dont_save_default: true);
            intelligence = mpCat.CreateEntry("intelligence", 0f, "Intelligence", "Currently has no effect.", dont_save_default: true);
            /*
            LemonAction<float, float> refreshStatsAct = (prev, cur) => reloadAvatarStats();
            agility.OnEntryValueChanged.Subscribe(refreshStatsAct, int.MaxValue);
            strengthUpper.OnEntryValueChanged.Subscribe(refreshStatsAct, int.MaxValue);
            strengthLower.OnEntryValueChanged.Subscribe(refreshStatsAct, int.MaxValue);
            vitality.OnEntryValueChanged.Subscribe(refreshStatsAct, int.MaxValue);
            speed.OnEntryValueChanged.Subscribe(refreshStatsAct, int.MaxValue)
            intelligence.OnEntryValueChanged.Subscribe(refreshStatsAct, int.MaxValue);
            */
            loadStats = mpCat.CreateEntry("loadStats", false, "Reload stats", "reloads the stats of the current avatar into the preferences.");
            loadStats.OnEntryValueChanged.Subscribe((prev, cur) => {
                if (cur)
                {
                    loadStatValues();
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
            /*
            LemonAction<float, float> refreshMassAct = (prev, cur) => reloadAvatarMass();
            massChest.OnEntryValueChanged.Subscribe(refreshMassAct, int.MaxValue);
            massPelvis.OnEntryValueChanged.Subscribe(refreshMassAct, int.MaxValue);
            massHead.OnEntryValueChanged.Subscribe(refreshMassAct, int.MaxValue);
            massArm.OnEntryValueChanged.Subscribe(refreshMassAct, int.MaxValue);
            massLeg.OnEntryValueChanged.Subscribe(refreshMassAct, int.MaxValue);
            */
            loadMasses = mpCat.CreateEntry("loadMasses", false, "Reload masses", "Reloads the mass of the current avatar into the preferences.");
            loadMasses.OnEntryValueChanged.Subscribe((prev, cur) => {
                if (cur)
                {
                    loadMassValues();
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
        }

        public override void OnLateInitializeMelon()
        {
            Type bonelibType = typeof(BoneLib.BuildInfo);
            FieldInfo versionField = bonelibType.GetField("Version");
            Version boneLibVersion;
            bool versionParsed = Version.TryParse(versionField.GetRawConstantValue() as string, out boneLibVersion);
            if (versionParsed)
            {
                if (boneLibVersion.Major >= 2) //add bonemenu for bonelib 2.0.0+
                {
                    Log("BoneLib >= 2.0.0 detected, adding to BoneMenu");
                    StatsBoneMenu.init();
                    MassesBoneMenu.init();
                }
                else
                    Log("BoneLib < 2.0.0 detected, BoneMenu functionality disabled. Consider updating BoneLib if possible.");
            }
            else
                Warn("Could not parse BoneLib version, not loading BoneMenu functionality");
        }

        /*
        public void reloadAvatarStats()
        {
            Avatar lastLoadedAvatar = Player.GetCurrentAvatar();
            if (lastLoadedAvatar != null && !lastLoadedAvatar.getLoadingStats())
            {
                lastLoadedAvatar.setOverriding(true);
                lastLoadedAvatar.RefreshBodyMeasurements();
                lastLoadedAvatar.setOverriding(false);
            }
        }

        public void reloadAvatarMass()
        {
            Avatar lastLoadedAvatar = Player.GetCurrentAvatar();
            if (lastLoadedAvatar != null && !lastLoadedAvatar.getLoadingMass())
            {
                lastLoadedAvatar.setOverriding(true);
                lastLoadedAvatar.RefreshBodyMeasurements();
                lastLoadedAvatar.setOverriding(false);
            }
        }
        */

        public static void SaveStatsToFile()
        {
            Avatar lastLoadedAvatar = Player.GetCurrentAvatar();
            if (lastLoadedAvatar != null)
            {
                if (!lastLoadedAvatar.isEmptyRig())
                {
                    if (!Directory.Exists(AvatarStatsMod.STATS_FOLDER))
                    {
                        DirectoryInfo info = Directory.CreateDirectory(AvatarStatsMod.STATS_FOLDER);
                        Log("Avatar stats folder did not exist, created at " + info.Name);
                    }
                    string statsFile = AvatarStatsMod.STATS_FOLDER + "\\" + lastLoadedAvatar.getName() + ".json";
                    Log("Saving stats to " + statsFile);
                    File.WriteAllText(statsFile, JsonConvert.SerializeObject(new AvatarStats(agility.Value, strengthUpper.Value, strengthLower.Value, vitality.Value, speed.Value, intelligence.Value)));
                }
            }
        }

        internal static void loadStatValues()
        {
            Avatar lastLoadedAvatar = Player.GetCurrentAvatar();
            if (lastLoadedAvatar != null) loadStatValues(lastLoadedAvatar);
        }

        internal static void loadStatValues(Avatar avatar)
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
            Avatar lastLoadedAvatar = Player.GetCurrentAvatar();
            if (lastLoadedAvatar != null)
            {
                if (!lastLoadedAvatar.isEmptyRig())
                {
                    if (!Directory.Exists(AvatarStatsMod.MASS_FOLDER))
                    {
                        DirectoryInfo info = Directory.CreateDirectory(AvatarStatsMod.MASS_FOLDER);
                        AvatarStatsMod.Log("Avatar masses folder did not exist, created at " + info.Name);
                    }
                    string massFile = AvatarStatsMod.MASS_FOLDER + "\\" + lastLoadedAvatar.getName() + ".json";
                    AvatarStatsMod.Log("Saving masses to " + massFile);
                    File.WriteAllText(massFile, JsonConvert.SerializeObject(new AvatarMass(massChest.Value, massPelvis.Value, massHead.Value, massArm.Value, massLeg.Value)));
                }
            }
        }

        internal static void loadMassValues()
        {
            Avatar lastLoadedAvatar = Player.GetCurrentAvatar();
            if (lastLoadedAvatar != null) loadMassValues(lastLoadedAvatar);
        }

        internal static void loadMassValues(Avatar avatar)
        {
            massChest.Value = avatar._massChest;
            massPelvis.Value = avatar._massPelvis;
            massHead.Value = avatar._massHead;
            massArm.Value = avatar._massArm;
            massLeg.Value = avatar._massLeg;
        }

        internal static void Log(string str) => instance.LoggerInstance.Msg(str);

        internal static void Log(object obj) => instance.LoggerInstance.Msg(obj?.ToString() ?? "null");

        internal static void Warn(string str) => instance.LoggerInstance.Warning(str);

        internal static void Warn(object obj) => instance.LoggerInstance.Warning(obj?.ToString() ?? "null");

        internal static void Error(string str) => instance.LoggerInstance.Error(str);

        internal static void Error(object obj) => instance.LoggerInstance.Error(obj?.ToString() ?? "null");
    }

    [HarmonyPatch(typeof(Avatar), "ComputeBaseStats")]
    public static class AvatarComputeStatChange
    {
        public static void Postfix(Avatar __instance)
        {
            if (__instance.isEmptyRig())
                return;
            string name = __instance.getName();
            /*
            if (__instance.getOverriding())
            {
                AvatarStatsMod.Log("Overriding stats for " + name + " with values from preferences.");
                __instance._agility = AvatarStatsMod.agility.Value;
                __instance._strengthUpper = AvatarStatsMod.strengthUpper.Value;
                __instance._strengthUpper = AvatarStatsMod.strengthUpper.Value;
                __instance._vitality = AvatarStatsMod.vitality.Value;
                __instance._speed = AvatarStatsMod.speed.Value;
                __instance._intelligence = AvatarStatsMod.intelligence.Value;
            }
            else
            {
                __instance.setLoadingStats(true);
            */
                __instance.setDefStats();
                AvatarStatsMod.Log("Load stats: " + name);
                if (Directory.Exists(AvatarStatsMod.STATS_FOLDER))
                {
                    string statsFile = AvatarStatsMod.STATS_FOLDER + "\\" + name + ".json";
                    if (File.Exists(statsFile))
                    {
                        AvatarStatsMod.Log("Overriding stats with values from " + statsFile);
                        JsonConvert.DeserializeObject<AvatarStats>(File.ReadAllText(statsFile)).apply(__instance);
                    }
                }
            //    __instance.setLoadingStats(false);
            //}
        }
    }
    public class AvatarStats
    {
        public float agility;
        public float strengthUpper;
        public float strengthLower;
        public float vitality;
        public float speed;
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

        public void apply(Avatar avatar)
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
            if (__instance.isEmptyRig())
                return;
            string name = __instance.getName();
            /*
            if (__instance.getOverriding())
            {
                AvatarStatsMod.Log("Overriding mass for " + name + " with values from preferences.");
                __instance._massChest = AvatarStatsMod.massChest.Value;
                __instance._massPelvis = AvatarStatsMod.massPelvis.Value;
                __instance._massHead = AvatarStatsMod.massHead.Value;
                __instance._massArm = AvatarStatsMod.massArm.Value;
                __instance._massLeg = AvatarStatsMod.massLeg.Value;
                __instance.recalculateTotalMass();
            }
            else
            {
                __instance.setLoadingMass(true);
            */
            __instance.setDefMasses();
                AvatarStatsMod.Log("Load mass: " + name);
                if (Directory.Exists(AvatarStatsMod.MASS_FOLDER))
                {
                    string massFile = AvatarStatsMod.MASS_FOLDER + "\\" + name + ".json";
                    if (File.Exists(massFile))
                    {
                        AvatarStatsMod.Log("Overriding mass with values from " + massFile);
                        JsonConvert.DeserializeObject<AvatarMass>(File.ReadAllText(massFile)).apply(__instance);
                    }
                }
            //    __instance.setLoadingMass(false);
            //}
        }
    }

    public class AvatarMass
    {
        public float massChest;
        public float massPelvis;
        public float massHead;
        public float massArm;
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

        public void apply(Avatar avatar)
        {
            avatar._massChest = massChest;
            avatar._massPelvis = massPelvis;
            avatar._massHead = massHead;
            avatar._massArm = massArm;
            avatar._massLeg = massLeg;
            avatar.recalculateTotalMass();
        }
    }
}
