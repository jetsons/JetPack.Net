using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jetsons.JetPack {
	public static class NumberUInts {
		
		/// <summary>
		/// Snaps the given value to the given step value.
		/// </summary>
		public static uint Snap(this uint value, uint step) {
			return (uint)Math.Round((double)value / (double)step) * step;
		}

		/// <summary>
		/// Limits the given value to the given range.
		/// The min/max value can be flipped and the result will still be correct.
		/// </summary>
		public static uint Limit(this uint value, uint min, uint max) {
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
		public static uint Min(this uint value, uint otherValue) {
			return value < otherValue ? value : otherValue;
		}

		/// <summary>
		/// Returns the larger value between this number and the other number.
		/// </summary>
		public static uint Max(this uint value, uint otherValue) {
			return value > otherValue ? value : otherValue;
		}

		/// <summary>
		/// Ensures that the number is at least the given value.
		/// </summary>
		public static uint AtLeast(this uint value, uint minValue) {
			return value < minValue ? minValue : value;
		}

		/// <summary>
		/// Ensures that the number is at most the given value.
		/// </summary>
		public static uint AtMost(this uint value, uint maxValue) {
			return value > maxValue ? maxValue : value;
		}

		/// <summary>
		/// Converts this decimal value to hexadecimal, with the given prefix.
		/// </summary>
		public static string DecimalToHex(this uint value, string prefix = "0x") {
			return prefix + value.ToString("X");
		}

		/// <summary>
		/// Print the byte value in a human-readable form. Eg "25.1 MB" or "53.2 KB".
		/// </summary>
		public static string BytesToString(this uint value, int decimalPlaces = 1) {
			return NumberLongs.BytesToString((long)value, decimalPlaces);
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
		public static uint Map(this uint value, uint inputMin, uint inputMax, uint outputMin, uint outputMax, bool flip = false, bool restrict = true) {

			// force the input value to fit within the input range
			value = value.Limit(inputMin, inputMax);

			double result;

			// translate the input value based on the input range
			if (inputMax > inputMin) {
				result = (double)value / (double)(inputMax - inputMin);
			}
			else {
				result = (double)value / (double)(inputMin - inputMax);
			}

			// inverse the output value
			if (flip) {
				result = 1 - result;
			}

			// translate the value to the output range
			if (outputMax > outputMin) {
				result = (result * (double)(outputMax - outputMin)) + outputMin;
			}
			else {
				result = (result * (double)(outputMin - outputMax)) + outputMin;
			}

			uint typedResult = (uint)Math.Round(result);

			// force the output value to fit within the output range
			if (restrict) {
				typedResult = typedResult.Limit(outputMin, outputMax);
			}

			return typedResult;
		}

	}
}
