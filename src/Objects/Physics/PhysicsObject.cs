using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace SquarePlatformer
{
    public class PhysicsObject : Object
    {
        public bool affectedByGravity = true;
        public double gravity = 1;
        public double weight = 1;
        public Vec2 maxVelocity = new Vec2(10, 100);
        public Vec2 velocity = new();
        public bool pushable = false;
        public PhysicsObject(string name, Vec2 pos, Vec2 size) : base(name, pos, size)
        {
            PhysicsManager.AddPhysicsObject(this);
            tags.Add("Physics");
        }
        public void UpdatePhysicsSide()
        {
            velocity = Vec2.Clamp(velocity, maxVelocity * -1, maxVelocity);  
            Move(velocity * new Vec2(1, 0));
        }
        public void UpdatePhysicsVertical()
        {
            if (affectedByGravity)
            {
                velocity.y -= 0.5 * gravity * weight;
            }
            velocity = Vec2.Clamp(velocity, maxVelocity * -1, maxVelocity);  
            Move(velocity * new Vec2(0, 1));
        }
        public void Collision(CollisionInformation info)
        {
            CollisionStart(info);
            
            if (!pushable || (!info.obj.pushable && !info.firstUpdate))
            {
                CollisionEnd(info);
                return;
            }
            if (info.side == Side.Left || info.side == Side.Right)
            {
                double overlapAmountX = (size.x + info.obj.size.x) / 2 - Math.Abs(position.x - info.obj.position.x);
                SetPos(new Vec2(position.x + overlapAmountX * (info.side == Side.Right ? -1 : 1), position.y));
                velocity.x = 0;
                CollisionEnd(info);
                return;
            }
            double overlapAmountY = (size.y + info.obj.size.y) / 2 - Math.Abs(position.y - info.obj.position.y);
            SetPos(new Vec2(position.x, position.y + overlapAmountY * (info.side == Side.Up ? -1 : 1)));
            velocity.y = 0;
            CollisionEnd(info);
        }
        public virtual void CollisionStart(CollisionInformation info)
        {

        }
        public virtual void CollisionEnd(CollisionInformation info)
        {
            
        }
        public bool CollisionAtPoint(Vec2 point)
        {
            return TouchesPoint(point);
        }
    }
}