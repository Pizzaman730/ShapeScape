using System;
using System.Collections.Generic;

namespace ShapeScape
{
    /// <summary>
    /// The UIManager is responsible for managing the UI components within the game.
    /// It maintains a list of active UI objects and handles setting, updating, and destroying UI menus.
    /// </summary>
    public static class UIManager
    {
        /// <summary>
        /// List of active UI objects currently displayed.
        /// </summary>
        public static List<UIObject> uiObjects { get; private set; } = new();

        /// <summary>
        /// List of UI objects marked for destruction.
        /// </summary>
        public static List<UIObject> toDestroy { get; private set; } = new();

        /// <summary>
        /// The current active menu displayed by the UI manager.
        /// </summary>
        private static IMenu currentMenu;

        /// <summary>
        /// Initializes the UI manager by setting the default menu.
        /// </summary>
        public static void Init()
        {
            // Initialize the default menu (e.g., main menu)
            SetMenu(new UIMainMenu());
        }

        /// <summary>
        /// Sets the current menu to the specified menu and displays it.
        /// Hides the previous menu if one was set.
        /// </summary>
        /// <param name="menu">The new menu to be displayed.</param>
        public static void SetMenu(IMenu menu)
        {
            // Hide the current menu if there's one
            currentMenu?.Hide();

            // Set the new menu and show it
            currentMenu = menu;
            currentMenu.Show();
        }

        /// <summary>
        /// Updates all UI objects and the current active menu.
        /// </summary>
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

        /// <summary>
        /// Destroys UI objects that have been marked for destruction.
        /// </summary>
        private static void DestroyNeededObjects()
        {
            foreach (UIObject obj in toDestroy)
            {
                RemoveObject(obj);
            }
            toDestroy.Clear();
        }

        /// <summary>
        /// Adds a new UI object to the active list if it is not already present.
        /// </summary>
        /// <param name="obj">The UI object to be added.</param>
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

        /// <summary>
        /// Removes a UI object from the active list if it exists.
        /// </summary>
        /// <param name="obj">The UI object to be removed.</param>
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

        /// <summary>
        /// Marks a UI object for destruction at the end of the current update cycle.
        /// </summary>
        /// <param name="obj">The UI object to be marked for destruction.</param>
        public static void AddToDestroy(UIObject obj)
        {
            toDestroy.Add(obj);
        }
    }
}

