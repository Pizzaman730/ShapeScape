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
        public Vec2 size; //{ get; private set; }

        public Vec2 position 
        { 
            get 
            {
                return corner + size / 2;
            }
            private set
            {
                corner = value - size / 2;
                Logger.Debug($"Position set to {position} (Corner updated to {corner})");
            } 
        }

        public Rectangle Hitbox
        {
            get
            {
                Logger.Debug($"Calculating hitbox for {name}. Position: {position}, Size: {size}");
                return new Rectangle(
                    (int)(position.x - size.x / 2), 
                    (int)(position.y - size.y / 2), 
                    (int)size.x, 
                    (int)size.y);
            }
        }

        public Object()
        {
            Logger.Debug("Creating unnamed Object...");
            ConstructObject("Unnamed Object", new Vec2());
        }

        public Object(string name)
        {
            Logger.Debug($"Creating Object with name: {name}");
            ConstructObject(name, new Vec2());
        }

        public Object(string name, Vec2 pos)
        {
            Logger.Debug($"Creating Object with name: {name}, Position: {pos}");
            ConstructObject(name, pos);
        }

        public Object(string name, Vec2 pos, Vec2 size)
        {
            Logger.Debug($"Creating Object with name: {name}, Position: {pos}, Size: {size}");
            ConstructObject(name, pos, size);
        }

        private void ConstructObject(string name, Vec2 pos)
        {
            Logger.Debug($"Constructing Object: {name}, Position: {pos}, Default Size: (50, 50)");
            ConstructObject(name, pos, new Vec2(50, 50));
        }

        private void ConstructObject(string name, Vec2 pos, Vec2 size)
        {
            Logger.Debug($"Object Constructor called. Adding object to ObjectManager. Name: {name}, Position: {pos}, Size: {size}");
            ObjectManager.AddObject(this);
            objectTexture = AssetManager.GetObjectTexture(name);
            this.name = name;
            this.size = size;
            this.position = pos;

            Logger.Debug($"Object '{name}' constructed with Position: {position}, Size: {size}");
        }

        public virtual void Update()
        {
            Logger.Debug($"Updating Object: {name}");
        }

        public void SetPos(Vec2 pos)
        {
            Logger.Debug($"Setting position for {name} to {pos}");
            position = pos;
        }

        public void Move(Vec2 movement)
        {
            Logger.Debug($"Moving Object: {name} by {movement}. Current position: {position}");
            position += movement;
            Logger.Debug($"New position for {name}: {position}");
        }

        public bool TouchesPoint(Vec2 point)
        {
            Logger.Debug($"Checking if {name} touches point {point}");
            bool collideX = position.x + (size.x / 2) > point.x && position.x - size.x / 2 < point.x;
            bool collideY = position.y + (size.y / 2) > point.y && position.y - size.y / 2 < point.y;
            bool collide = collideX && collideY;

            Logger.Debug($"Touch result for {name}: {collide} (CollideX: {collideX}, CollideY: {collideY})");
            return collide;
        }

        public Animation CreateAnimation(string animationName)
        {
            Logger.Debug($"Creating animation for Object: {name}, Animation: {animationName}");
            Animation animation = AssetManager.GetAnimation(animationName);
            AnimationManager.animations.Add(animation);
            animation.obj = objectTexture;
            Logger.Debug($"Animation {animationName} created and added to AnimationManager for {name}");
            return animation;
        }
    }
}
