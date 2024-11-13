using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace SquarePlatformer
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
                if (framePart.textureChange != null && framePart.textureChange != "")
                {
                    textureInfo.textureName = framePart.textureChange;
                    textureInfo.UpdateTexture();
                }
                if (framePart.offsetSet.x != 0 && framePart.offsetSet.y != 0)
                {
                    textureInfo.offset = framePart.offsetSet;
                }
                if (framePart.offsetChange.x != 0 && framePart.offsetChange.y != 0)
                {
                    textureInfo.offset += framePart.offsetChange;
                }
                if (framePart.sizeSet.x != 0 && framePart.sizeSet.y != 0)
                {
                    textureInfo.size = framePart.sizeSet;
                }
                if (framePart.sizeChange.x != 0 && framePart.sizeChange.y != 0)
                {
                    textureInfo.size += framePart.sizeChange;
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