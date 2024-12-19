using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace ShapeScape
{
    /// <summary>
    /// Represents an animation, which is a sequence of keyframes.
    /// </summary>
    public class Animation
    {
        /// <summary>
        /// The time in the animation, measured in milliseconds.
        /// </summary>
        public int time = 0;
        /// <summary>
        /// Whether the animation is currently playing.
        /// </summary>
        public bool active = false;
        /// <summary>
        /// The name of the animation.
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// The keyframes of the animation.
        /// </summary>
        public Dictionary<int, AnimationFrame> keyframes { get; set; }
        /// <summary>
        /// The ObjectTexture to which the animation belongs.
        /// </summary>
        public ObjectTexture obj;
        /// <summary>
        /// Applies the changes defined in the given frame to the ObjectTexture.
        /// </summary>
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
        /// <summary>
        /// Starts the animation.
        /// </summary>
        public void Start()
        {
            time = 0;
            active = true;
        }
        /// <summary>
        /// Stops the animation.
        /// </summary>
        public void End()
        {
            time = 0;
            active = false;
        }
    }
}

