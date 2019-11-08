using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Jetsons.JetPack {
	public static class RichTexts {

		/// <summary>
		/// Finds and highlights all the instances of the given words in the RTF textbox
		/// </summary>
		public static void HighlightAll(this RichTextBox rtf, string terms, bool scrollToFirst = false) {
			bool first = true;
			var terms2 = rtf.Text.ToLower().Contains(terms.ToLower()) ? new List<string> { terms } : terms.CodeWords();
			foreach (string word in terms2) {
				int startindex = 0;
				while (startindex < rtf.TextLength) {
					int wordstartIndex = rtf.Find(word, startindex, RichTextBoxFinds.None);
					if (wordstartIndex != -1) {

						rtf.SelectionStart = wordstartIndex;
						rtf.SelectionLength = word.Length;
						rtf.SelectionBackColor = Color.Yellow;

						if (scrollToFirst && first) {
							first = false;
							rtf.ScrollToCaret();
						}
					}
					else {
						break;
					}
					startindex += wordstartIndex + word.Length;
				}
			}
		}


	}
}
