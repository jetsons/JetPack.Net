using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jetsons.JetPack {
	public static class Dicts {

		/// <summary>
		/// Add or overwrite the given property into the ExpandoObject
		/// </summary>
		/// <param name="expando">ExpandoObject. Must not be null.</param>
		/// <param name="property">Property name you want to set</param>
		/// <param name="value">New value</param>
		public static void SetProp(this ExpandoObject expando, string property, object value) {
			var dict = expando as IDictionary<string, object>;
			if (dict.ContainsKey(property)) {
				dict[property] = value;
			} else {
				dict.Add(property, value);
			}
		}
		/// <summary>
		/// Return the value of the given property from the ExpandoObject, or the given default if it does not exist
		/// </summary>
		/// <param name="expando">ExpandoObject. Must not be null.</param>
		/// <param name="property">Property name you want to get</param>
		/// <param name="defaultValue">Value to return if the property is not found</param>
		public static object GetProp(this ExpandoObject expando, string property, object defaultValue = null) {
			var dict = expando as IDictionary<string, object>;
			if (dict.ContainsKey(property)) {
				return dict[property];
			} else {
				return defaultValue;
			}
		}

		/// <summary>
		/// Add or overwrite the given property into the Dictionary
		/// </summary>
		/// <param name="dict">Typed Dictionary. Must not be null.</param>
		/// <param name="property">Property name you want to set.</param>
		/// <param name="value">New value</param>
		public static void SetProp<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey property, TValue value) {
			if (dict.ContainsKey(property)) {
				dict[property] = value;
			} else {
				dict.Add(property, value);
			}
		}
		/// <summary>
		/// Return the value of the given property from the Dictionary, or the given default if it does not exist
		/// </summary>
		/// <param name="dict">Typed Dictionary. Must not be null.</param>
		/// <param name="property">Property name you want to get.</param>
		/// <param name="defaultValue">Value to return if the property is not found</param>
		public static TValue GetProp<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey property, TValue defaultValue = default(TValue)) {
			if (dict.ContainsKey(property)) {
				return dict[property];
			} else {
				return defaultValue;
			}
		}


		/// <summary>
		/// Add or overwrite the given property or path into the Dictionary, creating sub-objects as necessary.
		/// </summary>
		/// <param name="dict">Untyped Dictionary. Must not be null.</param>
		/// <param name="propertyOrPath">Property name you want to set. Can be a deep property path which is dot-seperated.</param>
		/// <param name="value">New value</param>
		public static void SetPath(this IDictionary<string, object> dict, string propertyOrPath, object value) {

			// deep path
			if (propertyOrPath.Contains('.')) {

				// get the parts of the path
				var obj = dict;
				var parts = propertyOrPath.SmartSplit(".");
				var lastPart = parts.RemoveLast();

				// get or create the last sub-object
				foreach (string part in parts) {
					if (obj.ContainsKey(part)) {

						// get the existing sub-object
						obj = obj[part] as IDictionary<string, object>;

					} else {

						// create a new sub-object
						obj[part] = obj = new Dictionary<string, object>();
					}
				}

				// set the last prop on the last sub-object
				obj.SetProp(lastPart, value);
				return;
			}

			// shallow path
			dict.SetProp(propertyOrPath, value);

		}
		/// <summary>
		/// Return the value of the given property or path from the Dictionary, or the given default if it does not exist
		/// </summary>
		/// <param name="dict">Untyped Dictionary. Must not be null.</param>
		/// <param name="propertyOrPath">Property name you want to get. Can be a deep property path which is dot-seperated.</param>
		/// <param name="defaultValue">Value to return if the property is not found</param>
		public static object GetPath(this IDictionary<string, object> dict, string propertyOrPath, object defaultValue = null) {

			// deep path
			if (propertyOrPath.Contains('.')) {

				// get the parts of the path
				var obj = dict;
				var parts = propertyOrPath.SmartSplit(".");
				var lastPart = parts.RemoveLast();

				// get or create the last sub-object
				foreach (string part in parts) {
					if (obj.ContainsKey(part)) {

						// get the existing sub-object
						obj = obj[part] as IDictionary<string, object>;

					} else {

						// not found
						return defaultValue;
					}
				}


				// return the last prop from the last sub-object
				return obj.GetProp(lastPart, defaultValue);
			}

			// shallow path
			return dict.GetProp(propertyOrPath);
		}

	}
}
