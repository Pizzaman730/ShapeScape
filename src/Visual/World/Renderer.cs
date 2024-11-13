using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Screens;

namespace SquarePlatformer
{
    public static class Renderer
    {
        private static SpriteBatch spriteBatch;
        public static void Init(SpriteBatch sb)
        {
            spriteBatch = sb;
        }
        public static void DrawAll()
        {
            List<Object> objects = ObjectManager.objects;
            spriteBatch.Begin();
            foreach (Object obj in objects)
            {
                Draw(obj);
            }
            spriteBatch.End(); 
        }
        public static void Draw(Object obj)
        {
            foreach (var info in obj.objectTexture.textures)
            {
                //if (!info.enabled) return;
                Vec2 pos = (obj.tags.Contains("UI") && ((UIObject)obj).overlay) ? (WindowManager.size / 2 + obj.corner + info.offset) : Camera.TranslatePos((new Vec2(obj.corner.x, obj.corner.y + obj.size.y) + info.offset) * new Vec2(1, -1));
                SpriteEffects effects = SpriteEffects.None;
                if (obj.flipTexture) effects = SpriteEffects.FlipHorizontally;
                //Logger.Log(info.offset);
                if (info.name == "Dirt") info.offset = new Vec2(0, -100);
                spriteBatch.Draw(
                    info.texture,
                    pos,
                    null,
                    Color.White,
                    0,
                    Vector2.Zero,
                    1,
                    effects,
                    0
                    );
            }
        }
        //public static Vec2 WorldToPixel();
    }
}