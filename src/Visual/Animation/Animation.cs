using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace ShapeScape
{
    public class Animation
    {
        public int time = 0;
        public bool active = false;
        public string name { get; set; }
        public Dictionary<int, AnimationFrame> keyframes { get; set; }
        public ObjectTexture obj;
        public void DoAnimation(AnimationFrame frame)
        {
            foreach (AnimationFramePart framePart in frame.parts)
            {
                TextureInfo textureInfo = obj.GetTexturesAsDictionary()[framePart.name];
                if (framePart.changeTexture)
                {
                    textureInfo.textureName = framePart.textureChange;
                    textureInfo.UpdateTexture();
                }
                if (framePart.setOffset)
                {
                    textureInfo.offset = framePart.offsetSet;
                }
                if (framePart.changeOffset)
                {
                    textureInfo.offset += framePart.offsetChange;
                }
                if (framePart.setSize)
                {
                    textureInfo.size = framePart.sizeSet;
                }
                if (framePart.changeSize)
                {
                    textureInfo.size += framePart.sizeChange;
                }
                if (framePart.changeFlipped)
                {
                    textureInfo.flip = framePart.flip;
                }
            }

        }
        public void Start()
        {
            time = 0;
            active = true;
        }
        public void End()
        {
            time = 0;
            active = false;
        }
    }
}