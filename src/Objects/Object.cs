using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ShapeScape
{
    /// <summary>
    /// Represents a basic object in the game.
    /// </summary>
    public class Object
    {
        /// <summary>
        /// Contains all the animations for this object.
        /// </summary>
        public Dictionary<string, Animation> animations = new();

        /// <summary>
        /// The name of this object.
        /// </summary>
        public string name { get; private set; }

        /// <summary>
        /// The texture for this object.
        /// </summary>
        public ObjectTexture objectTexture;

        /// <summary>
        /// The top-left corner of this object.
        /// </summary>
        public Vec2 corner { get; private set; }

        /// <summary>
        /// Whether or not the object's texture should be flipped.
        /// </summary>
        public bool flipTexture = false;

        /// <summary>
        /// A list of tags for this object.
        /// </summary>
        public List<string> tags = new();

        /// <summary>
        /// The size of this object.
        /// </summary>
        public Vec2 size; //{ get; private set; }

        /// <summary>
        /// The position of this object.
        /// </summary>
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

        /// <summary>
        /// Calculates the hitbox for this object.
        /// </summary>
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

        /// <summary>
        /// Creates a new object with the default name "Unnamed Object".
        /// </summary>
        public Object()
        {
            Logger.Debug("Creating unnamed Object...");
            ConstructObject("Unnamed Object", new Vec2());
        }

        /// <summary>
        /// Creates a new object with the given name.
        /// </summary>
        /// <param name="name">The name of the object.</param>
        public Object(string name)
        {
            Logger.Debug($"Creating Object with name: {name}");
            ConstructObject(name, new Vec2());
        }

        /// <summary>
        /// Creates a new object with the given name and position.
        /// </summary>
        /// <param name="name">The name of the object.</param>
        /// <param name="pos">The position of the object.</param>
        public Object(string name, Vec2 pos)
        {
            Logger.Debug($"Creating Object with name: {name}, Position: {pos}");
            ConstructObject(name, pos);
        }

        /// <summary>
        /// Creates a new object with the given name, position, and size.
        /// </summary>
        /// <param name="name">The name of the object.</param>
        /// <param name="pos">The position of the object.</param>
        /// <param name="size">The size of the object.</param>
        public Object(string name, Vec2 pos, Vec2 size)
        {
            Logger.Debug($"Creating Object with name: {name}, Position: {pos}, Size: {size}");
            ConstructObject(name, pos, size);
        }

        /// <summary>
        /// Performs the necessary work to create an object.
        /// </summary>
        /// <param name="name">The name of the object.</param>
        /// <param name="pos">The position of the object.</param>
        private void ConstructObject(string name, Vec2 pos)
        {
            Logger.Debug($"Constructing Object: {name}, Position: {pos}, Default Size: (50, 50)");
            ConstructObject(name, pos, new Vec2(50, 50));
        }

        /// <summary>
        /// Performs the necessary work to create an object.
        /// </summary>
        /// <param name="name">The name of the object.</param>
        /// <param name="pos">The position of the object.</param>
        /// <param name="size">The size of the object.</param>
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

        /// <summary>
        /// Updates the object each frame.
        /// </summary>
        public virtual void Update()
        {
            Logger.Debug($"Updating Object: {name}");
        }

        /// <summary>
        /// Sets the position of the object.
        /// </summary>
        /// <param name="pos">The new position of the object.</param>
        public void SetPos(Vec2 pos)
        {
            Logger.Debug($"Setting position for {name} to {pos}");
            position = pos;
        }

        /// <summary>
        /// Moves the object by the given amount.
        /// </summary>
        /// <param name="movement">The amount to move the object.</param>
        public void Move(Vec2 movement)
        {
            Logger.Debug($"Moving Object: {name} by {movement}. Current position: {position}");
            position += movement;
            Logger.Debug($"New position for {name}: {position}");
        }

        /// <summary>
        /// Checks if the object touches the given point.
        /// </summary>
        /// <param name="point">The point to check against.</param>
        /// <returns>Whether or not the object touches the point.</returns>
        public bool TouchesPoint(Vec2 point)
        {
            Logger.Debug($"Checking if {name} touches point {point}");
            bool collideX = position.x + (size.x / 2) > point.x && position.x - size.x / 2 < point.x;
            bool collideY = position.y + (size.y / 2) > point.y && position.y - size.y / 2 < point.y;
            bool collide = collideX && collideY;

            Logger.Debug($"Touch result for {name}: {collide} (CollideX: {collideX}, CollideY: {collideY})");
            return collide;
        }

        /// <summary>
        /// Creates a new animation for this object.
        /// </summary>
        /// <param name="animationName">The name of the animation to create.</param>
        /// <returns>The animation created.</returns>
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

