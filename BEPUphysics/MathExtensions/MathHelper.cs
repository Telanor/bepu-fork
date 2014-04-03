using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BEPUphysics.MathExtensions
{
    /// <summary>
    /// Contains helper values and methods for math.
    /// </summary>
    public static class MathHelper
    {
        /// <summary>
        /// Approximate value of Pi.
        /// </summary>
        public const float Pi = 3.141592653589793239f;

        /// <summary>
        /// Approximate value of Pi multiplied by two.
        /// </summary>
        public const float TwoPi = 6.283185307179586477f;

        /// <summary>
        /// Approximate value of Pi divided by two.
        /// </summary>
        public const float PiOver2 = 1.570796326794896619f;

        /// <summary>
        /// Approximate value of Pi divided by four.
        /// </summary>
        public const float PiOver4 = 0.785398163397448310f;

        /// <summary>
        /// Reduces the angle into a range from -Pi to Pi.
        /// </summary>
        /// <param name="angle">Angle to wrap.</param>
        /// <returns>Wrapped angle.</returns>
        public static float WrapAngle(float angle)
        {
            angle = (float)Math.IEEERemainder(angle, TwoPi);
            if (angle < -Pi)
            {
                angle += TwoPi;
                return angle;
            }
            if (angle >= Pi)
            {
                angle -= TwoPi;
            }
            return angle;

        }

        /// <summary>
        /// Clamps a value to between the given minimum and maximum.
        /// </summary>
        /// <param name="value">Value to clamp.</param>
        /// <param name="minimum">Minimum allowed value.</param>
        /// <param name="maximum">Maximum allowed value.</param>
        /// <returns>Clamped value.</returns>
        public static float Clamp(float value, float minimum, float maximum)
        {
            if (value > maximum)
                value = maximum;
            else if (value < minimum)
                value = minimum;
            return value;

        }
    }
}
