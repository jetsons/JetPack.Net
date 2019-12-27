using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jetsons.JetPack {
	public static class StringLiterals {

		/// <summary>
		/// Encode the string as a string literal, by escaping the special characters.
		/// Surrounds the string with the quote character, if any given.
		/// Suitable for C++, C#, JS, PHP and similar.
		/// </summary>
		/// <param name="text">The string to be encoded</param>
		/// <param name="surroundWith">The quote character to surround the string with</param>
		/// <returns></returns>
		public static string EncodeStringLiteral(this string text, string surroundWith = null) {
			var sb = new StringBuilder();
			if (surroundWith != null) {
				sb.Append(surroundWith);
			}
			foreach (var c in text) {

				// encode special characters with a preceeding slash
				if (c == '\"') {
					sb.Append("\\\"");
				}
				else if (c == '\\') {
					sb.Append("\\\\");
				}
				else if (c == '\n') {
					sb.Append("\\n");
				}
				else if (c == '\r') {
					sb.Append("\\r");
				}
				else if (c == '\t') {
					sb.Append("\\t");
				}
				else {

					// encode normal characters
					sb.Append(c);
				}
			}
			if (surroundWith != null) {
				sb.Append(surroundWith);
			}
			return sb.ToString();
		}

		/// <summary>
		/// Decode the string literal into the actual string, by interpreting the escaped special characters.
		/// Strips out the quote character surrounding the literal, if any.
		/// Suitable for C++, C#, JS, PHP and similar.
		/// </summary>
		/// <param name="text">The string literal to be decoded</param>
		/// <returns></returns>
		public static string DecodeStringLiteral(this string text) {

			// remove the starting and ending quotes, if any
			var isQuoted = text[0] == Chars.SingleQuoteChar || text[0] == Chars.DoubleQuoteChar;
			var start = isQuoted ? 1 : 0;
			var end = isQuoted ? text.Length - 1 : text.Length;

			// decode all string chars
			var sb = new StringBuilder();
			var prevWasSlash = false;
			for (int i = start; i < end; i++) {
				var c = text[i];

				// state machine for reading special char after the slash
				if (c == '\\') {
					prevWasSlash = true;
					continue;
				} else {
					if (prevWasSlash) {
						prevWasSlash = false;

						// interpret special char after the slash
						if (c == '\"') {
							sb.Append("\"");
						}
						else if (c == '\\') {
							sb.Append("\\");
						}
						else if (c == '\n') {
							sb.Append("\n");
						}
						else if (c == '\r') {
							sb.Append("\r");
						}
						else if (c == '\t') {
							sb.Append("\t");
						}
					}
					else {

						// interpret normal characters
						sb.Append(c);
					}
				}
			}
			return sb.ToString();
		}

	}
}