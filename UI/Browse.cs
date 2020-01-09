#if !STANDARD

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Jetsons.JetPack {
	public static class Browse {

		/// <summary>
		/// Show a folder browser dialog and return the folder path selected. Returns null if the dialog was closed.
		/// </summary>
		public static string Folder(string title) {
			var dialog = new FolderBrowserDialog();
			dialog.Description = title;
			dialog.RootFolder = Environment.SpecialFolder.MyComputer;
			if (dialog.ShowDialog() == DialogResult.OK) {
				return dialog.SelectedPath;
			}
			return null;
		}

		/// <summary>
		/// Show a multiple-file browser dialog and return the file paths selected. Returns null if the dialog was closed.
		/// </summary>
		public static List<string> Files(string title, string filter) {
			var dialog = new OpenFileDialog();
			dialog.Title = title;
			dialog.Filter = filter;
			dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer);
			dialog.DereferenceLinks = true;
			dialog.CheckFileExists = true;
			dialog.Multiselect = true;
			if (dialog.ShowDialog() == DialogResult.OK) {
				return dialog.FileNames.ToList<string>();
			}
			return null;
		}

		/// <summary>
		/// Show a single-file browser dialog and return the file path selected. Returns null if the dialog was closed.
		/// </summary>
		public static string File(string title, string filter, bool saveFile = false) {
			FileDialog dialog = saveFile ? (FileDialog)new SaveFileDialog() : (FileDialog)new OpenFileDialog();
			dialog.Title = title;
			dialog.Filter = filter;
			dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer);
			dialog.DereferenceLinks = true;
			if (!saveFile) {
				dialog.CheckFileExists = true;
			}
			if (dialog.ShowDialog() == DialogResult.OK) {
				return dialog.FileName;
			}
			return null;
		}

	}
}

#endif