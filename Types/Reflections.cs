using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Jetsons.JetPack {

	/// <summary>
	/// Reflection utils. Implementation taken from stackoverflow and improved.
	/// 
	/// @url		https://stackoverflow.com/questions/1196991/get-property-value-from-string-using-reflection-in-c-sharp
	/// @author		Jason Heddings
	/// </summary>
	public static class Reflections {

		/// <summary>
		/// Get the value of a property or field on any type of object.
		/// </summary>
		/// <param name="nameOrPath">Property name or dot-path of the property</param>
		public static object GetPropValue(this object obj, string nameOrPath) {

			// lookup by path
			if (nameOrPath.Contains(".")) {
				foreach (string part in nameOrPath.Split('.')) {
					obj = GetPropOrField(obj, part);
				}
				return obj;
			}

			// lookup single prop
			else {
				return GetPropOrField(obj, nameOrPath);
			}
		}

		private static object GetPropOrField(object obj, string prop) {
			if (obj == null) { return null; }

			// fixed types - Dictionary and ExpandoObject
			if (obj is IDictionary<string, object>) {
				var dict = (IDictionary<string, object>)obj;
				object dictValue;
				if (dict.TryGetValue(prop, out dictValue)) {
					return dictValue;
				}
				return null;

			} else {

				// dynamic types - use reflection

				// get type info
				Type type = obj.GetType();

				// get field
				FieldInfo info = type.GetField(prop);
				if (info != null) {
					return info.GetValue(obj);
				} else {

					// get property
					PropertyInfo info2 = type.GetProperty(prop);
					if (info2 != null) {
						return info2.GetValue(obj, null);
					}
				}

			}

			return null;
		}

		/// <summary>
		/// Get the value of a property or field on any type of object, and typecast it to the given type.
		/// </summary>
		/// <typeparam name="T">Type of the property you want to retrieve</typeparam>
		/// <param name="nameOrPath">Property name or dot-path of the property</param>
		public static T GetPropValue<T>(this object obj, string nameOrPath) {
			var value = GetPropValue(obj, nameOrPath);
			if (value != null) {
				return (T)value;
			}
			return default(T);
		}

		/// <summary>
		/// Set the value of a property or field on any type of object.
		/// </summary>
		/// <param name="nameOrPath">Property name or dot-path of the property</param>
		/// <param name="value">New value you wish to set</param>
		/// <param name="isField">You want to fetch a field (true) or a property (false)?</param>
		public static void SetPropValue(this object obj, string nameOrPath, object value) {

			// lookup by path
			if (nameOrPath.Contains(".")) {
				var path = nameOrPath.Split('.');
				string lastPart = path.RemoveLast();

				foreach (string part in path) {
					obj = GetPropOrField(obj, part);
				}

				obj.SetPropValue(lastPart, value);
			}

			// lookup single prop
			else {

				// fixed types - Dictionary and ExpandoObject
				if (obj is IDictionary<string, object>) {
					var dict = (IDictionary<string, object>)obj;
					dict.Add(nameOrPath, value);
				} else {

					// dynamic types - use reflection

					// get type info
					Type type = obj.GetType();

					// get field
					FieldInfo info = type.GetField(nameOrPath);
					if (info != null) {
						info.SetValue(obj, value);
					} else {

						// get property
						PropertyInfo info2 = type.GetProperty(nameOrPath);
						if (info2 != null) {
							info2.SetValue(obj, value);
						}
					}

				}

			}
			
		}

	}
}
