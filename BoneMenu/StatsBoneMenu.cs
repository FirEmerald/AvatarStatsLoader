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
            agility = new EntryMenu(menu, "Agility", () => Player.GetCurrentAvatar()._agility, AvatarStatsMod.agility);
            strengthUpper = new EntryMenu(menu, "Strength Upper", () => Player.GetCurrentAvatar()._strengthUpper, AvatarStatsMod.strengthUpper);
            strengthLower = new EntryMenu(menu, "Strength Lower", () => Player.GetCurrentAvatar()._strengthLower, AvatarStatsMod.strengthLower);
            vitality = new EntryMenu(menu, "Vitality", () => Player.GetCurrentAvatar()._vitality, AvatarStatsMod.vitality);
            speed = new EntryMenu(menu, "Speed", () => Player.GetCurrentAvatar()._speed, AvatarStatsMod.speed);
            intelligence = new EntryMenu(menu, "Intelligence", () => Player.GetCurrentAvatar()._intelligence, AvatarStatsMod.intelligence);
            saveStats = menu.CreateFunctionElement("Save stats", "ffffff", () => AvatarStatsMod.SaveStatsToFile());
        }
    }
}
