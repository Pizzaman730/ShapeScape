using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MonoGame.Extended.Screens;

namespace ShapeScape
{
    /// <summary>
    /// A UIObject is an Object that is used to represent a UI element in the game.
    /// </summary>
    public class UIObject : Object
    {
        /// <summary>
        /// The actual position of the UIObject.
        /// </summary>
        public Vec2 actualPos;
        /// <summary>
        /// Whether the UIObject should be rendered as an overlay.
        /// </summary>
        public bool overlay;
        /// <summary>
        /// The OverlayZone that the UIObject belongs to.
        /// </summary>
        public OverlayZone zone = OverlayZone.Middle;
        /// <summary>
        /// Creates a new UIObject.
        /// </summary>
        /// <param name="name">The name of the UIObject.</param>
        /// <param name="pos">The position of the UIObject.</param>
        /// <param name="size">The size of the UIObject.</param>
        /// <param name="overlay">Whether the UIObject should be rendered as an overlay.</param>
        public UIObject(string name, Vec2 pos, Vec2 size, bool overlay) : base(name, pos * new Vec2(-1, -1), size)
        {
            actualPos = pos * new Vec2(-1, -1);
            UIManager.AddObject(this);
            this.overlay = overlay;
            tags.Add("UI");
        }
        /// <summary>
        /// Called once per frame, and can be used to update the UIObject.
        /// </summary>
        public virtual void UpdateUI()
        {
            
        }
        /// <summary>
        /// Updates the position of the UIObject to match the specified OverlayZone.
        /// </summary>
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
