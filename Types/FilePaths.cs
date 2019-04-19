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
			return path.Exists() && path.Length > 4 && path[0].IsLetter() && path[1] == ':' && path[2] == PathSeperator;
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
			return Path.Combine(path, fileOrFolder);
		}
		
	}
}