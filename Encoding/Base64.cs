using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jetsons.JetPack {
	public static class Base64 {


		/// <summary>
		/// Encode the string as an ANSI or Unicode formatted string and encode it as a Base64 string.
		/// </summary>
		/// <param name="text">The string to be encoded</param>
		/// <param name="unicode">Save the file as unicode (true) or ANSI (false)</param>
		/// <param name="codepage">ANSI Codepage to use while reading the file</param>
		/// <returns></returns>
		public static string EncodeBase64(this string text, bool unicode = true, int codepage = 1252) {
			return Convert.ToBase64String(unicode ? text.EncodeUTF8() : text.EncodeANSI(codepage));
		}

		/// <summary>
		/// Decodes the given Base64 string into a string.
		/// </summary>
		/// <param name="text">The Base64-formatted string to be decoded</param>
		/// <param name="unicode">Save the file as unicode (true) or ANSI (false)</param>
		/// <param name="codepage">ANSI Codepage to use while reading the file</param>
		/// <returns></returns>
		public static byte[] DecodeBase64(this string text, bool unicode = true, int codepage = 1252) {
			return Convert.FromBase64String(text);
		}

		/// <summary>
		/// Encode the binary data into a Base64 string.
		/// </summary>
		/// <param name="data">The data to be encoded</param>
		public static string EncodeBase64(this byte[] data) {
			return Convert.ToBase64String(data);
		}

	}
}
