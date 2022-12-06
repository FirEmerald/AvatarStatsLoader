using MelonLoader;
using HarmonyLib;
using SLZ.VRMK;
using Il2CppSystem.IO;
using Newtonsoft.Json;

namespace AvatarStatsLoader
{
    public class AvatarStatsMod : MelonMod
    {
        internal static readonly string STATS_FOLDER = MelonUtils.UserDataDirectory + "\\AvatarStats";
        internal static readonly string STATS_DEV_FOLDER = STATS_FOLDER + "\\dev";
        internal static readonly string MASS_FOLDER = MelonUtils.UserDataDirectory + "\\AvatarMass";
        internal static readonly string MASS_DEV_FOLDER = MASS_FOLDER + "\\dev";
        internal static AvatarStatsMod instance;
        internal static MelonPreferences_Category mpCat;
        internal static MelonPreferences_Entry<bool> devMode;

        public AvatarStatsMod() => instance = this;

        public override void OnInitializeMelon()
        {
            mpCat = MelonPreferences.CreateCategory(nameof(AvatarStatsMod));
            devMode = mpCat.CreateEntry("devMode", false, "devMode", "Developer mode - will output default loaded avatar stats to " + STATS_DEV_FOLDER + " and masses to " + MASS_DEV_FOLDER, false, false, null, null);
            mpCat.SaveToFile(true);
        }

        internal static void Log(string str) => instance.LoggerInstance.Msg(str);

        //internal static void Log(object obj) => instance.LoggerInstance.Msg(obj?.ToString() ?? "null");

        //internal static void Warn(string str) => instance.LoggerInstance.Warning(str);

        //internal static void Warn(object obj) => instance.LoggerInstance.Warning(obj?.ToString() ?? "null");

        //internal static void Error(string str) => instance.LoggerInstance.Error(str);

        //internal static void Error(object obj) => instance.LoggerInstance.Error(obj?.ToString() ?? "null");
    }

    [HarmonyPatch(typeof(Avatar), "ComputeBaseStats")]
    public static class AvatarComputeStatChange
    {
        public static void Postfix(Avatar __instance)
        {
            string name = __instance.name;
            if (name == "[RealHeptaRig (Marrow1)]") //don't load for empty rig
                return;
            else if (name.EndsWith("(Clone)")) //remove "(Clone)" from mod avatars
                name = __instance.name.Substring(0, __instance.name.Length - "(Clone)".Length);
            if (AvatarStatsMod.devMode.Value)
            {
                if (!Directory.Exists(AvatarStatsMod.STATS_DEV_FOLDER))
                {
                    DirectoryInfo info = Directory.CreateDirectory(AvatarStatsMod.STATS_DEV_FOLDER);
                    AvatarStatsMod.Log("Avatar stats developer folder did not exist, created at " + info.Name);
                }
                string outputFile = AvatarStatsMod.STATS_DEV_FOLDER + "\\" + name + ".json";
                AvatarStatsMod.Log("Saving default stats to " + outputFile);
                File.WriteAllText(outputFile, JsonConvert.SerializeObject(new AvatarStats(__instance)));
            }
            if (!Directory.Exists(AvatarStatsMod.STATS_FOLDER))
            {
                DirectoryInfo info = Directory.CreateDirectory(AvatarStatsMod.STATS_FOLDER);
                AvatarStatsMod.Log("Avatar stats folder did not exist, created at " + info.Name);
            }
            string statsFile = AvatarStatsMod.STATS_FOLDER + "\\" + name + ".json";
            if (File.Exists(statsFile))
            {
                AvatarStatsMod.Log("Overriding stats with values from " + statsFile);
                JsonConvert.DeserializeObject<AvatarStats>(File.ReadAllText(statsFile)).apply(__instance);
            }
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

        public AvatarStats()
        {
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
            string name = __instance.name;
            if (name == "[RealHeptaRig (Marrow1)]") //don't load for empty rig
                return;
            else if (name.EndsWith("(Clone)")) //remove "(Clone)" from mod avatars
                name = __instance.name.Substring(0, __instance.name.Length - "(Clone)".Length);
            if (AvatarStatsMod.devMode.Value)
            {
                if (!Directory.Exists(AvatarStatsMod.MASS_DEV_FOLDER))
                {
                    DirectoryInfo info = Directory.CreateDirectory(AvatarStatsMod.MASS_DEV_FOLDER);
                    AvatarStatsMod.Log("Avatar mass developer folder did not exist, created at " + info.Name);
                }
                string outputFile = AvatarStatsMod.MASS_DEV_FOLDER + "\\" + name + ".json";
                AvatarStatsMod.Log("Saving default mass to " + outputFile);
                File.WriteAllText(outputFile, JsonConvert.SerializeObject(new AvatarMass(__instance, normalizeTo82)));
            }
            if (!Directory.Exists(AvatarStatsMod.MASS_FOLDER))
            {
                DirectoryInfo info = Directory.CreateDirectory(AvatarStatsMod.MASS_FOLDER);
                AvatarStatsMod.Log("Avatar masses folder did not exist, created at " + info.Name);
            }
            string massFile = AvatarStatsMod.MASS_FOLDER + "\\" + name + ".json";
            if (File.Exists(massFile))
            {
                AvatarStatsMod.Log("Overriding mass with values from " + massFile);
                JsonConvert.DeserializeObject<AvatarMass>(File.ReadAllText(massFile)).apply(__instance, normalizeTo82);
            }
        }
    }

    public class AvatarMass
    {
        public float massChest;
        public float massPelvis;
        public float massHead;
        public float massArm;
        public float massLeg;

        public AvatarMass()
        {
        }

        public AvatarMass(Avatar avatar, float scale)
        {
            massChest = avatar._massChest / scale;
            massPelvis = avatar._massPelvis / scale;
            massHead = avatar._massHead / scale;
            massArm = avatar._massArm / scale;
            massLeg = avatar._massLeg / scale;
        }

        public void apply(Avatar avatar, float scale)
        {
            avatar._massChest = massChest * scale;
            avatar._massPelvis = massPelvis * scale;
            avatar._massHead = massHead * scale;
            avatar._massArm = massArm * scale;
            avatar._massLeg = massLeg * scale;
            avatar._massTotal = (massChest + massPelvis + massHead + ((massArm + massLeg) * 2)) * scale;
        }
    }
}
