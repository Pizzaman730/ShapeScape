using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SquarePlatformer
{
    public static class AnimationManager
    {
        public static List<Animation> animations = new();
        public static void Update()
        {
            foreach (Animation anim in animations)
            {
                AnimationFrame animFrame;
                if (anim.active) anim.time ++;
                if (anim.active && anim.keyframes.TryGetValue(anim.time, out animFrame))
                {
                    anim.DoAnimation(animFrame);
                }
            }
        }
    }
}