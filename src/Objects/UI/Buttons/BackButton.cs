using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShapeScape
{
    public class BackButton : Button
    {
        public BackButton(Vec2 pos) : base("BackButton", pos, new Vec2(100, 100))
        {
            tags.Add("BackButton");
            zone = OverlayZone.TopLeft;
            UpdatePosForOverlay();
        }
        public override void OnButtonClick()
        {
            // LevelManager.startButton = null;
            LevelManager.firstUpdateDone = false;
            LevelManager.gameState = GameState.InMenu;
            ObjectManager.AddAllObjectsToDestroy();
        }
    }
}