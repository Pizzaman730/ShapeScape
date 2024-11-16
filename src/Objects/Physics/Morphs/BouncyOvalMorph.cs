using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShapeScape
{
    public class BouncyOvalMorph : IMorph
    {
        public void ApplyMorph(Player player)
        {
            Vec2 oldPos = player.position;
            Vec2 oldVelocity = player.velocity;
            player.velocity.x = 1;
            player.size = new Vec2(100, 50);
            
            foreach (PhysicsObject obj in PhysicsManager.physicsObjects)
            {
                if (player != obj && PhysicsManager.CollisionCheck(player, obj, true))
                {
                    foreach (PhysicsObject obj2 in PhysicsManager.physicsObjects)
                    {
                        if (player != obj2 && PhysicsManager.CollisionCheck(player, obj2, true, false))
                        {
                            RevertMorph(player);
                            player.velocity = oldVelocity;
                            return;
                        }
                    }
                }
            }
            player.velocity = oldVelocity;
            
            player.jumpHeight = 20;
            player.size = new Vec2(100, 50);
            player.turnLeftAnim = player.CreateAnimation("BouncyOvalTurnLeft");
            player.turnRightAnim = player.CreateAnimation("BouncyOvalTurnRight");
            player.objectTexture.GetTexturesAsDictionary()["Body"].SetTexture(AssetManager.GetTexture("BouncyOvalMorph"));
            player.objectTexture.GetTexturesAsDictionary()["Face"].SetTexture(AssetManager.GetTexture("BouncyOvalFaceRight"));
        }

        public void RevertMorph(Player player)
        {
            player.jumpHeight = 15;
            player.size = new Vec2(50, 50);
            player.objectTexture.GetTexturesAsDictionary()["Body"].SetTexture(AssetManager.GetTexture("PlayerBody"));
            player.objectTexture.GetTexturesAsDictionary()["Face"].SetTexture(AssetManager.GetTexture("PlayerFaceRight"));
            player.turnLeftAnim = player.CreateAnimation("PlayerTurnLeft");
            player.turnRightAnim = player.CreateAnimation("PlayerTurnRight");
        }
    }
}