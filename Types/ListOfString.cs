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

	}
}
