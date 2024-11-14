// namespace SquarePlatformer {
//     public class UIMenuEditor : IMenu
//     {
//         private Button saveButton;
//         private Button loadButton;

//         public void Init()
//         {
//             saveButton = new Button("Save Level", new Vec2(100, 200), SaveLevelClicked);
//             loadButton = new Button("Load Level", new Vec2(100, 300), LoadLevelClicked);
//         }

//         private void SaveLevelClicked()
//         {
//             // Prompt for level name (could use a text input or preset name)
//             string levelName = "new_level";
//             LevelEditor.SaveLevel(levelName);
//         }

//         private void LoadLevelClicked()
//         {
//             // Prompt for level name
//             string levelName = "new_level";
//             LevelEditor.LoadLevel(levelName);
//         }

//         public void Update()
//         {
//             saveButton.Update();
//             loadButton.Update();
//         }

//         public void Draw()
//         {
//             saveButton.Draw();
//             loadButton.Draw();
//         }
//     }

// }
