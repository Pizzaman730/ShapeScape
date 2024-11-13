using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace SquarePlatformer
{
    public static class LevelManager
    {
        public static int alivePlayers = 0;
        public static bool firstUpdateDone = false;
        public static GameState gameState = GameState.InMenu;
        public static StartButton startButton;
        public static void Update()
        {
            if (gameState == GameState.InMenu)
            {
                if (!firstUpdateDone)
                {
                    firstUpdateDone = true;
                    new Ground(new Vec2(0, 0), new Vec2(4000, 2000));
                    startButton = new StartButton(new Vec2());
                    Camera.center = startButton.position;
                }
                /*
                if (InputManager.GetButtonDown(MouseButton.LeftButton) && startButton.TouchesPoint(InputManager.MousePosWorld()))
                {
                    startButton = null;
                    firstUpdateDone = false;
                    gameState = GameState.InLevel;
                    ObjectManager.DestroyAllObjects();
                    StartLevel(1);
                }
                */
            }
            if (gameState == GameState.InLevel && alivePlayers == 0)
            {
                ObjectManager.AddAllObjectsToDestroy();
                //StartLevel(1);
                gameState = GameState.InMenu;
                firstUpdateDone = false;
            }
        }
        public static void StartLevel(int levelNum)
        {
            if (levelNum == 1)
            {
                gameState = GameState.InLevel;
                firstUpdateDone = true;
                new Player(new Vec2(0, 25));
                //new Enemy(new Vec2(-200, 25));
                //new Enemy(new Vec2(200, 25));
                new Enemy(new Vec2(1600, 225));
                new Enemy(new Vec2(1200, 25));
                new Ground(new Vec2(0, -200), new Vec2(1000, 400));
                new Ground(new Vec2(1400, -200), new Vec2(1000, 400));
                new Ground(new Vec2(-1400, -100), new Vec2(1000, 600));
                new Ground(new Vec2(1650, -100), new Vec2(500, 600));
                Camera.FollowTargets(false);
            }
        }
    }
}