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
		/// </summary>
		public static long Limit(this long value, long min, long max) {
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

		/// <summary>
		/// Print the byte value in a human-readable form. Eg "25.1 MB" or "53.2 KB".
		/// </summary>
		public static string BytesToString(this long value, int decimalPlaces = 1) {
			return NumberLongs.BytesToString((long)value, decimalPlaces);
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


	}
}
