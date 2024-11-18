using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShapeScape
{
    public class TrapezoidMorph : IMorph
    {
        public void ApplyMorph(Player player)
        {
            player.jumpHeight = 5;
            player.maxVelocity.x = 20;
            player.size = new Vec2(50, 25);
            player.turnLeftAnim = player.CreateAnimation("TrapezoidEnemyTurnLeft");
            player.turnRightAnim = player.CreateAnimation("TrapezoidEnemyTurnRight");
            player.objectTexture.GetTexturesAsDictionary()["Body"].SetTexture(AssetManager.GetTexture("TrapezoidMorph"));
            player.objectTexture.GetTexturesAsDictionary()["Face"].SetTexture(AssetManager.GetTexture("TrapezoidEnemyFaceRight"));
        }

        public void RevertMorph(Player player)
        {
            player.jumpHeight = 15;
            player.size = new Vec2(50, 50);
            player.maxVelocity.x = 10;
            player.objectTexture.GetTexturesAsDictionary()["Body"].SetTexture(AssetManager.GetTexture("PlayerBody"));
            player.objectTexture.GetTexturesAsDictionary()["Face"].SetTexture(AssetManager.GetTexture("PlayerFaceRight"));
            player.turnLeftAnim = player.CreateAnimation("PlayerTurnLeft");
            player.turnRightAnim = player.CreateAnimation("PlayerTurnRight");
        }
    }
}