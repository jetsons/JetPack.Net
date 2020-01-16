using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jetsons.JetPack {
	public static class BinaryFiles {
		
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
		/// <param name="ext">The extension of the new temporary file</param>
		public static string SaveToTempFile(this byte[] buffer, string ext) {
			var path = FilePaths.CreateTempPath(ext);
			buffer.SaveToFile(path);
			return path;
		}


	}
}
