using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShapeScape
{
    public class BouncyOval : PhysicsObject
    {
        public bool facingRight = true;
        public Animation turnLeftAnim;
        public Animation turnRightAnim;
        public BouncyOval(Vec2 pos) : base("BouncyOval", pos, new Vec2(100, 50))
        {
            affectedByGravity = true;
            pushable = true;
            maxVelocity.x = 5;
            turnLeftAnim = CreateAnimation("BouncyOvalTurnLeft");
            turnRightAnim = CreateAnimation("BouncyOvalTurnRight");
            tags.Add("BouncyOval");
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
        public override void CollisionStart(CollisionInformation info)
        {
            if (info.obj.tags.Contains("Player") && (info.side == Side.Left || info.side == Side.Right))
            {
                velocity.x = -velocity.x;

                // Flip the facing direction
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
                ((Player)info.obj).Jump(20);
                ((Player)info.obj).jumping = false;
                return;
            }
        }
    }
}