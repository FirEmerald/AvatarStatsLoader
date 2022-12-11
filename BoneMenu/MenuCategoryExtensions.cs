using BoneLib;
using BoneLib.BoneMenu.Elements;
using MelonLoader;
using UnityEngine;

namespace AvatarStatsLoader.BoneMenu
{
    public static class MenuCategoryExtensions
    {
        public static EntryFloatElement CreateEntryFloatElement(this MenuCategory category, string name, string hexColor, MelonPreferences_Entry<float> entry)
        {
            Color32 color32;
            ColorUtility.DoTryParseHtmlColor(hexColor, out color32);
            return category.CreateEntryFloatElement(name, color32, entry);
        }

        public static EntryFloatElement CreateEntryFloatElement(this MenuCategory category, string name, Color color, MelonPreferences_Entry<float> entry)
        {
            EntryFloatElement floatElement = new EntryFloatElement(name, color, entry);
            category.Elements?.Add(floatElement);
            SafeActions.InvokeActionSafe<MenuCategory, MenuElement>(MenuCategory.OnElementCreated, category, floatElement);
            return floatElement;
        }
    }
}
