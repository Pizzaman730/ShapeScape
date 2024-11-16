using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShapeScape
{
    public class SettingsButton : Button
    {
        public SettingsButton(Vec2 pos) : base("SettingsButton", pos, new Vec2(100, 100))
        {
            tags.Add("SettingsButton");
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