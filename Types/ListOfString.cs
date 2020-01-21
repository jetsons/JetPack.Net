using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jetsons.JetPack {
	public static class ListOfString {

		/// <summary>
		/// Join the strings with the given seperator
		/// </summary>
		public static string Join(this IList<string> values, string seperator) {
			StringBuilder sb = new StringBuilder();

			// per value
			for (int i = 0; i < values.Count; i++) {

				// add value
				sb.Append(values[i]);

				// add seperator if not last
				bool isLast = i == values.Count - 1;
				if (!isLast) {
					sb.Append(seperator);
				}
			}

			return sb.ToString();
		}
		/// <summary>
		/// Sorts the strings by their length
		/// </summary>
		public static List<string> SortByLength(this IList<string> values, bool shortestFirst = true) {
			if (shortestFirst) {
				return (from s in values orderby s.Length ascending select s).ToList<string>();
			} else {
				return (from s in values orderby s.Length descending select s).ToList<string>();
			}
		}


		/// <summary>
		/// Checks if the string equals any given term, and returns a results struct. Never returns null.
		/// </summary>
		/// <param name="data">List of strings to check</param>
		/// <param name="terms">Search terms</param>
		/// <param name="caseSensitive">Use case sensitive search?</param>
		/// <returns></returns>
		public static SearchResult3 EqualsAny(this IList<string> data, List<string> terms, bool caseSensitive = true) {
			var opts = caseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase;
			int t = 0;
			int d = 0;
			foreach (string text in data) {
				int len = text.Length;
				foreach (string term in terms) {
					if (len == term.Length) {
						if (text.Equals(term, opts)) {
							return new SearchResult3 {
								Found = true,
								Term = term,
								TermIndex = t,
								CharIndex = 0,
								Data = text,
								DataIndex = d
							};
						}
					}
					t++;
				}
				d++;
			}
			return new SearchResult3 {
				Found = false,
				TermIndex = -1,
				CharIndex = -1,
				DataIndex = -1
			};
		}

		/// <summary>
		/// Checks if the string contains any given term, and returns a results struct. Never returns null.
		/// </summary>
		/// <param name="data">List of strings to check</param>
		/// <param name="terms">Search terms</param>
		/// <param name="caseSensitive">Use case sensitive search?</param>
		/// <returns></returns>
		public static SearchResult3 ContainsAny(this IList<string> data, List<string> terms, bool caseSensitive = true) {
			var opts = caseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase;
			int t = 0;
			int d = 0;
			foreach (string text in data) {
				int len = text.Length;
				foreach (string term in terms) {
					if (len >= term.Length) {
						var i = text.IndexOf(term, opts);
						if (i > -1) {
							return new SearchResult3 {
								Found = true,
								Term = term,
								TermIndex = t,
								CharIndex = i,
								Data = text,
								DataIndex = d
							};
						}
					}
					t++;
				}
				d++;
			}
			return new SearchResult3 {
				Found = false,
				TermIndex = -1,
				CharIndex = -1,
				DataIndex = -1
			};
		}

		/// <summary>
		/// Checks if the string contains any given term, and returns a results struct. Never returns null.
		/// </summary>
		/// <param name="data">List of strings to check</param>
		/// <param name="terms">Search terms</param>
		/// <param name="caseSensitive">Use case sensitive search?</param>
		/// <returns></returns>
		public static SearchResult3 IndexOfAny(this IList<string> data, List<string> terms, bool caseSensitive = true) {
			return data.ContainsAny(terms);
		}

		/// <summary>
		/// Checks if the string begins with any given term, and returns a results struct. Never returns null.
		/// </summary>
		/// <param name="data">List of strings to check</param>
		/// <param name="terms">Search terms</param>
		/// <param name="caseSensitive">Use case sensitive search?</param>
		/// <returns></returns>
		public static SearchResult3 BeginsWithAny(this IList<string> data, List<string> terms, bool caseSensitive = true) {
			int t = 0;
			int d = 0;
			foreach (string text in data) {
				int len = text.Length;
				foreach (string term in terms) {
					if (len >= term.Length) {
						if (text.BeginsWith(term, caseSensitive)) {
							return new SearchResult3 {
								Found = true,
								Term = term,
								TermIndex = t,
								CharIndex = 0,
								Data = text,
								DataIndex = d
							};
						}
					}
					t++;
				}
				d++;
			}
			return new SearchResult3 {
				Found = false,
				TermIndex = -1,
				CharIndex = -1,
				DataIndex = -1
			};
		}

		/// <summary>
		/// Checks if the string ends with any given term, and returns a results struct. Never returns null.
		/// </summary>
		/// <param name="data">List of strings to check</param>
		/// <param name="terms">Search terms</param>
		/// <param name="caseSensitive">Use case sensitive search?</param>
		/// <returns></returns>
		public static SearchResult3 EndsWithAny(this IList<string> data, List<string> terms, bool caseSensitive = true) {
			int t = 0;
			int d = 0;
			foreach (string text in data) {
				int len = text.Length;
				foreach (string term in terms) {
					if (len >= term.Length) {
						if (text.EndsWith(term, caseSensitive)) {
							return new SearchResult3 {
								Found = true,
								Term = term,
								TermIndex = t,
								CharIndex = len - term.Length,
								Data = text,
								DataIndex = d
							};
						}
					}
					t++;
				}
				d++;
			}
			return new SearchResult3 {
				Found = false,
				TermIndex = -1,
				CharIndex = -1,
				DataIndex = -1
			};
		}

		/// <summary>
		/// Checks if the list contains any string matching the given term, using case-insensitive comparison.
		/// </summary>
		public static bool ContainsCI(this IList<string> data, string term) {
			for (int i = 0; i < data.Count; i++) {
				if (data[i] != null && data[i].EqualsCI(term)) {
					return true;
				}
			}
			return false;
		}

		/// <summary>
		/// Returns the index of the first string that matches the given term, using case-insensitive comparison.
		/// If no item is found then it returns -1.
		/// </summary>
		public static int IndexOfCI(this IList<string> data, string term) {
			for (int i = 0; i < data.Count; i++) {
				if (data[i] != null && data[i].EqualsCI(term)) {
					return i;
				}
			}
			return -1;
		}

		/// <summary>
		/// Returns the index of the last string that matches the given term, using case-insensitive comparison.
		/// If no item is found then it returns -1.
		/// </summary>
		public static int LastIndexOfCI(this IList<string> data, string term) {
			for (int i = (data.Count - 1); i >= 0; i--) {
				if (data[i] != null && data[i].EqualsCI(term)) {
					return i;
				}
			}
			return -1;
		}


	}

	/// <summary>
	/// Results of a search in a list of strings
	/// </summary>
	public struct SearchResult3 {
		/// <summary>
		/// any term found?
		/// </summary>
		public bool Found;
		/// <summary>
		/// which term was found?
		/// </summary>
		public string Term;
		/// <summary>
		/// what was the index of the term found within the list of terms given?
		/// </summary>}
		public int TermIndex;
		/// <summary>
		/// what char index did the IndexOf() call return?
		/// </summary>
		public int CharIndex;
		/// <summary>
		/// what was the index of the data in the list that matched the term?
		/// </summary>
		public int DataIndex;
		/// <summary>
		/// what was the data in the list that matched the term?
		/// </summary>
		public string Data;
	}

}
