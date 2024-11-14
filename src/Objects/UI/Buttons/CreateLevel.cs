using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SquarePlatformer
{
    public class EditorButton : Button
    {
        public EditorButton(Vec2 pos) : base("StartButton", pos, new Vec2(200, 100))
        {
            tags.Add("EditorButton");
        }
        public override void OnButtonClick()
        {
            LevelManager.firstUpdateDone = false;
            LevelManager.gameState = GameState.InEditor;
            ObjectManager.AddAllObjectsToDestroy();
        }
    }
}