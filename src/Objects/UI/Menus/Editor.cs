 namespace ShapeScape {
     public class UIMenuEditor : IMenu
     {
         private SaveButton saveButton;
         private LoadButton loadButton;
         private BackButton backButton;

         public void Show()
         {
            backButton = new BackButton(new Vec2(50, -50));
            saveButton = new SaveButton(new Vec2(150, -25));
            loadButton = new LoadButton(new Vec2(-50, -25));
         }


         public void Hide()
         {
            UIManager.RemoveObject(backButton);
            UIManager.RemoveObject(saveButton);
            UIManager.RemoveObject(loadButton);
         }
         public void Update()
         {
            
         }
     }
}
