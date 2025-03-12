using BoneLib.BoneMenu;
using UnityEngine;

namespace AvatarStatsLoader.BoneMenu
{
    class MassesBoneMenu
    {
        public static Page menu;
        public static EntryMenu massChest, massPelvis, massHead, massArm, massLeg;
        public static FunctionElement saveMasses;

        public static void Init()
        {
            menu = Page.Root.CreatePage("Avatar Mass", Color.white);
            massChest = new EntryMenu(menu, "Chest Mass", () => AvatarStatsMod.currentAvatar.GetLoadMassChest(), AvatarStatsMod.massChest);
            massPelvis = new EntryMenu(menu, "Pelvis Mass", () => AvatarStatsMod.currentAvatar.GetLoadMassPelvis(), AvatarStatsMod.massPelvis);
            massHead = new EntryMenu(menu, "Head Mass", () => AvatarStatsMod.currentAvatar.GetLoadMassHead(), AvatarStatsMod.massHead);
            massArm = new EntryMenu(menu, "Arm Mass", () => AvatarStatsMod.currentAvatar.GetLoadMassArm(), AvatarStatsMod.massArm);
            massLeg = new EntryMenu(menu, "Leg Mass", () => AvatarStatsMod.currentAvatar.GetLoadMassLeg(), AvatarStatsMod.massLeg);
            saveMasses = menu.CreateFunction("Save masses", Color.white, AvatarStatsMod.SaveMassesToFile);
        }
    }
}
