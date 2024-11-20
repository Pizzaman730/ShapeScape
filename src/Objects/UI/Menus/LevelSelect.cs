using System.Collections.Generic;
using System.IO;

namespace ShapeScape {
    public class LevelSelectionPopup : IMenu
    {
        private List<Button> levelButtons = new List<Button>();

        public void Show()
        {
            var files = Directory.GetFiles("Levels", "*.json");

            foreach (var file in files)
            {
                string levelName = Path.GetFileNameWithoutExtension(file);
                // var button = new Button(levelName, new Vec2(0, 0), new Vec2(150, 50));
                // button.OnClick += () => LoadLevel(levelName); 
                // levelButtons.Add(button);
            }

            foreach (var button in levelButtons)
            {
                UIManager.AddObject(button); 
            }
        }

        public void Hide()
        {
            foreach (var button in levelButtons)
            {
                UIManager.RemoveObject(button);
            }
        }

        public void Update()
        {
            
        }
    }
}
