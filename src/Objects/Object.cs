using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ShapeScape
{
    public class Object
    {
        public Dictionary<string, Animation> animations = new();
        public string name { get; private set; }
        public ObjectTexture objectTexture;
        public Vec2 corner { get; private set; }
        public bool flipTexture = false;
        public List<string> tags = new();
        public Vec2 position 
        { 
            get 
            {
                return corner + size/2;
            }
            private set
            {
                corner = value - size/2;
            } 
        }
        
        public Vec2 size; //{ get; private set; }

        public Rectangle Hitbox
        {
            get
            {
                return new Rectangle(
                    (int)(position.x - size.x / 2), 
                    (int)(position.y - size.y / 2), 
                    (int)size.x, 
                    (int)size.y);
            }
        }

        public Object()
        {
            ConstructObject("Unnamed Object", new Vec2());
        }
        public Object(string name)
        {
            ConstructObject(name, new Vec2());
        }
        public Object(string name, Vec2 pos)
        {
            ConstructObject(name, pos);
        }
        public Object(string name, Vec2 pos, Vec2 size)
        {
            ConstructObject(name, pos, size);
        }

        private void ConstructObject(string name, Vec2 pos)
        {
            ConstructObject(name, pos, new Vec2(50, 50));
        }
        private void ConstructObject(string name, Vec2 pos, Vec2 size)
        {
            ObjectManager.AddObject(this);
            objectTexture = AssetManager.GetObjectTexture(name);
            this.name = name;
            this.size = size;
            this.position = pos;
        }
        public virtual void Update()
        {

        }
        public void SetPos(Vec2 pos)
        {
            position = pos;
        }
        public void Move(Vec2 movement)
        {
            position += movement;
        }
        public bool TouchesPoint(Vec2 point)
        {
            bool collideX = position.x + (size.x/2) > point.x && position.x - size.x/2 < point.x;
            bool collideY = position.y + (size.y/2) > point.y && position.y - size.y/2 < point.y;
            bool collide = collideX && collideY;
            return collide;
        }
        public Animation CreateAnimation(string animationName)
        {
            Animation animation = AssetManager.GetAnimation(animationName);
            AnimationManager.animations.Add(animation);
            animation.obj = objectTexture;
            return animation;
        }
    }
}