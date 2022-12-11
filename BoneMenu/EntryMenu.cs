using System;
using BoneLib.BoneMenu.Elements;
using MelonLoader;

namespace AvatarStatsLoader.BoneMenu
{
    class EntryMenu
    {
        public readonly MenuCategory menu;
        public readonly EntryFloatElement value;
        public readonly FunctionElement loadFromAvatar, loadFromAvatarCalculated, setToOne;

        public EntryMenu(MenuCategory parentMenu, string name, Func<float> getFromAvatar, MelonPreferences_Entry<float> entry)
        {
            menu = parentMenu.CreateCategory(name, "ffffff");
            value = menu.CreateEntryFloatElement("value", "ffffff", entry);
            loadFromAvatar = menu.CreateFunctionElement("Set to avatar's current value", "ffffff", () => {
                value.SetValue(getFromAvatar.Invoke());
            });
            loadFromAvatarCalculated = menu.CreateFunctionElement("Set to avatar's computed value", "ffffff", () => {
                entry.ResetToDefault();
            });
            setToOne = menu.CreateFunctionElement("Set to 1.0", "ffffff", () => {
                value.SetValue(1.0f);
            });
        }
    }
}
