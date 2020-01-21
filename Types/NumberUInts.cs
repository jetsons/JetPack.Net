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
		/// </summary>
		public static uint Limit(this uint value, uint min, uint max) {
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


	}
}
