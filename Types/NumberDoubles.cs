using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jetsons.JetPack {
	public static class NumberDoubles {

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


	}
}
