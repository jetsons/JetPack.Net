using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Jetsons.JetPack {
	public static class Messages {

		/// <summary>
		/// Display a messagebox with yes/no buttons, and return true if the user presses yes.
		/// </summary>
		public static bool Question(string message, string title = "Confirmation") {
			return MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
		}
		/// <summary>
		/// Display a messagebox with some information.
		/// </summary>
		public static void Info(string message, string title = "Notice") {
			MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
		}
		/// <summary>
		/// Display a messagebox with an error.
		/// </summary>
		public static void Error(string message, string title = "Error!") {
			MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
		/// <summary>
		/// Display a messagebox with the full stack trace and message of an error.
		/// </summary>
		public static void Error(Exception ex, string title = "Error!") {
			MessageBox.Show(ex.Message + Chars.NL2 + ExceptionToString(ex), title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}

		/// <summary>
		/// Prints the stack of an exception in a neat way.
		/// Implementation taken from stackoverflow and improved.
		/// 
		/// @url		https://stackoverflow.com/questions/4272579/how-to-print-full-stack-trace-in-exception
		/// @author		Oguzhan Kircali
		/// </summary>
		private static string ExceptionToString(Exception x) {

			StringBuilder sb = new StringBuilder();
			bool first = true;

			var st = new StackTrace(x, true);
			var frames = st.GetFrames();

			foreach (var frame in frames) {
				if (frame.GetFileLineNumber() < 1)
					continue;

				if (!first) {
					sb.Append("> ");
				}

				// class name
				sb.Append(frame.GetMethod().DeclaringType.FullName);

				// dot
				sb.Append('.');

				// method
				sb.Append(frame.GetMethod().Name);
				sb.Append("()");

				// line number
				if (frame.GetFileLineNumber() != 0) {
					sb.Append(":");
					sb.Append(frame.GetFileLineNumber());
				}

				sb.AppendLine();
			}

			return sb.ToString();
		}

	}
}
