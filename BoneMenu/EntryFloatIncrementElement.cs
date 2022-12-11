using BoneLib.BoneMenu.Elements;
using MelonLoader;
using UnityEngine;

namespace AvatarStatsLoader.BoneMenu
{
    public class EntryFloatIncrementElement : MenuElement
    {
        protected readonly MelonPreferences_Entry<float> entry;
        protected readonly float increment;

        public EntryFloatIncrementElement(string name, Color color, MelonPreferences_Entry<float> entry, float increment) : base(name, color)
        {
            this.entry = entry;
            this.increment = increment;
        }

        public override ElementType Type => ElementType.Value;

        public float GetValue()
        {
            return entry.Value;
        }

        public void SetValue(float value)
        {
            entry.Value = value;
        }

        public override string DisplayValue => "+/- " + increment.ToString();

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
