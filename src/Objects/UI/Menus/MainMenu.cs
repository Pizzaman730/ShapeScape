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
        private GameText titleText; // Title text for "Shape Scape"
        private GameText versionText; // Version text

        public UIMainMenu()
        {
        }

        public void Show()
        {
            // Show buttons and UI
            new Ground(new Vec2(0, 0), new Vec2(4000, 2000));

            // Start Button
            startButton = new StartButton(new Vec2(-150, -100)); 
            playText = new GameText("Play", 2, new Vec2(-150, -100), Color.Black);

            // Uncomment for editor
            editorButton = new EditorButton(new Vec2(150, -100));
            editorText = new GameText("Editor", 2, new Vec2(150, -100), Color.Black);

            // Settings Button
            settingsButton = new SettingsButton(new Vec2(-50, 50));

            // Title Text - Shape Scape
            titleText = new GameText("Shape Scape", 4, new Vec2(0, 50), Color.Black);

            // Version Text - Displaying version number 
            versionText = new GameText("Version 0.3.0-alpha", 1, new Vec2(125, 20), Color.Gray);
            versionText.zone = OverlayZone.BottomLeft;
            versionText.UpdatePosForOverlay();

            Camera.center = startButton.position;
        }

        public void Hide()
        {
            UIManager.RemoveObject(startButton);
            UIManager.RemoveObject(editorButton); 
            UIManager.RemoveObject(settingsButton);
            UIManager.RemoveObject(playText);
            UIManager.RemoveObject(editorText);
            UIManager.RemoveObject(titleText); 
            UIManager.RemoveObject(versionText); 
        }

        public void Update()
        {
        }
    }
}
