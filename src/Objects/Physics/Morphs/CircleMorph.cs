using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShapeScape
{
    public class CircleMorph : IMorph
    {
        public void ApplyMorph(Player player)
        {
            player.objectTexture.GetTexturesAsDictionary()["Body"].SetTexture(AssetManager.GetTexture("CircleMorph"));
        }

        public void RevertMorph(Player player)
        {
            player.objectTexture.GetTexturesAsDictionary()["Body"].SetTexture(AssetManager.GetTexture("PlayerBody"));
        }
    }
}