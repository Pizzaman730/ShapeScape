/*************  ✨ Codeium Command 🌟  *************/
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
    /// <summary>
    /// The AssetManager class is responsible for managing game assets such as textures, object textures, 
    /// and animations. It provides methods to initialize assets, fetch content, create textures, and retrieve them.
    /// </summary>
    public static class AssetManager
    {
        /// <summary>
        /// Dictionary holding all loaded textures indexed by their names.
        /// </summary>
        public static Dictionary<string, Texture2D> textures { get; private set; } = [];

        /// <summary>
        /// Dictionary holding all loaded object textures indexed by their names.
        /// </summary>
        public static Dictionary<string, ObjectTexture> objectTextures { get; private set; } = [];

        /// <summary>
        /// Dictionary holding all loaded animations indexed by their names.
        /// </summary>
        public static Dictionary<string, Animation> animations { get; private set; } = [];

        private static SpriteBatch spriteBatch;
        private static bool initialized = false;
        private static ContentManager contentManager;

        /// <summary>
        /// Property to access the graphics device from the sprite batch.
        /// </summary>
        private static GraphicsDevice graphicsDevice => spriteBatch.GraphicsDevice;

        private static string textureDataFile;
        private static string animationDataFile;

        /// <summary>
        /// Fonts used in the game.
        /// </summary>
        
        public static SpriteFont font;
        public static SpriteFont shoppingCartFont;
        public static SpriteFont grapeSodaFont;

        /// <summary>
        /// SamplerState configuration for texture filtering and addressing.
        /// </summary>
        public static readonly SamplerState samplerState = new()
        {
            Filter = TextureFilter.Point,
            AddressU = TextureAddressMode.Clamp,
            AddressV = TextureAddressMode.Clamp,
            AddressW = TextureAddressMode.Clamp
        };

        /// <summary>
        /// Initializes the AssetManager with the specified sprite batch.
        /// </summary>
        /// <param name="sb">The SpriteBatch for rendering.</param>
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

        /// <summary>
        /// Loads all necessary game content including fonts, textures, and animations.
        /// </summary>
        public static void FetchAllContent()
        {
            if (!initialized)
            {
                Logger.Log("Can't fetch content, not initialized yet");
                return;
            }

            try
            {
                shoppingCartFont = contentManager.Load<SpriteFont>("CartFont");
                grapeSodaFont = contentManager.Load<SpriteFont>("GrapeSoda");
                font = grapeSodaFont;
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
                    { "TriangleMorph", (50, 50)},
                    { "TriangleMorphFaceRight", (50, 50)},
                    { "TriangleMorphFaceStraight", (50, 50)},
                    { "TrapezoidEnemyFaceRight", (50, 25) },
                    { "TrapezoidEnemyFaceStraight", (50, 25) },
                    { "TrapezoidEnemyBody", (50, 25) },
                    { "TrapezoidMorph", (50, 25) },
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
                    { "SettingsButton", (100, 100) },
                    { "BackButton", (100, 100) },
                    { "RestartButton", (100, 100) },
                    { "SwitchRed", (50, 25) },
                    { "SwitchHalf", (50, 25) },
                    { "SwitchGreen", (50, 25) },
                    { "EditorButton", (200, 100) },
                    { "SaveButton", (200, 100) },
                    { "LoadButton", (200, 100) },
                    { "StonePlatformLeft", (50, 50) },
                    { "StonePlatformMiddle", (50, 50) },
                    { "StonePlatformRight", (50, 50) },
                    { "StonePlatformSingle", (50, 50) }
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

        /// <summary>
        /// Creates textures for all object textures using data from textureDataFile.
        /// </summary>
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
                    if (info.name == "Empty")
                    {
                        info.SetTexture(CreateMissingTexture());
                        continue;
                    }
                    Logger.Log("Creating texture: " + info.name);
                    info.UpdateTexture();
                }
            }
        }

        /// <summary>
        /// Creates all animations using data from animationDataFile.
        /// </summary>
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

        /// <summary>
        /// Creates a texture with the given name, width, and height.
        /// If the texture cannot be loaded, a default "missing texture" is used.
        /// </summary>
        /// <param name="name">The name of the texture.</param>
        /// <param name="width">The width of the texture.</param>
        /// <param name="height">The height of the texture.</param>
        /// <returns>The created texture.</returns>
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

        /// <summary>
        /// Tiles a texture over a specified size, creating a new texture in the process.
        /// </summary>
        /// <param name="info">Information about the texture to tile.</param>
        /// <param name="size">The size to tile the texture over.</param>
        /// <returns>The tiled texture info.</returns>

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
        /// <summary>
        /// Retrieves a texture by its name. If the requested texture is "Text", null is returned.
        /// </summary>
        /// <param name="name">The name of the texture to retrieve.</param>
        /// <returns>The requested Texture2D object.</returns>
        public static Texture2D GetTexture(string name)
        {
            //Logger.Log("Getting texture: " + name);
            if (name == "Text") return null;
            return textures[name];
        }

        /// <summary>
        /// Retrieves an object texture by its name. If the texture is not found, logs an error and returns an "Empty" texture.
        /// </summary>
        /// <param name="name">The name of the object texture to retrieve.</param>
        /// <returns>A copy of the requested ObjectTexture.</returns>
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
                return objectTextures["Empty"];
            }
        }

        /// <summary>
        /// Retrieves an animation by its name.
        /// </summary>
        /// <param name="name">The name of the animation to retrieve.</param>
        /// <returns>A copy of the requested Animation.</returns>
        public static Animation GetAnimation(string name)
        {
            Animation animation = animations[name];
            return new Animation
            {
                name = animation.name,
                keyframes = animation.keyframes
            };
            
        }

        /// <summary>
        /// Creates a "missing texture" with a standard checkerboard pattern for use when a texture cannot be found.
        /// </summary>
        /// <returns>A Texture2D object representing the missing texture.</returns>
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
