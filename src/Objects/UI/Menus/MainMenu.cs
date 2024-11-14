using System;
using Microsoft.Xna.Framework;

namespace SquarePlatformer
{
    public class UIMainMenu : IMenu
    {
        private StartButton startButton;
        // private ExitButton exitButton;

        public UIMainMenu()
        {
            // Initialize buttons or other UI elements
            //startButton = new StartButton(new Vec2(100, 100));
            // exitButton = new ExitButton(new Vec2(100, 200));
        }

        public void Show()
        {
            // Show buttons and UI elements
            // UIManager.AddObject(exitButton);

            new Ground(new Vec2(0, 0), new Vec2(4000, 2000));
            startButton = new StartButton(new Vec2());
            UIManager.AddObject(startButton);
            Camera.center = startButton.position;
        }

        public void Hide()
        {
            // Hide buttons and UI elements
            UIManager.RemoveObject(startButton);
            // UIManager.RemoveObject(exitButton);
        }

        public void Update()
        {

        }
    }
}
