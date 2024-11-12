using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace SquarePlatformer
{
    public struct Vec2
    {
        public double x { get; set; }
        public double y { get; set; } 
        public Vec2()
        {
            x = 0;
            y = 0;
        }
        public Vec2(double num)
        {
            x = num;
            y = num;
        }
        public Vec2(double x, double y)
        {
            this.x = x;
            this.y = y;
        }
        public override string ToString()
        {
            return "{" + x + ", " + y + "}";
        }
        public static Vec2 Clamp(Vec2 value, Vec2 min, Vec2 max)
        {
            return new Vec2(Math.Clamp(value.x, min.x, max.x), Math.Clamp(value.y, min.y, max.y));
        }
        public static implicit operator Vector2(Vec2 vec)
        {
            return new Vector2((float)vec.x, (float)vec.y);
        }
        public static Vec2 operator +(Vec2 v1, Vec2 v2)
        {
            return new Vec2(v1.x + v2.x, v1.y + v2.y);
        }
        public static Vec2 operator +(Vec2 v, double num)
        {
            return new Vec2(v.x + num, v.y + num);
        }
        public static Vec2 operator -(Vec2 v1, Vec2 v2)
        {
            return new Vec2(v1.x - v2.x, v1.y - v2.y);
        }
        public static Vec2 operator -(Vec2 v, double num)
        {
            return new Vec2(v.x - num, v.y - num);
        }
        public static Vec2 operator /(Vec2 v1, Vec2 v2)
        {
            return new Vec2(v1.x / v2.x, v1.y / v2.y);
        }
        public static Vec2 operator /(Vec2 v, double num)
        {
            return new Vec2(v.x / num, v.y / num);
        }
        public static Vec2 operator *(Vec2 v1, Vec2 v2)
        {
            return new Vec2(v1.x * v2.x, v1.y * v2.y);
        }
        public static Vec2 operator *(Vec2 v, double num)
        {
            return new Vec2(v.x * num, v.y * num);
        }
    }
}