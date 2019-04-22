using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jetsons.JetPack {
	public class ReverseCaseLearner {

		private string WordsFilePath;
		public List<string> Words = new List<string>();

		public ReverseCaseLearner(string filePath) {
			WordsFilePath = filePath;
		}

		/// <summary>
		/// Learn a multi-word string (seperated by a specific seperator)
		/// </summary>
		public void LearnMultiWord(string value, string seperator) {

			// only learn words containing underscores as the rest are invalid
			if (value.Contains(seperator)) {

				List<string> words = value.SmartSplit(seperator, true, true);

				foreach (string word in words) {
					LearnWord(word);
				}

			}
		}
		/// <summary>
		/// Learn a single-word string
		/// </summary>
		/// <param name="value"></param>
		public void LearnWord(string value) {

			// only take 2-char strings and longer
			if (value.Length <= 1) {
				return;
			}

			// skip numeric junk
			if (value[0].IsNumber()) {
				return;
			}
			if (value.Last().IsNumber()) {
				return;
			}

			value = value.ToLower();
			Words.AddOnce(value);

		}

		/// <summary>
		/// Prepare the word list for checking
		/// </summary>
		public void Compile() {
			Words = Words.SortByLength(false);
		}
		public void SaveToDisk() {
			Words.Sort();
			Words.Join("\n").SaveToFile(WordsFilePath);
		}
		public void LoadFromDisk() {
			Words = WordsFilePath.LoadTextFile().Lines(true, true);
		}

	}
}
