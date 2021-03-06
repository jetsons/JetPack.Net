﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jetsons.JetPack {
	public static class NumberFloats {

		/// <summary>
		/// Rounds the given float, returning an int.
		/// </summary>
		public static int Round(this float value) {
			return (int)Math.Round(value);
		}

		/// <summary>
		/// Rounds the given float, preserving the given number of digits after the decimal point;
		/// </summary>
		public static float RoundToDigits(this float value, int digits) {
			return (float)Math.Round(value, digits);
		}

		/// <summary>
		/// Ceilings the given float, returning an int.
		/// </summary>
		public static int Ceiling(this float value) {
			return (int)Math.Ceiling(value);
		}

		/// <summary>
		/// Floors the given float, returning an int.
		/// </summary>
		public static int Floor(this float value) {
			return (int)Math.Floor(value);
		}

		/// <summary>
		/// Snaps the given value to the given step value.
		/// </summary>
		public static float Snap(this float value, float step) {
			return (float)Math.Round(value / step) * step;
		}

		/// <summary>
		/// Limits the given value to the given range.
		/// The min/max value can be flipped and the result will still be correct.
		/// </summary>
		public static float Limit(this float value, float min, float max) {
			if (min < max) {
				if (value < min) {
					return min;
				}
				if (value > max) {
					return max;
				}
			}
			else {
				if (value < max) {
					return max;
				}
				if (value > min) {
					return min;
				}
			}
			return value;
		}

		/// <summary>
		/// Returns the smaller value between this number and the other number.
		/// </summary>
		public static float Min(this float value, float otherValue) {
			return value < otherValue ? value : otherValue;
		}

		/// <summary>
		/// Returns the larger value between this number and the other number.
		/// </summary>
		public static float Max(this float value, float otherValue) {
			return value > otherValue ? value : otherValue;
		}

		/// <summary>
		/// Ensures that the number is at least the given value.
		/// </summary>
		public static float AtLeast(this float value, float minValue) {
			return value < minValue ? minValue : value;
		}

		/// <summary>
		/// Ensures that the number is at most the given value.
		/// </summary>
		public static float AtMost(this float value, float maxValue) {
			return value > maxValue ? maxValue : value;
		}

		/// <summary>
		/// Linearly maps the value from the input range to the output range.
		/// Min/max values of the input and output ranges can be flipped and the result will still be correct.
		/// The math is performed on floating point values and always accurate.
		/// </summary>
		/// <param name="value">The input value that may or may not be within the input range. It is forced to fit within the input range.</param>
		/// <param name="inputMin">The start of the input range</param>
		/// <param name="inputMax">The end of the input range</param>
		/// <param name="outputMin">The start of the new output range</param>
		/// <param name="outputMax">The end of the new output range</param>
		/// <param name="flip">Should the output value be inversed?</param>
		/// <param name="restrict">Should the output value be forced to fit within the output range?</param>
		/// <returns></returns>
		public static float Map(this float value, float inputMin, float inputMax, float outputMin, float outputMax, bool flip = false, bool restrict = true) {

			// force the input value to fit within the input range
			value = value.Limit(inputMin, inputMax);

			float result;

			// translate the input value based on the input range
			if (inputMax > inputMin) {
				result = value / (inputMax - inputMin);
			}
			else {
				result = value / (inputMin - inputMax);
			}

			// inverse the output value
			if (flip) {
				result = 1 - result;
			}

			// translate the value to the output range
			if (outputMax > outputMin) {
				result = (result * (outputMax - outputMin)) + outputMin;
			}
			else {
				result = (result * (outputMin - outputMax)) + outputMin;
			}

			// force the output value to fit within the output range
			if (restrict) {
				result = result.Limit(outputMin, outputMax);
			}

			return result;
		}


	}
}
