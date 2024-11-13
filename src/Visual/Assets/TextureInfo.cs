using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace SquarePlatformer
{
    public class TextureInfo
    {
        public string name { get; set; }
        public string textureName { get; set; }
        public Texture2D texture { get; private set; }
        public Vec2 size { get; set; }
        public Vec2 offset { get; set; }
        public double rotationOffset { get; set; }
        public bool enabled { get; set; }
        public void UpdateTexture()
        {
            texture = AssetManager.GetTexture(textureName);
        }
        public void SetTexture(Texture2D texture)
        {
            this.texture = texture;
        }
        public TextureInfo Copy()
        {
            return new TextureInfo() {
                name = this.name,
                textureName = this.textureName,
                texture = this.texture,
                size = this.size,
                offset = this.offset,
                rotationOffset = this.rotationOffset,
                enabled = this.enabled
            };
        }
    }
}