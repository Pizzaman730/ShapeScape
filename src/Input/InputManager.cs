using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Autofac;

namespace SquarePlatformer
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
            Point mousePos = Mouse.GetState().Position;
            return new Vec2(mousePos.X, mousePos.Y);
        }
        public static Vec2 MousePosWorld()
        {
            return Camera.TranslatePos(new Vec2()-MousePos());
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