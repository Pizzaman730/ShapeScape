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
            Vector2 sizeBad = AssetManager.font.MeasureString(text);
            size = new Vec2(sizeBad.X, sizeBad.Y) * textScale;
            SetPos(position - size/2);
            this.text = text;
        }
    }
}