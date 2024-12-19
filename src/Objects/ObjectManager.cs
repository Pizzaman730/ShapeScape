using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ShapeScape
{
    /// <summary>
    /// Handles all objects in the game, including physics objects and UI objects.
    /// </summary>
    public static class ObjectManager
    {
        /// <summary>
        /// Contains all objects in the game.
        /// </summary>
        public static List<Object> objects { get; private set; } = new();
        /// <summary>
        /// Contains all objects that need to be destroyed on the next update.
        /// </summary>
        public static List<Object> toDestroy { get; private set; } = new();
        /// <summary>
        /// Initializes the object manager.
        /// </summary>
        public static void Init()
        {
            
        }
        /// <summary>
        /// Adds an object to the object manager.
        /// </summary>
        /// <param name="obj">The object to be added.</param>
        public static void AddObject(Object obj)
        {
            if (!objects.Contains(obj))
            {
                objects.Add(obj);
                return;
            }
            Logger.Log("Object " + obj.name + " already added to object list!");
        }
        /// <summary>
        /// Destroys an object from the object manager.
        /// </summary>
        /// <param name="obj">The object to be destroyed.</param>
        public static void DestroyObject(Object obj)
        {
            if (objects.Contains(obj))
            {
                objects.Remove(obj);
                if (obj.tags.Contains("Physics"))
                {
                    PhysicsManager.RemovePhysicsObject((PhysicsObject)obj);
                }
                if (obj.tags.Contains("UI"))
                {
                    UIManager.RemoveObject((UIObject)obj);
                }
                return;
            }
            Logger.Log("Object " + obj.name + " not added to object list!");
        }
        /// <summary>
        /// Updates all objects in the object manager.
        /// </summary>
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
        /// <summary>
        /// Adds all objects to the toDestroy list.
        /// </summary>
        public static void AddAllObjectsToDestroy()
        {
            List<Object> oldObjects = new(objects);
            foreach (Object obj in oldObjects)
            {
                AddToDestroy(obj);
            }
        }
        /// <summary>
        /// Destroys all objects in the toDestroy list.
        /// </summary>
        private static void DestroyNeededObjects()
        {
            foreach (Object obj in toDestroy)
            {
                DestroyObject(obj);
            }
            toDestroy = new();
        }
        /// <summary>
        /// Adds an object to the toDestroy list.
        /// </summary>
        /// <param name="obj">The object to be destroyed.</param>
        public static void AddToDestroy(Object obj)
        {
            toDestroy.Add(obj);
        }
    }
}
