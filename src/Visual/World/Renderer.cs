using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Screens;

namespace SquarePlatformer
{
    public static class Renderer
    {
        private static SpriteBatch spriteBatch;
        private static Texture2D whiteTexture;
        public static void Init(SpriteBatch sb, GraphicsDevice graphicsDevice)
        {
            spriteBatch = sb;
            whiteTexture = new Texture2D(graphicsDevice, 1, 1);
            whiteTexture.SetData([Color.White]);
        }
        public static void DrawAll()
        {
            List<Object> objects = ObjectManager.objects;
            spriteBatch.Begin();
            if (LevelManager.gameState == GameState.InEditor)
            {
                DrawEditorUI();
            }

            foreach (Object obj in objects)
            {
                Draw(obj);

                if (Developer.devMode)
                {
                    DrawHitbox(obj);
                }
            }
            spriteBatch.End(); 
        }
        public static void Draw(Object obj)
        {
            foreach (var info in obj.objectTexture.textures)
            {
                if (!info.enabled) return;
                Vec2 pos = (obj.tags.Contains("UI") && ((UIObject)obj).overlay) ? (WindowManager.size / 2 + obj.corner + info.offset) : Camera.TranslatePos((new Vec2(obj.corner.x, obj.corner.y + obj.size.y) + info.offset) * new Vec2(1, -1));
                SpriteEffects effects = SpriteEffects.None;
                if (obj.flipTexture || info.flip) effects = SpriteEffects.FlipHorizontally;
                //Logger.Log(info.offset);
                //if (info.name == "Dirt") info.offset = new Vec2(0, -100);
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


        private static void DrawEditorUI()
        {
            if (LevelEditor.isPlacingPlatform)
            {
                // Draw a preview of the platform while dragging
                Vec2 start = Camera.TranslatePos(LevelEditor.objectPlacementPosition * new Vec2(-1, 1));
                Vec2 size = LevelEditor.platformSize * new Vec2(-1, 1);

                // Draw a semi-transparent platform preview
                spriteBatch.Draw(
                    whiteTexture, 
                    new Rectangle((int)start.x, (int)start.y, (int)size.x, (int)size.y), 
                    new Color(0f, 0f, 1f, 0.5f)  // Semi-transparent blue
                );
            }
        }


        private static void DrawHitbox(Object obj)
        {
            if (obj.Hitbox != null)  
            {
                Logger.Log($"Original Hitbox Position: X={obj.Hitbox.X}, Y={obj.Hitbox.Y}");

                Vec2 objectPos = obj.position;

                Vec2 hitboxPos = Camera.TranslatePos(new Vec2(objectPos.x - obj.size.x / 2, -(objectPos.y - obj.size.y / 2)));

                Logger.Log($"Transformed Hitbox Position: X={hitboxPos.x}, Y={hitboxPos.y}");

                // Set the color for the hitbox outline
                Color hitboxColor = Color.Red;

                // Draw the top border of the hitbox
                spriteBatch.Draw(
                    whiteTexture,  
                    new Rectangle((int)hitboxPos.x, (int)hitboxPos.y, (int)obj.size.x, 3),  // Top border (3px height)
                    hitboxColor
                );

                // Draw the bottom border of the hitbox
                spriteBatch.Draw(
                    whiteTexture,  
                    new Rectangle((int)hitboxPos.x, (int)(hitboxPos.y + obj.size.y - 3), (int)obj.size.x, 3),  // Bottom border (3px height)
                    hitboxColor
                );

                // Draw the left border of the hitbox
                spriteBatch.Draw(
                    whiteTexture,  
                    new Rectangle((int)hitboxPos.x, (int)hitboxPos.y, 3, (int)obj.size.y),  // Left border (3px width)
                    hitboxColor
                );

                // Draw the right border of the hitbox
                spriteBatch.Draw(
                    whiteTexture,  
                    new Rectangle((int)(hitboxPos.x + obj.size.x - 3), (int)hitboxPos.y, 3, (int)obj.size.y),  // Right border (3px width)
                    hitboxColor
                );
            }
        }

    }
}