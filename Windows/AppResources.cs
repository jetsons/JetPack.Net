using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;

namespace Jetsons.JetPack {
	public static class AppResources {
		
		/// <summary>
		/// Extracts the resource at the given namespace path and returns it as a string
		/// </summary>
		public static string GetText(string resourcePath) {
			var assembly = Assembly.GetEntryAssembly();
			using (Stream stream = assembly.GetManifestResourceStream(resourcePath)) {
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
			var assembly = Assembly.GetEntryAssembly();
			using (Stream stream = assembly.GetManifestResourceStream(resourcePath)) {
				return stream.ToBytes();
			}
		}

#if !STANDARD

		/// <summary>
		/// Extracts the resource at the given namespace path and returns it as an Icon
		/// </summary>
		public static Icon GetIcon(string resourcePath) {
			var assembly = Assembly.GetEntryAssembly();
			using (Stream stream = assembly.GetManifestResourceStream(resourcePath)) {
				return new Icon(stream);
			}
		}

		/// <summary>
		/// Extracts the resource at the given namespace path and returns it as a Bitmap
		/// </summary>
		public static Bitmap GetBitmap(string resourcePath) {
			var assembly = Assembly.GetEntryAssembly();
			using (Stream stream = assembly.GetManifestResourceStream(resourcePath)) {
				return new Bitmap(stream);
			}
		}

#endif

	}
}
