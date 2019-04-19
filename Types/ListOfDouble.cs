using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jetsons.JetPack {
	public static class ListOfDouble {
		/// <summary>
		/// Finds the smallest value in the array, and returns its slot index
		/// </summary>
		public static int IndexOfMin(this IList<double> list) {
			if (list.Count == 0) {
				return -1;
			}
			if (list.Count == 1) {
				return 0;
			}
			double min = list[0];
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
		public static int IndexOfMax(this IList<double> list) {
			if (list.Count == 0) {
				return -1;
			}
			if (list.Count == 1) {
				return 0;
			}
			double max = list[0];
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
		public static double Max(this IList<double> list, bool sortedAscending = false) {
			if (list.Count == 0) {
				return 0;
			}
			if (list.Count == 1) {
				return list[0];
			}
			if (sortedAscending) {
				return list.Last();
			}
			double maxVal = list[0];
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
		public static double Min(this IList<double> list, bool sortedAscending = false) {
			if (list.Count == 0) {
				return 0;
			}
			if (list.Count == 1) {
				return list[0];
			}
			if (sortedAscending) {
				return list.First();
			}
			double minVal = list[0];
			for (int a = 0, al = list.Count; a < al; a++) {
				if (list[a] < minVal) {
					minVal = list[a];
				}
			}
			return minVal;
		}
	}
}