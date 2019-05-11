using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jetsons.JetPack {
	public static class HTML {

		/// <summary>
		/// Escapes all the HTML special characters in the string with HTML entities.
		/// </summary>
		/// <param name="text">The string to escape</param>
		/// <returns></returns>
		public static string EscapeHTML(this string text) {
			var sb = new StringBuilder();
			foreach (var c in text) {
				if (c <= '>') {
					switch (c) {
						case '<':
							sb.Append("&lt;");
							break;
						case '>':
							sb.Append("&gt;");
							break;
						case '"':
							sb.Append("&quot;");
							break;
						case '\'':
							sb.Append("&#39;");
							break;
						case '&':
							sb.Append("&amp;");
							break;
						default:
							sb.Append(c);
							break;
					}
				} else {
					if (c > 127 && c < 0x10000) {
						// value needs to be encoded
						sb.Append("\\u");
						sb.Append( ((int)c).ToString("x4"));
					} else {
						sb.Append(c);
					}
				}
			}
			return sb.ToString();
		}

	}
}
