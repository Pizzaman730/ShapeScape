using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShapeScape
{
    public class PlayerMorph : IMorph
    {
        public void ApplyMorph(Player player)
        {
            player.jumpHeight = 15;
            player.size = new Vec2(50, 50);
            player.turnLeftAnim = player.CreateAnimation("PlayerTurnLeft");
            player.turnRightAnim = player.CreateAnimation("PlayerTurnRight");
            player.objectTexture.GetTexturesAsDictionary()["Body"].SetTexture(AssetManager.GetTexture("PlayerBody"));
        }

        public void RevertMorph(Player player)
        {
        }
    }
}