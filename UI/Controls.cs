#if !STANDARD

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Jetsons.JetPack {
	public static class Controls {

		/// <summary>
		/// Execute the method on the Control's thread only if necessary. Otherwise executes it on this thread.
		/// </summary>
		public static void CrossThread(this Control control, Action method) {
			if (control.InvokeRequired) {
				MethodInvoker invoker = () => { method(); };
				control.BeginInvoke(invoker);
			}
			else {
				method();
			}
		}

		/// <summary>
		/// Set the Control's visibility on its own thread if required. Otherwise sets it on this thread.
		/// </summary>
		public static void VisibleCrossThread(this Control control, bool visible) {
			control.CrossThread(delegate () {
				control.Visible = visible;
			});
		}

		/// <summary>
		/// Set the Control's enabled on its own thread if required. Otherwise sets it on this thread.
		/// </summary>
		public static void EnabledCrossThread(this Control control, bool enabled) {
			control.CrossThread(delegate () {
				control.Enabled = enabled;
			});
		}

	}
}

#endif