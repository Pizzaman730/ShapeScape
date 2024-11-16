using System;
using Microsoft.Xna.Framework;

namespace ShapeScape
{
    public class UISettingsMenu : IMenu
    {
        private BackButton backButton;

        public UISettingsMenu()
        {
        }

        public void Show()
        {
            // Show buttons and UI
            new Ground(new Vec2(0, 0), new Vec2(4000, 2000));

            // UIManager.AddObject(editorButton);
            backButton = new BackButton(new Vec2(50, -50));

            Camera.center = new Vec2();
        }

        public void Hide()
        {
            UIManager.RemoveObject(backButton);
        }

        public void Update()
        {

        }
    }
}
