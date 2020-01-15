using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Jetsons.JetPack {
	public static class XmlFiles {


		/// <summary>
		/// Load the given XML file as an XML Document, or return null if it does not exist.
		/// </summary>
		/// <param name="filename">File path</param>
		/// <param name="codepage">ANSI Codepage to use while reading the file</param>
		/// <param name="unicode">Use unicode as default (true) or ANSI as default (false)</param>
		/// <returns></returns>
		public static XmlDocument LoadXML(this string filename, bool unicode = true, int codepage = 1252) {

			// load the XML file with unicode/ANSI
			var text = filename.LoadTextFile(unicode, codepage);
			if (text == null) {
				return null;
			}

			// convert the XML to a document
			var doc = new XmlDocument();
			doc.LoadXml(text);
			return doc;
		}

		/// <summary>
		/// Save the given XML Document to an XML file
		/// </summary>
		/// <param name="xml">XML Document</param>
		/// <param name="fileName">File path, overwritten if it already exists</param>
		/// <param name="createFolder">Create the parent folder?</param>
		/// <param name="unicode">Save the file as unicode (true) or ANSI (false)</param>
		/// <param name="codepage">ANSI Codepage to use while reading the file</param>
		public static void SaveToFile(this XmlDocument xml, string fileName, bool createFolder = false, bool unicode = true, int codepage = 1252) {

			// convert the XML document to text
			var text = xml.OuterXml;

			// save the text file
			text.SaveToFile(fileName, createFolder, unicode, codepage);
		}


		/// <summary>
		/// Save the given string to a temporary file and returns the path
		/// </summary>
		/// <param name="xml">XML Document</param>
		/// <param name="unicode">Save the file as unicode (true) or ANSI (false)</param>
		/// <param name="codepage">ANSI Codepage to use while reading the file</param>
		public static string SaveToTempFile(this XmlDocument xml, bool unicode = true, int codepage = 1252) {
			string path = Path.GetTempPath() + FilePaths.PathSeperator + Path.GetTempFileName();
			xml.SaveToFile(path, false, unicode, codepage);
			return path;
		}

	}
}
