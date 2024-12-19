using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace ShapeScape
{
    /// <summary>
    /// A 2D vector
    /// </summary>
    public struct Vec2
    {
        /// <summary>
        /// The x component of the vector
        /// </summary>
        public double x { get; set; }
        /// <summary>
        /// The y component of the vector
        /// </summary>
        public double y { get; set; } 
        /// <summary>
        /// Creates a new vector with both components set to 0
        /// </summary>
        public Vec2()
        {
            x = 0;
            y = 0;
        }
        /// <summary>
        /// Creates a new vector with both components set to the given value
        /// </summary>
        /// <param name="num">The value to which both components are set</param>
        public Vec2(double num)
        {
            x = num;
            y = num;
        }
        /// <summary>
        /// Creates a new vector with the given x and y values
        /// </summary>
        /// <param name="x">The x component of the vector</param>
        /// <param name="y">The y component of the vector</param>
        public Vec2(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        /// <summary>
        /// A string representation of the vector
        /// </summary>
        /// <returns>A string in the format {x, y}</returns>
        public override string ToString()
        {
            return "{" + x + ", " + y + "}";
        }
        /// <summary>
        /// Returns a new vector with the absolute value of each component
        /// </summary>
        /// <param name="vec">The vector to be modified</param>
        /// <returns>A vector with the absolute value of each component</returns>
        public static Vec2 Abs(Vec2 vec)
        {
            return new Vec2(Math.Abs(vec.x), Math.Abs(vec.y));
        }
        /// <summary>
        /// Returns a new vector with the absolute value of each component
        /// </summary>
        /// <returns>A vector with the absolute value of each component</returns>
        public Vec2 Abs()
        {
            return new Vec2(Math.Abs(x), Math.Abs(y));
        }
        /// <summary>
        /// Returns a new vector with each component clamped to the range [min, max]
        /// </summary>
        /// <param name="value">The vector to be clamped</param>
        /// <param name="min">The minimum value to which the vector is clamped</param>
        /// <param name="max">The maximum value to which the vector is clamped</param>
        /// <returns>A vector with each component clamped to the range [min, max]</returns>
        public static Vec2 Clamp(Vec2 value, Vec2 min, Vec2 max)
        {
            return new Vec2(Math.Clamp(value.x, min.x, max.x), Math.Clamp(value.y, min.y, max.y));
        }
        /// <summary>
        /// Converts the vector to an XNA Vector2
        /// </summary>
        /// <param name="vec">The vector to be converted</param>
        /// <returns>A Vector2 with the same components as the given Vec2</returns>
        public static implicit operator Vector2(Vec2 vec)
        {
            return new Vector2((float)vec.x, (float)vec.y);
        }
        /// <summary>
        /// Adds two vectors componentwise
        /// </summary>
        /// <param name="v1">The first vector</param>
        /// <param name="v2">The second vector</param>
        /// <returns>The sum of the two vectors</returns>
        public static Vec2 operator +(Vec2 v1, Vec2 v2)
        {
            return new Vec2(v1.x + v2.x, v1.y + v2.y);
        }
        /// <summary>
        /// Adds a scalar to a vector componentwise
        /// </summary>
        /// <param name="v">The vector</param>
        /// <param name="num">The scalar</param>
        /// <returns>The sum of the vector and the scalar</returns>
        public static Vec2 operator +(Vec2 v, double num)
        {
            return new Vec2(v.x + num, v.y + num);
        }
        /// <summary>
        /// Subtracts two vectors componentwise
        /// </summary>
        /// <param name="v1">The first vector</param>
        /// <param name="v2">The second vector</param>
        /// <returns>The difference of the two vectors</returns>
        public static Vec2 operator -(Vec2 v1, Vec2 v2)
        {
            return new Vec2(v1.x - v2.x, v1.y - v2.y);
        }
        /// <summary>
        /// Subtracts a scalar from a vector componentwise
        /// </summary>
        /// <param name="v">The vector</param>
        /// <param name="num">The scalar</param>
        /// <returns>The difference of the vector and the scalar</returns>
        public static Vec2 operator -(Vec2 v, double num)
        {
            return new Vec2(v.x - num, v.y - num);
        }
        /// <summary>
        /// Divides two vectors componentwise
        /// </summary>
        /// <param name="v1">The first vector</param>
        /// <param name="v2">The second vector</param>
        /// <returns>The quotient of the two vectors</returns>
        public static Vec2 operator /(Vec2 v1, Vec2 v2)
        {
            return new Vec2(v1.x / v2.x, v1.y / v2.y);
        }
        /// <summary>
        /// Divides a vector componentwise by a scalar
        /// </summary>
        /// <param name="v">The vector</param>
        /// <param name="num">The scalar</param>
        /// <returns>The quotient of the vector and the scalar</returns>
        public static Vec2 operator /(Vec2 v, double num)
        {
            return new Vec2(v.x / num, v.y / num);
        }
        /// <summary>
        /// Multiplies two vectors componentwise
        /// </summary>
        /// <param name="v1">The first vector</param>
        /// <param name="v2">The second vector</param>
        /// <returns>The product of the two vectors</returns>
        public static Vec2 operator *(Vec2 v1, Vec2 v2)
        {
            return new Vec2(v1.x * v2.x, v1.y * v2.y);
        }
        /// <summary>
        /// Multiplies a vector componentwise by a scalar
        /// </summary>
        /// <param name="v">The vector</param>
        /// <param name="num">The scalar</param>
        /// <returns>The product of the vector and the scalar</returns>
        public static Vec2 operator *(Vec2 v, double num)
        {
            return new Vec2(v.x * num, v.y * num);
        }
    }
}
