using System;
using System.Xml.Linq;
using BoneLib.BoneMenu;
using MelonLoader;
using UnityEngine;

namespace AvatarStatsLoader.BoneMenu
{
    class EntryMenu
    {
        public readonly Page menu;
        public readonly FloatElement incrementOne, incrementPointOne, incrementPointZeroOne, incrementPointZeroZeroOne;
        public readonly FunctionElement setToOne, loadFromAvatar, loadFromAvatarCalculated;

        public EntryMenu(Page parentMenu, string name, Func<float> getFromLoaded, MelonPreferences_Entry<float> entry)
        {
            menu = parentMenu.CreatePage(name, Color.white, 7);
            incrementOne = MakeIncrement(menu, 1f, entry);
            incrementPointOne = MakeIncrement(menu, 0.1f, entry);
            incrementPointZeroOne = MakeIncrement(menu, 0.01f, entry);
            incrementPointZeroZeroOne = MakeIncrement(menu, 0.01f, entry);
            setToOne = menu.CreateFunction("Set to 1.0", Color.white, () => entry.Value = 1.0f);
            loadFromAvatar = menu.CreateFunction("Load from avatar's loaded value", Color.white, () => entry.Value = getFromLoaded.Invoke());
            loadFromAvatarCalculated = menu.CreateFunction("Load from avatar's computed value", Color.white, () => entry.ResetToDefault());
            entry.OnEntryValueChanged.Subscribe((prev, cur) =>
            {
                if (cur != incrementOne.Value) incrementOne.Value = cur; //avoid cyclical value setting
                if (cur != incrementPointOne.Value) incrementPointOne.Value = cur; //avoid cyclical value setting
                if (cur != incrementPointZeroOne.Value) incrementPointZeroOne.Value = cur; //avoid cyclical value setting
                if (cur != incrementPointZeroZeroOne.Value) incrementPointZeroZeroOne.Value = cur; //avoid cyclical value setting
            }, int.MaxValue);
        }

        static FloatElement MakeIncrement(Page page, float increment, MelonPreferences_Entry<float> entry)
        {
            return page.CreateFloat("+/-" + increment, Color.white, 0, increment, float.NegativeInfinity, float.PositiveInfinity, value => {
                if (value != entry.Value) entry.Value = value; //avoid cyclical value setting
            });
        }
    }
}
