using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShapeScape
{
    public class Ground : PhysicsObject
    {
        public Ground(Vec2 pos, Vec2 size) : base("Ground", pos, size)
        {
            pushable = false;
            affectedByGravity = false;

            size.x = Math.Max(size.x, 0.1f); // Prevent zero width
            size.y = Math.Max(size.y, 0.1f); // Prevent zero height

            // Create textures
            objectTexture.textures[0] = AssetManager.TileTexture(objectTexture.textures[0], new Vec2(size.x, size.y)); 
            objectTexture.textures[1] = AssetManager.TileTexture(objectTexture.textures[1], new Vec2(size.x, size.y)); 

            tags.Add("Surface");
        }
    }
}