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
		public static int ToInt(this object value, int defaultValue = 0) {
			if (value is int) {
				return (int)value;
			}
			if (value is uint) {
				return (int)((uint)value);
			}
			if (value is float) {
				return (int)((float)value);
			}
			if (value is double) {
				return (int)((double)value);
			}
			if (value is byte) {
				return (int)((byte)value);
			}
			if (value is sbyte) {
				return (int)((sbyte)value);
			}
			if (value is long) {
				return (int)((long)value);
			}
			if (value is ulong) {
				return (int)((ulong)value);
			}
			if (value is decimal) {
				return (int)((decimal)value);
			}
			if (value is bool) {
				return (bool)value == true ? 1 : 0;
			}
			if (value is string) {
				int result;
				if (int.TryParse((string)value, out result)) {
					return result;
				}
			}
			return defaultValue;
		}
		/// <summary>
		/// Converts or casts the given value to an unsigned int
		/// </summary>
		public static uint ToUInt(this object value, uint defaultValue = 0u) {
			if (value is uint) {
				return (uint)value;
			}
			if (value is int) {
				return (uint)((int)value);
			}
			if (value is float) {
				return (uint)((float)value);
			}
			if (value is double) {
				return (uint)((double)value);
			}
			if (value is byte) {
				return (uint)((byte)value);
			}
			if (value is sbyte) {
				return (uint)((sbyte)value);
			}
			if (value is long) {
				return (uint)((long)value);
			}
			if (value is ulong) {
				return (uint)((ulong)value);
			}
			if (value is decimal) {
				return (uint)((decimal)value);
			}
			if (value is bool) {
				return (bool)value == true ? 1u : 0u;
			}
			if (value is string) {
				uint result;
				if (uint.TryParse((string)value, out result)) {
					return result;
				}
			}
			return defaultValue;
		}
		/// <summary>
		/// Converts or casts the given value to a float
		/// </summary>
		public static float ToFloat(this object value, float defaultValue = 0f) {
			if (value is float) {
				return (float)value;
			}
			if (value is double) {
				return (float)((double)value);
			}
			if (value is decimal) {
				return (float)((decimal)value);
			}
			if (value is int) {
				return (float)((int)value);
			}
			if (value is uint) {
				return (float)((uint)value);
			}
			if (value is byte) {
				return (float)((byte)value);
			}
			if (value is sbyte) {
				return (float)((sbyte)value);
			}
			if (value is long) {
				return (float)((long)value);
			}
			if (value is ulong) {
				return (float)((ulong)value);
			}
			if (value is bool) {
				return (bool)value == true ? 1f : 0f;
			}
			if (value is string) {
				float result;
				if (float.TryParse((string)value, out result)) {
					return result;
				}
			}
			return defaultValue;
		}
		/// <summary>
		/// Converts or casts the given value to a double
		/// </summary>
		public static double ToDouble(this object value, double defaultValue = 0.0) {
			if (value is double) {
				return (double)value;
			}
			if (value is float) {
				return (double)((float)value);
			}
			if (value is decimal) {
				return (double)((decimal)value);
			}
			if (value is int) {
				return (double)((int)value);
			}
			if (value is uint) {
				return (double)((uint)value);
			}
			if (value is byte) {
				return (double)((byte)value);
			}
			if (value is sbyte) {
				return (double)((sbyte)value);
			}
			if (value is long) {
				return (double)((long)value);
			}
			if (value is ulong) {
				return (double)((ulong)value);
			}
			if (value is bool) {
				return (bool)value == true ? 1.0 : 0.0;
			}
			if (value is string) {
				double result;
				if (double.TryParse((string)value, out result)) {
					return result;
				}
			}
			return defaultValue;
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
			if (value is string) {
				var text = (string)value;
				return text.EqualsCI("true") || text.EqualsCI("yes");
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
