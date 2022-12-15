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
            massChest = new EntryMenu(menu, "Chest Mass", () => AvatarStatsMod.currentAvatar.getLoadMassChest(), AvatarStatsMod.massChest);
            massPelvis = new EntryMenu(menu, "Pelvis Mass", () => AvatarStatsMod.currentAvatar.getLoadMassPelvis(), AvatarStatsMod.massPelvis);
            massHead = new EntryMenu(menu, "Head Mass", () => AvatarStatsMod.currentAvatar.getLoadMassHead(), AvatarStatsMod.massHead);
            massArm = new EntryMenu(menu, "Arm Mass", () => AvatarStatsMod.currentAvatar.getLoadMassArm(), AvatarStatsMod.massArm);
            massLeg = new EntryMenu(menu, "Leg Mass", () => AvatarStatsMod.currentAvatar.getLoadMassLeg(), AvatarStatsMod.massLeg);
            saveMasses = menu.CreateFunctionElement("Save masses", "ffffff", () => AvatarStatsMod.SaveMassesToFile());
        }
    }
}
