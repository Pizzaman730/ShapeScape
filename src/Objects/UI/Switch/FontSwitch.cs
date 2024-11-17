using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShapeScape
{
    public class FontSwitch : UIObject, ISwitch
    {
        private Animation switchOnAnimation;
        private Animation switchOffAnimation;
        public bool switchOn;
        public FontSwitch(Vec2 pos) : base("FontSwitch", pos, new Vec2(50, 25), true)
        {
            switchOnAnimation = CreateAnimation("SwitchOnAnimation");
            switchOffAnimation = CreateAnimation("SwitchOffAnimation");
            if (AssetManager.font == AssetManager.grapeSodaFont)
            {
                switchOn = false;
            }
            else
            {
                switchOn = true;
                objectTexture.textures[0].SetTexture(AssetManager.GetTexture("SwitchGreen"));
            }
        }
        public void ToggleOn()
        {
            switchOn = true;
            AssetManager.font = AssetManager.shoppingCartFont;
            switchOnAnimation.Start();
            UpdateAllText();
        }
        public void ToggleOff()
        {
            switchOn = false;
            AssetManager.font = AssetManager.grapeSodaFont;
            switchOffAnimation.Start();
            UpdateAllText();
        }
        public override void UpdateUI()
        {
            if (InputManager.ClickThisFrame() && TouchesPoint(InputManager.MousePos()))
            {
                if (switchOn)
                {
                    ToggleOff();
                }
                else
                {
                    ToggleOn();
                }
            }
        }
        private void UpdateAllText()
        {
            foreach (UIObject obj in UIManager.uiObjects)
            {
                if (obj.name == "Text")
                {
                    ((GameText)obj).UpdateTextLocation();
                }
            }
        }
    }
}