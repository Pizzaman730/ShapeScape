using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SquarePlatformer
{
    public static class ObjectManager
    {
        public static List<Object> objects { get; private set; } = new();
        public static List<Object> toDestroy { get; private set; } = new();
        public static void Init()
        {
            
        }
        public static void AddObject(Object obj)
        {
            if (!objects.Contains(obj))
            {
                objects.Add(obj);
                return;
            }
            Console.WriteLine("Object already added to object list!");
        }
        public static void DestroyObject(Object obj)
        {
            if (objects.Contains(obj))
            {
                objects.Remove(obj);
                if (obj.isPhysicsObject)
                {
                    PhysicsManager.RemovePhysicsObject((PhysicsObject)obj);
                }
                return;
            }
            Console.WriteLine("Object not added to object list!");
        }
        public static void UpdateAll()
        {
            List<Object> oldObjects = objects;
            foreach (Object obj in oldObjects)
            {
                if (obj == null)
                {
                    AddToDestroy(obj);
                    continue;
                }
                obj.Update();
            }
            DestroyNeededObjects();
        }
        public static void DestroyAllObjects()
        {
            List<Object> oldObjects = new(objects);
            foreach (Object obj in oldObjects)
            {
                DestroyObject(obj);
            }
        }
        private static void DestroyNeededObjects()
        {
            foreach (Object obj in toDestroy)
            {
                DestroyObject(obj);
            }
            toDestroy = new();
        }
        public static void AddToDestroy(Object obj)
        {
            toDestroy.Add(obj);
        }
    }
}