using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Threading.Tasks;

namespace ShapeScape
{
    public class AnimationFramePart
    {
        public bool changeOffset { get; set; }
        public bool setOffset { get; set; }
        public Vec2 offsetChange { get; set; }
        public Vec2 offsetSet { get; set; }
        public bool changeSize { get; set; }
        public bool setSize { get; set; }
        public Vec2 sizeSet { get; set; }
        public Vec2 sizeChange { get; set; }
        public bool changeTexture { get; set; }
        public string textureChange { get; set; }
        public string name { get; set; }
        public bool changeFlipped { get; set; }
        public bool flip { get; set; }
    }
}