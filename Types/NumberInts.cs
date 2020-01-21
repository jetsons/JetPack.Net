using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jetsons.JetPack {
	public static class NumberInts {
		
		/// <summary>
		/// Snaps the given value to the given step value.
		/// </summary>
		public static int Snap(this int value, int step) {
			return (int)Math.Round((double)value / (double)step) * step;
		}

		/// <summary>
		/// Limits the given value to the given range.
		/// </summary>
		public static int Limit(this int value, int min, int max) {
			if (value < min) {
				return min;
			}
			if (value > max) {
				return max;
			}
			return value;
		}
		
		/// <summary>
		/// Returns the smaller value between this number and the other number.
		/// </summary>
		public static int Min(this int value, int otherValue) {
			return value < otherValue ? value : otherValue;
		}

		/// <summary>
		/// Returns the larger value between this number and the other number.
		/// </summary>
		public static int Max(this int value, int otherValue) {
			return value > otherValue ? value : otherValue;
		}

		/// <summary>
		/// Ensures that the number is at least the given value.
		/// </summary>
		public static int AtLeast(this int value, int minValue) {
			return value < minValue ? minValue : value;
		}

		/// <summary>
		/// Ensures that the number is at most the given value.
		/// </summary>
		public static int AtMost(this int value, int maxValue) {
			return value > maxValue ? maxValue : value;
		}

		/// <summary>
		/// Converts this decimal value to hexadecimal, with the given prefix.
		/// </summary>
		public static string DecimalToHex(this int value, string prefix = "0x") {
			return prefix + value.ToString("X");
		}

		/// <summary>
		/// Converts this hexadecimal value to a decimal number.
		/// </summary>
		public static int HexToDecimal(this string value) {
			value = value.RemovePrefix("0x").RemovePrefix("0X").RemovePrefix("#");
			return Convert.ToInt32(value, 16);
		}

		/// <summary>
		/// Print the byte value in a human-readable form. Eg "25.1 MB" or "53.2 KB".
		/// </summary>
		public static string BytesToString(this int value, int decimalPlaces = 1) {
			return NumberLongs.BytesToString((long)value, decimalPlaces);
		}

	}
}
