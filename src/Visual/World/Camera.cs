using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace ShapeScape
{
    /// <summary>
    /// A static class that handles the game camera.
    /// </summary>
    public static class Camera
    {
        /// <summary>
        /// The scale of the camera.
        /// </summary>
        public static double scale = 1;
        /// <summary>
        /// The position of the camera.
        /// </summary>
        public static Vec2 center = new();
        /// <summary>
        /// The matrix that is used to transform screen coordinates to world coordinates.
        /// </summary>
        public static Matrix matrix { get; private set; }
        /// <summary>
        /// The objects that the camera is following.
        /// </summary>
        public static List<Object> targets = new();
        
        /// <summary>
        /// Makes the camera follow the targets.
        /// </summary>
        /// <param name="ease">If the camera should smoothly move to the targets, or snap to them.</param>
        public static void FollowTargets(bool ease = true)
        {
            if (targets.Count() == 0) return;
            Vec2 sum = new Vec2();
            foreach (Object obj in targets)
            {
                sum += obj.position;
            }
            if (!ease) 
            {
                center = sum / targets.Count;
                return;
            }
            center += ((sum/targets.Count) - center) * (1 - Math.Pow(1 - 0.025f, 2));
        }
        /// <summary>
        /// Translates a position from world coordinates to screen coordinates.
        /// </summary>
        /// <param name="startPos">The position in world coordinates.</param>
        /// <returns>The position in screen coordinates.</returns>
        public static Vec2 TranslatePos(Vec2 startPos)
        {
            return startPos + (center * new Vec2(-1, 1)) + (WindowManager.size / 2);
        }
    }
}
