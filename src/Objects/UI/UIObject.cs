using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SquarePlatformer
{
    public class UIObject : Object
    {
        public bool overlay;
        public UIObject(string name, Vec2 pos, Vec2 size, bool overlay) : base(name, pos, size)
        {
            this.overlay = overlay;
            tags.Add("UI");
        }        
    }
}