using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jetsons.JetPack {
	public static class HTML {

		/// <summary>
		/// Escapes all the special characters in the HTML snippet with HTML entities.
		/// </summary>
		/// <param name="text">The string to escape</param>
		/// <returns></returns>
		public static string EncodeHTML(this string text) {
			var sb = new StringBuilder();
			foreach (var c in text) {
				if (c <= '>') {
					switch (c) {

						// add a known special character
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

							// add the regular character
							sb.Append(c);
							break;
					}
				}
				else {
					if (c > 127 && c < 0x10000) {
						// value needs to be encoded
						sb.Append("&#");
						sb.Append((int)c);
						sb.Append(";");
					}
					else {

						// add the regular character
						sb.Append(c);
					}
				}
			}
			return sb.ToString();
		}

		/// <summary>
		/// Decodes all the HTML entities in the HTML snippet into a proper Unicode string.
		/// </summary>
		/// <param name="text">The string to escape</param>
		/// <returns></returns>
		public static string DecodeHTML(this string text) {
			StringBuilder sb = new StringBuilder(text.Length);

			// prepare the tables (one time cost)
			if (HTMLEntities.EntityToChar == null) {
				HTMLEntities.EntityToChar = new Dictionary<string, char>();
				for (int j = 0; j < HTMLEntities.Entities.Length; j++) {
					HTMLEntities.EntityToChar[HTMLEntities.Entities[j]] = HTMLEntities.Chars[j];
				}
			}

			// go thru the HTML snippet
			int len = text.Length;
			for (int i = 0; i < len;) {
				char c = text[i];

				// if there is an entity here
				int end = c == '&' ? text.IndexOf(';', i + 1) : -1;
				if (c == '&' && end > 0 && end <= i + 8) {

					// read the entity
					string entity = text.Part(i + 1, end, false);
					i += entity.Length + 2;

					// add the decoded entity value if found
					if (HTMLEntities.EntityToChar.ContainsKey(entity)) {
						sb.Append(HTMLEntities.EntityToChar[entity]);
					}
					else {

						// decode the hex or integer value to a char code point
						var hex = entity.TrimStart('#');
						var value = 0;
						if (hex[0] == 'x' || hex[0] == 'X') {
							value = hex.HexToDecimal();
						}
						else if (hex.IsSingleNumber()) {
							value = int.Parse(hex);
						}

						// add the decoded hex or integer value as a decoded character
						if (value > 0) {
							try {
								sb.Append(Convert.ToChar(value));
							}
							catch {
							}
						}
					}
				}
				else {

					// add the regular character
					sb.Append(c);
					i++;
				}
			}
			return sb.ToString();
		}


	}
}