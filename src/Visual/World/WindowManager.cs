using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace ShapeScape
{
    public static class WindowManager
    {
        public static Vec2 size { get; private set; } = new();
        public static GameWindow window { get; private set; }
        public static void SetupWindow(GameWindow window)
        {
            window.AllowUserResizing = true;
            window.ClientSizeChanged += UpdateSize;
            WindowManager.window = window;
            UpdateSize();
        }

        private static void UpdateSize(object sender, EventArgs e)
        {
            size = new Vec2(window.ClientBounds.Width, window.ClientBounds.Height);
            foreach (UIObject obj in UIManager.uiObjects)
            {
                obj.UpdatePosForOverlay();
            }
        }
        private static void UpdateSize()
        {
            size = new Vec2(window.ClientBounds.Width, window.ClientBounds.Height);
            foreach (UIObject obj in UIManager.uiObjects)
            {
                obj.UpdatePosForOverlay();
            }
        }

        public static void UpdateInfo()
        {

        }
    }
}