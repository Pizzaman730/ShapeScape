using System;
using System.Collections.Generic;

namespace SquarePlatformer
{
    public static class UIManager
    {
        public static List<UIObject> uiObjects { get; private set; } = new();
        public static List<UIObject> toDestroy { get; private set; } = new();
        private static IMenu currentMenu;

        public static void Init()
        {
            // Initialize the default menu (e.g., main menu)
            SetMenu(new UIMainMenu());
        }

        public static void SetMenu(IMenu menu)
        {
            // Hide the current menu if there's one
            currentMenu?.Hide();

            // Set the new menu and show it
            currentMenu = menu;
            currentMenu.Show();
        }

        public static void UpdateAll()
        {
            // Update the current active menu
            currentMenu?.Update();
            
            // Update other UI objects
            foreach (UIObject obj in uiObjects)
            {
                if (obj != null)
                {
                    obj.UpdateUI();
                }
            }

            DestroyNeededObjects();
        }

        private static void DestroyNeededObjects()
        {
            foreach (UIObject obj in toDestroy)
            {
                RemoveObject(obj);
            }
            toDestroy.Clear();
        }

        public static void AddObject(UIObject obj)
        {
            if (!uiObjects.Contains(obj))
            {
                uiObjects.Add(obj);
            }
            else
            {
                Logger.Log("UIObject " + obj.name + " already added to object list!");
            }
        }

        public static void RemoveObject(UIObject obj)
        {
            if (uiObjects.Contains(obj))
            {
                uiObjects.Remove(obj);
            }
            else
            {
                Logger.Log("UIObject " + obj.name + " not found in object list!");
            }
        }

        public static void AddToDestroy(UIObject obj)
        {
            toDestroy.Add(obj);
        }
    }
}
