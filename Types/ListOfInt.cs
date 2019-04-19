using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jetsons.JetPack {
	public static class ListOfInt {
		public static int MinIndex(this int A, int B) {
			if (A <= -1) {
				return B;
			}
			if (B <= -1) {
				return A;
			}
			return A < B ? A : B;
		}
		public static int MaxIndex(this int A, int B) {
			if (A <= -1) {
				return B;
			}
			if (B <= -1) {
				return A;
			}
			return A > B ? A : B;
		}
		/// <summary>
		/// Finds the smallest value in the array, and returns its slot index
		/// </summary>
		public static int IndexOfMin(this IList<int> list) {
			if (list.Count == 0) {
				return -1;
			}
			if (list.Count == 1) {
				return 0;
			}
			int min = list[0];
			int index = 0;
			for (int a = 0, al = list.Count; a < al; a++) {
				if (list[a] < min) {
					min = list[a];
					index = a;
				}
			}
			return index;
		}
		/// <summary>
		/// Finds the largest value in the array, and returns its slot index
		/// </summary>
		public static int IndexOfMax(this IList<int> list) {
			if (list.Count == 0) {
				return -1;
			}
			if (list.Count == 1) {
				return 0;
			}
			int max = list[0];
			int index = 0;
			for (int a = 0, al = list.Count; a < al; a++) {
				if (list[a] > max) {
					max = list[a];
					index = a;
				}
			}
			return index;
		}
		/// <summary>
		/// Returns the largest value in the array
		/// </summary>
		/// <param name="list"></param>
		/// <returns></returns>
		public static int Max(this IList<int> list, bool sortedAscending = false) {
			if (list.Count == 0) {
				return 0;
			}
			if (list.Count == 1) {
				return list[0];
			}
			if (sortedAscending) {
				return list.Last();
			}
			int maxVal = list[0];
			for (int a = 0, al = list.Count; a < al; a++) {
				if (list[a] > maxVal) {
					maxVal = list[a];
				}
			}
			return maxVal;
		}
		/// <summary>
		/// Returns the smallest value in the array
		/// </summary>
		/// <param name="list"></param>
		/// <returns></returns>
		public static int Min(this IList<int> list, bool sortedAscending = false) {
			if (list.Count == 0) {
				return 0;
			}
			if (list.Count == 1) {
				return list[0];
			}
			if (sortedAscending) {
				return list.First();
			}
			int minVal = list[0];
			for (int a = 0, al = list.Count; a < al; a++) {
				if (list[a] < minVal) {
					minVal = list[a];
				}
			}
			return minVal;
		}
	}
}