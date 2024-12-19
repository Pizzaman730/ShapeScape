using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Threading.Tasks;

namespace ShapeScape
{
    /// <summary>
    /// AnimationFramePart is a part of an AnimationFrame.
    /// It contains information about the changes that need to be made to a single object in the animation.
    /// </summary>
    public class AnimationFramePart
    {
        /// <summary>
        /// Whether the offset of the object should be changed.
        /// </summary>
        public bool changeOffset { get; set; }

        /// <summary>
        /// Whether the offset of the object should be set.
        /// </summary>
        public bool setOffset { get; set; }

        /// <summary>
        /// The amount to change the offset of the object by.
        /// </summary>
        public Vec2 offsetChange { get; set; }

        /// <summary>
        /// The amount to set the offset of the object to.
        /// </summary>
        public Vec2 offsetSet { get; set; }

        /// <summary>
        /// Whether the size of the object should be changed.
        /// </summary>
        public bool changeSize { get; set; }

        /// <summary>
        /// Whether the size of the object should be set.
        /// </summary>
        public bool setSize { get; set; }

        /// <summary>
        /// The amount to set the size of the object to.
        /// </summary>
        public Vec2 sizeSet { get; set; }

        /// <summary>
        /// The amount to change the size of the object by.
        /// </summary>
        public Vec2 sizeChange { get; set; }

        /// <summary>
        /// Whether the texture of the object should be changed.
        /// </summary>
        public bool changeTexture { get; set; }

        /// <summary>
        /// The new texture for the object.
        /// </summary>
        public string textureChange { get; set; }

        /// <summary>
        /// The name of the object that this part of the frame applies to.
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Whether the flip of the object should be changed.
        /// </summary>
        public bool changeFlipped { get; set; }

        /// <summary>
        /// The new flip of the object.
        /// </summary>
        public bool flip { get; set; }
    }
}
