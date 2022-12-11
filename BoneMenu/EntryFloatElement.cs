using BoneLib.BoneMenu.Elements;
using MelonLoader;
using UnityEngine;

namespace AvatarStatsLoader.BoneMenu
{
    public class EntryFloatElement : MenuElement
    {
        protected MelonPreferences_Entry<float> entry { get; }

        public EntryFloatElement(string name, Color color, MelonPreferences_Entry<float> entry) : base(name, color)
        {
            this.entry = entry;
            entry.OnEntryValueChanged.Subscribe((prev, cur) => {

            }, int.MinValue);
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

        public override string DisplayValue => this.GetValue().ToString();

        public override void OnSelectLeft()
        {
            entry.Value += .01f;
        }

        public override void OnSelectRight()
        {
            entry.Value -= .01f;
        }
    }
}
