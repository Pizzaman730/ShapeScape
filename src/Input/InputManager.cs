using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Autofac;

namespace ShapeScape
{
    public static class InputManager
    {
        public static bool previousLeftClick;
        public static bool leftClickThisFrame = false;
        public static bool GetKeyDown(Keys key)
        {
            return Keyboard.GetState().IsKeyDown(key);
        }
        public static bool GetKeyDown(Keys[] keys)
        {
            foreach (Keys key in keys)
            {
                if (GetKeyDown(key)) return true;
            }
            return false;
        }
        public static bool GetButtonDown(MouseButton button)
        {
            return (button == MouseButton.LeftButton ? Mouse.GetState().LeftButton : (button == MouseButton.RightButton ? Mouse.GetState().RightButton : Mouse.GetState().MiddleButton)) == ButtonState.Pressed ? true : false;
        }
        public static Vec2 MousePos()
        {
            Vec2 mousePos = new Vec2(Mouse.GetState().Position.X, Mouse.GetState().Position.Y);
            
            
            return WindowManager.size / 2 + mousePos * new Vec2(-1, 1) - new Vec2(0, WindowManager.size.y);
        }
        public static Vec2 MousePosWorld()
        {
            Vec2 mousePos = Camera.TranslatePos(new Vec2()-MousePos()) * new Vec2(1, -1);
            mousePos.x -= WindowManager.size.x / 2;
            mousePos.y += WindowManager.size.y / 2;
            mousePos *= new Vec2(1, -1);
            return mousePos; 
        }
        public static void Update()
        {
            bool currentLeftClick = GetButtonDown(MouseButton.LeftButton);

            if (currentLeftClick && !previousLeftClick)
            {
                leftClickThisFrame = true;
            }
            else
            {
                leftClickThisFrame = false;
            }
            previousLeftClick = currentLeftClick;
        }
        public static bool ClickThisFrame()
        {
            return leftClickThisFrame;
        }
    }
}