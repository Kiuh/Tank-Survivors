using UnityEngine.UIElements;

namespace AYellowpaper.Editor
{
    internal class Tab : Toggle
    {
        public Tab(string text) : base()
        {
            base.text = text;
            RemoveFromClassList(Toggle.ussClassName);
            AddToClassList(ussClassName);
        }
    }
}