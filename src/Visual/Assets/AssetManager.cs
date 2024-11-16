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

namespace ShapeScape
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

                Logger.Log("Loading textures...");
                var texturesToLoad = new Dictionary<string, (int width, int height)>
                {
                    { "PlayerBody", (50, 50) },
                    { "PlayerFaceRight", (50, 50) },
                    { "PlayerFaceStraight", (50, 50) },
                    { "Grass", (100, 100) },
                    { "Dirt", (100, 100) },
                    { "StartButton", (200, 100) },
                    { "PlayerBodyJumpF1", (50, 50) },
                    { "PlayerBodyJumpF2", (50, 50) },
                    { "PlayerFaceJumpF1", (50, 50) },
                    { "PlayerFaceJumpF2", (50, 50) },
                    { "PlayerFaceJumpF3", (50, 50) },
                    { "CircleMorph", (50, 50) },
                    { "BouncyOvalMorph", (100, 50)},
                    { "CircleEnemyFaceRight", (50, 50) },
                    { "CircleEnemyFaceStraight", (50, 50) },
                    { "CircleEnemyBody", (50, 50) },
                    { "TriangleEnemyFaceRight", (50, 50) },
                    { "TriangleEnemyFaceStraight", (50, 50) },
                    { "TriangleEnemyBody", (50, 50) },
                    { "RectangleEnemyFaceRight", (100, 50) },
                    { "RectangleEnemyFaceStraight", (100, 50) },
                    { "RectangleEnemyBody", (100, 50) },
                    { "BouncyOvalFaceRight", (100, 50) },
                    { "BouncyOvalFaceStraight", (100, 50) },
                    { "BouncyOvalBody", (100, 50) },
                    { "BouncyOvalFaceSquishF1", (100, 50) },
                    { "BouncyOvalFaceSquishF2", (100, 50) },
                    { "BouncyOvalFaceSquishF3", (100, 50) },
                    { "BouncyOvalFaceSquishF4", (100, 50) },
                    { "BouncyOvalBodySquish1", (100, 50) },
                    { "BouncyOvalBodySquish2", (100, 50) },
                    { "SmileFace", (50, 50) },
                    { "SmirkFace", (50, 50) },
                    { "FrownFace", (50, 50) },
                    { "SadFace", (50, 50) },
                    { "MadFace", (50, 50) },
                    { "FrownFaceBrow", (50, 50) },
                    { "HappyFaceStraight", (50, 50) },
                    { "SadFaceStraight", (50, 50) },
                    { "MadFaceStraight", (50, 50) },
                    { "SettingsButton", (100, 100) }
                };

                foreach (var texture in texturesToLoad)
                {
                    CreateTexture(texture.Key, texture.Value.width, texture.Value.height);
                }

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

                // If texture is not found, use the missing texture instead
                if (texture == null)
                {
                    Logger.Log($"Failed to load texture: {name}. Using missing texture.");
                    texture = CreateMissingTexture();
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
                Texture2D missingTexture = CreateMissingTexture(); // Fallback in case of exception
                return missingTexture;
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

        private static Texture2D CreateMissingTexture()
        {
            int width = 64;  // Standard size for missing textures
            int height = 64;
            Color[] data = new Color[width * height];

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    // Create a checkerboard pattern (alternating black and purple)
                    if ((x / 8 + y / 8) % 2 == 0)
                        data[y * width + x] = Color.Black;
                    else
                        data[y * width + x] = new Color(128, 0, 128);  // Purple
                }
            }

            Texture2D texture = new Texture2D(graphicsDevice, width, height);
            texture.SetData(data);
            return texture;
        }

    }
}