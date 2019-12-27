using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jetsons.JetPack {
	public static class Arrays {

		/// <summary>
		/// Returns the index of the first occurance of the given item in the array.
		/// </summary>
		/// <param name="list">The array. Cannot be null.</param>
		/// <param name="item">The item to find.</param>
		/// <returns></returns>
		public static int IndexOf<T>(this T[] list, T item) {
			for (int i = 0; i < list.Length; i++) {
				if (EqualityComparer<T>.Default.Equals(list[i], item)) {
					return i;
				}
			}
			return -1;
		}

		/// <summary>
		/// Returns the index of the last occurance of the given item in the array.
		/// </summary>
		/// <param name="list">The array. Cannot be null.</param>
		/// <param name="item">The item to find.</param>
		/// <returns></returns>
		public static int LastIndexOf<T>(this T[] list, T item) {
			for (int i = list.Length - 1; i >= 0; i--) {
				if (EqualityComparer<T>.Default.Equals(list[i], item)) {
					return i;
				}
			}
			return -1;
		}

		/// <summary>
		/// Returns true if the array contains the given item at least once.
		/// </summary>
		/// <param name="list">The array. Cannot be null.</param>
		/// <param name="item">The item to find.</param>
		/// <returns></returns>
		public static bool Contains<T>(this T[] list, T item) {
			for (int i = 0; i < list.Length; i++) {
				if (EqualityComparer<T>.Default.Equals(list[i], item)) {
					return true;
				}
			}
			return false;
		}


	}
}