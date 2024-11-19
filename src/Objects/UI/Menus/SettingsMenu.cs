using System;
using Microsoft.Xna.Framework;

namespace ShapeScape
{
    public class UISettingsMenu : IMenu
    {
        private BackButton backButton;
        private FontSwitch fontSwitch;
        private GameText fontSwitchText;

        public UISettingsMenu()
        {
        }

        public void Show()
        {
            // Show buttons and UI
            new Ground(new Vec2(0, 0), new Vec2(4000, 2000));

            // UIManager.AddObject(editorButton);
            backButton = new BackButton(new Vec2(50, -50));
            fontSwitch = new FontSwitch(new Vec2(0, 0));
            fontSwitchText = new GameText("Toggle special font", 1, new Vec2(0, 20), Color.White);

            Camera.center = new Vec2();
        }

        public void Hide()
        {
            UIManager.RemoveObject(backButton);
            UIManager.RemoveObject(fontSwitch);
            UIManager.RemoveObject(fontSwitchText);
        }

        public void Update()
        {

        }
    }
}
