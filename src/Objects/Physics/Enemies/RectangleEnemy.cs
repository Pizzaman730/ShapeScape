using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShapeScape
{
    public class RectangleEnemy : PhysicsObject
    {
        public bool facingRight = true;
        public Animation turnLeftAnim;
        public Animation turnRightAnim;
        public RectangleEnemy(Vec2 pos) : base("RectangleEnemy", pos, new Vec2(100, 50))
        {
            affectedByGravity = true;
            pushable = true;
            maxVelocity.x = 5;
            turnLeftAnim = CreateAnimation("RectangleEnemyTurnLeft");
            turnRightAnim = CreateAnimation("RectangleEnemyTurnRight");
            tags.Add("RectangleEnemy");
            tags.Add("Enemy");
        }
        public override void Update()
        {
            bool surfaceAtDestination = false;
            bool onGround = false;
            foreach (PhysicsObject obj in PhysicsManager.physicsObjects.OfType<PhysicsObject>().Where(o => o.tags.Contains("Surface")))
            {
                if (obj.CollisionAtPoint(new Vec2(position.x + 26 * (facingRight ? 1 : -1), position.y - 26)))
                {
                    surfaceAtDestination = true;
                }
                if (obj.CollisionAtPoint(new Vec2(position.x, position.y - 26)))
                {
                    onGround = true;
                }
                if (onGround && surfaceAtDestination) break;
            }
            if (!surfaceAtDestination && onGround)
            {
                if (facingRight)
                {
                    facingRight = false;
                    turnLeftAnim.Start();
                }
                else
                {
                    facingRight = true;
                    turnRightAnim.Start();
                }

                //flipTexture = !facingRight;
            }
            velocity.x += facingRight ? 1 : -1;
        }
        public override void CollisionEnd(CollisionInformation info)
        {
            if (info.side == Side.Left || info.side == Side.Right)
            {
                if (facingRight)
                {
                    facingRight = false;
                    turnLeftAnim.Start();
                }
                else
                {
                    facingRight = true;
                    turnRightAnim.Start();
                }
            }
            if (info.obj.tags.Contains("Player") && info.side == Side.Up) 
            {
                //ObjectManager.AddToDestroy(this);
                info.obj.Move(Vec2.Clamp(velocity, maxVelocity * -1, maxVelocity));
                return;
            }
            if (info.obj.tags.Contains("Player"))
            {
                ((Player)info.obj).Kill("RectangleEnemy");
            }
        }
    }
}