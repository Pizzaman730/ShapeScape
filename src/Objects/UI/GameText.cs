using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using System.Threading.Tasks;

namespace ShapeScape
{
    public class GameText : UIObject
    {
        public string text;
        public double textScale;
        public Color color;
        public GameText(string text, double textScale, Vec2 pos, Color color) : base("Text", pos, new Vec2(), true)
        {
            this.textScale = textScale;
            this.color = color;
            this.text = text;
            UpdateTextLocation();
        }
        public void UpdateTextLocation()
        {
            Vec2 oldSize = size;
            Vector2 sizeBad = AssetManager.font.MeasureString(text);
            size = new Vec2(sizeBad.X, sizeBad.Y) * textScale;
            Move(oldSize/2 - size/2);
        }
    }
}