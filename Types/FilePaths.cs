using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Jetsons.JetPack {
	public static class FilePaths {

		/// <summary>
		/// Set depending on your OS
		/// </summary>
		public static char PathSeperator = Path.DirectorySeparatorChar;

		/// <summary>
		/// Returns true if the given file or folder path is a valid absolute Windows filepath
		/// </summary>
		/// <param name="path">File or folder path</param>
		/// <returns></returns>
		public static bool IsPathValid(this string path) {
			if (path.Exists() && path.Length > 4) {

				// check if local path
				if (path[0].IsLetter() && path[1] == ':' && path[2] == PathSeperator) {
					return true;
				}

				// check if network path
				if (path[0] == '\\' && path[1] == '\\' && path[2].IsLetter()) {
					return true;
				}

			}
			return false;
		}

		/// <summary>
		/// Returns true if the given path ends with a slash/backslash
		/// </summary>
		/// <param name="path">File or folder path</param>
		/// <returns></returns>
		public static bool IsFolderPath(this string path) {
			return path.IsPathValid() && path.EndsWith(PathSeperator);
		}

		/// <summary>
		/// Returns only the filename and extension of the given a file path
		/// </summary>
		/// <param name="path">File path</param>
		/// <param name="defaultValue">Value to return if path is invalid or not a file path</param>
		/// <returns></returns>
		public static string FilenameAndExt(this string path, string defaultValue = "") {

			// exit if invalid path
			if (!path.IsPathValid()) {
				return defaultValue;
			}

			// exit if folder path
			if (path.EndsWith(PathSeperator)) {
				return defaultValue;
			}

			// get the filename
			string name = path.AfterLast(PathSeperator.ToString(), false);
			return name == "" ? defaultValue : name;
		}

		/// <summary>
		/// Returns only the filename of the given a file path
		/// </summary>
		/// <param name="path">File path</param>
		/// <param name="defaultValue">Value to return if path is invalid or not a file path</param>
		/// <returns></returns>
		public static string Filename(this string path, string defaultValue = "") {

			// get the filename and extension
			string name = FilenameAndExt(path, null);
			if (name.Exists()) {

				// remove the extension
				return name.BeforeLast(".");
			}
			return defaultValue;
		}

		/// <summary>
		/// Returns only the folder name of the given a file path.
		/// If the input path ends with a slash, then the name of the folder before the slash is returned.
		/// In all other cases the input path is assumed to be a file path, and the parent folder name is returned.
		/// </summary>
		/// <param name="path">File path</param>
		/// <returns></returns>
		public static string FolderName(this string path) {
			return path.BeforeLast(PathSeperator.ToString()).AfterLast(PathSeperator.ToString());
		}

		/// <summary>
		/// Returns only the extension of the given a file path
		/// </summary>
		/// <param name="path">File path</param>
		/// <param name="defaultValue">Value to return if path is invalid or not a file path</param>
		/// <returns></returns>
		public static string Extension(this string path, string defaultValue = "") {

			// exit if invalid path
			if (path == null) {
				return defaultValue;
			}

			// get the extension
			string ext = path.AfterLast(".", false).ToLower();
			return ext == "" ? defaultValue : ext;
		}

		/// <summary>
		/// Returns the parent folder path with the last slash
		/// </summary>
		/// <param name="path">File or folder path</param>
		/// <param name="returnBlankIfFile">Returns blank if its a file path (true) or returns the input path (false)</param>
		/// <returns></returns>
		public static string ParentFolder(this string path, bool returnBlankIfFile = false) {

			// exit if invalid path
			if (!path.IsPathValid()) {
				return "";
			}

			// exit quickly if its a folder
			if (path.IsFolderPath()) {
				return path;
			}

			// return the folder
			if (path.Contains(PathSeperator)) {
				return path.BeforeLast(PathSeperator.ToString());
			} else {
				return returnBlankIfFile ? "" : path;
			}
		}


		/// <summary>
		/// Add an element to this file path
		/// </summary>
		/// <param name="path">File or folder path</param>
		/// <param name="fileOrFolder">File or folder name to add</param>
		public static string AddPath(this string path, string fileOrFolder) {
			
			// fast mode if there is exactly one slash between path & file
			var pathHasSep = path.EndsWith(PathSeperator);
			var fileHasSep = fileOrFolder.BeginsWith(PathSeperator);
			if ((pathHasSep && !fileHasSep) || (!pathHasSep && fileHasSep)) {
				return path + fileOrFolder;
			}

			// slow mode if slashes need to be fixed
			if (pathHasSep && fileHasSep) {
				return path + fileOrFolder.Substring(1);
			}
			if (!pathHasSep && !fileHasSep) {
				return path + PathSeperator + fileOrFolder;
			}

			// nothing
			return null;
		}

		/// <summary>
		/// Modifies only the filename and extension of the given a file path, and returns the complete path.
		/// </summary>
		/// <param name="path">File path</param>
		/// <param name="filenameAndExt">Value to set as the new filename and extension</param>
		/// <returns></returns>
		public static string SetFilenameAndExt(this string path, string filenameAndExt) {

			// exit if invalid path
			if (!path.IsPathValid()) {
				return path;
			}

			// exit if folder path
			if (path.EndsWith(PathSeperator)) {
				return path;
			}

			// make the new path
			string prefix = path.BeforeLast(PathSeperator.ToString(), false);
			return prefix == "" ? path : prefix + PathSeperator + filenameAndExt;
		}

		/// <summary>
		/// Modifies only the filename of the given a file path, and returns the complete path.
		/// </summary>
		/// <param name="path">File path</param>
		/// <param name="filename">Value to set as the new filename</param>
		/// <returns></returns>
		public static string SetFilename(this string path, string filename) {

			// exit if invalid path
			if (!path.IsPathValid()) {
				return path;
			}

			// exit if folder path
			if (path.EndsWith(PathSeperator)) {
				return path;
			}

			// make the new path
			string prefix = path.BeforeLast(PathSeperator.ToString(), false);
			return prefix == "" ? path : prefix + PathSeperator + filename + "." + path.AfterLast(".");
		}

		/// <summary>
		/// Modifies only the extension of the given a file path, and returns the complete path.
		/// </summary>
		/// <param name="path">File path</param>
		/// <param name="extension">Value to set as the new extension</param>
		/// <returns></returns>
		public static string SetExtension(this string path, string extension) {

			// exit if invalid path
			if (path == null) {
				return path;
			}

			// make the new path
			string prefix = path.BeforeLast(".", false);
			return prefix == "" ? path : prefix + "." + extension;
		}

		/// <summary>
		/// Replaces all forward and backward slashes with the one you require.
		/// </summary>
		/// <param name="path">File path</param>
		/// <param name="slash">Slash character that you require. Must be forward or backward slash.</param>
		/// <returns></returns>
		public static string SetSlash(this string path, char slash) {

			// exit if invalid path
			if (path == null) {
				return path;
			}

			// make the new path
			if (slash == '\\') {
				return path.Replace('/', '\\');
			} else if (slash == '/') {
				return path.Replace('\\', '/');
			} else return path;
		}

	}
}