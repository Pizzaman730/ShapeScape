using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Microsoft.Xna.Framework.Input;

namespace ShapeScape
{
    public static class LevelEditor
    {
        public static List<Object> currentLevelObjects = new();
        public static int selectedObjectIndex = 0;
        public static Vec2 platformStartPos = new();
        public static bool isPlacingPlatform = false;
        public static bool openedThisFrame = true;
        public static string placementType = "Platform";

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
            if (InputManager.ClickThisFrame())
            {
                if (placementType == "Platform")
                {
                    isPlacingPlatform = true;
                    platformStartPos = InputManager.MousePosWorld();
                }
            }
            if (isPlacingPlatform)
            {
                if (!InputManager.GetButtonDown(MouseButton.LeftButton))
                {
                    isPlacingPlatform = false;
                    CreatePlatform(platformStartPos, InputManager.MousePosWorld());
                }
            }
        }

        public static void AddObjectToLevel(Object obj)
        {
            currentLevelObjects.Add(obj);
            ObjectManager.AddObject(obj);
        }
        public static void CreatePlatform(Vec2 pos1, Vec2 pos2)
        {
            pos1.x -= WindowManager.size.x / 2;
            pos2.x -= WindowManager.size.x / 2;
            pos1.y += WindowManager.size.y / 2;
            pos2.y += WindowManager.size.y / 2;
            pos1 *= new Vec2(1, -1);
            pos2 *= new Vec2(1, -1);
            // Calculate size (absolute difference between coordinates)
            Vec2 size = new Vec2(Math.Abs(pos2.x - pos1.x), Math.Abs(pos2.y - pos1.y));
            if (size.x == 0 || size.y == 0)
            {
                return;
            }

            // Calculate center
            Vec2 center = new Vec2((pos1.x + pos2.x) / 2, (pos1.y + pos2.y) / 2);

            // Create the platform with the calculated center and size
            currentLevelObjects.Add(new Ground(center, size));
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
                var obj = new Ground(data.Position, data.Size);
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
}
