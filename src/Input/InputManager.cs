using System;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace ShapeScape
{
    public static class InputManager
    {
        // Stores the previous left mouse button state
        private static bool previousLeftClick;
        private static bool leftClickThisFrame;

        // Stores the previous keyboard and mouse states
        private static KeyboardState previousKeyboardState;
        private static MouseState previousMouseState;

        /// <summary>
        /// Whether a given key is currently being pressed
        /// </summary>
        /// <param name="key">The key to check</param>
        /// <returns>True if the key is being pressed, false otherwise</returns>
        public static bool GetKeyDown(Keys key)
        {
            return Keyboard.GetState().IsKeyDown(key);
        }

        /// <summary>
        /// Whether any of the given keys are currently being pressed
        /// </summary>
        /// <param name="keys">The keys to check</param>
        /// <returns>True if any of the keys are being pressed, false otherwise</returns>
        public static bool GetKeyDown(Keys[] keys)
        {
            foreach (Keys key in keys)
            {
                if (GetKeyDown(key)) return true;
            }
            return false;
        }

        /// <summary>
        /// Whether a given key has just been pressed
        /// </summary>
        /// <param name="key">The key to check</param>
        /// <returns>True if the key has just been pressed, false otherwise</returns>
        public static bool GetKeyPressed(Keys key)
        {
            return Keyboard.GetState().IsKeyDown(key) && !previousKeyboardState.IsKeyDown(key);
        }

        /// <summary>
        /// Whether a given mouse button is currently being pressed
        /// </summary>
        /// <param name="button">The button to check</param>
        /// <returns>True if the button is being pressed, false otherwise</returns>
        public static bool GetButtonDown(MouseButton button)
        {
            var currentState = Mouse.GetState();
            return (button == MouseButton.LeftButton ? currentState.LeftButton : (button == MouseButton.RightButton ? currentState.RightButton : currentState.MiddleButton)) == ButtonState.Pressed;
        }

        /// <summary>
        /// Whether a given mouse button has just been pressed
        /// </summary>
        /// <param name="button">The button to check</param>
        /// <returns>True if the button has just been pressed, false otherwise</returns>
        public static bool GetButtonPressed(MouseButton button)
        {
            var currentState = Mouse.GetState();
            var isPressed = (button == MouseButton.LeftButton ? currentState.LeftButton : (button == MouseButton.RightButton ? currentState.RightButton : currentState.MiddleButton)) == ButtonState.Pressed;
            var wasPressed = (button == MouseButton.LeftButton ? previousMouseState.LeftButton : (button == MouseButton.RightButton ? previousMouseState.RightButton : previousMouseState.MiddleButton)) == ButtonState.Pressed;
            return isPressed && !wasPressed;
        }

        /// <summary>
        /// Gets the position of the mouse
        /// </summary>
        /// <returns>The position of the mouse</returns>
        public static Vec2 MousePos()
        {
            var mousePos = new Vec2(Mouse.GetState().Position.X, Mouse.GetState().Position.Y);
            return WindowManager.size / 2 + mousePos * new Vec2(-1, 1) - new Vec2(0, WindowManager.size.y);
        }

        /// <summary>
        /// Gets the position of the mouse relative to the game world
        /// </summary>
        /// <returns>The position of the mouse in game world space</returns>
        public static Vec2 MousePosWorld()
        {
            Vec2 mousePos = Camera.TranslatePos(new Vec2()-MousePos()) * new Vec2(1, -1);
            mousePos.x -= WindowManager.size.x / 2;
            mousePos.y += WindowManager.size.y / 2;
            mousePos *= new Vec2(1, -1);
            return mousePos; 
        }

        /// <summary>
        /// Updates the state of the input manager
        /// </summary>
        public static void Update()
        {
            var currentMouseState = Mouse.GetState();
            var currentLeftClick = GetButtonDown(MouseButton.LeftButton);

            leftClickThisFrame = currentLeftClick && !previousLeftClick;
            previousLeftClick = currentLeftClick;

            previousKeyboardState = Keyboard.GetState();
            previousMouseState = currentMouseState;
        }

        /// <summary>
        /// Checks if the left mouse button was pressed this frame
        /// </summary>
        /// <returns>True if the left mouse button was pressed this frame, false otherwise</returns>
        public static bool ClickThisFrame()
        {
            return leftClickThisFrame;
        }
    }
}

