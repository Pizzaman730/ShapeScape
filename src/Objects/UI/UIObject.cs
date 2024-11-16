using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShapeScape
{
    public class UIObject : Object
    {
        public bool overlay;
        public UIObject(string name, Vec2 pos, Vec2 size, bool overlay) : base(name, pos * new Vec2(-1, -1), size)
        {
            UIManager.AddObject(this);
            this.overlay = overlay;
            tags.Add("UI");
        }
        public virtual void UpdateUI()
        {
            
        }    
    }
}