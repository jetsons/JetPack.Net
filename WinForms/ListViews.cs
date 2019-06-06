using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Jetsons.JetPack {
	public static class ListViews {

		public static void SetListItems(this ComboBox combo, List<string> items, int defaultItem = 0) {
			foreach (var item in items) {
				combo.Items.Add(item);
			}
			combo.SelectedIndex = defaultItem;
		}


	}
}
