using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SquarePlatformer
{
    public static class UIManager
    {
        public static List<UIObject> uiObjects { get; private set; } = new();
        public static List<UIObject> toDestroy { get; private set; } = new();
        public static void Init()
        {
            
        }
        public static void AddObject(UIObject obj)
        {
            if (!uiObjects.Contains(obj))
            {
                uiObjects.Add(obj);
                return;
            }
            Console.WriteLine("UIObject " + obj.name + " already added to object list!");
        }
        public static void RemoveObject(UIObject obj)
        {
            if (uiObjects.Contains(obj))
            {
                uiObjects.Remove(obj);
                return;
            }
            Console.WriteLine("UIObject " + obj.name + " not added to object list!");
        }
        public static void UpdateAll()
        {
            List<UIObject> oldObjects = uiObjects;
            foreach (UIObject obj in oldObjects)
            {
                if (obj == null)
                {
                    //AddToDestroy(obj);
                    continue;
                }
                obj.UpdateUI();
                
            }
            DestroyNeededObjects();
        }
        private static void DestroyNeededObjects()
        {
            foreach (UIObject obj in toDestroy)
            {
                RemoveObject(obj);
            }
            toDestroy = new();
        }
        public static void AddToDestroy(UIObject obj)
        {
            toDestroy.Add(obj);
        }
    }
}