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
            player.objectTexture.GetTexturesAsDictionary()["Body"].SetTexture(AssetManager.GetTexture("PlayerBody"));
        }

        public void RevertMorph(Player player)
        {
        }
    }
}