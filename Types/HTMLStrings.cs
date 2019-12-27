using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jetsons.JetPack {

	public static class HTMLStrings {

		/// <summary>
		/// Bolds the given string using HTML formatting, by surrounding it with B tags.
		/// </summary>
		/// <param name="text">string to be formatted</param>
		/// <returns></returns>
		public static string Bold(this string text) {
			return "<b>" + text + "</b>";
		}

		/// <summary>
		/// Italicizes the given string using HTML formatting, by surrounding it with I tags.
		/// </summary>
		/// <param name="text">string to be formatted</param>
		/// <returns></returns>
		public static string Italic(this string text) {
			return "<i>" + text + "</i>";
		}

		/// <summary>
		/// Links the given string to a URL using HTML formatting, by surrounding it with A tags.
		/// </summary>
		/// <param name="text">string to be formatted</param>
		/// <param name="url">URL of the link</param>
		/// <returns></returns>
		public static string Link(this string text, string url) {
			return "<a href=\"" + url + "\">" + text + "</a>";
		}

		/// <summary>
		/// Converts the given string to an HTML header, by surrounding it with H tags.
		/// </summary>
		/// <param name="text">string to be formatted</param>
		/// <param name="level">1 for h1, 2 for h2, etc</param>
		/// <returns></returns>
		public static string Header(this string text, int level = 1) {
			return "<h" + level + ">" + text + "</h" + level + ">";
		}

	}
}
