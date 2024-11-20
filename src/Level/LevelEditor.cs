using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
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
            Logger.Debug("Update called.");
            if (openedThisFrame)
            {
                Logger.Debug("LevelEditor opened this frame. Setting `openedThisFrame` to false.");
                openedThisFrame = false;
                return;
            }
            HandleInput();
        }

        private static void HandleInput()
        {
            Logger.Debug("Handling input.");
            Vec2 mousePos = InputManager.MousePosWorld();
            Logger.Debug($"Mouse position: {mousePos}");

            if (!isPlacingPlatform && InputManager.GetKeyDown(numKeys))
            {
                if (InputManager.GetKeyDown(Keys.D1))
                {
                    placementType = "Ground";
                }
                else if (InputManager.GetKeyDown(Keys.D2))
                {
                    placementType = "Player";
                }
                else if (InputManager.GetKeyDown(Keys.D3))
                {
                    placementType = "CircleEnemy";
                }
                else if (InputManager.GetKeyDown(Keys.D4))
                {
                    placementType = "TriangleEnemy";
                }
                else if (InputManager.GetKeyDown(Keys.D5))
                {
                    placementType = "RectangleEnemy";
                }
                else if (InputManager.GetKeyDown(Keys.D6))
                {
                    placementType = "BouncyOval";
                }
                else if (InputManager.GetKeyDown(Keys.D7))
                {
                    placementType = "TrapezoidEnemy";
                }
                Logger.Debug($"Placement type changed to: {placementType}");
            }

            if (InputManager.ClickThisFrame())
            {
                Logger.Debug($"Mouse clicked. Placement type: {placementType}");
                if (placementType == "Ground")
                {
                    isPlacingPlatform = true;
                    platformStartPos = mousePos;
                    Logger.Debug($"Started placing platform at position: {platformStartPos}");
                }
                else
                {
                    Logger.Debug($"Adding object of type {placementType} at position: {mousePos}");
                    currentLevelObjects.Add(new EditorObject(placementType, mousePos, new Vec2(50, 50)));
                }
            }

            if (isPlacingPlatform)
            {
                Logger.Debug("Currently placing a platform.");
                if (!InputManager.GetButtonDown(MouseButton.LeftButton))
                {
                    Logger.Debug($"Finished placing platform. Start: {platformStartPos}, End: {mousePos}");
                    isPlacingPlatform = false;
                    CreatePlatform(platformStartPos, mousePos);
                }
            }
        }

        public static void AddObjectToLevel(EditorObject obj)
        {
            Logger.Debug($"Adding object to level: {obj.name} at {obj.position} with size {obj.size}");
            currentLevelObjects.Add(obj);
        }

        public static void CreatePlatform(Vec2 pos1, Vec2 pos2)
        {
            Logger.Debug($"Creating platform. Start: {pos1}, End: {pos2}");
            Vec2 size = new Vec2(Math.Abs(pos2.x - pos1.x), Math.Abs(pos2.y - pos1.y));
            if (size.x == 0 || size.y == 0)
            {
                Logger.Debug("Platform creation skipped due to zero size.");
                return;
            }

            Vec2 center = new Vec2((pos1.x + pos2.x) / 2, (pos1.y + pos2.y) / 2);
            Logger.Debug($"Platform created at center: {center} with size: {size}");
            currentLevelObjects.Add(new EditorObject("Ground", center, size));
        }

        public static void SaveLevel(string levelName)
        {
            Logger.Debug($"Saving level with name: {levelName}");
            var json = JsonSerializer.Serialize(currentLevelObjects, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            Directory.CreateDirectory("Levels");
            File.WriteAllText($"Levels/{levelName}.json", json);
            Logger.Debug($"Level saved to Levels/{levelName}.json");
        }

        public static void LoadLevel(string levelName)
        {
            Logger.Debug($"Loading level with name: {levelName}");
            ObjectManager.AddAllObjectsToDestroy();
            currentLevelObjects.Clear();

            string filePath = $"Levels/{levelName}.json";
            if (!File.Exists(filePath))
            {
                Logger.Log($"Level file '{levelName}' not found.");
                return;
            }

            var json = File.ReadAllText(filePath);
            Logger.Debug("Level file read successfully.");

            var levelData = JsonSerializer.Deserialize<List<EditorObject>>(json);

            if (levelData == null)
            {
                Logger.Log("Failed to load level: data was null.");
                return;
            }

            foreach (var data in levelData)
            {
                var obj = new EditorObject(data.name, data.position, data.size);
                AddObjectToLevel(obj);
            }
            Logger.Debug("Level loaded successfully.");
        }
    }

    [Serializable]
    public class EditorObject
    {
        public string name { get; set; }
        public Vec2 position { get; set; }
        public Vec2 size { get; set; }

        [JsonIgnore] 
        public ObjectTexture objectTexture;

        public EditorObject(string name, Vec2 pos, Vec2 size)
        {
            Logger.Debug($"Creating EditorObject: {name} at {pos} with size {size}");
            this.name = name;
            this.position = pos;
            this.size = size;
            InitializeObjectTexture();
        }

        public EditorObject() { }

        private void InitializeObjectTexture()
        {
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
            Logger.Debug("Object texture initialized.");
        }
    }
}
