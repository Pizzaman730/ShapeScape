using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShapeScape
{
    public class LoadButton : Button
    {
        public LoadButton(Vec2 pos) : base("LoadButton", pos, new Vec2(100, 50))
        {
            tags.Add("LoadButton");
            zone = OverlayZone.TopRight;
            UpdatePosForOverlay();
        }
        public override void OnButtonClick()
        {

        }
    }
}