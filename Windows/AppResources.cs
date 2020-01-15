using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;

namespace Jetsons.JetPack {
	public static class AppResources {

		private static Assembly EntryAssembly;

		/// <summary>
		/// Extracts the resource at the given namespace path and returns it as a string
		/// </summary>
		public static string GetText(string resourcePath) {
			if (EntryAssembly == null) {
				EntryAssembly = Assembly.GetEntryAssembly();
			}
			using (Stream stream = EntryAssembly.GetManifestResourceStream(resourcePath)) {
				using (StreamReader reader = new StreamReader(stream)) {
					string result = reader.ReadToEnd();
					return result;
				}
			}
		}

		/// <summary>
		/// Extracts the resource at the given namespace path and returns it as a byte array
		/// </summary>
		public static byte[] GetBytes(string resourcePath) {
			if (EntryAssembly == null) {
				EntryAssembly = Assembly.GetEntryAssembly();
			}
			using (Stream stream = EntryAssembly.GetManifestResourceStream(resourcePath)) {
				return stream.ToBytes();
			}
		}

#if !STANDARD

		/// <summary>
		/// Extracts the resource at the given namespace path and returns it as an Icon
		/// </summary>
		public static Icon GetIcon(string resourcePath) {
			if (EntryAssembly == null) {
				EntryAssembly = Assembly.GetEntryAssembly();
			}
			using (Stream stream = EntryAssembly.GetManifestResourceStream(resourcePath)) {
				return new Icon(stream);
			}
		}

		/// <summary>
		/// Extracts the resource at the given namespace path and returns it as a Bitmap
		/// </summary>
		public static Bitmap GetBitmap(string resourcePath) {
			if (EntryAssembly == null) {
				EntryAssembly = Assembly.GetEntryAssembly();
			}
			using (Stream stream = EntryAssembly.GetManifestResourceStream(resourcePath)) {
				return new Bitmap(stream);
			}
		}

#endif

	}
}
