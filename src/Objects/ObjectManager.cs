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
        public static void Init()
        {
            //objects = new();
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
            foreach (Object obj in objects)
            {
                if (obj == null)
                {
                    DestroyObject(obj);
                    continue;
                }
                obj.Update();
            }
        }
        public static void DestroyAllObjects()
        {
            List<Object> oldObjects = objects;
            foreach (Object obj in oldObjects)
            {
                DestroyObject(obj);
            }
        }
    }
}