using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jetsons.JetPack {
	public static class UTF8 {

		/// <summary>
		/// Encode the string as a Unicode formatted string and returns the binary data.
		/// </summary>
		/// <param name="text">The string to be encoded</param>
		/// <returns></returns>
		public static byte[] EncodeUTF8(this string text) {
			return Encoding.UTF8.GetBytes(text);
		}

		/// <summary>
		/// Encode the string as an ANSI formatted string and returns the binary data.
		/// </summary>
		/// <param name="text">The string to be encoded</param>
		/// <param name="codepage">ANSI Codepage to use while reading the file</param>
		/// <returns></returns>
		public static byte[] EncodeANSI(this string text, int codepage = 1252) {
			return Encoding.GetEncoding(codepage).GetBytes(text);
		}

		/// <summary>
		/// Decode the given Unicode binary data into its string representation.
		/// </summary>
		/// <param name="text">The string to be encoded</param>
		/// <returns></returns>
		public static string DecodeUTF8(this byte[] data) {
			return Encoding.UTF8.GetString(data);
		}

		/// <summary>
		/// Decode the given ANSI binary data into its string representation.
		/// </summary>
		/// <param name="text">The string to be encoded</param>
		/// <param name="codepage">ANSI Codepage to use while reading the file</param>
		/// <returns></returns>
		public static string DecodeANSI(this byte[] data, int codepage = 1252) {
			return Encoding.GetEncoding(codepage).GetString(data);
		}

	}
}
