using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Threading.Tasks;

namespace SquarePlatformer
{
    public class AnimationFramePart
    {
        public Vec2 offsetChange { get; set; }
        public Vec2 offsetSet { get; set; }
        public Vec2 sizeSet { get; set; }
        public Vec2 sizeChange { get; set; }
        public string textureChange { get; set; }
        public string name { get; set; }
    }
}