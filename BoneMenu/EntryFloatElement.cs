﻿using BoneLib.BoneMenu.Elements;
using MelonLoader;
using UnityEngine;

namespace AvatarStatsLoader.BoneMenu
{
    public class EntryFloatElement : MenuElement
    {
        protected readonly MelonPreferences_Entry<float> entry;
        protected readonly float increment;

        public EntryFloatElement(string name, Color color, MelonPreferences_Entry<float> entry, float increment) : base(name, color)
        {
            this.entry = entry;
            this.increment = increment;
        }

        public override ElementType Type => ElementType.Value;

        public override string DisplayValue => entry.Value.ToString();

        public override void OnSelectLeft()
        {
            entry.Value += increment;
        }

        public override void OnSelectRight()
        {
            entry.Value -= increment;
        }
    }
}
