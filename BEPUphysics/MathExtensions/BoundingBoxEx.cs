using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpDX;

namespace BEPUphysics.MathExtensions
{
    /// <summary>
    /// Contains helper methods for use with BoundingBox objects.
    /// </summary>
    public static class BoundingBoxEx
    {
        /// <summary>
        /// Merges two bounding boxes into one.
        /// </summary>
        /// <param name="a">First bounding box to merge.</param>
        /// <param name="b">Second bounding box to merge.</param>
        /// <param name="merged">Merged bounding box containing both constituent bounding boxes.</param>
        public static void Merge(ref BoundingBox a, ref BoundingBox b, out BoundingBox merged)
        {
            Vector3.Min(ref a.Minimum, ref b.Minimum, out merged.Minimum);
            Vector3.Max(ref a.Maximum, ref b.Maximum, out merged.Maximum);

        }

        /// <summary>
        /// Tests to see if two bounding boxes are intersecting.
        /// </summary>
        /// <param name="a">"This" bounding box.</param>
        /// <param name="b">Other bounding box.</param>
        /// <param name="intersecting">Whether or not the two bounding boxes intersect.</param>
        public static void Intersects(this BoundingBox a, ref BoundingBox b, out bool intersecting)
        {
            intersecting = a.Maximum.X >= b.Minimum.X && a.Minimum.X <= b.Maximum.X && 
                           a.Maximum.Y >= b.Minimum.Y && a.Minimum.Y <= b.Maximum.Y && 
                           a.Maximum.Z >= b.Minimum.Z && a.Minimum.Z <= b.Maximum.Z;
        }

    }
}
