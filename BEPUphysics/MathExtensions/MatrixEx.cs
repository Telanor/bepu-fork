using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpDX;

namespace BEPUphysics.MathExtensions
{
    /// <summary>
    /// Contains helper methods for use with Matrix objects.
    /// </summary>
    public static class MatrixEx
    {
        public static Matrix CreateMatrix(float m11, float m12, float m13, float m14,
                                   float m21, float m22, float m23, float m24,
                                   float m31, float m32, float m33, float m34,
                                   float m41, float m42, float m43, float m44)
        {
            return new Matrix()
            {
                M11 = m11,
                M12 = m12,
                M13 = m13,
                M14 = m14,
                M21 = m21,
                M22 = m22,
                M23 = m23,
                M24 = m24,
                M31 = m31,
                M32 = m32,
                M33 = m33,
                M34 = m34,
                M41 = m41,
                M42 = m42,
                M43 = m43,
                M44 = m44
            };
        }

        /// <summary>
        /// Gets the translation component of a transformation.
        /// </summary>
        /// <param name="m">Matrix to extract the translation from.</param>
        /// <returns>Translation component of the matrix.</returns>
        public static Vector3 GetTranslation(this Matrix m)
        {
            return new Vector3(m.M41, m.M42, m.M43);
        }

        /// <summary>
        /// Sets the translation component of a transformation.
        /// </summary>
        /// <param name="m">Matrix to set the translation of.</param>
        /// <param name="v">New translation.</param>
        public static void SetTranslation(ref Matrix m, Vector3 v)
        {
            m.M41 = v.X;
            m.M42 = v.Y;
            m.M43 = v.Z;

        }

    }
}
