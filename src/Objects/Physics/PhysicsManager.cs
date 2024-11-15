using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Particles.Modifiers;

namespace ShapeScape
{
    public static class PhysicsManager
    {
        public static readonly double checkAmount = 0.1;
        public static List<PhysicsObject> physicsObjects { get; private set; } = new();
        public static void Init()
        {

        }
        public static void AddPhysicsObject(PhysicsObject obj)
        {
            if (!physicsObjects.Contains(obj))
            {
                physicsObjects.Add(obj);
                return;
            }
            Logger.Log("PhysicsObject " + obj.name + " already added to object list!");
        }
        public static void RemovePhysicsObject(PhysicsObject obj)
        {
            if (physicsObjects.Contains(obj))
            {
                physicsObjects.Remove(obj);
                return;
            }
            Logger.Log("PhysicsObject " + obj.name + " not added to object list!");
        }
        public static void UpdateAll()
        {
            UpdateCollisions();
        }
        public static void UpdateCollisions()
        {
            foreach (PhysicsObject obj in physicsObjects)
            {
                if (obj == null)
                {
                    RemovePhysicsObject(obj);
                    continue;
                }
            }
            foreach (PhysicsObject obj in physicsObjects)
            {
                obj.UpdatePhysicsSide();
                foreach (PhysicsObject obj2 in physicsObjects)
                {
                    if (obj != obj2)
                    {
                        CollisionCheck(obj, obj2, true);
                    }
                }
                obj.UpdatePhysicsVertical();
                foreach (PhysicsObject obj2 in physicsObjects)
                {
                    if (obj != obj2)
                    {
                        CollisionCheck(obj, obj2, false);
                    }
                }
            }
        }
        public static bool CollisionCheck(PhysicsObject obj1, PhysicsObject obj2, bool onSide = false)
        {
            bool collideX = obj1.position.x + (obj1.size.x/2) > obj2.position.x - (obj2.size.x/2) && obj1.position.x - (obj1.size.x/2) < obj2.position.x + (obj2.size.x/2);
            bool collideY = obj1.position.y + (obj1.size.y/2) > obj2.position.y - (obj2.size.y/2) && obj1.position.y - (obj1.size.y/2) < obj2.position.y + (obj2.size.y/2);
            bool collide = collideX && collideY;
            if (collide)
            {
                if (onSide)
                {
                    obj1.Collision(new CollisionInformation(obj2, obj1.velocity.x > 0 ? Side.Right : Side.Left, true));
                    obj2.Collision(new CollisionInformation(obj1, obj1.velocity.x > 0 ? Side.Left : Side.Right, false));
                }
                else
                {
                    obj1.Collision(new CollisionInformation(obj2, obj1.velocity.y > 0 ? Side.Up : Side.Down, true));
                    obj2.Collision(new CollisionInformation(obj1, obj1.velocity.y > 0 ? Side.Down : Side.Up, false));
                }
                return true;
            }
            return false;
        }
    }
}