﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jetsons.JetPack {
	public static class Lists {

		/// <summary>
		/// Checks if the given list exists and has any items in it
		/// </summary>
		public static bool Exists(this IList list) {
			return list != null && list.Count > 0;
		}
		/// <summary>
		/// Checks if the given list exists and has any items in it
		/// </summary>
		public static bool Exists<T>(this T[] list) {
			return list != null && list.Length > 0;
		}
		/// <summary>
		/// Checks if the given list exists and has any items in it
		/// </summary>
		public static bool Exists<T>(this IList<T> list) {
			return list != null && list.Count > 0;
		}

		/// <summary>
		/// Checks if the given list exists and has any items in it
		/// </summary>
		public static bool Exists<T>(this List<T> list) {
			return list != null && list.Count > 0;
		}

		/// <summary>
		/// Checks if the given list is null or has no items in it
		/// </summary>
		public static bool Blank(this IList list) {
			return list == null && list.Count == 0;
		}
		/// <summary>
		/// Checks if the given list is null or has no items in it
		/// </summary>
		public static bool Blank<T>(this T[] list) {
			return list == null && list.Length == 0;
		}
		/// <summary>
		/// Checks if the given list is null or has no items in it
		/// </summary>
		public static bool Blank<T>(this IList<T> list) {
			return list == null && list.Count == 0;
		}

		/// <summary>
		/// Checks if the given list is null or has no items in it
		/// </summary>
		public static bool Blank<T>(this List<T> list) {
			return list == null && list.Count == 0;
		}

		/// <summary>
		/// Returns the value of the slot or null if the slot is outside the bounds of the list, instead of throwing an exception
		/// </summary>
		public static T Get<T>(this List<T> list, int slot, T defaultValue = default(T)) {
			if (list.HasSlot(slot)) {
				return list[slot];
			}
			return defaultValue;
		}

		/// <summary>
		/// Returns the value of the slot or null if the slot is outside the bounds of the list, instead of throwing an exception
		/// </summary>
		public static T Get<T>(this IList<T> list, int slot, T defaultValue = default(T)) {
			if (list.HasSlot(slot)) {
				return list[slot];
			}
			return defaultValue;
		}

		/// <summary>
		/// Sets the value of the slot, adding nulls to end of the list if the slot is beyond the end of the list.
		/// </summary>
		public static void Set<T>(this List<T> list, int slot, T value) {
			if (slot >= 0) {
				while (list.Count <= slot) {
					list.Add(default(T));
				}
				list[slot] = value;
			}
		}

		/// <summary>
		/// Returns true if the list contains the given slot index
		/// </summary>
		public static bool HasSlot<T>(this List<T> list, int slot) {
			return slot >= 0 && slot <= (list.Count - 1);
		}

		/// <summary>
		/// Returns true if the list contains the given slot index
		/// </summary>
		public static bool HasSlot<T>(this IList<T> list, int slot) {
			return slot >= 0 && slot <= (list.Count - 1);
		}

		/// <summary>
		/// Returns true if the list contains the given slot index, and if the value is non-default
		/// </summary>
		public static bool HasSlotAndValue<T>(this List<T> list, int slot) {
			return slot >= 0 && slot <= (list.Count - 1) && !object.Equals(list[slot], default(T));
		}

		/// <summary>
		/// Returns true if the list contains the given slot index, and if the value is non-default
		/// </summary>
		public static bool HasSlotAndValue<T>(this IList<T> list, int slot) {
			return slot >= 0 && slot <= (list.Count - 1) && !object.Equals(list[slot], default(T));
		}

		/// <summary>
		/// Returns the first value in the list, or the default if not found
		/// </summary>
		/// <returns></returns>
		public static T First<T>(this List<T> list) {
			if (list.Count == 0) {
				return default(T);
			}
			return list[0];
		}

		/// <summary>
		/// Returns the first value in the list, or the default if not found
		/// </summary>
		/// <returns></returns>
		public static T First<T>(this IList<T> list) {
			if (list.Count == 0) {
				return default(T);
			}
			return list[0];
		}

		/// <summary>
		/// Returns the last value in the list, or the default if not found
		/// </summary>
		/// <returns></returns>
		public static T Last<T>(this List<T> list) {
			if (list.Count == 0) {
				return default(T);
			}
			return list[list.Count - 1];
		}

		/// <summary>
		/// Returns the last value in the list, or the default if not found
		/// </summary>
		/// <returns></returns>
		public static T Last<T>(this IList<T> list) {
			if (list.Count == 0) {
				return default(T);
			}
			return list[list.Count - 1];
		}

		/// <summary>
		/// Repeats the given value N times and returns the list
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="value">Which value</param>
		/// <param name="times">N times</param>
		/// <returns></returns>
		public static List<T> Repeat<T>(this T value, int times) {
			List<T> list = new List<T>();
			for (int r = 0; r < times; r++) {
				list.Add(value);
			}
			return list;
		}

		/// <summary>
		/// Converts the given IList to a typed List
		/// </summary>
		public static List<T> ToList<T>(this IEnumerable array) {
			if (array == null) {
				return null;
			}
			List<T> list = new List<T>();
			foreach (object obj in array) {
				list.Add((T)obj);
			}
			return list;
		}
		/// <summary>
		/// Converts the given ArrayList to a typed List
		/// </summary>
		public static List<T> ToList<T>(this ArrayList array) {
			if (array == null) {
				return null;
			}
			List<T> list = new List<T>();
			for (int i = 0, il = array.Count; i < il; i++) {
				list.Add((T)array[i]);
			}
			return list;
		}

		/// <summary>
		/// Converts the given list to an array
		/// </summary>
		public static T[] ToArray<T>(this ArrayList array) {
			T[] list = new T[array.Count];
			for (int i = 0, il = array.Count; i < il; i++) {
				list[i] = ((T)array[i]);
			}
			return list;
		}

		/// <summary>
		/// Adds the given item into the list if it is non-null
		/// </summary>
		public static void AddIfExists(this IList list, object item) {
			if (item != null) {
				list.Add(item);
			}
		}

		/// <summary>
		/// Adds the given item into the list only if its not already in the list. Returns true if the list was modified.
		/// </summary>
		public static bool AddOnce(this IList list, object item) {
			if (item != null && list.IndexOf(item) == -1) {
				list.Add(item);
				return true;
			}
			return false;
		}

		/// <summary>
		/// Transposes the given 2D array
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="list">2D array</param>
		/// <returns></returns>
		public static List<List<T>> Transpose<T>(this List<List<T>> list) {

			List<List<T>> result = new List<List<T>>();

			int xLen = list.Count;
			for (int x = 0; x < xLen; x++) {

				int yLen = list[x].Count;
				for (int y = 0; y < yLen; y++) {

					if ((result.Count - 1) < y) {
						result.Add(new List<T>());
					}
					result[y].Add(list[x][y]);
				}
			}

			return result;
		}

		/// <summary>
		/// Ensures the given index is within the list.
		/// Always returns a valid index within the list, or -1 if the list has no items.
		/// </summary>
		/// <param name="list">List of items</param>
		/// <param name="index">Index you want to check</param>
		/// <returns></returns>
		public static int EnsureValidIndex<T>(this IList<T> list, int index) {
			int count = list.Count;
			if (count == 0) {
				return -1;
			}
			if (index < 0) {
				return 0;
			}
			if (index >= count) {
				return count - 1;
			}
			return index;
		}

		/// <summary>
		/// Gets a specific range of elements from the list. Returns fewer elements that wanted if the start or end index is outside bounds.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="list">List you want a part of</param>
		/// <param name="startIndex">Index of first element you want</param>
		/// <param name="endIndex">Index of last element you want (if inclusive is true)</param>
		/// <param name="inclusive">Return the end index item (true) or not (false)</param>
		/// <returns></returns>
		public static List<T> Part<T>(this IList<T> list, int startIndex, int endIndex, bool inclusive = true) {

			List<T> result = new List<T>();

			if (list.Count > 0) {

				// support exclusive mode, where item corresponding to endIndex is not returned
				if (!inclusive) {
					endIndex--;
				}

				// ensure indices are within the list
				startIndex = list.EnsureValidIndex(startIndex);
				endIndex = list.EnsureValidIndex(endIndex);

				// add all items
				for (int x = startIndex; x <= endIndex; x++) {
					result.Add(list[x]);
				}
			}

			return result;
		}

		/// <summary>
		/// Removes the first item from the list and returns it.
		/// </summary>
		/// <param name="list">The list which to remove the item from. It is modified.</param>
		/// <returns></returns>
		public static T RemoveFirst<T>(this IList<T> list) {
			if (list.Count == 0) {
				return default(T);
			}
			var first = list[0];
			list.RemoveAt(0);
			return first;
		}

		/// <summary>
		/// Removes the last item from the list and returns it.
		/// </summary>
		/// <param name="list">The list which to remove the item from. It is modified.</param>
		/// <returns></returns>
		public static T RemoveLast<T>(this IList<T> list) {
			if (list.Count == 0) {
				return default(T);
			}
			var i = list.Count - 1;
			var last = list[i];
			list.RemoveAt(i);
			return last;
		}

		/// <summary>
		/// Shallow clones the list by copying each item to a new list.
		/// </summary>
		/// <param name="list">The list to clone. Not modified. Can be null.</param>
		/// <returns></returns>
		public static List<T> ShallowClone<T>(this IList<T> list) {
			if (list == null) {
				return null;
			}
			var newList = new List<T>();
			for (int i = 0; i < list.Count; i++) {
				newList.Add(list[i]);
			}
			return newList;
		}

		/// <summary>
		/// Add the given item into the list and return the list.
		/// </summary>
		/// <param name="list">The list. Cannot be null.</param>
		/// <param name="item">The item to add.</param>
		/// <returns></returns>
		public static List<T> AddAndReturn<T>(this List<T> list, T item) {
			list.Add(item);
			return list;
		}

		/// <summary>
		/// Remove the given item from the list and return the list.
		/// </summary>
		/// <param name="list">The list. Cannot be null.</param>
		/// <param name="item">The item to remove.</param>
		/// <returns></returns>
		public static List<T> RemoveAndReturn<T>(this List<T> list, T item) {
			list.Remove(item);
			return list;
		}

		/// <summary>
		/// Returns the index of the last occurance of the search term in the list, or -1 if not found.
		/// </summary>
		/// <param name="list">The list. Can be null.</param>
		/// <param name="searchTerm">Item to look for in the list.</param>
		/// <returns></returns>
		public static int LastIndexOf<T>(this IList<T> list, T searchTerm) {
			if (list == null || list.Count == 0) {
				return -1;
			}
			for (int i = list.Count - 1; i >= 0; i--) {
				if (EqualityComparer<T>.Default.Equals(list[i], searchTerm)) {
					return i;
				}
			}
			return -1;
		}

		/// <summary>
		/// Returns a new list with the items before the given index
		/// </summary>
		/// <param name="list">The list. Can be null.</param>
		/// <param name="index">Item index</param>
		/// <param name="inclusive">Return the text including the given char index, or excluding it?</param>
		/// <returns>Never returns null. Always returns a new list. The input list is never returned.</returns>
		public static List<T> BeforeIndex<T>(this IList<T> list, int index = 0, bool inclusive = false) {
			if (list == null || index < 0) {
				return new List<T>();
			}
			if (index > list.Count) {
				return list.ShallowClone();
			}
			return inclusive ? list.Part(0, index) : list.Part(0, index - 1);
		}

		/// <summary>
		/// Returns a new list with the items after the given index
		/// </summary>
		/// <param name="list">The list. Can be null.</param>
		/// <param name="index">Item index</param>
		/// <param name="inclusive">Return the text including the given char index, or excluding it?</param>
		/// <returns>Never returns null. Always returns a new list. The input list is never returned.</returns>
		public static List<T> AfterIndex<T>(this IList<T> list, int index = 0, bool inclusive = false) {
			if (list == null || index > list.Count) {
				return new List<T>();
			}
			if (index < 0) {
				return list.ShallowClone();
			}
			return inclusive ? list.Part(index, list.Count - 1) : list.Part(index + 1, list.Count - 1);
		}

		/// <summary>
		/// Returns a new list with the items after the *first* occurrence of the search term
		/// </summary>
		/// <param name="list">The list. Can be null.</param>
		/// <param name="searchTerm">Search term, not included in result</param>
		/// <param name="returnAll">If search term is not found, return all or nothing?</param>
		/// <returns>Never returns null. Always returns a new list. The input list is never returned.</returns>
		public static List<T> After<T>(this IList<T> list, T searchTerm, bool returnAll = true) {
			int start = list.IndexOf(searchTerm);
			if (start == -1) {
				return returnAll ? list.ShallowClone() : new List<T>();
			}
			return list.Part(start + 1, list.Count - 1);
		}

		/// <summary>
		/// Returns a new list with the items after the *last* occurence of the search term
		/// </summary>
		/// <param name="list">The list. Can be null.</param>
		/// <param name="searchTerm">Search term, not included in result</param>
		/// <param name="returnAll">If search term is not found, return all or nothing?</param>
		/// <returns>Never returns null. Always returns a new list. The input list is never returned.</returns>
		public static List<T> AfterLast<T>(this IList<T> list, T searchTerm, bool returnAll = true) {
			int start = list.LastIndexOf(searchTerm);
			if (start == -1) {
				return returnAll ? list.ShallowClone() : new List<T>();
			}
			return list.Part(start + 1, list.Count - 1);
		}

		/// <summary>
		/// Returns a new list with the items before the *first* occurrence of the search term
		/// </summary>
		/// <param name="list">The list. Can be null.</param>
		/// <param name="searchTerm">Search term, not included in result</param>
		/// <param name="returnAll">If search term is not found, return all or nothing?</param>
		/// <returns>Never returns null. Always returns a new list. The input list is never returned.</returns>
		public static List<T> Before<T>(this IList<T> list, T searchTerm, bool returnAll = true) {
			int start = list.IndexOf(searchTerm);
			if (start == -1) {
				return returnAll ? list.ShallowClone() : new List<T>();
			}
			return list.Part(0, start - 1);
		}

		/// <summary>
		/// Returns a new list with the items before the *last* occurrence of the search term
		/// </summary>
		/// <param name="list">The list. Can be null.</param>
		/// <param name="searchTerm">Search term, not included in result</param>
		/// <param name="returnAll">If search term is not found, return all or nothing?</param>
		/// <returns>Never returns null. Always returns a new list. The input list is never returned.</returns>
		public static List<T> BeforeLast<T>(this IList<T> list, T searchTerm, bool returnAll = true) {
			int start = list.LastIndexOf(searchTerm);
			if (start == -1) {
				return returnAll ? list.ShallowClone() : new List<T>();
			}
			return list.Part(0, start - 1);
		}

		/// <summary>
		/// Replaces the *first* occurrence of the search term with the given value.
		/// Returns the index of the occurance, or -1 if it was not found.
		/// </summary>
		/// <param name="list">The list. Cannot be null.</param>
		/// <param name="searchTerm">Search term</param>
		/// <param name="replaceTerm">If search term is not found, it is replaced with this</param>
		public static int ReplaceFirst<T>(this IList<T> list, T searchTerm, T replaceTerm) {
			int index = list.IndexOf(searchTerm);
			if (index > -1) {
				list[index] = replaceTerm;
			}
			return index;
		}

		/// <summary>
		/// Replaces the *last* occurrence of the search term with the given value.
		/// Returns the index of the occurance, or -1 if it was not found.
		/// </summary>
		/// <param name="list">The list. Cannot be null.</param>
		/// <param name="searchTerm">Search term</param>
		/// <param name="replaceTerm">If search term is not found, it is replaced with this</param>
		public static int ReplaceLast<T>(this IList<T> list, T searchTerm, T replaceTerm) {
			int index = list.LastIndexOf(searchTerm);
			if (index > -1) {
				list[index] = replaceTerm;
			}
			return index;
		}

		/// <summary>
		/// Replaces all the occurrences of the search term with the given value.
		/// Returns the number of times replaced, or 0 if it was not found.
		/// </summary>
		/// <param name="list">The list. Cannot be null.</param>
		/// <param name="searchTerm">Search term</param>
		/// <param name="replaceTerm">If search term is not found, it is replaced with this</param>
		public static int Replace<T>(this IList<T> list, T searchTerm, T replaceTerm) {
			int count = 0;
			for (int i = 0; i < list.Count; i++) {
				if (EqualityComparer<T>.Default.Equals(list[i], searchTerm)) {
					count++;
					list[i] = replaceTerm;
				}
			}
			return count;
		}

	}
}