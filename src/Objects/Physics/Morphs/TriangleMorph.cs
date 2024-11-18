using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShapeScape
{
    public class TriangleMorph : IMorph
    {
        public void ApplyMorph(Player player)
        {
            player.jumpHeight = 10;
            player.turnLeftAnim = player.CreateAnimation("TriangleMorphTurnLeft");
            player.turnRightAnim = player.CreateAnimation("TriangleMorphTurnRight");
            player.objectTexture.GetTexturesAsDictionary()["Body"].SetTexture(AssetManager.GetTexture("TriangleMorph"));
            player.objectTexture.GetTexturesAsDictionary()["Face"].SetTexture(AssetManager.GetTexture("TriangleMorphFaceRight"));
        }

        public void RevertMorph(Player player)
        {
            player.jumpHeight = 15;
            player.maxVelocity.x = 10;
            player.objectTexture.GetTexturesAsDictionary()["Body"].SetTexture(AssetManager.GetTexture("PlayerBody"));
            player.objectTexture.GetTexturesAsDictionary()["Face"].SetTexture(AssetManager.GetTexture("PlayerFaceRight"));
            player.turnLeftAnim = player.CreateAnimation("PlayerTurnLeft");
            player.turnRightAnim = player.CreateAnimation("PlayerTurnRight");
        }
    }
}