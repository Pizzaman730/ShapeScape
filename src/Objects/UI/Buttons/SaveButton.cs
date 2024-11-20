using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShapeScape
{
    public class SaveButton : Button
    {
        public SaveButton(Vec2 pos) : base("SaveButton", pos, new Vec2(100, 50))
        {
            tags.Add("SaveButton");
            zone = OverlayZone.TopLeft;
            UpdatePosForOverlay();
        }

        public override void OnButtonClick()
        {
            string levelName = "NewLevel"; 
            LevelEditor.SaveLevel(levelName);
            Logger.Log($"Level saved as {levelName}.json");
        }
    }

}