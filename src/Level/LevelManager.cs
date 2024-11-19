namespace ShapeScape
{
    public static class LevelManager
    {
        public static int alivePlayers = 0;
        public static bool firstUpdateDone = false;
        public static GameState gameState = GameState.InMenu;

        public static void Update()
        {
            if (gameState == GameState.InEditor)
            {
                if (!firstUpdateDone)
                {
                    firstUpdateDone = true;
                    UIManager.SetMenu(new UIMenuEditor());
                }
                LevelEditor.Update();
            }
            if (gameState == GameState.InMenu)
            {
                // Make sure the UI is showing the main menu
                if (!firstUpdateDone)
                {
                    firstUpdateDone = true;
                    UIManager.Init(); // Initialize the UI
                    UIManager.SetMenu(new UIMainMenu()); // Set the main menu
                }
            }
            if (gameState == GameState.InSettings)
            {
                // Make sure the UI is showing the main menu
                if (!firstUpdateDone)
                {
                    firstUpdateDone = true;
                    UIManager.Init(); // Initialize the UI
                    UIManager.SetMenu(new UISettingsMenu()); // Set the main menu
                }
            }
            if (gameState == GameState.InLevel && alivePlayers == 0)
            {
                // Transition back to the main menu after level completion or player death
                UIManager.SetMenu(new UIDeathScreenMenu());
                gameState = GameState.DeathScreen;
                firstUpdateDone = false;
            }
        }

        public static void StartLevel(int levelNum)
        {
            if (levelNum == 1)
            {
                gameState = GameState.InLevel;
                firstUpdateDone = true;
                
                // Player
                new Player(new Vec2(0, 25));

                // Enemies
                new TriangleEnemy(new Vec2(-1600, 225));

                // Rectangles
                new RectangleEnemy(new Vec2(2400, 325));

                // Circles
                new CircleEnemy(new Vec2(1600, 225));
                new CircleEnemy(new Vec2(1200, 25));

                // BouncyOvals
                new BouncyOval(new Vec2(3300, -75));

                //Trapezoids
                new TrapezoidEnemy(new Vec2(4000, 313));

                // Platforms
                new Ground(new Vec2(3000, -300), new Vec2(1000, 400));
                new Ground(new Vec2(2400, -100), new Vec2(500, 800));
                new Ground(new Vec2(0, -200), new Vec2(1000, 400));
                new Ground(new Vec2(1400, -200), new Vec2(1000, 400));
                new Ground(new Vec2(-1400, -100), new Vec2(1000, 600));
                new Ground(new Vec2(1650, -100), new Vec2(500, 600));
                new Ground(new Vec2(4000, -100), new Vec2(1000, 800));
                Camera.FollowTargets(false);
            }
        }

        public static void StartEditor()
        {
            gameState = GameState.InEditor;
            ObjectManager.AddAllObjectsToDestroy(); 
        }
    }
}
