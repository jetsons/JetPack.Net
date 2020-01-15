#if !STANDARD

using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jetsons.JetPack {
	public class FileAssociation {
		public string Extension { get; set; }
		public string UniqueId { get; set; }
		public string Description { get; set; }
	}

	/// <summary>
	/// Original implementation from stackoverflow.
	/// 
	/// @author		Kirill Osenkov
	/// @url		https://stackoverflow.com/a/44816953/11170692
	/// </summary>
	public class FileTypeHandlers {

		// needed so that Explorer windows get refreshed after the registry is updated
		[System.Runtime.InteropServices.DllImport("Shell32.dll")]
		private static extern int SHChangeNotify(int eventId, int flags, IntPtr item1, IntPtr item2);

		private const int SHCNE_ASSOCCHANGED = 0x8000000;
		private const int SHCNF_FLUSH = 0x1000;
		

		/// <summary>
		/// Ensure that the given file extensions are bound to the current application, or the given exePath.
		/// 
		/// Sample: new FileAssociation {Extension = ".ucs",ProgId = "UCS_Editor_File",FileTypeDescription = "UCS File"}
		/// </summary>
		/// <param name="associations"></param>
		/// <param name="exePath"></param>
		public static void Register(FileAssociation[] associations, string exePath = null) {
			
			if (exePath == null) {
				exePath = Process.GetCurrentProcess().MainModule.FileName;
			}

			bool madeChanges = false;
			foreach (var assoc in associations) {
				madeChanges |= SetAssociation(assoc.Extension, assoc.UniqueId, assoc.Description, exePath);
			}

			if (madeChanges) {
				SHChangeNotify(SHCNE_ASSOCCHANGED, SHCNF_FLUSH, IntPtr.Zero, IntPtr.Zero);
			}
		}

		private static bool SetAssociation(string extension, string progId, string fileTypeDescription, string applicationFilePath) {
			bool madeChanges = false;
			madeChanges |= SetKeyDefaultValue(@"Software\Classes\" + extension, progId);
			madeChanges |= SetKeyDefaultValue(@"Software\Classes\" + progId, fileTypeDescription);
			madeChanges |= SetKeyDefaultValue($@"Software\Classes\{progId}\shell\open\command", "\"" + applicationFilePath + "\" \"%1\"");
			return madeChanges;
		}

		private static bool SetKeyDefaultValue(string keyPath, string value) {
			using (var key = Registry.CurrentUser.CreateSubKey(keyPath)) {
				if (key.GetValue(null) as string != value) {
					key.SetValue(null, value);
					return true;
				}
			}

			return false;
		}
	}
}

#endif