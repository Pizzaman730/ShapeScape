using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace ShapeScape
{
    /// <summary>
    /// A struct representing a set of keys for a player's input.
    /// </summary>
    public struct InputProfile
    {
        /// <summary>
        /// The keys used for moving the player up.
        /// </summary>
        public Keys[] Up { get; }

        /// <summary>
        /// The keys used for moving the player right.
        /// </summary>
        public Keys[] Right { get; }

        /// <summary>
        /// The keys used for moving the player down.
        /// </summary>
        public Keys[] Down { get; }

        /// <summary>
        /// The keys used for moving the player left.
        /// </summary>
        public Keys[] Left { get; }

        /// <summary>
        /// The key used for switching to the player morph.
        /// </summary>
        public Keys PlayerMorphButton { get; set; } = Keys.D1;

        /// <summary>
        /// The key used for switching to the circle morph.
        /// </summary>
        public Keys CircleMorphButton { get; set; } = Keys.D2;

        /// <summary>
        /// The key used for switching to the oval morph.
        /// </summary>
        public Keys OvalMorphButton { get; set; } = Keys.D3;

        /// <summary>
        /// The key used for switching to the trapezoid morph.
        /// </summary>
        public Keys TrapezoidMorphButton { get; set; } = Keys.D4;

        /// <summary>
        /// The key used for switching to the triangle morph.
        /// </summary>
        public Keys TriangleMorphButton { get; set; } = Keys.D5;

        /// <summary>
        /// Creates a new InputProfile with the given up, right, down, and left keys.
        /// </summary>
        /// <param name="up">The keys used for moving the player up.</param>
        /// <param name="right">The keys used for moving the player right.</param>
        /// <param name="down">The keys used for moving the player down.</param>
        /// <param name="left">The keys used for moving the player left.</param>
        public InputProfile(Keys[] up, Keys[] right, Keys[] down, Keys[] left)
        {
            Up = up;
            Right = right;
            Down = down;
            Left = left;
        }

        /// <summary>
        /// Creates a new InputProfile with the default keys.
        /// </summary>
        public InputProfile()
        {
            Up = [Keys.Up];
            Right = [Keys.Right];
            Down = [Keys.Down];
            Left = [Keys.Left];
        }
    }
}

