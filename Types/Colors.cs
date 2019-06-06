using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jetsons.JetPack {
	public static class Colors {

		public static Color ToColor(this int argb) {
			return Color.FromArgb(argb);
		}
		public static int ToInt(this Color argb) {
			return argb.ToArgb();
		}
		public static int ToInt(this decimal value) {
			return Convert.ToInt32(value);
		}

	}
}
