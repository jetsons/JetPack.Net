using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jetsons.JetPack {
	public static class NumberLongs {
		
		/// <summary>
		/// Snaps the given value to the given step value.
		/// </summary>
		public static long Snap(this long value, long step) {
			return (long)Math.Round((double)value / (double)step) * step;
		}

		/// <summary>
		/// Limits the given value to the given range.
		/// The min/max value can be flipped and the result will still be correct.
		/// </summary>
		public static long Limit(this long value, long min, long max) {
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
		public static long Min(this long value, long otherValue) {
			return value < otherValue ? value : otherValue;
		}

		/// <summary>
		/// Returns the larger value between this number and the other number.
		/// </summary>
		public static long Max(this long value, long otherValue) {
			return value > otherValue ? value : otherValue;
		}

		/// <summary>
		/// Ensures that the number is at least the given value.
		/// </summary>
		public static long AtLeast(this long value, long minValue) {
			return value < minValue ? minValue : value;
		}

		/// <summary>
		/// Ensures that the number is at most the given value.
		/// </summary>
		public static long AtMost(this long value, long maxValue) {
			return value > maxValue ? maxValue : value;
		}

		/// <summary>
		/// Converts this decimal value to hexadecimal, with the given prefix.
		/// </summary>
		public static string DecimalToHex(this long value, string prefix = "0x") {
			return prefix + value.ToString("X");
		}

		private static readonly string[] SizeSuffixes = { "bytes", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };

		/// <summary>
		/// Print the byte value in a human-readable form. Eg "25.1 MB" or "53.2 KB".
		/// 
		/// Original implementation from stackoverflow.
		/// 
		/// @url	https://stackoverflow.com/a/14488941/9009598
		/// @author	JLRishe
		/// </summary>
		public static string BytesToString(this long value, int decimalPlaces = 1) {
			if (value < 0) { return "-" + BytesToString(-value); }
			int i = 0;
			decimal dValue = (decimal)value;
			while (Math.Round(dValue, decimalPlaces) >= 1000) {
				dValue /= 1024;
				i++;
			}
			return string.Format("{0:n" + decimalPlaces + "} {1}", dValue, SizeSuffixes[i]);
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
		public static long Map(this long value, long inputMin, long inputMax, long outputMin, long outputMax, bool flip = false, bool restrict = true) {

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

			long typedResult = (long)Math.Round(result);

			// force the output value to fit within the output range
			if (restrict) {
				typedResult = typedResult.Limit(outputMin, outputMax);
			}

			return typedResult;
		}

	}
}
