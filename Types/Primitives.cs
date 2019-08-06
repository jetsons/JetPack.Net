using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jetsons.JetPack {
	public static class Primitives {

		/// <summary>
		/// Checks if the value is equal to any of the listed values
		/// </summary>
		public static bool IsAny<T>(this T value, IList<T> list) {
			if (list.Contains(value)) {
				return true;
			}
			return false;
		}

		/// <summary>
		/// Checks if the string is equal to any of the listed strings, using case-insensitive comparison
		/// </summary>
		public static bool IsAnyCI(this string value, List<string> list) {
			if (list.ContainsAny(list, false).Found) {
				return true;
			}
			return false;
		}

		/// <summary>
		/// Converts or casts the given value to an integer
		/// </summary>
		public static int ToInt(this object value) {
			if (value is int) {
				return (int)value;
			}
			return Convert.ToInt32(value);
		}
		/// <summary>
		/// Converts or casts the given value to an unsigned int
		/// </summary>
		public static uint ToUInt(this object value) {
			if (value is uint) {
				return (uint)value;
			}
			return Convert.ToUInt32(value);
		}
		/// <summary>
		/// Converts or casts the given value to a float
		/// </summary>
		public static float ToFloat(this object value) {
			if (value is float) {
				return (float)value;
			}
			return Convert.ToSingle(value);
		}
		/// <summary>
		/// Converts or casts the given value to a double
		/// </summary>
		public static double ToDouble(this object value) {
			if (value is double) {
				return (double)value;
			}
			return Convert.ToDouble(value);
		}
		/// <summary>
		/// Converts or casts the given value to a string
		/// </summary>
		public static string ToString(this object value) {
			if (value is string) {
				return (string)value;
			}
			return Convert.ToString(value);
		}
		/// <summary>
		/// Converts or casts the given value to a boolean
		/// </summary>
		public static bool ToBool(this object value) {
			if (value is bool) {
				return (bool)value;
			}
			return Convert.ToBoolean(value);
		}
		/// <summary>
		/// Converts or casts the given value to a date/time value
		/// </summary>
		public static DateTime ToDateTime(this object value) {
			if (value is DateTime) {
				return (DateTime)value;
			}
			return Convert.ToDateTime(value);
		}

	}
}
