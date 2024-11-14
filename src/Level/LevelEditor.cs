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
        public static Vec2 platformSize = new(0, 0);
        public static bool isPlacingPlatform = false;

        public static void Update()
        {
            HandleInput();
        }

        private static void HandleInput()
        {
            var mousePos = InputManager.MousePosWorld() * new Vec2(1, -1);

            // Start placing platform
            if (InputManager.GetButtonDown(MouseButton.LeftButton) && !isPlacingPlatform)
            {
                objectPlacementPosition = mousePos;
                platformSize = new Vec2(0, 0);
                isPlacingPlatform = true;
            }

            // While dragging to resize the platform
            else if (InputManager.GetButtonDown(MouseButton.LeftButton) && isPlacingPlatform)
            {
                platformSize = new Vec2(mousePos.x - objectPlacementPosition.x, mousePos.y - objectPlacementPosition.y);
            }

            // Place the platform when the mouse is released
            if (!InputManager.GetButtonDown(MouseButton.LeftButton) && isPlacingPlatform)
            {
                isPlacingPlatform = false;
                AddObjectToLevel(new Ground(objectPlacementPosition + (platformSize/2), platformSize));
            }

            // Move object placement position (for other objects if needed)
            if (InputManager.GetKeyDown(Keys.Right))
                objectPlacementPosition.x += 5;
            if (InputManager.GetKeyDown(Keys.Left))
                objectPlacementPosition.x -= 5;
            if (InputManager.GetKeyDown(Keys.Up))
                objectPlacementPosition.y += 5;
            if (InputManager.GetKeyDown(Keys.Down))
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
