using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpDX;

namespace BEPUphysics.MathExtensions
{
    /// <summary>
    /// Contains helper methods for Quaternion objects.
    /// </summary>
    public static class QuaternionEx
    {
        /// <summary>
        /// Concatenates two quaternions together.  The result is rotation a, followed by rotation b.
        /// This matches the order of XNA's concatenate.
        /// </summary>
        /// <param name="a">First rotation to apply.</param>
        /// <param name="b">Second rotation to apply.</param>
        /// <param name="result">Resulting orientation.</param>
        public static void Concatenate(ref Quaternion a, ref Quaternion b, out Quaternion result)
        {
            Quaternion.Multiply(ref b, ref a, out result);
        }

        /// <summary>
        /// Concatenates two quaternions together.  The result is rotation a, followed by rotation b.
        /// This matches the order of XNA's concatenate.
        /// </summary>
        /// <param name="a">First rotation to apply.</param>
        /// <param name="b">Second rotation to apply.</param>
        /// <returns>Resulting orientation.</returns>
        public static Quaternion Concatenate(Quaternion a, Quaternion b)
        {
            Quaternion result;
            Quaternion.Multiply(ref b, ref a, out result);
            return result;
        }

        /// <summary>
        /// Multiplies two quaternions together using XNA ordering.
        /// </summary>
        /// <param name="a">First quaternion to multiply.</param>
        /// <param name="b">Second quaternion to multiply.</param>
        /// <param name="result">Resulting orientation.</param>
        public static void Multiply(ref Quaternion a, ref Quaternion b, out Quaternion result)
        {
            Quaternion.Multiply(ref a, ref b, out result);
        }

        /// <summary>
        /// Multiplies two quaternions together using XNA ordering.
        /// </summary>
        /// <param name="a">First quaternion to multiply.</param>
        /// <param name="b">Second quaternion to multiply.</param>
        /// <returns>Resulting orientation.</returns>
        public static Quaternion Multiply(Quaternion a, Quaternion b)
        {
            Quaternion result;
            Quaternion.Multiply(ref a, ref b, out result);
            return result;
        }

    }
}
