using System;
using Microsoft.Xna.Framework;

namespace ShapeScape
{
    public class UIMainMenu : IMenu
    {
        private StartButton startButton;
        private EditorButton editorButton; 
        private SettingsButton settingsButton;
        private GameText playText;
        private GameText editorText;

        public UIMainMenu()
        {
        }

        public void Show()
        {
            // Show buttons and UI
            new Ground(new Vec2(0, 0), new Vec2(4000, 2000));

            startButton = new StartButton(new Vec2()); 
            playText = new GameText("Play", 2, new Vec2(0, 0), Color.Black);


            // Uncomment for editor
            editorButton = new EditorButton(new Vec2(0, -150));
            editorText = new GameText("Editor", 2, new Vec2(0, -150), Color.Black);
            // UIManager.AddObject(editorButton);

            settingsButton = new SettingsButton(new Vec2(-50, 50));

            Camera.center = startButton.position;
        }

        public void Hide()
        {
            UIManager.RemoveObject(startButton);
            UIManager.RemoveObject(editorButton); 
            UIManager.RemoveObject(settingsButton);
            UIManager.RemoveObject(playText);
            UIManager.RemoveObject(editorText);
        }

        public void Update()
        {

        }
    }
}
