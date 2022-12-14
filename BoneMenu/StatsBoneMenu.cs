using BoneLib;
using BoneLib.BoneMenu;
using BoneLib.BoneMenu.Elements;

namespace AvatarStatsLoader.BoneMenu
{
    class StatsBoneMenu
    {
        public static MenuCategory menu;
        public static EntryMenu agility, strengthUpper, strengthLower, vitality, speed, intelligence;
        public static FunctionElement saveStats;

        public static void init()
        {
            menu = MenuManager.CreateCategory("Avatar Stats", "ffffff");
            agility = new EntryMenu(menu, "Agility", () => Player.GetCurrentAvatar().getLoadAgility(), AvatarStatsMod.agility);
            strengthUpper = new EntryMenu(menu, "Strength Upper", () => Player.GetCurrentAvatar().getLoadStrengthUpper(), AvatarStatsMod.strengthUpper);
            strengthLower = new EntryMenu(menu, "Strength Lower", () => Player.GetCurrentAvatar().getLoadStrengthLower(), AvatarStatsMod.strengthLower);
            vitality = new EntryMenu(menu, "Vitality", () => Player.GetCurrentAvatar().getLoadVitality(), AvatarStatsMod.vitality);
            speed = new EntryMenu(menu, "Speed", () => Player.GetCurrentAvatar().getLoadSpeed(), AvatarStatsMod.speed);
            intelligence = new EntryMenu(menu, "Intelligence", () => Player.GetCurrentAvatar().getLoadIntelligence(), AvatarStatsMod.intelligence);
            saveStats = menu.CreateFunctionElement("Save stats", "ffffff", () => AvatarStatsMod.SaveStatsToFile());
        }
    }
}
