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
			}
			else {
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
			}
			else {
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
			}
			else {
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
			}
			else {
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

					}
					else {

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

					}
					else {

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

		/// <summary>
		/// Creates a dictionary by indexing the given object by a certain property or field.
		/// This method assumes that key values are unique.
		/// If the same key value is encountered multiple times, the last object is indexed in the dictionary.
		/// </summary>
		/// <typeparam name="TKey">The type of the keyProp</typeparam>
		/// <typeparam name="TItem">The type of the items in the list</typeparam>
		/// <param name="items">The list to convert.</param>
		/// <param name="keyPropOrPath">Property name or dot-path of the property</param>
		/// <returns>Never returns null. Returns a blank dictionary if none of the objects have key values.</returns>
		public static Dictionary<TKey, TItem> IndexByUniqueKey<TKey, TItem>(this IList<TItem> items, string keyPropOrPath) {
			var result = new Dictionary<TKey, TItem>();
			foreach (var item in items) {
				TKey key = item.GetPropValue<TKey>(keyPropOrPath);
				if (key != null) {
					result.SetProp(key, item);
				}
			}
			return result;
		}

		/// <summary>
		/// Creates a dictionary by indexing the given object by a certain property or field.
		/// This method allows for non-unique key values.
		/// All the objects with a given key are listed in the dictionary.
		/// </summary>
		/// <typeparam name="TKey">The type of the keyProp</typeparam>
		/// <typeparam name="TItem">The type of the items in the list</typeparam>
		/// <param name="items">The list to convert.</param>
		/// <param name="keyPropOrPath">Property name or dot-path of the property</param>
		/// <param name="isPropList">Does the property hold a list of keys (true) or a scalar key (false)</param>
		/// <returns>Never returns null. Returns a blank dictionary if none of the objects have key values.</returns>
		public static Dictionary<TKey, List<TItem>> IndexByNonUniqueKey<TKey, TItem>(this IList<TItem> items, string keyPropOrPath, bool isPropList = false) {
			var result = new Dictionary<TKey, List<TItem>>();

			// per item in the list
			foreach (var item in items) {
				if (isPropList) {

					// if the property holds a list of keys
					IList<TKey> keys = item.GetPropValue<IList<TKey>>(keyPropOrPath);
					if (keys != null) {
						foreach (TKey key in keys) {
							if (key != null) {
								if (result.ContainsKey(key)) {
									result[key].Add(item);
								}
								else {
									result.Add(key, new List<TItem> { item });
								}
							}
						}
					}
				}
				else {

					// if the property holds a scalar key
					TKey key = item.GetPropValue<TKey>(keyPropOrPath);
					if (key != null) {
						if (result.ContainsKey(key)) {
							result[key].Add(item);
						}
						else {
							result.Add(key, new List<TItem> { item });
						}
					}
				}
			}
			return result;
		}

		/// <summary>
		/// Creates a dictionary of all the keys and marks a flag against the key.
		/// Useful to mark which keys are encountered in the object.
		/// </summary>
		/// <typeparam name="TKey">The type of the keyProp</typeparam>
		/// <typeparam name="TItem">The type of the items in the list</typeparam>
		/// <typeparam name="TFlag">The type of the flag value</typeparam>
		/// <param name="items">The list to convert.</param>
		/// <param name="keyPropOrPath">Property name or dot-path of the property</param>
		/// <param name="flag">Flag to store against the key</param>
		/// <returns>Never returns null. Returns a blank dictionary if none of the objects have key values.</returns>
		public static Dictionary<TKey, TFlag> IndexByFlags<TKey, TItem, TFlag>(this IList<TItem> items, string keyPropOrPath, TFlag flag) {
			var result = new Dictionary<TKey, TFlag>();
			foreach (var item in items) {
				TKey key = item.GetPropValue<TKey>(keyPropOrPath);
				if (key != null) {
					if (!result.ContainsKey(key)) {
						result.Add(key, flag);
					}
				}
			}
			return result;
		}

	}
}