using MelonLoader;
using MelonLoader.Preferences;
using HarmonyLib;
using SLZ.VRMK;
using Il2CppSystem.IO;
using Newtonsoft.Json;

namespace AvatarStatsLoader
{
    public class AvatarStatsMod : MelonMod
    {
        internal static readonly string JSON_FOLDER = MelonUtils.UserDataDirectory + "\\AvatarStats";
        internal static readonly string DEV_FOLDER = MelonUtils.UserDataDirectory + "\\AvatarStats\\dev";
        internal static AvatarStatsMod instance;
        internal static MelonPreferences_Category mpCat;
        internal static MelonPreferences_Entry<bool> devMode;

        public AvatarStatsMod() => instance = this;

        public override void OnInitializeMelon()
        {
            mpCat = MelonPreferences.CreateCategory(nameof(AvatarStatsMod));
            devMode = mpCat.CreateEntry<bool>("devMode", false, "devMode", "Developer mode - will output default loaded avatar stats to " + DEV_FOLDER, false, false, (ValueValidator)null, (string)null);
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
            if (name == "[RealHeptaRig (Marrow1)]")
                return;
            else if (name.EndsWith("(Clone)"))
               name = __instance.name.Substring(0, __instance.name.Length - "(Clone)".Length);
            if (AvatarStatsMod.devMode.Value)
            {
                if (!Directory.Exists(AvatarStatsMod.DEV_FOLDER))
                {
                    DirectoryInfo info = Directory.CreateDirectory(AvatarStatsMod.DEV_FOLDER);
                    AvatarStatsMod.Log("Avatar stats developer folder did not exist, created at " + info.Name);
                }
                string outputFile = AvatarStatsMod.DEV_FOLDER + "\\" + name + ".json";
                AvatarStatsMod.Log("Saving default stats to " + outputFile);
                AvatarStats stats = new AvatarStats(__instance);
                File.WriteAllText(outputFile, JsonConvert.SerializeObject(stats));
            }
            if (!Directory.Exists(AvatarStatsMod.JSON_FOLDER))
            {
                DirectoryInfo info = Directory.CreateDirectory(AvatarStatsMod.JSON_FOLDER);
                AvatarStatsMod.Log("Avatar stats folder did not exist, created at " + info.Name);
            }
            string statsFile = AvatarStatsMod.JSON_FOLDER + "\\" + name + ".json";
            if (File.Exists(statsFile))
            {
                AvatarStatsMod.Log("Overriding stats now!");
                AvatarStats stats = JsonConvert.DeserializeObject<AvatarStats>(File.ReadAllText(statsFile));
                __instance._agility = stats.agility;
                __instance._strengthUpper = stats.strengthUpper;
                __instance._strengthLower = stats.strengthLower;
                __instance._vitality = stats.vitality;
                __instance._speed = stats.speed;
                __instance._intelligence = stats.intelligence;
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
    }
}
