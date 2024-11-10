using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace SquarePlatformer
{
    public class Player : PhysicsObject
    {
        private bool jumpable = false;
        private bool jumping = false;
        private int jumpHeight = 15;
        private int timeSinceOnFloor = 0;
        public Player(Vec2 pos) : base("Player", pos, new Vec2(50, 50))
        {
            LevelManager.alivePlayers++;
            affectedByGravity = true;
            pushable = true;
            Camera.targets.Add(this);
        }
        public override void Update()
        {
            //Console.WriteLine("Test");
            //Move(new Vec2(1,0));
            if (position.y <= -800)
            {
                //SetPos(new Vec2(position.x, 800));
                Kill();
            }
            if (velocity.x > 0) velocity.x --;
            else if (velocity.x < 0) velocity.x ++;
            UpdateControls();
            timeSinceOnFloor++;
        }
        public void UpdateControls()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.D)) 
            {
                velocity.x += 2;
                if (flipTexture)
                {
                    flipTexture = false;
                }
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A)) 
            {
                velocity.x -= 2;
                if (!flipTexture)
                {
                    flipTexture = true;
                }
            }
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                if (jumpable && timeSinceOnFloor <= 10) 
                {
                    jumpable = false;
                    velocity.y = jumpHeight;
                    jumping = true;
                }
            }
            else if (jumping)
            {
                if (velocity.y > 0) velocity.y -= velocity.y * 0.25 * gravity * weight;
            }
        }
        public override void CollisionEnd(PhysicsObject obj, bool onSide)
        {
            if (obj.name == "Enemy") Kill();
            if (onSide || obj.position.y > position.y) return;
            jumpable = true;
            timeSinceOnFloor = 0;
            jumping = false;
        }
        public void Kill()
        {
            ObjectManager.AddToDestroy(this);
            LevelManager.alivePlayers--;
            Camera.targets.Remove(this);
        }
    }
}