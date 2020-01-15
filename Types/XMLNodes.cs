using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Jetsons.JetPack {
	public static class XMLNodes {

		/// <summary>
		/// Returns the first XmlNode of the given type, or null if not found
		/// </summary>
		public static XmlNode Node(this XmlNode node, string type) {
			if (node == null || node.ChildNodes == null) {
				return null;
			}
			foreach (XmlNode child in node.ChildNodes) {
				if (child.Name == type) {
					return child;
				}
			}
			return null;
		}

		/// <summary>
		/// Returns the value of the first XmlNode of the given type, or the default value if not found
		/// </summary>
		public static string NodeValue(this XmlNode node, string type, string defaultValue = null) {
			if (node == null || node.ChildNodes == null) {
				return defaultValue;
			}
			foreach (XmlNode child in node.ChildNodes) {
				if (child.Name == type) {
					return child.InnerText.ToString();
				}
			}
			return defaultValue;
		}

		/// <summary>
		/// Returns the value of the first attribute with the given name, or the default value if not found
		/// </summary>
		public static string Attribute(this XmlNode node, string name, string defaultValue = null) {
			if (node == null || node.Attributes == null) {
				return defaultValue;
			}
			foreach (XmlAttribute attrib in node.Attributes) {
				if (attrib.Name == name) {
					return attrib.Value.ToString();
				}
			}
			return defaultValue;
		}

	}
}
