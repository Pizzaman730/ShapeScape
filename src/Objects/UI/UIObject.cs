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
            if (zone == OverlayZone.Middle)
            {
                SetPos(actualPos);
            }
            if (zone == OverlayZone.TopLeft)
            {
                SetPos(new Vec2(actualPos.x + WindowManager.size.x / 2, actualPos.y - WindowManager.size.y / 2));
            }
            if (zone == OverlayZone.TopRight)
            {
                SetPos(new Vec2(actualPos.x - WindowManager.size.x / 2, actualPos.y - WindowManager.size.y / 2));
            }
            if (zone == OverlayZone.TopMiddle)
            {
                SetPos(new Vec2(actualPos.x, actualPos.y - WindowManager.size.y / 2));
            }
            if (zone == OverlayZone.MiddleLeft)
            {
                SetPos(new Vec2(actualPos.x + WindowManager.size.x / 2, actualPos.y));
            }
            if (zone == OverlayZone.MiddleRight)
            {
                SetPos(new Vec2(actualPos.x - WindowManager.size.x / 2, actualPos.y));
            }
            if (zone == OverlayZone.BottomLeft)
            {
                SetPos(new Vec2(actualPos.x + WindowManager.size.x / 2, actualPos.y + WindowManager.size.y / 2));
            }
            if (zone == OverlayZone.BottomMiddle)
            {
                SetPos(new Vec2(actualPos.x, actualPos.y + WindowManager.size.y / 2));
            }
            if (zone == OverlayZone.BottomRight)
            {
                SetPos(new Vec2(actualPos.x - WindowManager.size.x / 2, actualPos.y + WindowManager.size.y / 2));
            }
        }
    }
}