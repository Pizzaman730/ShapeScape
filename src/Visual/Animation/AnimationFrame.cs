using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Threading.Tasks;

namespace ShapeScape
{
    /// <summary>
    /// AnimationFrame is a class that represents a single frame in an animation.
    /// It contains a list of AnimationFrameParts, which are the individual changes
    /// that need to be made to objects in the animation to move from this frame
    /// to the next.
    /// </summary>
    public class AnimationFrame
    {
        /// <summary>
        /// The list of AnimationFrameParts in this AnimationFrame.
        /// </summary>
        public AnimationFramePart[] parts { get; set; }
    }
}
