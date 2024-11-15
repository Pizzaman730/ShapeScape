using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShapeScape
{
    public struct CollisionInformation
    {
        public Side side;
        public bool firstUpdate;
        public PhysicsObject obj;
        public CollisionInformation(PhysicsObject obj, Side side, bool firstUpdate)
        {
            this.obj = obj;
            this.side = side;
            this.firstUpdate = firstUpdate;
        }
    }
}