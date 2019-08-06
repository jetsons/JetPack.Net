using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Jetsons.JetPack {
	public static class Uris {

		/// <summary>
		/// Downloads the given URL to the given filepath.
		/// If async is true, the file is downloaded in the background and control returns to the thread.
		/// Returns true if the file was (re)downloaded.
		/// </summary>
		public static bool DownloadURLToFile(this string url, string filePath, bool overwrite = true, bool async = false) {
			if (!overwrite && filePath.FileExists()) {
				return false;
			}
			using (var client = new WebClient()) {
				if (async) {
					client.DownloadFileAsync(new Uri(url), filePath);
				}
				else {
					client.DownloadFile(url, filePath);
				}
				return true;
			}
		}
	}
}
