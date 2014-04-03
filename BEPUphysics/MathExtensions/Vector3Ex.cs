using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpDX;

namespace BEPUphysics.MathExtensions
{
    /// <summary>
    /// Contains helper methods for use with Vector3 objects.
    /// </summary>
    public static class Vector3Ex
    {
        //These methods are designed to match the XNA-style usage throughout the engine.
        
        /// <summary>
        /// Computes the dot product of two vectors.
        /// </summary>
        /// <param name="a">First vector in the product.</param>
        /// <param name="b">Second vector in the product.</param>
        /// <param name="dot">Result of the dot product.</param>
        public static void Dot(ref Vector3 a, ref Vector3 b, out float dot)
        {
            dot = a.X * b.X + a.Y * b.Y + a.Z * b.Z;
        }

        /// <summary>
        /// Computes the squared distance between two vectors.
        /// </summary>
        /// <param name="a">Starting location of the distance test.</param>
        /// <param name="b">Ending location of the distance test.</param>
        /// <param name="distanceSquared">Squared distance between the points.</param>
        public static void DistanceSquared(ref Vector3 a, ref Vector3 b, out float distanceSquared)
        {
            Vector3 offset; 
            Vector3.Subtract(ref a, ref b, out offset);
            distanceSquared = offset.LengthSquared();
        }

        /// <summary>
        /// Computes the distance between two vectors.
        /// </summary>
        /// <param name="a">Starting location of the distance test.</param>
        /// <param name="b">Ending location of the distance test.</param>
        /// <param name="distance">Distance between the points.</param>
        public static void Distance(ref Vector3 a, ref Vector3 b, out float distance)
        {
            Vector3 offset;
            Vector3.Subtract(ref a, ref b, out offset);
            distance = offset.Length();
        }

        /// <summary>
        /// Transforms the vector by a 4x4 matrix.
        /// </summary>
        /// <param name="v">Vector to transform.</param>
        /// <param name="transform">Transformation to apply.</param>
        /// <param name="result">Result of the transformation.</param>
        public static void Transform(ref Vector3 v, ref Matrix transform, out Vector3 result)
        {
            Vector4 temp;
            Vector3.Transform(ref v, ref transform, out temp);
            result.X = temp.X;
            result.Y = temp.Y;
            result.Z = temp.Z;

        }

        /// <summary>
        /// Transforms the vector by a 4x4 matrix.
        /// </summary>
        /// <param name="v">Vector to transform.</param>
        /// <param name="transform">Transformation to apply.</param>
        /// <returns>Result of the transformation.</returns>
        public static Vector3 Transform(Vector3 v, Matrix transform)
        {
            Vector4 temp;
            Vector3.Transform(ref v, ref transform, out temp);
            Vector3 result;
            result.X = temp.X;
            result.Y = temp.Y;
            result.Z = temp.Z;
            return result;

        }


    }
}
