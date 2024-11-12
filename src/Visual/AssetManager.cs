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
        private static SpriteBatch spriteBatch;
        private static bool initialized = false;
        private static ContentManager contentManager;
        private static GraphicsDevice graphicsDevice => spriteBatch.GraphicsDevice;
        private static string textureDataFile;
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
                Console.WriteLine("AssetManager already initialized!");
                return;
            }
            spriteBatch = sb;
            contentManager = Game1.game.Content;
            initialized = true;
        }
        public static void FetchAllContent()
        {
            if (!initialized)
            {
                Console.WriteLine("Can't fetch content, not initialized yet");
                return;
            }
            textureDataFile = File.ReadAllText("Content/texturedata.json");
            CreateTexture("Player", 50, 50);
            CreateTexture("Enemy", 50, 50);
            CreateTexture("Grass", 100, 100);
            CreateTexture("Dirt", 100, 100);
            CreateTexture("StartButton", 200, 100);
            
            CreateAllObjectTextures();
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
                    info.UpdateTexture();
                }
                new Rectangle();
            }
        }
        public static Texture2D CreateTexture(string name, int width, int height)
        {
            Texture2D texture = contentManager.Load<Texture2D>(name);
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
        public static TextureInfo TileTexture(TextureInfo info, Vec2 size)
        {
            int originalWidth = info.texture.Width;
            int originalHeight = info.texture.Height;
            if (size.x <= 0 || size.y <= 0 || (originalWidth == size.x && originalHeight == size.y) || info == null) return info;

            Vec2 newTextureSize = new();
            TextureInfo newInfo = new();
            newInfo.name = info.name;

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

            newInfo.SetTexture(target);
            spriteBatch.End();
            graphicsDevice.SetRenderTarget(null);
            
            return newInfo;
        }
        public static Texture2D GetTexture(string name)
        {
            return textures[name];
        }
        public static ObjectTexture GetObjectTexture(string name)
        {
            ObjectTexture objTexture = objectTextures[name];
            return new ObjectTexture
            {
                name = objTexture.name,
                textures = objTexture.textures?.Select(a => a.Copy()).ToArray()
            };
            
        }
    }
}