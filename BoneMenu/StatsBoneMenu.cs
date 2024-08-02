using BoneLib.BoneMenu;
using UnityEngine;

namespace AvatarStatsLoader.BoneMenu
{
    class StatsBoneMenu
    {
        public static Page menu;
        public static EntryMenu agility, strengthUpper, strengthLower, vitality, speed, intelligence;
        public static FunctionElement saveStats, loadStats;

        public static void Init()
        {
            menu = Menu.CreatePage("Avatar Stats", Color.white, 7);
            agility = new EntryMenu(menu, "Agility", () => AvatarStatsMod.currentAvatar.GetLoadAgility(), AvatarStatsMod.agility);
            strengthUpper = new EntryMenu(menu, "Strength Upper", () => AvatarStatsMod.currentAvatar.GetLoadStrengthUpper(), AvatarStatsMod.strengthUpper);
            strengthLower = new EntryMenu(menu, "Strength Lower", () => AvatarStatsMod.currentAvatar.GetLoadStrengthLower(), AvatarStatsMod.strengthLower);
            vitality = new EntryMenu(menu, "Vitality", () => AvatarStatsMod.currentAvatar.GetLoadVitality(), AvatarStatsMod.vitality);
            speed = new EntryMenu(menu, "Speed", () => AvatarStatsMod.currentAvatar.GetLoadSpeed(), AvatarStatsMod.speed);
            intelligence = new EntryMenu(menu, "Intelligence", () => AvatarStatsMod.currentAvatar.GetLoadIntelligence(), AvatarStatsMod.intelligence);
            saveStats = menu.CreateFunction("Save stats", Color.white, AvatarStatsMod.SaveStatsToFile);
        }
    }
}
