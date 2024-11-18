using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Microsoft.Xna.Framework.Input;

namespace ShapeScape
{
    public static class LevelEditor
    {
        public static List<EditorObject> currentLevelObjects = new();
        public static int selectedObjectIndex = 0;
        public static Vec2 platformStartPos = new();
        public static bool isPlacingPlatform = false;
        public static bool openedThisFrame = true;
        public static string placementType = "Ground";
        public static Keys[] numKeys = {Keys.D1, Keys.D2, Keys.D3, Keys.D4, Keys.D5, Keys.D6, Keys.D7, Keys.D8, Keys.D9};

        public static void Update()
        {
            if (openedThisFrame)
            {
                openedThisFrame = false;
                return;
            }
            HandleInput();
        }

        private static void HandleInput()
        {
            Vec2 mousePos = InputManager.MousePosWorld();
            if (!isPlacingPlatform && InputManager.GetKeyDown(numKeys))
            {
                if (InputManager.GetKeyDown(Keys.D1))
                {
                    placementType = "Ground";
                }
                else if (InputManager.GetKeyDown(Keys.D2))
                {
                    placementType = "CircleEnemy";
                }
            }
            if (InputManager.ClickThisFrame())
            {
                if (placementType == "Ground")
                {
                    isPlacingPlatform = true;
                    platformStartPos = mousePos;
                }
                if (placementType == "CircleEnemy")
                {
                    currentLevelObjects.Add(new EditorObject("CircleEnemy", mousePos, new Vec2(50, 50)));
                }
            }
            if (isPlacingPlatform)
            {
                if (!InputManager.GetButtonDown(MouseButton.LeftButton))
                {
                    isPlacingPlatform = false;
                    CreatePlatform(platformStartPos, mousePos);
                }
            }
        }

        public static void AddObjectToLevel(EditorObject obj)
        {
            currentLevelObjects.Add(obj);
        }
        public static void CreatePlatform(Vec2 pos1, Vec2 pos2)
        {
            // Calculate size (absolute difference between coordinates)
            Vec2 size = new Vec2(Math.Abs(pos2.x - pos1.x), Math.Abs(pos2.y - pos1.y));
            if (size.x == 0 || size.y == 0)
            {
                return;
            }

            // Calculate center
            Vec2 center = new Vec2((pos1.x + pos2.x) / 2, (pos1.y + pos2.y) / 2);

            // Create the platform with the calculated center and size
            currentLevelObjects.Add(new EditorObject("Ground", center, size));
        }

        public static void SaveLevel(string levelName)
        {
            var levelData = new List<SerializedObjectData>();
            foreach (var obj in currentLevelObjects)
            {
                levelData.Add(new SerializedObjectData
                {
                    Name = obj.name,
                    Position = obj.position,
                    Size = obj.size
                });
            }

            var json = JsonSerializer.Serialize(levelData);
            File.WriteAllText($"Levels/{levelName}.json", json);
        }

        public static void LoadLevel(string levelName)
        {
            ObjectManager.AddAllObjectsToDestroy(); // Clear existing objects
            currentLevelObjects.Clear();

            var json = File.ReadAllText($"Levels/{levelName}.json");
            var levelData = JsonSerializer.Deserialize<List<SerializedObjectData>>(json);

            foreach (var data in levelData)
            {
                var obj = new EditorObject("Ground", data.Position, data.Size);
                AddObjectToLevel(obj);
            }
        }
    }


    public class SerializedObjectData
    {
        public string Name { get; set; }
        public Vec2 Position { get; set; }
        public Vec2 Size { get; set; }
    }
    public class EditorObject
    {
        public ObjectTexture objectTexture;
        public string name;
        public Vec2 position;
        public Vec2 size;
        public EditorObject(string name, Vec2 pos, Vec2 size)
        {
            this.name = name;
            this.position = pos;
            this.size = size;
            objectTexture = AssetManager.GetObjectTexture(name);
            if (name == "Ground")
            {
                objectTexture.textures[0] = AssetManager.TileTexture(objectTexture.textures[0], new Vec2(size.x, size.y)); 
                if (size.y >= 100)
                {
                    objectTexture.textures[1] = AssetManager.TileTexture(objectTexture.textures[1], new Vec2(size.x, size.y - 100)); 
                    Logger.Log("Creating object with bottom");
                }
                else
                {
                    objectTexture.textures[1].enabled = false;
                    Logger.Log("Creating object with no bottom");
                }
            }
            Logger.Log("Created object");
        }
    }
}
