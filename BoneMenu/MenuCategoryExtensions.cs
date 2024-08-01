using BoneLib;
using BoneLib.BoneMenu.Elements;
using MelonLoader;
using UnityEngine;

namespace AvatarStatsLoader.BoneMenu
{
    public static class MenuCategoryExtensions
    {
        public static EntryFloatElement CreateEntryFloatElement(this MenuCategory category, string name, string hexColor, MelonPreferences_Entry<float> entry, float increment)
        {
            ColorUtility.DoTryParseHtmlColor(hexColor, out Color32 color32);
            return category.CreateEntryElement(name, color32, entry, increment);
        }

        public static EntryFloatElement CreateEntryElement(this MenuCategory category, string name, Color color, MelonPreferences_Entry<float> entry, float increment)
        {
            EntryFloatElement floatElement = new(name, color, entry, increment);
            category.Elements?.Add(floatElement);
            SafeActions.InvokeActionSafe<MenuCategory, MenuElement>(MenuCategory.OnElementCreated, category, floatElement);
            return floatElement;
        }

        public static EntryFloatIncrementElement CreateEntryFloatIncrementElement(this MenuCategory category, string hexColor, MelonPreferences_Entry<float> entry, float increment)
        {
            ColorUtility.DoTryParseHtmlColor(hexColor, out Color32 color32);
            return category.CreateEntryFloatIncrementElement(color32, entry, increment);
        }

        public static EntryFloatIncrementElement CreateEntryFloatIncrementElement(this MenuCategory category, Color color, MelonPreferences_Entry<float> entry, float increment)
        {
            EntryFloatIncrementElement floatElement = new("", color, entry, increment);
            category.Elements?.Add(floatElement);
            SafeActions.InvokeActionSafe<MenuCategory, MenuElement>(MenuCategory.OnElementCreated, category, floatElement);
            return floatElement;
        }
    }
}
