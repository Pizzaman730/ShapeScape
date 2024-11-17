using System;
using Microsoft.Xna.Framework;

namespace ShapeScape
{
    public class UIDeathScreenMenu : IMenu
    {
        private BackButton backButton;
        private RestartButton restartButton;
        private GameText text;

        public UIDeathScreenMenu()
        {
        }

        public void Show()
        {
            // Show buttons and UI

            backButton = new BackButton(new Vec2(50, -50));
            restartButton = new RestartButton(new Vec2(-50, -50));
            text = new GameText("You died", 5, new Vec2(0, -100), Color.Black);
            text.zone = OverlayZone.TopMiddle;
            text.UpdatePosForOverlay();
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
