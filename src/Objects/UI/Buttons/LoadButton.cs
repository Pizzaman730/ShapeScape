using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShapeScape
{
    public class LoadButton : Button
    {
        public LoadButton(Vec2 pos) : base("LoadButton", pos, new Vec2(200, 100))
        {
            tags.Add("LoadButton");
            zone = OverlayZone.TopRight;
            UpdatePosForOverlay();
        }
        public override void OnButtonClick()
        {
            string levelName = "NewLevel"; 
            LevelEditor.LoadLevel(levelName);
            Logger.Log($"Level {levelName}.json loaded.");
        }
    }
}