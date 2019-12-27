using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jetsons.JetPack {

	/// <summary>
	/// Commonly used character codes
	/// </summary>
	public static class Chars {

		public const string Tab = "\t";

		public const string NL = "\n";
		public const string WinNL = "\r\n";
		public const string NL2 = "\n\n";
		public const string WinNL2 = "\r\n\r\n";

		public const string SingleQuote = "\'";
		public const string DoubleQuote = "\"";
		public const char SingleQuoteChar = '\'';
		public const char DoubleQuoteChar = '\"';


		/// <summary>
		/// Checks if the char is a number digit across any script
		/// </summary>
		public static bool IsNumber(this char c) {
			return Char.IsNumber(c);
		}
		/// <summary>
		/// Checks if the char is an ASCII number digit (0-9)
		/// </summary>
		public static bool IsAsciiNumber(this char c) {
			return (c >= '0' && c <= '9');
		}
		/// <summary>
		/// Checks if the char is a symbol digit
		/// </summary>
		public static bool IsSymbol(this char c) {
			return Char.IsSymbol(c);
		}
		/// <summary>
		/// Checks if the char is a letter digit across any script
		/// </summary>
		public static bool IsLetter(this char c) {
			return Char.IsLetter(c);
		}
		/// <summary>
		/// Checks if the char is a hexadecimal compatible digit (0-9, a-f, A-F)
		/// </summary>
		public static bool IsHexDigit(this char c) {
			return (c >= 0 && c <= 9) || (c >= 'a' && c <= 'f') || (c >= 'A' && c <= 'F');
		}
		/// <summary>
		/// Checks if the char is an ASCII letter (a-z, A-Z)
		/// </summary>
		public static bool IsAsciiLetter(this char c) {
			return (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z');
		}
		/// <summary>
		/// Checks if the char is a letter digit
		/// </summary>
		public static bool IsLetterOrDigit(this char c) {
			return Char.IsLetterOrDigit(c);
		}
		/// <summary>
		/// Checks if the char is a lowercase letter
		/// </summary>
		public static bool IsLower(this char c) {
			return Char.IsLower(c);
		}
		/// <summary>
		/// Checks if the char is an uppercase letter
		/// </summary>
		public static bool IsUpper(this char c) {
			return Char.IsUpper(c);
		}
		/// <summary>
		/// Checks if the char is a newline character
		/// </summary>
		public static bool IsNewline(this char c) {
			return c == '\r' || c == '\n';
		}
		/// <summary>
		/// Checks if the char is a space, tab or newline character
		/// </summary>
		public static bool IsWhitespace(this char c) {
			return c == ' ' || c == '\t' || c == '\r' || c == '\n';
		}
		/// <summary>
		/// Converts this letter to lowercase
		/// </summary>
		public static char ToLower(this char c) {
			return Char.ToLower(c);
		}
		/// <summary>
		/// Converts this letter to uppercase
		/// </summary>
		public static char ToUpper(this char c) {
			return Char.ToUpper(c);
		}

	}
}
