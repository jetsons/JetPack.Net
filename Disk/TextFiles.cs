using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jetsons.JetPack {
	public static class TextFiles {
		
		/// <summary>
		/// Load the given text file as string, or null if it does not exist.
		/// </summary>
		/// <param name="fileName">File path</param>
		/// <param name="codepage">ANSI Codepage to use while reading the file</param>
		/// <param name="unicode">Use unicode as default (true) or ANSI as default (false)</param>
		/// <returns></returns>
		public static string LoadTextFile(this string filename, bool unicode = true, int codepage = 1252) {

			// load the header of the text file to check for BOM
			byte[] bom = filename.LoadBytes(3);

			// exit if the file does not exist
			if (bom == null) {
				return null;
			}

			// check if BOM is UTF 8
			if (bom.Length >= 3 && bom[0] == 0xEF && bom[1] == 0xBB && bom[2] == 0xBF) {
				return File.ReadAllText(filename, Encoding.UTF8);
			}

			if (unicode) {

				// return UTF8 as default if wanted
				return File.ReadAllText(filename, Encoding.UTF8);

			}
			else {

				// return ANSI as default
				return File.ReadAllText(filename, Encoding.GetEncoding(codepage));
			}
		}

		/// <summary>
		/// Save the given string to a text file
		/// </summary>
		/// <param name="buffer">File data</param>
		/// <param name="fileName">File path, overwritten if it already exists</param>
		/// <param name="createFolder">Create the parent folder?</param>
		/// <param name="unicode">Save the file as unicode (true) or ANSI (false)</param>
		/// <param name="codepage">ANSI Codepage to use while reading the file</param>
		public static void SaveToFile(this string text, string fileName, bool createFolder = false, bool unicode = true, int codepage = 1252) {

			// ensure the folder exists if wanted
			if (createFolder) {
				fileName.EnsureFolderExists(true);
			}

			// save the string to file
			if (unicode) {
				File.WriteAllText(fileName, text, Encoding.UTF8);
			}
			else {
				File.WriteAllText(fileName, text, Encoding.GetEncoding(codepage));
			}
		}

		/// <summary>
		/// Save the given string to a temporary file and returns the path
		/// </summary>
		/// <param name="text">File content</param>
		/// <param name="unicode">Save the file as unicode (true) or ANSI (false)</param>
		/// <param name="codepage">ANSI Codepage to use while reading the file</param>
		/// <param name="ext">The extension of the new temporary file</param>
		public static string SaveToTempFile(this string text, bool unicode = true, int codepage = 1252, string ext = "txt") {
			var path = FilePaths.CreateTempPath(ext);
			text.SaveToFile(path, false, unicode, codepage);
			return path;
		}


	}
}
