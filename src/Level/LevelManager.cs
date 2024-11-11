using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SquarePlatformer
{
    public static class LevelManager
    {
        private static bool levelStarted = false;
        public static int alivePlayers = 0;
        public static void Update()
        {
            if (!levelStarted)
            {
                levelStarted = true;
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
            if (alivePlayers == 0)
            {
                levelStarted = false;
                ObjectManager.DestroyAllObjects();
            }
        }
    }
}