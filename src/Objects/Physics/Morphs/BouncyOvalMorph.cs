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