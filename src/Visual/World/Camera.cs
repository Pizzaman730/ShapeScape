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
    public static class Camera
    {
        public static double scale = 1;
        public static Vec2 center = new();
        public static Matrix matrix { get; private set; }
        public static List<Object> targets = new();
        
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
            center = center + ((sum/targets.Count) - center) * (1 - Math.Pow(1 - 0.025f, 2));
        }
        public static Vec2 TranslatePos(Vec2 startPos)
        {
            return startPos + (center * new Vec2(-1, 1)) + (WindowManager.size / 2);
        }
    }
}