using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace Jetsons.JetPack {

	public static class Files {

		/// <summary>
		/// Load the given file as an array of bytes, or null if it does not exist.
		/// </summary>
		/// <param name="fileName">File path</param>
		/// <param name="onlyBytes">To read a specific number of bytes</param>
		/// <returns></returns>
		public static byte[] LoadBytes(this string fileName, int onlyBytes = 0) {

			// if the file exists
			if (fileName.FileExists()) {

				// open a stream to the file
				FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);

				// calc bytes to read
				int streamLen = (int)stream.Length;
				int length = onlyBytes == 0 ? streamLen : Math.Min(streamLen, onlyBytes);

				// read the file as a byte array
				byte[] data = new byte[length];
				stream.Read(data, 0, length);

				// dispose the stream
				stream.Close();
				stream.Dispose();

				return data;
			}
			return null;
		}

		/// <summary>
		/// Load the given text file as string, or null if it does not exist.
		/// </summary>
		/// <param name="fileName">File path</param>
		/// <param name="codepage">ANSI Codepage to use while reading the file</param>
		/// <param name="unicode">Use unicode as default (true) or ANSI as default (false)</param>
		/// <returns></returns>
		public static string LoadTextFile(this string filename, bool unicode = true, int codepage = 1252) {

			// load the header of the text file to check for BOM
			byte[] bom = LoadBytes(filename, 3);

			// exit if the file does not exist
			if (bom == null) {
				return null;
			}

			// check if BOM is UTF 8
			if (bom.Length >= 3 && bom[0] == 0xEF && bom[1] == 0xBB && bom[2] == 0xBF) {
				return System.IO.File.ReadAllText(filename, Encoding.UTF8);
			}

			if (unicode) {

				// return UTF8 as default if wanted
				return System.IO.File.ReadAllText(filename, Encoding.UTF8);

			}
			else {

				// return ANSI as default
				return System.IO.File.ReadAllText(filename, Encoding.GetEncoding(codepage));
			}
		}

		/// <summary>
		/// Saves the given stream to a path.
		/// </summary>
		/// <param name="buffer">File data</param>
		/// <param name="fileName">File path, overwritten if it already exists</param>
		/// <param name="createFolder">Create the parent folder?</param>
		public static void SaveToFile(this Stream buffer, string fileName, bool createFolder = true) {
			buffer.ToBytes().SaveToFile(fileName, createFolder);
		}

		/// <summary>
		/// Saves the given byte array to a path.
		/// </summary>
		/// <param name="buffer">File data</param>
		/// <param name="fileName">File path, overwritten if it already exists</param>
		/// <param name="createFolder">Create the parent folder?</param>
		public static void SaveToFile(this byte[] buffer, string fileName, bool createFolder = true) {

			// ensure the folder exists if wanted
			if (createFolder) {
				fileName.EnsureFolderExists(true);
			}

			// open a file stream
			FileStream stream = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.Write);

			// write this data into it
			int length = (int)buffer.Length;
			stream.Write(buffer, 0, length);

			// close the stream
			stream.Close();
			stream.Dispose();

		}

		/// <summary>
		/// Save the given byte array to a temporary file and returns the path
		/// </summary>
		/// <param name="buffer">File data</param>
		public static string SaveToTempFile(this byte[] buffer) {
			string path = Path.GetTempPath() + FilePaths.PathSeperator + Path.GetTempFileName();
			buffer.SaveToFile(path);
			return path;
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
				System.IO.File.WriteAllText(fileName, text, Encoding.UTF8);
			}
			else {
				System.IO.File.WriteAllText(fileName, text, Encoding.GetEncoding(codepage));
			}
		}
		/// <summary>
		/// Save the given string to a temporary file and returns the path
		/// </summary>
		/// <param name="buffer">File data</param>
		/// <param name="unicode">Save the file as unicode (true) or ANSI (false)</param>
		/// <param name="codepage">ANSI Codepage to use while reading the file</param>
		public static string SaveToTempFile(this string text, bool unicode = true, int codepage = 1252) {
			string path = Path.GetTempPath() + FilePaths.PathSeperator + Path.GetTempFileName();
			text.SaveToFile(path, false, unicode, codepage);
			return path;
		}

		/// <summary>
		/// Convert a stream into an array of bytes
		/// </summary>
		/// <param name="stream">Input stream</param>
		/// <returns></returns>
		public static byte[] ToBytes(this Stream stream) {

			// if its a memory stream
			if (stream is MemoryStream) {

				// call the platform method
				var mStream = stream as MemoryStream;
				return mStream.ToArray();

			}
			else {

				// rewind to the start of the stream if possible
				if (stream.CanSeek) {
					stream.Seek(0, SeekOrigin.Begin);
				}

				// read from here to the end
				int count = (int)stream.Length;
				var bytes = new byte[count];
				int read, pos = 0;
				while (pos < count && (read = stream.Read(bytes, pos, count - pos)) > 0) {
					pos += read;
				}
				return bytes;

			}
		}

		/// <summary>
		/// Returns true if the given file exists
		/// </summary>
		/// <param name="file">File path</param>
		/// <returns></returns>
		public static bool FileExists(this string file) {
			return file.Exists() && System.IO.File.Exists(file);
		}
		/// <summary>
		/// Deletes a file while supressing any exceptions.
		/// </summary>
		public static void DeleteFile(this string file) {
			try {
				File.Delete(file);
			}
			catch (Exception ex) {
			}
		}

		/// <summary>
		/// Returns true if the given folder exists
		/// </summary>
		/// <param name="folder">Folder path</param>
		/// <returns></returns>
		public static bool FolderExists(this string folder) {
			return folder.Exists() && Directory.Exists(folder);
		}

		/// <summary>
		/// Ensures that the given folder exists (or the containing folder of the given file path)
		/// </summary>
		/// <param name="path">File or folder path</param>
		/// <param name="isFilePath">Is the path parameter a file or folder path?</param>
		public static void EnsureFolderExists(this string path, bool isFilePath = false) {

			// convert the file path to a folder path if wanted
			if (isFilePath) {
				path = path.ParentFolder();
			}

			// if it does not exist
			if (!Directory.Exists(path)) {
				try {

					// create it
					Directory.CreateDirectory(path);

				}
				catch (Exception ex) {
				}
			}
		}
		/// <summary>
		/// Recursively deletes a folder and everything in it.
		/// </summary>
		/// <param name="folder">Folder path</param>
		public static void DeleteFolder(this string folder) {
			if (Directory.Exists(folder)) {
				try {
					Directory.Delete(folder, true);
				}
				catch (Exception ex) {
				}
			}
		}
		/// <summary>
		/// Deletes and recreates the given folder, emptying it in the process.
		/// Ensures that the folder exists.
		/// </summary>
		/// <param name="folder">Folder path</param>
		public static void EmptyFolder(this string folder) {
			folder.DeleteFolder();
			folder.EnsureFolderExists();
		}

		/// <summary>
		/// Returns the file size in bytes.
		/// </summary>
		/// <returns></returns>
		public static long FileSize(this string path) {
			var info = new System.IO.FileInfo(path);
			if (info == null) {
				return 0;
			}
			return info.Length;
		}

		/// <summary>
		/// Returns the file created date.
		/// </summary>
		/// <returns></returns>
		public static DateTime FileCreatedDate(this string path, bool UTC = false) {
			var info = new System.IO.FileInfo(path);
			if (info == null) {
				return DateTime.MinValue;
			}
			return UTC ? info.CreationTimeUtc : info.CreationTime;
		}

		/// <summary>
		/// Returns the file modified date.
		/// </summary>
		/// <returns></returns>
		public static DateTime FileModifiedDate(this string path, bool UTC = false) {
			var info = new System.IO.FileInfo(path);
			if (info == null) {
				return DateTime.MinValue;
			}
			return UTC ? info.LastWriteTimeUtc : info.LastWriteTime;
		}

		/// <summary>
		/// Modifies the file created date.
		/// </summary>
		/// <returns></returns>
		public static void FileSetCreatedDate(this string path, DateTime date, bool UTC = false) {
			if (path.FileExists()) {
				if (UTC) {
					File.SetCreationTimeUtc(path, date);
				}
				else {
					File.SetCreationTime(path, date);
				}
			}
		}

		/// <summary>
		/// Modifies the file modified date.
		/// </summary>
		/// <returns></returns>
		public static void FileSetModifiedDate(this string path, DateTime date, bool UTC = false) {
			if (path.FileExists()) {
				if (UTC) {
					File.SetLastWriteTimeUtc(path, date);
				}
				else {
					File.SetLastWriteTime(path, date);
				}
			}
		}

		/// <summary>
		/// Gets a list of the files in a given directory, filtering by file extensions and optionally by a wildcard filter.
		/// Entirely skips the directories marked for exclusion.
		/// Returns a list of absolute paths.
		/// </summary>
		/// <param name="folderPath">The directory path to look inside</param>
		/// <param name="recursive">Recurse into child directories?</param>
		/// <param name="extension">Only return files of a given extension?</param>
		/// <param name="filter">Only return directories with the given name. Wildcards are supported.</param>
		/// <param name="excludeDirNames">Do not recurse into directories with the given names, useful for skipping system directories.</param>
		/// <returns></returns>
		public static List<string> GetFilesInDirectory(this string folderPath, string extension = null, bool recursive = true, string filter = null, List<string> excludeDirNames = null) {
			return GetFilesInDirectory(folderPath, new List<string> { extension }, recursive, filter, excludeDirNames);
		}

		/// <summary>
		/// Gets a list of the files in a given directory, filtering by multiple extensions and optionally by a wildcard filter.
		/// Entirely skips the directories marked for exclusion.
		/// Returns a list of absolute paths.
		/// </summary>
		/// <param name="folderPath">The directory path to look inside</param>
		/// <param name="recursive">Recurse into child directories?</param>
		/// <param name="extensions">Only return files of a given extension?</param>
		/// <param name="filter">Only return directories with the given name. Wildcards are supported.</param>
		/// <param name="excludeDirNames">Do not recurse into directories with the given names, useful for skipping system directories.</param>
		/// <returns></returns>
		public static List<string> GetFilesInDirectory(this string folderPath, List<string> extensions = null, bool recursive = true, string filter = null, List<string> excludeDirNames = null) {

			//------------------------------------------------------------------------------------------
			// WARNING!
			//------------------------------------------------------------------------------------------
			// We do not use the .NET inbuilt recursive function because it is unsafe,
			// ie it does not provide the ability to skip system directories like "node_modules"
			// and crashes if you attempt to recursively list files in an NPM-managed repo
			//------------------------------------------------------------------------------------------

			// get a file listing, safely going folder by folder
			var results = new List<string>();
			GetFilesLoop(results, folderPath, extensions, recursive, filter, excludeDirNames);
			return results;
		}

		private static void GetFilesLoop(List<string> results, string folderPath, List<string> extensions, bool recursive, string filter, List<string> excludeDirNames = null) {

			// FILES LOOP
			// get a file listing with a filter, and ONLY list files directly under this directory (non recursive)
			var filtering = filter.Exists();
			var matchedDirs = filtering ? Directory.GetFiles(folderPath, filter) : Directory.GetFiles(folderPath);
			if (matchedDirs != null) {
				foreach (var path in matchedDirs) {

					// add only if the extension matches
					if (extensions == null || path.Extension().IsAny(extensions)) {

						// add only the filtered dirs
						results.Add(path);
					}
				}
			}

			// DIRECTORY RECURSION LOOP
			// get a directory listing ONLY of the subdirs directly under this directory
			if (recursive) {
				var dirs = Directory.GetDirectories(folderPath);
				if (dirs != null) {
					foreach (var path in dirs) {

						// DO NOT recurse into excluded dirs!
						if (excludeDirNames == null || !path.Filename().IsAny(excludeDirNames)) {

							// recurse into child directories
							GetFilesLoop(results, path, extensions, recursive, filter, excludeDirNames);

						}
					}
				}
			}

		}


		/// <summary>
		/// Gets a list of the directories in a given directory, optionally filtering by a wildcard filter.
		/// Entirely skips the directories marked for exclusion.
		/// Returns a list of absolute paths.
		/// </summary>
		/// <param name="folderPath">The directory path to look inside</param>
		/// <param name="recursive">Recurse into child directories?</param>
		/// <param name="filter">Only return directories with the given name. Wildcards are supported.</param>
		/// <param name="excludeDirNames">Do not recurse into directories with the given names, useful for skipping system directories.</param>
		/// <returns></returns>
		public static List<string> GetDirectoriesInDirectory(this string folderPath, bool recursive = true, string filter = "**", List<string> excludeDirNames = null) {

			//------------------------------------------------------------------------------------------
			// WARNING!
			//------------------------------------------------------------------------------------------
			// We do not use the .NET inbuilt recursive function because it is unsafe,
			// ie it does not provide the ability to skip system directories like "node_modules"
			// and crashes if you attempt to recursively list directories in an NPM-managed repo
			//------------------------------------------------------------------------------------------

			// get a directory listing, safely going folder by folder
			var results = new List<string>();
			GetDirectoriesLoop(results, folderPath, recursive, filter, excludeDirNames);
			return results;
		}

		private static void GetDirectoriesLoop(List<string> results, string folderPath, bool recursive, string filter, List<string> excludeDirNames = null) {

			// FILTERING LOOP
			// get a directory listing with a filter, and ONLY list dirs directly under this directory (non recursive)
			var filtering = filter.Exists();
			var matchedDirs = filtering ? Directory.GetDirectories(folderPath, filter) : null;
			if (matchedDirs != null) {
				foreach (var path in matchedDirs) {

					// add only if not excluded
					if (excludeDirNames == null || !path.Filename().IsAny(excludeDirNames)) {

						// add only the filtered dirs
						results.Add(path);
					}
				}
			}

			// RECURSION LOOP
			// get a directory listing ONLY of the subdirs directly under this directory..
			// if not already found before by the above loop
			if (recursive || !filtering) {
				var dirs = Directory.GetDirectories(folderPath);
				if (dirs != null) {
					foreach (var path in dirs) {

						// DO NOT recurse into excluded dirs!
						if (excludeDirNames == null || !path.Filename().IsAny(excludeDirNames)) {

							// add this dir if not added by the filtering loop
							if (!filtering) {

								// add
								results.Add(path);
							}

							// recurse into child directories
							if (recursive) {
								GetDirectoriesLoop(results, path, recursive, filter, excludeDirNames);
							}

						}
					}
				}
			}

		}

		/// <summary>
		/// Opens the given filepath with its default application.
		/// </summary>
		public static void OpenFileInDefaultApp(this string path) {
			if (path.FileExists()) {
				Process.Start(path);
			}
		}

		/// <summary>
		/// Opens Windows Explorer to show the contents of the given folder path.
		/// </summary>
		public static void OpenFolderInExplorer(this string path) {
			Windows.WindowsExplorer.LaunchSingle(path);
		}

		/// <summary>
		/// Opens Windows Explorer to the parent directory and selects the given file path.
		/// </summary>
		public static void OpenFileInExplorer(this string path) {
			Windows.WindowsExplorer.LaunchSingle(path, false);
		}

		/// <summary>
		/// Search for the first file in the given folder matching the wildcard string.
		/// Returns the full file path, or null if not found.
		/// </summary>
		public static string SearchForFile(this string path, string wildcards, bool recursive = false) {
			if (path.IsPathValid() && path.FolderExists()) {
				var options = recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
				var results = Directory.GetFiles(path, wildcards, options);
				if (!results.Exists()) {
					return null;
				}
				return results[0];
			}
			return null;
		}

		/// <summary>
		/// Search for all the files in the given folder matching the wildcard string.
		/// Returns the full file path, or a blank list if not found. Never returns null.
		/// </summary>
		public static List<string> SearchForFiles(this string path, string wildcards, bool recursive = false) {
			if (path.IsPathValid() && path.FolderExists()) {
				var options = recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
				var results = Directory.GetFiles(path, wildcards, options);
				if (results.Exists()) {
					return results.ToList<string>();
				}
			}
			return new List<string>();
		}

		/// <summary>
		/// Copies the given file to another path, optionally overwriting it.
		/// Returns true if successful. Returns false if the file was not copied or not overwritten.
		/// </summary>
		public static bool CopyFile(this string path, string targetPath, bool overwrite = false) {

			// if both paths are valid
			if (path.IsPathValid() && targetPath.IsPathValid() && path.FileExists()) {

				// overwriting/skip behaviour
				if (targetPath.FileExists()) {
					if (overwrite) {
						targetPath.DeleteFile();
					}
					else {
						return false;
					}
				}

				// try copying the file
				try {
					File.Copy(path, targetPath);
					return true;
				}
				catch (Exception ex) { }
			}
			return false;
		}

		/// <summary>
		/// Moves the given file to another path, optionally overwriting it.
		/// Returns true if successful. Returns false if the file was not moved or not overwritten.
		/// </summary>
		public static bool MoveFile(this string path, string targetPath, bool overwrite = false) {

			// if both paths are valid
			if (path.IsPathValid() && targetPath.IsPathValid() && path.FileExists()) {

				// overwriting/skip behaviour
				if (targetPath.FileExists()) {
					if (overwrite) {
						targetPath.DeleteFile();
					}
					else {
						return false;
					}
				}

				// try moving the file
				try {
					File.Move(path, targetPath);
					return true;
				}
				catch (Exception ex) { }
			}
			return false;
		}
	}
}