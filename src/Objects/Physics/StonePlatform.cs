using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace ShapeScape
{
    public class StonePlatform : PhysicsObject
    {
        public StonePlatform(Vec2 pos, Vec2 size) : base("StonePlatform", pos, size)
        {
            size = ProperSize(size);
            pushable = false;
            affectedByGravity = false;


            // Create textures
            var dict = objectTexture.GetTexturesAsDictionary();
            if (size.x == 50)
            {
                dict["Single"].enabled = true;
            }
            else if (size.x == 100)
            {
                dict["Left"].enabled = true;
                dict["Right"].enabled = true;
            }
            else
            {
                dict["Left"].enabled = true;
                dict["Right"].enabled = true;
                dict["Right"].offset = new Vec2(size.x - 50, 0);
                dict["Middle"] = AssetManager.TileTexture(dict["Middle"], new Vec2(size.x - 100, size.y));
                dict["Middle"].enabled = true;

                //Set up offsets and tiling
            }
            tags.Add("Surface");
        }
        public Vec2 ProperSize(Vec2 size)
        {
            double rounded = Math.Round(size.x / 50) * 50;
            return new Vec2(Math.Max(rounded, 50), 50);
        }
    }
}