using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jetsons.JetPack {
	public static class Numbers {

		/// <summary>
		/// Rounds the given double, returning an int.
		/// </summary>
		public static int Round(this double value) {
			return (int)Math.Round(value);
		}

		/// <summary>
		/// Rounds the given double, preserving the given number of digits after the decimal point;
		/// </summary>
		public static double RoundToDigits(this double value, int digits) {
			return (double)Math.Round(value, digits);
		}

		/// <summary>
		/// Ceilings the given double, returning an int.
		/// </summary>
		public static int Ceiling(this double value) {
			return (int)Math.Ceiling(value);
		}

		/// <summary>
		/// Floors the given double, returning an int.
		/// </summary>
		public static int Floor(this double value) {
			return (int)Math.Floor(value);
		}

		/// <summary>
		/// Snaps the given value to the given step value.
		/// </summary>
		public static double Snap(this double value, double step) {
			return Math.Round(value / step) * step;
		}

		/// <summary>
		/// Snaps the given value to the given step value.
		/// </summary>
		public static int Snap(this int value, int step) {
			return (int)Math.Round((double)value / (double)step) * step;
		}

		/// <summary>
		/// Limits the given value to the given range.
		/// </summary>
		public static double Limit(this double value, double min, double max) {
			if (value < min) {
				return min;
			}
			if (value > max) {
				return max;
			}
			return value;
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


		private static readonly string[] SizeSuffixes = { "bytes", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };

		/// <summary>
		/// Print the byte value in a human-readable form. Eg "25.1 MB" or "53.2 KB".
		/// 
		/// Taken from Stackoverflow.
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
		/// Print the byte value in a human-readable form. Eg "25.1 MB" or "53.2 KB".
		/// </summary>
		public static string BytesToString(this int value, int decimalPlaces = 1) {
			return BytesToString((long)value, decimalPlaces);
		}


	}
}
