using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Screens;

namespace ShapeScape
{
    /// <summary>
    /// The Renderer class is responsible for rendering objects in the game world.
    /// It handles drawing all game objects, editor UI, and debug elements such as hitboxes.
    /// </summary>
    public static class Renderer
    {
        private static SpriteBatch spriteBatch;
        private static Texture2D whiteTexture;

        /// <summary>
        /// Initializes the renderer with the specified SpriteBatch and GraphicsDevice.
        /// </summary>
        /// <param name="sb">The SpriteBatch for rendering.</param>
        /// <param name="graphicsDevice">The GraphicsDevice used to create textures.</param>
        public static void Init(SpriteBatch sb, GraphicsDevice graphicsDevice)
        {
            spriteBatch = sb;
            whiteTexture = new Texture2D(graphicsDevice, 1, 1);
            whiteTexture.SetData([Color.White]);
        }

        /// <summary>
        /// Draws all objects in the game, including UI and debug elements.
        /// </summary>
        public static void DrawAll()
        {
            List<Object> objects = ObjectManager.objects;
            spriteBatch.Begin(SpriteSortMode.Deferred, null, AssetManager.samplerState);
            foreach (Object obj in objects)
            {
                Draw(obj);

                if (Developer.devMode)
                {
                    DrawHitbox(obj);
                }
            }
            if (LevelManager.gameState == GameState.InEditor)
            {
                DrawEditorUI();
            }

            spriteBatch.End();
        }

        /// <summary>
        /// Draws a single object, handling both graphical and UI elements.
        /// </summary>
        /// <param name="obj">The object to be drawn.</param>
        public static void Draw(Object obj)
        {
            if (obj.name == "Text")
            {
                GameText text = (GameText)obj;
                Vec2 pos = WindowManager.size / 2 + (obj.corner + new Vec2(obj.size.x, 0)) * new Vec2(-1, 1);
                spriteBatch.DrawString(AssetManager.font, text.text, pos, text.color, 0, new Vec2(), (float)text.textScale, SpriteEffects.None, 1f);
                return;
            }
            foreach (var info in obj.objectTexture.textures)
            {
                if (!info.enabled) return;
                Vec2 pos = (obj.tags.Contains("UI") && ((UIObject)obj).overlay) ? (WindowManager.size / 2 + (obj.corner + info.offset + new Vec2(obj.size.x, 0)) * new Vec2(-1, 1)) : Camera.TranslatePos((new Vec2(obj.corner.x, obj.corner.y + obj.size.y) + info.offset) * new Vec2(1, -1));
                SpriteEffects effects = SpriteEffects.None;
                if (obj.flipTexture || info.flip) effects = SpriteEffects.FlipHorizontally;
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

        /// <summary>
        /// Draws the UI elements specific to the level editor, including platform previews.
        /// </summary>
        private static void DrawEditorUI()
        {
            foreach (EditorObject obj in LevelEditor.currentLevelObjects)
                {
                    foreach (var info in obj.objectTexture.textures)
                    {
                        if (!info.enabled) continue;
                        Vec2 pos = Camera.TranslatePos((new Vec2(obj.position.x - obj.size.x / 2, obj.position.y + obj.size.y / 2) + info.offset) * new Vec2(1, -1));
                        SpriteEffects effects = SpriteEffects.None;
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
            if (LevelEditor.isPlacingPlatform)
            {
                Vec2 mousePos = InputManager.MousePosWorld();
                Vec2 pos1 = LevelEditor.platformStartPos;
                Vec2 pos2 = mousePos;
                pos1 *= new Vec2(1, -1);
                pos2 *= new Vec2(1, -1);
                pos1 = Camera.TranslatePos(pos1);
                pos2 = Camera.TranslatePos(pos2);

                Vec2 size = new Vec2(Math.Abs(pos2.x - pos1.x), Math.Abs(pos2.y - pos1.y));
                Vec2 start = new Vec2(Math.Min(pos1.x, pos2.x), Math.Min(pos1.y, pos2.y));

                // Draw a semi-transparent platform preview
                spriteBatch.Draw(
                    whiteTexture, 
                    new Rectangle((int)start.x, (int)start.y, (int)size.x, (int)size.y), 
                    new Color(0f, 0f, 1f, 0.5f)  // Semi-transparent blue
                );
            }
        }

        /// <summary>
        /// Draws the hitbox of an object for debugging purposes.
        /// </summary>
        /// <param name="obj">The object whose hitbox is to be drawn.</param>

        private static void DrawHitbox(Object obj)
        {
            // Logger.Log($"Original Hitbox Position: X={obj.Hitbox.X}, Y={obj.Hitbox.Y}");

            Vec2 objectPos = obj.position;

            Vec2 hitboxPos = (obj.tags.Contains("UI") && ((UIObject)obj).overlay) ? (WindowManager.size / 2 + (obj.corner + new Vec2(obj.size.x, 0)) * new Vec2(-1, 1)) : Camera.TranslatePos(new Vec2(obj.corner.x, obj.corner.y + obj.size.y) * new Vec2(1, -1));

            // Logger.Log($"Transformed Hitbox Position: X={hitboxPos.x}, Y={hitboxPos.y}");

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