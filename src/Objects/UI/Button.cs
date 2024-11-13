using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SquarePlatformer
{
    public class Button : UIObject
    {
        public Button(string name, Vec2 pos, Vec2 size) : base(name, pos, size, true)
        {
            tags.Add("Button");
        }
        public virtual void OnButtonClick()
        {

        }
        public override void UpdateUI()
        {
            if (InputManager.ClickThisFrame() && TouchesPoint(InputManager.MousePosWorld()))
            {
                OnButtonClick();
            }
        }
    }
}