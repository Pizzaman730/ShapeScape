using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShapeScape
{
    public class RestartButton : Button
    {
        public RestartButton(Vec2 pos) : base("RestartButton", pos, new Vec2(100, 100))
        {
            tags.Add("RestartButton");
            zone = OverlayZone.TopRight;
            UpdatePosForOverlay();
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