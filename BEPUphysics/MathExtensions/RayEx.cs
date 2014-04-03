using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpDX;

namespace BEPUphysics.MathExtensions
{
    /// <summary>
    /// Contains helper methods for Ray objects.
    /// </summary>
    public static class RayEx
    {
        /// <summary>
        /// Determines the distance to the intersection between the ray and the box, if any.
        /// </summary>
        /// <param name="ray">Ray to cast.</param>
        /// <param name="box">Box to test.</param>
        /// <param name="t">Distance along the ray's direction to the intersection.  Null if no hit.</param>
        public static void Intersects(this Ray ray, ref BoundingBox box, out float? t)
        {
            float distance;
            if (ray.Intersects(ref box, out distance))
            {
                t = distance;
            }
            else
                t = null;

        }
    }
}
