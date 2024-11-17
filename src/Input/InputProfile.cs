using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace ShapeScape
{
    public struct InputProfile
    {
        public Keys[] up;
        public Keys[] left;
        public Keys[] right;
        public Keys[] down;
        public Keys playerMorphButton = Keys.D1;
        public Keys circleMorphButton = Keys.D2;
        public Keys ovalMorphButton = Keys.D3;

        public InputProfile(Keys[] up, Keys[] right, Keys[] down, Keys[] left)
        {
            this.up = up;
            this.right = right;
            this.down = down;
            this.left = left;
        }
    }
}