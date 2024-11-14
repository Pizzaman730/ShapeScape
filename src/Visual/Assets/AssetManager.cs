using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SquarePlatformer
{
    public static class AssetManager
    {
        public static Dictionary<string, Texture2D> textures { get; private set; } = [];
        public static Dictionary<string, ObjectTexture> objectTextures { get; private set; } = [];
        public static Dictionary<string, Animation> animations { get; private set; } = [];
        private static SpriteBatch spriteBatch;
        private static bool initialized = false;
        private static ContentManager contentManager;
        private static GraphicsDevice graphicsDevice => spriteBatch.GraphicsDevice;
        private static string textureDataFile;
        private static string animationDataFile;
        private static readonly SamplerState samplerState = new()
        {
            Filter = TextureFilter.Point,
            AddressU = TextureAddressMode.Clamp,
            AddressV = TextureAddressMode.Clamp
        };
        public static void Init(SpriteBatch sb)
        {
            if (initialized)
            {
                Logger.Log("AssetManager already initialized!");
                return;
            }
            spriteBatch = sb;
            contentManager = Main.game.Content;
            initialized = true;
        }
        public static void FetchAllContent()
        {
            if (!initialized)
            {
                Logger.Log("Can't fetch content, not initialized yet");
                return;
            }

            try
            {
                textureDataFile = File.ReadAllText("Content/texturedata.json");
                animationDataFile = File.ReadAllText("Content/animationdata.json");

                // Log the texture loading process
                Logger.Log("Loading textures...");
                CreateTexture("PlayerLeft", 50, 50);
                CreateTexture("PlayerRight", 50, 50);
                CreateTexture("PlayerStraight", 50, 50);

                //CreateTexture("Enemy", 50, 50);

                CreateTexture("Grass", 100, 100);
                CreateTexture("Dirt", 100, 100);

                CreateTexture("StartButton", 200, 100);

                CreateTexture("PlayerFaceRight", 50, 50);
                CreateTexture("PlayerFaceStraight", 50, 50);
                CreateTexture("PlayerBody", 50, 50);
                
                CreateTexture("PlayerBodyJumpF1", 50, 50);
                CreateTexture("PlayerBodyJumpF2", 50, 50);
                CreateTexture("PlayerFaceJumpF1", 50, 50);
                CreateTexture("PlayerFaceJumpF2", 50, 50);
                CreateTexture("PlayerFaceJumpF3", 50, 50);

                CreateTexture("CircleEnemyFaceRight", 50, 50);
                CreateTexture("CircleEnemyFaceStraight", 50, 50);
                CreateTexture("CircleEnemyBody", 50, 50);

                CreateTexture("TriangleEnemyFaceRight", 50, 50);
                CreateTexture("TriangleEnemyFaceStraight", 50, 50);
                CreateTexture("TriangleEnemyBody", 50, 50);

                CreateTexture("RectangleEnemyFaceRight", 100, 50);
                CreateTexture("RectangleEnemyFaceStraight", 100, 50);
                CreateTexture("RectangleEnemyBody", 100, 50);
                

                // Log ObjectTextures and Animations creation
                Logger.Log("Creating object textures...");
                CreateAllObjectTextures();
                
                Logger.Log("Creating animations...");
                CreateAllAnimations();
            }
            catch (Exception ex)
            {
                Logger.Log($"Error during content fetch: {ex.Message}");
            }
        }

        public static void CreateAllObjectTextures()
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var data = JsonSerializer.Deserialize<ObjectTexture[]>(textureDataFile, options);

            foreach (var obj in data)
            {
                objectTextures.Add(obj.name, obj);
                
                foreach (TextureInfo info in obj.textures)
                {
                    Logger.Log("Creating texture: " + info.name);
                    info.UpdateTexture();
                }
            }
        }
        public static void CreateAllAnimations()
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var data = JsonSerializer.Deserialize<Animation[]>(animationDataFile, options);

            foreach (var obj in data)
            {
                animations.Add(obj.name, obj);
            }
        }
        public static Texture2D CreateTexture(string name, int width, int height)
        {
            try
            {
                Texture2D texture = contentManager.Load<Texture2D>(name);
                
                // Check if the texture is null after loading
                if (texture == null)
                {
                    Logger.Log($"Failed to load texture: {name}");
                    return null; 
                }

                RenderTarget2D target = new RenderTarget2D(graphicsDevice, width, height);
                graphicsDevice.SetRenderTarget(target);
                graphicsDevice.Clear(Color.Transparent);

                spriteBatch.Begin(SpriteSortMode.Deferred, null, samplerState);
                spriteBatch.Draw(texture, new Rectangle(0, 0, width, height), Color.White);
                spriteBatch.End();

                graphicsDevice.SetRenderTarget(null);
                textures.Add(name, target);
                return target;
            }
            catch (ContentLoadException ex)
            {
                Logger.Log($"Error loading texture '{name}': {ex.Message}");
                return null;
            }
        }

        public static TextureInfo TileTexture(TextureInfo info, Vec2 size)
        {
            int originalWidth = info.texture.Width;
            int originalHeight = info.texture.Height;
            if (size.x <= 0 || size.y <= 0 || (originalWidth == size.x && originalHeight == size.y) || info == null) return info;

            Vec2 newTextureSize = new();

            RenderTarget2D target = new RenderTarget2D(graphicsDevice, (int)size.x, (int)size.y);
            graphicsDevice.SetRenderTarget(target);
            spriteBatch.Begin(SpriteSortMode.Deferred, null, samplerState);
            graphicsDevice.Clear(Color.Transparent);

            for (int j = 0; j < size.y / originalHeight; j++)
            {
                for (int i = 0; i < size.x / originalWidth; i++)
                {
                    spriteBatch.Draw(info.texture, new Rectangle(originalWidth * i, originalHeight * j, originalWidth, originalHeight), Color.White);
                }
            }

            info.SetTexture(target);
            spriteBatch.End();
            graphicsDevice.SetRenderTarget(null);
            
            return info;
        }
        public static Texture2D GetTexture(string name)
        {
            //Logger.Log("Getting texture: " + name);
            return textures[name];
        }
        public static ObjectTexture GetObjectTexture(string name)
        {
            if (objectTextures.ContainsKey(name))
            {
                ObjectTexture objTexture = objectTextures[name];
                return new ObjectTexture
                {
                    name = objTexture.name,
                    textures = objTexture.textures?.Select(a => a.Copy()).ToArray()
                };
            }
            else
            {
                Logger.Log($"Error: Object texture '{name}' not found.");
                return null; 
            }
        }

        public static Animation GetAnimation(string name)
        {
            Animation animation = animations[name];
            return new Animation
            {
                name = animation.name,
                keyframes = animation.keyframes
            };
            
        }
    }
}