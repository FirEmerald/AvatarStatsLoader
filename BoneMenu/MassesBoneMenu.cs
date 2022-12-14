using BoneLib;
using BoneLib.BoneMenu;
using BoneLib.BoneMenu.Elements;

namespace AvatarStatsLoader.BoneMenu
{
    class MassesBoneMenu
    {
        public static MenuCategory menu;
        public static EntryMenu massChest, massPelvis, massHead, massArm, massLeg;
        public static FunctionElement saveMasses;

        public static void init()
        {
            menu = MenuManager.CreateCategory("Avatar Mass", "ffffff");
            massChest = new EntryMenu(menu, "Chest Mass", () => Player.GetCurrentAvatar().getLoadMassChest(), AvatarStatsMod.massChest);
            massPelvis = new EntryMenu(menu, "Pelvis Mass", () => Player.GetCurrentAvatar().getLoadMassPelvis(), AvatarStatsMod.massPelvis);
            massHead = new EntryMenu(menu, "Head Mass", () => Player.GetCurrentAvatar().getLoadMassHead(), AvatarStatsMod.massHead);
            massArm = new EntryMenu(menu, "Arm Mass", () => Player.GetCurrentAvatar().getLoadMassArm(), AvatarStatsMod.massArm);
            massLeg = new EntryMenu(menu, "Leg Mass", () => Player.GetCurrentAvatar().getLoadMassLeg(), AvatarStatsMod.massLeg);
            saveMasses = menu.CreateFunctionElement("Save masses", "ffffff", () => AvatarStatsMod.SaveMassesToFile());
        }
    }
}
