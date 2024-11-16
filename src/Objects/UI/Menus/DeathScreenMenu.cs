using System;
using Microsoft.Xna.Framework;

namespace ShapeScape
{
    public class UIDeathScreenMenu : IMenu
    {
        private BackButton backButton;
        private RestartButton restartButton;

        public UIDeathScreenMenu()
        {
        }

        public void Show()
        {
            // Show buttons and UI

            backButton = new BackButton(new Vec2(50, -50));
            restartButton = new RestartButton(new Vec2(-50, -50));
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
