using BoneLib.BoneMenu;
using BoneLib.BoneMenu.Elements;

namespace AvatarStatsLoader.BoneMenu
{
    class StatsBoneMenu
    {
        public static MenuCategory menu;
        public static EntryMenu agility, strengthUpper, strengthLower, vitality, speed, intelligence;
        public static FunctionElement saveStats;

        public static void Init()
        {
            menu = MenuManager.CreateCategory("Avatar Stats", "ffffff");
            agility = new EntryMenu(menu, "Agility", () => AvatarStatsMod.currentAvatar.getLoadAgility(), AvatarStatsMod.agility);
            strengthUpper = new EntryMenu(menu, "Strength Upper", () => AvatarStatsMod.currentAvatar.getLoadStrengthUpper(), AvatarStatsMod.strengthUpper);
            strengthLower = new EntryMenu(menu, "Strength Lower", () => AvatarStatsMod.currentAvatar.getLoadStrengthLower(), AvatarStatsMod.strengthLower);
            vitality = new EntryMenu(menu, "Vitality", () => AvatarStatsMod.currentAvatar.getLoadVitality(), AvatarStatsMod.vitality);
            speed = new EntryMenu(menu, "Speed", () => AvatarStatsMod.currentAvatar.getLoadSpeed(), AvatarStatsMod.speed);
            intelligence = new EntryMenu(menu, "Intelligence", () => AvatarStatsMod.currentAvatar.getLoadIntelligence(), AvatarStatsMod.intelligence);
            saveStats = menu.CreateFunctionElement("Save stats", "ffffff", () => AvatarStatsMod.SaveStatsToFile());
        }
    }
}
