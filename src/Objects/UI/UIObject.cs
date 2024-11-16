using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MonoGame.Extended.Screens;

namespace ShapeScape
{
    public class UIObject : Object
    {
        public Vec2 actualPos;
        public bool overlay;
        public OverlayZone zone = OverlayZone.Middle;
        public UIObject(string name, Vec2 pos, Vec2 size, bool overlay) : base(name, pos * new Vec2(-1, -1), size)
        {
            actualPos = pos * new Vec2(-1, -1);
            UIManager.AddObject(this);
            this.overlay = overlay;
            tags.Add("UI");
        }
        public virtual void UpdateUI()
        {
            
        }
        public void UpdatePosForOverlay()
        {
            Vec2 offset = new Vec2(0, 0);

            if (zone == OverlayZone.TopLeft || zone == OverlayZone.MiddleLeft || zone == OverlayZone.BottomLeft)
            {
                offset.x = WindowManager.size.x / 2;
            }
            if (zone == OverlayZone.TopRight || zone == OverlayZone.MiddleRight || zone == OverlayZone.BottomRight)
            {
                offset.x = -WindowManager.size.x / 2;
            }
            if (zone == OverlayZone.TopLeft || zone == OverlayZone.TopMiddle || zone == OverlayZone.TopRight)
            {
                offset.y = -WindowManager.size.y / 2;
            }
            if (zone == OverlayZone.BottomLeft || zone == OverlayZone.BottomMiddle || zone == OverlayZone.BottomRight)
            {
                offset.y = WindowManager.size.y / 2;
            }

            SetPos(actualPos + offset);
        }
    }
}