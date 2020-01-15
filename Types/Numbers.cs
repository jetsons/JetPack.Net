﻿using System;
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
		/// Print the byte value in a human-readable form. Eg "25.1 MB" or "53.2 KB".
		/// </summary>
		public static string BytesToString(this int value, int decimalPlaces = 1) {
			return BytesToString((long)value, decimalPlaces);
		}

		/// <summary>
		/// Returns the smaller value between this number and the other number.
		/// </summary>
		public static double Min(this double value, double otherValue) {
			return value < otherValue ? value : otherValue;
		}

		/// <summary>
		/// Returns the larger value between this number and the other number.
		/// </summary>
		public static double Max(this double value, double otherValue) {
			return value > otherValue ? value : otherValue;
		}

		/// <summary>
		/// Ensures that the number is at least the given value.
		/// </summary>
		public static double AtLeast(this double value, double minValue) {
			return value < minValue ? minValue : value;
		}

		/// <summary>
		/// Ensures that the number is at most the given value.
		/// </summary>
		public static double AtMost(this double value, double maxValue) {
			return value > maxValue ? maxValue : value;
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
		/// Converts this decimal value to hexadecimal, with the given prefix.
		/// </summary>
		public static string DecimalToHex(this uint value, string prefix = "0x") {
			return prefix + value.ToString("X");
		}

		/// <summary>
		/// Converts this hexadecimal value to a decimal number.
		/// </summary>
		public static int HexToDecimal(this string value) {
			value = value.RemovePrefix("0x").RemovePrefix("0X").RemovePrefix("#");
			return Convert.ToInt32(value, 16);
		}

	}
}
