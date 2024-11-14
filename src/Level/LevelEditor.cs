using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Microsoft.Xna.Framework.Input;

namespace SquarePlatformer
{
    public static class LevelEditor
    {
        public static List<Object> currentLevelObjects = new();
        public static int selectedObjectIndex = 0;
        public static Vec2 objectPlacementPosition = new(0, 0);
        public static Vec2 platformStartPosition = new(0, 0);
        public static Vec2 platformSize = new(0, 0);
        public static bool isPlacingPlatform = false;

        public static void Update()
        {
            HandleInput();
        }

        private static void HandleInput()
        {
            var mouseState = Mouse.GetState();
            var mousePos = new Vec2(mouseState.X, mouseState.Y);

            // Start placing platform
            if (mouseState.LeftButton == ButtonState.Pressed && !isPlacingPlatform)
            {
                platformStartPosition = mousePos;
                platformSize = new Vec2(0, 0);
                isPlacingPlatform = true;
            }

            // While dragging to resize the platform
            if (mouseState.LeftButton == ButtonState.Pressed && isPlacingPlatform)
            {
                platformSize = new Vec2(mousePos.x - platformStartPosition.x, mousePos.y - platformStartPosition.y);
            }

            // Place the platform when the mouse is released
            if (mouseState.LeftButton == ButtonState.Released && isPlacingPlatform)
            {
                isPlacingPlatform = false;
                AddObjectToLevel(new Ground(platformStartPosition, platformSize));
            }

            // Move object placement position (for other objects if needed)
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
                objectPlacementPosition.x += 5;
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
                objectPlacementPosition.x -= 5;
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
                objectPlacementPosition.y += 5;
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
                objectPlacementPosition.y -= 5;

            // Add object to level (e.g., pressing Enter)
            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                AddObjectToLevel(new Ground(objectPlacementPosition, new Vec2(50, 10)));
            }
        }

        public static void AddObjectToLevel(Object obj)
        {
            currentLevelObjects.Add(obj);
            ObjectManager.AddObject(obj);
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
