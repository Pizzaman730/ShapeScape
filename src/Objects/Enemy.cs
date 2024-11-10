using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SquarePlatformer
{
    public class Enemy : PhysicsObject
    {
        public Enemy(Vec2 pos) : base("Enemy", pos, new Vec2(50, 50))
        {
            affectedByGravity = true;
        }
        public override void Update()
        {
        }
    }
}