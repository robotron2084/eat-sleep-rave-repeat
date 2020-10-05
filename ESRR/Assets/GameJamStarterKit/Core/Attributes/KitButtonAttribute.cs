using System;

namespace GameJamStarterKit
{
    [AttributeUsage(AttributeTargets.Method)]
    public class KitButtonAttribute : Attribute
    {
        public string Text;
        public KitButtonSize ButtonSize;

        public KitButtonAttribute(KitButtonSize buttonSize = KitButtonSize.Small)
        {
            Text = string.Empty;
            ButtonSize = buttonSize;
        }

        public KitButtonAttribute(string text, KitButtonSize buttonSize = KitButtonSize.Small)
        {
            Text = text;
            ButtonSize = buttonSize;
        }
    }

    public enum KitButtonSize
    {
        Small,
        Medium,
        Large
    }
}