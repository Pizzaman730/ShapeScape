using System;
using Microsoft.Xna.Framework;

namespace ShapeScape
{
    public class UIMainMenu : IMenu
    {
        private StartButton startButton;
        private EditorButton editorButton; 
        private SettingsButton settingsButton;

        public UIMainMenu()
        {
        }

        public void Show()
        {
            // Show buttons and UI
            new Ground(new Vec2(0, 0), new Vec2(4000, 2000));

            startButton = new StartButton(new Vec2()); 

            // Uncomment for editor
            editorButton = new EditorButton(new Vec2(0, 100));
            // UIManager.AddObject(editorButton);
            settingsButton = new SettingsButton(new Vec2(-50, 50));

            Camera.center = startButton.position;
        }

        public void Hide()
        {
            UIManager.RemoveObject(startButton);
            UIManager.RemoveObject(editorButton); 
            UIManager.RemoveObject(settingsButton);
        }

        public void Update()
        {

        }
    }
}
