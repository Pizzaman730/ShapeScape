using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SquarePlatformer
{
    public class StartButton : Button
    {
        public StartButton(Vec2 pos) : base("StartButton", pos, new Vec2(200, 100))
        {
            tags.Add("StartButton");
        }
        public override void OnButtonClick()
        {
            // LevelManager.startButton = null;
            LevelManager.firstUpdateDone = false;
            LevelManager.gameState = GameState.InLevel;
            ObjectManager.AddAllObjectsToDestroy();
            LevelManager.StartLevel(1);
        }
    }
}