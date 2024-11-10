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
        public PhysicsObject(string name) : base(name)
        {
            PhysicsManager.AddPhysicsObject(this);
            isPhysicsObject = true;
        }
        public PhysicsObject(string name, Vec2 pos) : base(name, pos)
        {
            PhysicsManager.AddPhysicsObject(this);
            isPhysicsObject = true;
        }
        public PhysicsObject(string name, Vec2 pos, Vec2 size) : base(name, pos, size)
        {
            PhysicsManager.AddPhysicsObject(this);
            isPhysicsObject = true;
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
        public void Collision(PhysicsObject obj, bool onSide, bool firstUpdate)
        {
            CollisionStart(obj, onSide);
            
            if (!pushable || (!obj.pushable && !firstUpdate))
            {
                CollisionEnd(obj, onSide);
                return;
            }
            if (onSide)
            {
                bool right = velocity.x > 0;
                double overlapAmountX = (size.x + obj.size.x) / 2 - Math.Abs(position.x - obj.position.x);
                SetPos(new Vec2(position.x + overlapAmountX * (right ? -1 : 1), position.y));
                velocity.x = 0;
                CollisionEnd(obj, onSide);
                return;
            }
            bool up = velocity.y < 0;
            double overlapAmountY = (size.y + obj.size.y) / 2 - Math.Abs(position.y - obj.position.y);
            SetPos(new Vec2(position.x, position.y + overlapAmountY * (up ? 1 : -1)));
            velocity.y = 0;
            CollisionEnd(obj, onSide);
        }
        public virtual void CollisionStart(PhysicsObject obj, bool onSide)
        {

        }
        public virtual void CollisionEnd(PhysicsObject obj, bool onSide)
        {
            
        }
        public bool CollisionAtPoint(Vec2 point)
        {
            bool collideX = position.x + (size.x/2) > point.x && position.x - size.x/2 < point.x;
            bool collideY = position.y + (size.y/2) > point.y && position.y - size.y/2 < point.y;
            bool collide = collideX && collideY;
            return collide;
        }
    }
}