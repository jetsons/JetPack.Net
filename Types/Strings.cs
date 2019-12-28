using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Jetsons.JetPack {
	public static class Strings {

		/// <summary>
		/// Returns the alternate value if the string is empty or null
		/// </summary>
		/// <param name="text">String to check</param>
		/// <param name="alternate">The alternate value to return</param>
		/// <param name="trim">Trim the text before checking if its blank</param>
		/// <returns></returns>
		public static string Or(this string text, string alternate, bool trim = false) {
			if (text.Exists(trim)) {
				return text;
			}
			return alternate;
		}

		/// <summary>
		/// Checks if the given string exists and has any characters in it
		/// </summary>
		/// <param name="text">String to check</param>
		/// <param name="trim">Trim the text before checking if its blank</param>
		/// <returns></returns>
		public static bool Exists(this string text, bool trim = false) {
			if (text != null) {
				if (trim) {
					return text.Trim().Length > 0;
				}
				else {
					return text.Length > 0;
				}
			}
			return false;
		}

		/// <summary>
		/// Returns the substring between the given start and end search terms
		/// </summary>
		/// <param name="text">String to check</param>
		/// <param name="startTerm">Start marker</param>
		/// <param name="endTerm">End marker</param>
		/// <param name="returnAll">If search term is not found, return all or nothing?</param>
		/// <param name="startAt">Which char index to start searching at</param>
		/// <param name="forward">Search from left-to-right (true) or right-to-left (false)?</param>
		/// <returns></returns>
		public static string Between(this string text, string startTerm, string endTerm, bool returnAll = false, int startAt = 0, bool forward = true) {
			int start;
			if (!forward) {
				start = text.LastIndexOf(startTerm, text.Length - startAt, StringComparison.Ordinal);
				if (startTerm == endTerm) {
					start = text.LastIndexOf(startTerm, start - 1, StringComparison.Ordinal);
				}
			}
			else {
				start = text.IndexOf(startTerm, startAt, StringComparison.Ordinal);
			}
			if (start != -1) {
				start += startTerm.Length;
				int end = text.IndexOf(endTerm, start + 1, StringComparison.Ordinal);
				if (end != -1) {
					return text.Substring(start, end - start);
				}
			}
			return returnAll ? text : "";
		}

		/// <summary>
		/// Returns the substring before the given character index
		/// </summary>
		/// <param name="text">String to check</param>
		/// <param name="charIndex">Char index</param>
		/// <param name="inclusive">Return the text including the given char index, or excluding it?</param>
		/// <returns></returns>
		public static string BeforeIndex(this string text, int charIndex = 0, bool inclusive = false) {
			if (text == null || charIndex < 0) {
				return "";
			}
			if (charIndex > text.Length) {
				return text;
			}
			return inclusive ? text.Substring(0, charIndex + 1) : text.Substring(0, charIndex);
		}

		/// <summary>
		/// Returns the substring after the given character index
		/// </summary>
		/// <param name="text">String to check</param>
		/// <param name="charIndex">Char index</param>
		/// <param name="inclusive">Return the text including the given char index, or excluding it?</param>
		/// <returns></returns>
		public static string AfterIndex(this string text, int charIndex = 0, bool inclusive = false) {
			if (text == null || charIndex > text.Length) {
				return "";
			}
			if (charIndex < 0) {
				return text;
			}
			return inclusive ? text.Substring(charIndex) : text.Substring(charIndex + 1);
		}

		/// <summary>
		/// Returns the substring after the *first* occurrence of the provided character in the string
		/// </summary>
		/// <param name="text">String to check</param>
		/// <param name="searchTerm">Search term, not included in result</param>
		/// <param name="returnAll">If search term is not found, return all or nothing?</param>
		/// <returns></returns>
		public static string After(this string text, string searchTerm, bool returnAll = true) {
			int start = text.IndexOf(searchTerm, StringComparison.Ordinal);
			if (start == -1) {
				return returnAll ? text : "";
			}
			start += searchTerm.Length;
			return text.Substring(start);
		}

		/// <summary>
		/// Returns the substring after the last occurence of the search term
		/// </summary>
		/// <param name="text">String to check</param>
		/// <param name="searchTerm">Search term, not included in result</param>
		/// <param name="returnAll">If search term is not found, return all or nothing?</param>
		/// <returns></returns>
		public static string AfterLast(this string text, string searchTerm, bool returnAll = true) {
			int start = text.LastIndexOf(searchTerm, StringComparison.Ordinal);
			if (start == -1) {
				return returnAll ? text : "";
			}
			start += searchTerm.Length;
			return text.Substring(start);
		}

		/// <summary>
		/// Returns the substring before the *first* occurrence of the search term
		/// </summary>
		/// <param name="text">String to check</param>
		/// <param name="searchTerm">Search term, not included in result</param>
		/// <param name="returnAll">If search term is not found, return all or nothing?</param>
		/// <returns></returns>
		public static string Before(this string text, string searchTerm, bool returnAll = true) {
			int start = text.IndexOf(searchTerm, StringComparison.Ordinal);
			if (start == -1) {
				return returnAll ? text : "";
			}
			return text.Substring(0, start);
		}

		/// <summary>
		/// Returns everything before the last occurrence of the provided character in the string
		/// </summary>
		/// <param name="text">String to check</param>
		/// <param name="searchTerm">Search term, not included in result</param>
		/// <param name="returnAll">If search term is not found, return all or nothing?</param>
		/// <returns></returns>
		public static string BeforeLast(this string text, string searchTerm, bool returnAll = true) {
			int start = text.LastIndexOf(searchTerm, StringComparison.Ordinal);
			if (start == -1) {
				return returnAll ? text : "";
			}
			return text.Substring(0, start);
		}
		/// <summary>
		/// Returns true if the string contains the given character
		/// </summary>
		/// <param name="text">String to check</param>
		/// <param name="term">Search term</param>
		/// <returns></returns>
		public static bool Contains(this string text, char term) {
			return text.IndexOf(term) > -1;
		}
		/// <summary>
		/// Returns true if the string contains the given string (faster than platform implementation)
		/// </summary>
		/// <param name="text">String to check</param>
		/// <param name="term">Search term</param>
		/// <returns></returns>
		public static bool Contains(this string text, string term) {
			return text.Length >= term.Length && text.IndexOf(term) > -1;
		}
		/// <summary>
		/// Returns true if the string ends with the given string (faster than platform implementation)
		/// </summary>
		/// <param name="text">String to check</param>
		/// <param name="term">Search term</param>
		/// <returns></returns>
		public static bool EndsWith(this string text, string term) {
			return text.Length >= term.Length && text.EndsWith(term, StringComparison.Ordinal);
		}
		/// <summary>
		/// Returns true if the string ends with the given character
		/// </summary>
		/// <param name="text">String to check</param>
		/// <param name="term">Search term</param>
		/// <returns></returns>
		public static bool EndsWith(this string text, char term) {
			return text.Length > 0 && text[text.Length - 1] == term;
		}
		/// <summary>
		/// Returns true if the string ends with the given search term (faster than platform implementation)
		/// </summary>
		/// <param name="text">String to check</param>
		/// <param name="postfix">Search term</param>
		/// <param name="caseSensitive">Use case sensitive search?</param>
		/// <returns></returns>
		public static bool EndsWith(this string text, string postfix, bool caseSensitive = true) {

			// exit quickly if invalid data
			var textLen = text.Length;
			var termLen = postfix.Length;
			if (postfix == null || termLen == 0 || textLen == 0 || termLen > textLen) {
				return false;
			}

			// check quickly if 1 char
			if (caseSensitive && termLen == 1) {
				return text[text.Length - 1] == postfix[0];
			}

			// check equality if same length
			var opts = caseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase;
			if (termLen == textLen) {
				return string.Equals(text, postfix, opts);
			}

			// check if the string ends with the given term
			return text.EndsWith(postfix, opts);
		}
		/// <summary>
		/// Returns true if the string begins with the given character
		/// </summary>
		/// <param name="text">String to check</param>
		/// <param name="term">Search term</param>
		/// <returns></returns>
		public static bool BeginsWith(this string text, char term) {
			return text.Length > 0 && text[0] == term;
		}
		/// <summary>
		/// Returns true if the string begins with the given search term (faster than platform implementation)
		/// </summary>
		/// <param name="text">String to check</param>
		/// <param name="prefix">Search term</param>
		/// <param name="caseSensitive">Use case sensitive search?</param>
		/// <returns></returns>
		public static bool BeginsWith(this string text, string prefix, bool caseSensitive = true) {

			// exit quickly if invalid data
			var textLen = text.Length;
			var termLen = prefix.Length;
			if (prefix == null || termLen == 0 || textLen == 0 || termLen > textLen) {
				return false;
			}

			// check quickly if 1 char
			if (caseSensitive && termLen == 1) {
				return text[0] == prefix[0];
			}

			// check equality if same length
			var opts = caseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase;
			if (termLen == textLen) {
				return string.Equals(text, prefix, opts);
			}

			// check if the string starts with the given term
			return text.StartsWith(prefix, opts);
		}

		/// <summary>
		/// Split the string by the given seperator, optionally removing the blank elements and trimming the elements
		/// </summary>
		/// <param name="text">Input text</param>
		/// <param name="seperator">Delimiter to split by, not included in final list</param>
		/// <param name="deleteBlanks">Delete the blank elements?</param>
		/// <param name="trimValues">Trim the elements?</param>
		/// <returns></returns>
		public static List<string> SmartSplit(this string text, string seperator, bool deleteBlanks = false, bool trimValues = false) {

			// split by seperator, delete blanks
			var options = deleteBlanks && !trimValues ? StringSplitOptions.RemoveEmptyEntries : StringSplitOptions.None;
			var list = new List<string>(text.Split(new string[] { seperator }, options));

			// special algo to trim and delete blanks (need to trim first)
			if (trimValues && deleteBlanks) {
				var results = new List<string>();
				foreach (var item in list) {
					var item2 = item.Trim();
					if (item2.Length > 0) {
						results.Add(item2);
					}
				}
				return results;
			}

			// trim if wanted
			if (trimValues) {
				for (int i = 0; i < list.Count; i++) {
					list[i] = list[i].Trim();
				}
			}

			// return the elements
			return list;
		}
		/// <summary>
		/// Gets the characters in a string
		/// </summary>
		/// <param name="text">Input text</param>
		/// <returns></returns>
		public static List<string> Chars(this string text) {
			List<string> chars = new List<string>();
			foreach (char ch in text) {
				chars.Add(ch.ToString());
			}
			return chars;
		}

		/// <summary>
		/// Gets the lines in a string
		/// </summary>
		/// <param name="text">Input text</param>
		/// <param name="deleteBlanks">Delete the blank lines?</param>
		/// <param name="trimLines">Trim the lines?</param>
		/// <returns></returns>
		public static List<string> Lines(this string text, bool deleteBlanks = false, bool trimLines = false) {
			string sep;
			if (text.Contains("\r\n")) {
				sep = "\r\n";
			}
			else if (text.Contains('\r')) {
				sep = "\r";
			}
			else {
				sep = "\n";
			}
			return SmartSplit(text, sep, deleteBlanks, trimLines);
		}

		/// <summary>
		/// Gets the words in a string, by splitting the string by single or multiple spaces, tabs and newlines.
		/// </summary>
		/// <param name="text">Input text</param>
		/// <param name="symbols">Split at symbols too?</param>
		/// <returns></returns>
		public static List<string> Words(this string text, bool symbols = false) {

			// init
			List<string> words = new List<string>();
			StringBuilder word = new StringBuilder();

			// per char
			foreach (char ch in text) {

				// if its a space or newline or tab
				// skip the char but add the collected word 
				if (ch == '\r' || ch == '\n' || ch == ' ' || ch == '\t' || (symbols && Char.IsSymbol(ch))) {

					// add word
					if (word.Length > 0) {
						words.Add(word.ToString());
						word = new StringBuilder();
					}

				}
				else {

					// store any other character
					word.Append(ch);
				}
			}

			// add the last word
			if (word.Length > 0) {
				words.Add(word.ToString());
			}

			return words;
		}

		/// <summary>
		/// Gets the words in a camelCase, PascalCase or snake_case string.
		/// Splits by single or multiple spaces, tabs, newlines, underscores and seperates the camelCase words.
		/// </summary>
		/// <param name="text">Input text</param>
		/// <returns></returns>
		public static List<string> CodeWords(this string text) {
			return text.SplitCamelCase().Join(" ").Words(true);
		}

		/// <summary>
		/// Interprets the string as a camelCase or PascalCase string and returns an array of the individual words
		/// </summary>
		/// <param name="text">Input text</param>
		/// <returns></returns>
		public static List<string> SplitCamelCase(this string text) {
			bool lastWasLower = false;
			List<string> words = new List<string>();
			StringBuilder word = new StringBuilder();

			// per char
			foreach (char ch in text) {

				// if its a space or newline or tab
				// skip the char but add the collected word 
				if (ch == '\r' || ch == '\n' || ch == ' ' || ch == '\t' || ch == '_') {

					// add word
					if (word.Length > 0) {
						words.Add(word.ToString());
						word = new StringBuilder();
					}

				}
				else {

					// on switchover from lower to upper
					if (Char.IsUpper(ch) && lastWasLower) {

						// add word
						if (word.Length > 0) {
							words.Add(word.ToString());
							word = new StringBuilder();
						}
					}

					// check
					lastWasLower = Char.IsLower(ch);

					// store any other character
					word.Append(ch);
				}
			}

			// add last word
			if (word.Length > 0) {
				words.Add(word.ToString());
			}

			return words;
		}

		/// <summary>
		/// Transforms the words into camelCase or PascalCase. Requires the input string to be seperated by spaces or newlines.
		/// </summary>
		/// <param name="text">Input text</param>
		/// <param name="pascalCase">Whether you want the result as PascalCase (true) or camelCase (false)</param>
		/// <returns></returns>
		public static string ToCamelCase(this string text, bool pascalCase = false) {
			var sb = new StringBuilder();

			var firstWord = true;

			// per word
			foreach (string word in text.Words()) {
				if (firstWord && !pascalCase) {
					sb.Append(word.ToLower());
				}
				else {
					sb.Append(word.ToLower().FirstLetterUppercase());
				}
				firstWord = false;
			}

			return sb.ToString();
		}

		/// <summary>
		/// Gets the number of lines in a string (high performance version)
		/// </summary>
		/// <param name="text">Input text</param>
		/// <returns></returns>
		public static int LineCount(this string text) {
			int lineCount = 1;
			char detectedEOL = '0';
			foreach (char currentChar in text) {

				// once EOL is detected
				if (detectedEOL != '0') {
					if (currentChar == detectedEOL) {
						lineCount++;
					}

				}
				else {

					// first detect the EOL character
					if (currentChar == '\r' || currentChar == '\n') {
						detectedEOL = currentChar;
						lineCount++;
					}
				}
			}
			return lineCount;
		}

		/// <summary>
		/// Checks if the string begins with the given prefix and removes it, otherwise no changes are done
		/// </summary>
		/// <param name="text">String to check</param>
		/// <param name="prefix">Prefix to remove</param>
		/// <param name="caseSensitive">Use case sensitive search?</param>
		/// <returns></returns>
		public static string RemovePrefix(this string text, string prefix, bool caseSensitive = true) {
			if (text.BeginsWith(prefix, caseSensitive)) {
				return text.Substring(prefix.Length);
			}
			return text;
		}
		/// <summary>
		/// Checks if the string ends with the given postfix and removes it, otherwise no changes are done
		/// </summary>
		/// <param name="text">String to check</param>
		/// <param name="postfix">Postfix to remove</param>
		/// <param name="caseSensitive">Use case sensitive search?</param>
		/// <returns></returns>
		public static string RemovePostfix(this string text, string postfix, bool caseSensitive = true) {
			if (text.EndsWith(postfix, caseSensitive)) {
				return text.Substring(0, text.Length - postfix.Length);
			}
			return text;
		}

		/// <summary>
		/// Ensures that the string begins with the given prefix. If the prefix is already present, no changes are done.
		/// </summary>
		/// <param name="text">String to check</param>
		/// <param name="prefix">Prefix to add</param>
		/// <param name="caseSensitive">Use case sensitive search?</param>
		/// <returns></returns>
		public static string Prefix(this string text, string prefix, bool caseSensitive = true) {
			if (!text.BeginsWith(prefix, caseSensitive)) {
				return prefix + text;
			}
			return text;
		}
		/// <summary>
		/// Ensures that the string ends with the given postfix. If the postfix is already present, no changes are done.
		/// </summary>
		/// <param name="text">String to check</param>
		/// <param name="postfix">Postfix to add</param>
		/// <param name="caseSensitive">Use case sensitive search?</param>
		/// <returns></returns>
		public static string Postfix(this string text, string postfix, bool caseSensitive = true) {
			if (!text.EndsWith(postfix, caseSensitive)) {
				return text + postfix;
			}
			return text;
		}

		/// <summary>
		/// Returns true if the string is equal to the search term using case-insensitive comparison
		/// </summary>
		/// <param name="text">String to check</param>
		/// <param name="value">String to compare against</param>
		/// <returns></returns>
		public static bool EqualsCI(this string text, string value) {
			return text.Equals(value, StringComparison.OrdinalIgnoreCase);
		}

		/// <summary>
		/// Returns true if the string begins with the given prefix using case-insensitive comparison
		/// </summary>
		/// <param name="text">String to check</param>
		/// <param name="prefix">Prefix to check</param>
		/// <returns></returns>
		public static bool BeginsWithCI(this string text, string prefix) {
			return text.BeginsWith(text, false);
		}

		/// <summary>
		/// Returns true if the string ends with the given prefix using case-insensitive comparison
		/// </summary>
		/// <param name="text">String to check</param>
		/// <param name="postfix">Postfix to check</param>
		/// <returns></returns>
		public static bool EndsWithCI(this string text, string postfix) {
			return text.EndsWith(postfix, false);
		}

		/// <summary>
		/// Removes all instances of the search term from the string
		/// </summary>
		/// <param name="text">String to check</param>
		/// <param name="term">Term to remove</param>
		/// <returns></returns>
		public static string Remove(this string text, string term) {
			return text.Replace(term, "");
		}

		/// <summary>
		/// Checks if the string equals any given term, and returns a results struct. Never returns null.
		/// </summary>
		/// <param name="text">String to check</param>
		/// <param name="terms">Search terms</param>
		/// <param name="caseSensitive">Use case sensitive search?</param>
		/// <returns></returns>
		public static SearchResult2 EqualsAny(this string text, List<string> terms, bool caseSensitive = true) {
			var opts = caseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase;
			int len = text.Length;
			int t = 0;
			foreach (string term in terms) {
				if (len == term.Length) {
					if (text.Equals(term, opts)) {
						return new SearchResult2 {
							Found = true,
							Term = term,
							TermIndex = t,
							CharIndex = 0
						};
					}
				}
				t++;
			}
			return new SearchResult2 {
				Found = false,
				TermIndex = -1,
				CharIndex = -1
			};
		}

		/// <summary>
		/// Checks if the string contains any given term, and returns a results struct. Never returns null.
		/// </summary>
		/// <param name="text">String to check</param>
		/// <param name="terms">Search terms</param>
		/// <param name="caseSensitive">Use case sensitive search?</param>
		/// <returns></returns>
		public static SearchResult2 ContainsAny(this string text, List<string> terms, bool caseSensitive = true) {
			var opts = caseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase;
			int len = text.Length;
			int t = 0;
			foreach (string term in terms) {
				if (len >= term.Length) {
					var i = text.IndexOf(term, opts);
					if (i > -1) {
						return new SearchResult2 {
							Found = true,
							Term = term,
							TermIndex = t,
							CharIndex = i
						};
					}
				}
				t++;
			}
			return new SearchResult2 {
				Found = false,
				TermIndex = -1,
				CharIndex = -1
			};
		}

		/// <summary>
		/// Checks if the string contains any given term, and returns a results struct. Never returns null.
		/// </summary>
		/// <param name="text">String to check</param>
		/// <param name="terms">Search terms</param>
		/// <param name="caseSensitive">Use case sensitive search?</param>
		/// <returns></returns>
		public static SearchResult2 IndexOfAny(this string text, List<string> terms, bool caseSensitive = true) {
			return text.ContainsAny(terms);
		}

		/// <summary>
		/// Checks if the string begins with any given term, and returns a results struct. Never returns null.
		/// </summary>
		/// <param name="text">String to check</param>
		/// <param name="terms">Search terms</param>
		/// <param name="caseSensitive">Use case sensitive search?</param>
		/// <returns></returns>
		public static SearchResult2 BeginsWithAny(this string text, List<string> terms, bool caseSensitive = true) {
			int len = text.Length;
			int t = 0;
			foreach (string term in terms) {
				if (len >= term.Length) {
					if (text.BeginsWith(term, caseSensitive)) {
						return new SearchResult2 {
							Found = true,
							Term = term,
							TermIndex = t,
							CharIndex = 0
						};
					}
				}
				t++;
			}
			return new SearchResult2 {
				Found = false,
				TermIndex = -1,
				CharIndex = -1
			};
		}

		/// <summary>
		/// Checks if the string ends with any given term, and returns a results struct. Never returns null.
		/// </summary>
		/// <param name="text">String to check</param>
		/// <param name="terms">Search terms</param>
		/// <param name="caseSensitive">Use case sensitive search?</param>
		/// <returns></returns>
		public static SearchResult2 EndsWithAny(this string text, List<string> terms, bool caseSensitive = true) {
			int len = text.Length;
			int t = 0;
			foreach (string term in terms) {
				if (len >= term.Length) {
					if (text.EndsWith(term, caseSensitive)) {
						return new SearchResult2 {
							Found = true,
							Term = term,
							TermIndex = t,
							CharIndex = len - term.Length
						};
					}
				}
				t++;
			}
			return new SearchResult2 {
				Found = false,
				TermIndex = -1,
				CharIndex = -1
			};
		}

		/// <summary>
		/// Checks if the string contains a lowercase letter character
		/// </summary>
		/// <param name="text"></param>
		/// <returns></returns>
		public static bool ContainsLowercase(this string text) {
			foreach (char ch in text) {
				if (Char.IsLetter(ch) && char.IsLower(ch)) {
					return true;
				}
			}
			return false;
		}

		/// <summary>
		/// Checks if the string contains an uppercase letter character
		/// </summary>
		/// <param name="text"></param>
		/// <returns></returns>
		public static bool ContainsUppercase(this string text) {
			foreach (char ch in text) {
				if (Char.IsLetter(ch) && Char.IsUpper(ch)) {
					return true;
				}
			}
			return false;
		}

		/// <summary>
		/// Checks if the string contains a letter character
		/// </summary>
		/// <param name="text"></param>
		/// <returns></returns>
		public static bool ContainsLetter(this string text) {
			foreach (char ch in text) {
				if (Char.IsLetter(ch)) {
					return true;
				}
			}
			return false;
		}

		/// <summary>
		/// Checks if the string contains a numeric character
		/// </summary>
		/// <param name="text"></param>
		/// <returns></returns>
		public static bool ContainsNumber(this string text) {
			foreach (char ch in text) {
				if (Char.IsDigit(ch)) {
					return true;
				}
			}
			return false;
		}

		/// <summary>
		/// Checks if the string is a single floating-point or integer number
		/// </summary>
		/// <param name="text"></param>
		/// <returns></returns>
		public static bool IsSingleNumber(this string text) {
			double num;
			return double.TryParse(text, out num);
		}

		/// <summary>
		/// Checks if the string is multiline (it has at least one newline character)
		/// </summary>
		/// <param name="text"></param>
		/// <returns></returns>
		public static bool IsMultiline(this string text) {
			return text.Contains('\n');
		}

		/// <summary>
		/// Checks if the string is single-line (it has no newline characters)
		/// </summary>
		/// <param name="text"></param>
		/// <returns></returns>
		public static bool IsSingleline(this string text) {
			return !text.Contains('\n');
		}
		/// <summary>
		/// Uppercase the first letter in this string, preserving the case of all the other letters
		/// </summary>
		public static string FirstLetterUppercase(this string word) {
			if (word.Length == 0) {
				return word;
			}
			if (word.Length == 1) {
				return word.ToUpper();
			}
			return char.ToUpper(word[0]) + word.Substring(1);
		}
		/// <summary>
		/// Lowercase the first letter in this string, preserving the case of all the other letters
		/// </summary>
		public static string FirstLetterLowercase(this string word) {
			if (word.Length == 0) {
				return word;
			}
			if (word.Length == 1) {
				return word.ToLower();
			}
			return char.ToLower(word[0]) + word.Substring(1);
		}

		/// <summary>
		/// Eliminates all multiple-whitespace (newlines, tabs, multiple spaces) and joins this string to a single line.
		/// </summary>
		public static string SingleLine(this string text) {
			return Regex.Replace(text, @"\s+", " ").Trim();
		}

		/// <summary>
		/// Replaces all multiple-whitespace with single spaces.
		/// </summary>
		public static string RemoveMultipleSpaces(this string text) {
			var sb = new StringBuilder();
			int spaceCount = 0;
			foreach (var c in text) {
				bool isWhitespace = c == ' ' || c == '\t';
				if (isWhitespace) {
					spaceCount++;
				}
				else {
					spaceCount = 0;
				}
				if (spaceCount > 1) {
					continue;
				}
				else {
					if (isWhitespace) {
						sb.Append(' ');
					}
					else {
						sb.Append(c);
					}
				}
			}
			return sb.ToString();
		}

		/// <summary>
		/// Eliminates all whitespace (newlines, tabs, spaces) from the string.
		/// </summary>
		public static string RemoveSpaces(this string text) {
			var sb = new StringBuilder();
			foreach (var c in text) {
				if (c == ' ' || c == '\t' || c == '\r' || c == '\n') {
					continue;
				}
				else {
					sb.Append(c);
				}
			}
			return sb.ToString();
		}

		/// <summary>
		/// Replaces all multiple-newlines with single newlines.
		/// </summary>
		public static string RemoveMultipleNewlines(this string text) {
			var sb = new StringBuilder();
			int newlineCount = 0;
			foreach (var c in text) {
				bool isNewline = c == '\n' || c == '\r';
				if (isNewline) {
					newlineCount++;
				}
				else {
					newlineCount = 0;
				}
				if (newlineCount > 1) {
					continue;
				}
				else {
					if (isNewline) {
						sb.Append("\r\n");
					}
					else {
						sb.Append(c);
					}
				}
			}
			return sb.ToString();
		}

		/// <summary>
		/// Ensure the given string is at most X characters long.
		/// </summary>
		public static string Truncate(this string text, int maxLen) {
			if (text.Length < maxLen) {
				return text;
			}
			return text.Substring(0, maxLen);
		}

		/// <summary>
		/// Counts the number of times that the term is found at the beginning of the tring.
		/// </summary>
		public static int CountPrefix(this string text, string term) {
			if (text.Length < term.Length) {
				return 0;
			}
			var times = 0;
			var max = (text.Length - term.Length);
			for (int i = 0; i < max; i += term.Length) {
				if (text.Substring(i, term.Length) == term) {
					times++;
				}
				else {
					break;
				}
			}
			return times;
		}

		/// <summary>
		/// Counts the number of times that the term is found at the end of the tring.
		/// </summary>
		public static int CountPostfix(this string text, string term) {
			if (text.Length < term.Length) {
				return 0;
			}
			var times = 0;
			var max = (text.Length - term.Length);
			for (int i = max; i >= 0; i -= term.Length) {
				if (text.Substring(i, term.Length) == term) {
					times++;
				}
				else {
					break;
				}
			}
			return times;
		}

		/// <summary>
		/// Removes all special characters from the string, except the given allowances.
		/// Only letters and digits are allowed by default.
		/// </summary>
		public static string RemoveSymbols(this string text, List<char> allowChars = null) {
			var sb = new StringBuilder();
			foreach (char c in text) {
				if (c.IsLetter() || c.IsNumber() || (allowChars != null && allowChars.Contains(c))) {
					sb.Append(c);
				}
			}
			return sb.ToString();
		}

		/// <summary>
		/// Creates a web-safe version of this string optimized for filenames.
		/// Replaces all special characters with dash, or underscore, per the replaceChar parameter.
		/// </summary>
		public static string WebSafeFilename(this string text, char replaceChar = '-') {
			var sb = new StringBuilder();
			var lastIsRC = false;
			foreach (char c in text) {
				if (c.IsLetter() || c.IsNumber()) {
					sb.Append(c);
					lastIsRC = false;
				}
				else {
					if (!lastIsRC) {
						lastIsRC = true;
						sb.Append(replaceChar);
					}
				}
			}
			return sb.ToString().RemovePostfix(replaceChar.ToString());
		}

		/// <summary>
		/// Repeats the given string N times and returns the combined string
		/// </summary>
		/// <param name="text">String to repeat</param>
		/// <param name="times">Times to repeat</param>
		/// <returns></returns>
		public static string Repeat(this string text, int times) {
			StringBuilder sb = new StringBuilder();
			for (int r = 0; r < times; r++) {
				sb.Append(text);
			}
			return sb.ToString();
		}

		/// <summary>
		/// Ensures the given index is within the string.
		/// Always returns a valid index within the string, or -1 if the string is blank.
		/// </summary>
		/// <param name="text">String</param>
		/// <param name="index">Index you want to check</param>
		/// <returns></returns>
		public static int EnsureValidIndex(this string text, int index) {
			int count = text.Length;
			if (count == 0) {
				return -1;
			}
			if (index < 0) {
				return 0;
			}
			if (index >= count) {
				return count - 1;
			}
			return index;
		}

		/// <summary>
		/// Gets the substring between the start and end indices. Optionally returns the char at the end index.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="text">String you want a part of</param>
		/// <param name="startIndex">Index of first character you want</param>
		/// <param name="endIndex">Index of last character you want (if inclusive is true)</param>
		/// <param name="inclusive">Return the character at the end index (true) or not (false)</param>
		/// <returns></returns>
		public static string Part(this string text, int startIndex, int endIndex, bool inclusive = true) {

			if (text.Length > 0) {

				// support exclusive mode, where char at endIndex is not returned
				if (!inclusive) {
					endIndex--;
				}

				// ensure indices are within the string
				startIndex = text.EnsureValidIndex(startIndex);
				endIndex = text.EnsureValidIndex(endIndex);

				// return the selected substring 
				if (endIndex >= startIndex) {
					return text.Substring(startIndex, endIndex - startIndex);
				}
			}

			return "";
		}

		/// <summary>
		/// Transforms the given multi-word text into a valid URL component.
		/// Spaces are replaced by SEO-friendly dashes. Multiple spaces are replaced by a single dash.
		/// Only ASCII letters and digits are accepted and all symbols are stripped out.
		/// </summary>
		/// <param name="text">Input text</param>
		/// <returns></returns>
		public static string RestrictToURLComponent(this string text) {
			var result = new StringBuilder();
			var dashCount = 0;
			foreach (var c in text) {

				// replace multiple spaces with a dash
				if (c.IsWhitespace() || c == '-') {
					if (dashCount < 1) {
						result.Append('-');
					}
					dashCount++;
				}

				// only accept ASCII letters and digits
				if (c.IsAsciiLetter() || c.IsNumber()) {
					dashCount = 0;
					result.Append(c);
				}

			}
			return result.ToString();
		}

		/// <summary>
		/// Ensures that only Unicode letters and Unicode digits are accepted and all symbols are stripped out.
		/// </summary>
		/// <param name="text">Input text</param>
		/// <returns></returns>
		public static string RestrictToLettersNumbers(this string text) {
			var result = new StringBuilder();
			foreach (var c in text) {

				// only accept letters and digits
				if (c.IsLetter() || c.IsNumber()) {
					result.Append(c);
				}

			}
			return result.ToString();
		}

		/// <summary>
		/// Ensures that only ASCII letters and ASCII digits are accepted and all symbols and special characters are stripped out.
		/// </summary>
		/// <param name="text">Input text</param>
		/// <returns></returns>
		public static string RestrictToAsciiLettersNumbers(this string text) {
			var result = new StringBuilder();
			foreach (var c in text) {

				// only accept ASCII letters and digits
				if (c.IsAsciiLetter() || c.IsAsciiNumber()) {
					result.Append(c);
				}

			}
			return result.ToString();
		}

		/// <summary>
		/// Ensures that only ASCII digits are accepted and all symbols are stripped out.
		/// </summary>
		/// <param name="text">Input text</param>
		/// <param name="acceptDots">Accept the dot character or not. Useful for decimal values</param>
		/// <param name="acceptNegative">Accept the dash character or not. Useful for negative values</param>
		/// <returns></returns>
		public static string RestrictToAsciiNumbers(this string text, bool acceptDots = false, bool acceptNegative = false) {
			var result = new StringBuilder();
			foreach (var c in text) {

				// only accept digits and enabled symbols
				if (c.IsAsciiNumber() || (acceptDots && c == '.') || (acceptNegative && c == '-' && result.Length == 0)) {
					result.Append(c);
				}
			}
			return result.ToString();
		}

		/// <summary>
		/// Ensures that only characters accepted in a filename are preserved, all other characters are replaced with a delimiter.
		/// You can use this only for file names with extensions, or folder names, but not for entire paths.
		/// Generates filenames compatible with Windows, Unix, and web-safe file names.
		/// </summary>
		/// <param name="text">File name or folder name</param>
		/// <param name="delimiter">Delimiter to replace invalid characters with. Only one delimiter is added if multiple invalid chars are in sequence.</param>
		/// <returns></returns>
		public static string RestrictToFilename(this string text, char delimiter = '_') {
			var result = new StringBuilder();
			bool addedDelim = false;
			foreach (var c in text) {

				// skip invalid characters
				if (c == '^' || c == '<' || c == '>' || c == '|' || c == '?' || c == '+' ||
					c == '*' || c == '!' || c == ';' || c == '*' || c == '"' || c == '#' ||
					c == '\r' || c == '\n' || c == '\t' || c == '\\' || c == '/' || c == ':') {
					if (!addedDelim) {
						addedDelim = true;
						result.Append(delimiter);
					}
					continue;
				}
				else {

					// accept any other chars
					result.Append(c);
					addedDelim = false;
				}
			}
			return result.ToString();
		}

	}

	/// <summary>
	/// Results of a string search
	/// </summary>
	public struct SearchResult2 {
		/// <summary>
		/// any term found?
		/// </summary>
		public bool Found;
		/// <summary>
		/// which term was found?
		/// </summary>
		public string Term;
		/// <summary>
		/// what was the index of the term found within the list of terms given?
		/// </summary>}
		public int TermIndex;
		/// <summary>
		/// what char index did the IndexOf() call return?
		/// </summary>
		public int CharIndex;
	}
}