using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SquarePlatformer
{
    public class Enemy : PhysicsObject
    {
        public bool facingRight = true;
        public Enemy(Vec2 pos) : base("Enemy", pos, new Vec2(50, 50))
        {
            affectedByGravity = true;
            pushable = true;
            maxVelocity.x = 5;
        }
        public override void Update()
        {
            bool surfaceAtDestination = false;
            foreach (PhysicsObject obj in PhysicsManager.physicsObjects.OfType<PhysicsObject>().Where(o => o.name == "Platform"))
            {
                if (obj.CollisionAtPoint(new Vec2(position.x + 26 * (facingRight ? 1 : -1), position.y - 26)))
                {
                    surfaceAtDestination = true;
                    break;
                }
            }
            if (!surfaceAtDestination)
            {
                facingRight = !facingRight;
                flipTexture = !facingRight;
            }
            velocity.x += facingRight ? 1 : -1;
        }
        public override void CollisionEnd(CollisionInformation info)
        {
            if (info.side == Side.Left || info.side == Side.Right)
            {
                facingRight = !facingRight;
                flipTexture = !facingRight;
            }
            if (info.obj.name == "Player" && info.side == Side.Up) 
            {
                ObjectManager.AddToDestroy(this);
                ((Player)info.obj).Jump(16);
                return;
            }
            if (info.obj.name == "Player")
            {
                ((Player)info.obj).Kill();
            }
        }
    }
}