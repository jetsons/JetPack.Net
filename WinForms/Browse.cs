using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Jetsons.JetPack {
	public static class Browse {

		public static string Folder(string title) {
			var dialog = new FolderBrowserDialog();
			dialog.Description = title;
			dialog.RootFolder = Environment.SpecialFolder.MyComputer;
			if (dialog.ShowDialog() == DialogResult.OK) {
				return dialog.SelectedPath;
			}
			return null;
		}

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
