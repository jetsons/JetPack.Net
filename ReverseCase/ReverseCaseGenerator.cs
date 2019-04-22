using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jetsons.JetPack {
	public class ReverseCaseGenerator {

		public ReverseCaseLearner Learner;
		public ReverseCaseGenerator(ReverseCaseLearner learning) {
			Learner = learning;
		}

		public string Clean(string badString, string joinWordsBy = " ") {

			var original = badString;
			var words = new List<string>();

			// remove all seperator chars
			badString = badString.Remove("_").Remove(" ").ToLower();

			main: while (true) {

				// skip numbers and symbols in the string
				if (!badString[0].IsLetter()) {
					bool foundLetter = false;
					int foundLetterIndex = 0;
					for (int c = 0; c < badString.Length; c++) {
						if (badString[c].IsLetter()) {
							foundLetter = true;
							foundLetterIndex = c;
							break;
						}
					}

					// cut the string at this point
					badString = CutInputString(badString, words, foundLetter, foundLetterIndex);
					if (badString.Length == 0) {
						break;
					}
				}

				// if we found a word at this point, look thru all the learned words
				// and try to find a matching word
				for (int w = 0; w < Learner.Words.Count; w++) {
					var word = Learner.Words[w];

					// if this word is found in the string then cut it
					if (badString.BeginsWith(word, true)) {

						// typically cut at the found word len
						var len = word.Length;

						// fix for "S" issue ("processionstatus" becomes "Processions Tatus" instead of "Procession Status")
						if (len > 2) {
							var foundAtNextChar = badString.Substring(len - 1).BeginsWithAny(Learner.Words);
							if (foundAtNextChar.Found && foundAtNextChar.Term.Length > 2) {

								// now cut at the found word len - 1
								len = (len - 1);
							}
						}

						// cut string
						badString = CutInputString(badString, words, true, len);
						if (badString.Length == 0) {
							break;
						} else {
							w = 0;
							continue;
						}
					}

				}

				// add the remainder
				if (badString.Exists()) {
					words.Add(badString == "s" ? badString : badString.FirstLetterUppercase());
				}

				break;
			}

			var final = words.Join(joinWordsBy);
			return final;
		}

		private string CutInputString(string badString, List<string> words, bool cut, int cutAtIndex) {
			if (cut) {

				// cut the current word short
				var word = badString.Substring(0, cutAtIndex);
				badString = badString.Substring(cutAtIndex);

				// add the found word with proper casing
				words.Add(word.FirstLetterUppercase());

			} else {

				// otherwise just break out of this loop and add the remainder
				words.Add(badString == "s" ? badString : badString.FirstLetterUppercase());
				badString = "";
			}

			return badString;
		}

	}
}
