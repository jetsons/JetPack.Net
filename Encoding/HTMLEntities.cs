using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jetsons.JetPack {
	public class HTMLEntities {

		/// <summary>
		/// Conversion table generated at runtime from the below static tables
		/// </summary>
		public static Dictionary<string, char> EntityToChar;

		/// <summary>
		/// Entity to Unicode tables from Microsoft .NET Core Source
		/// https://github.com/dotnet/corefx/blob/release/3.0/src/System.Runtime.Extensions/src/System/Net/WebUtility.cs
		/// </summary>
		public static string[] Entities = {
			"quot","amp","apos","lt","gt","nbsp","iexcl","cent","pound","curren",
			"yen","brvbar","sect","uml","copy","ordf","laquo","not",
			"shy","reg","macr","deg","plusmn","sup2","sup3","acute","micro","para",
			"middot","cedil","sup1","ordm","raquo","frac14","frac12","frac34","iquest",
			"Agrave","Aacute","Acirc","Atilde","Auml","Aring","AElig","Ccedil","Egrave",
			"Eacute","Ecirc","Euml","Igrave","Iacute","Icirc","Iuml","ETH","Ntilde",
			"Ograve","Oacute","Ocirc","Otilde","Ouml","times","Oslash","Ugrave",
			"Uacute","Ucirc","Uuml","Yacute","THORN","szlig","agrave","aacute","acirc",
			"atilde","auml","aring","aelig","ccedil","egrave","eacute","ecirc","euml",
			"igrave","iacute","icirc","iuml","eth","ntilde","ograve","oacute","ocirc",
			"otilde","ouml","divide","oslash","ugrave","uacute","ucirc","uuml","yacute",
			"thorn","yuml","OElig","oelig","Scaron","scaron","Yuml","fnof","circ","tilde",
			"Alpha","Beta","Gamma","Delta","Epsilon","Zeta","Eta","Theta","Iota","Kappa",
			"Lambda","Mu","Nu","Xi","Omicron","Pi","Rho","Sigma","Tau","Upsilon","Phi",
			"Chi","Psi","Omega","alpha","beta","gamma","delta","epsilon","zeta","eta",
			"theta","iota","kappa","lambda","mu","nu","xi","omicron","pi","rho",
			"sigmaf","sigma","tau","upsilon","phi","chi","psi","omega","thetasym","upsih",
			"piv","ensp","emsp","thinsp","zwnj","zwj","lrm","rlm","ndash","mdash","lsquo",
			"rsquo","sbquo","ldquo","rdquo","bdquo","dagger","Dagger","bull","hellip",
			"permil","prime","Prime","lsaquo","rsaquo","oline","frasl","euro","image",
			"weierp","real","trade","alefsym","larr","uarr","rarr","darr","harr","crarr",
			"lArr","uArr","rArr","dArr","hArr","forall","part","exist","empty","nabla",
			"isin","notin","ni","prod","sum","minus","lowast","radic","prop","infin",
			"ang","and","or","cap","cup","int","there4","sim","cong","asymp","ne",
			"equiv","le","ge","sub","sup","nsub","sube","supe","oplus","otimes","perp",
			"sdot","lceil","rceil","lfloor","rfloor","lang","rang","loz",
			"spades","clubs","hearts","diams"};

		public static char[] Chars = {
			'\x0022','\x0026','\x0027','\x003c','\x003e','\x00a0','\x00a1','\x00a2','\x00a3',
			'\x00a4','\x00a5','\x00a6','\x00a7','\x00a8','\x00a9','\x00aa','\x00ab','\x00ac',
			'\x00ad','\x00ae','\x00af','\x00b0','\x00b1','\x00b2','\x00b3','\x00b4','\x00b5',
			'\x00b6','\x00b7','\x00b8','\x00b9','\x00ba','\x00bb','\x00bc','\x00bd','\x00be',
			'\x00bf','\x00c0','\x00c1','\x00c2','\x00c3','\x00c4','\x00c5','\x00c6','\x00c7',
			'\x00c8','\x00c9','\x00ca','\x00cb','\x00cc','\x00cd','\x00ce','\x00cf','\x00d0',
			'\x00d1','\x00d2','\x00d3','\x00d4','\x00d5','\x00d6','\x00d7','\x00d8','\x00d9',
			'\x00da','\x00db','\x00dc','\x00dd','\x00de','\x00df','\x00e0','\x00e1','\x00e2',
			'\x00e3','\x00e4','\x00e5','\x00e6','\x00e7','\x00e8','\x00e9','\x00ea','\x00eb',
			'\x00ec','\x00ed','\x00ee','\x00ef','\x00f0','\x00f1','\x00f2','\x00f3','\x00f4',
			'\x00f5','\x00f6','\x00f7','\x00f8','\x00f9','\x00fa','\x00fb','\x00fc','\x00fd',
			'\x00fe','\x00ff','\x0152','\x0153','\x0160','\x0161','\x0178','\x0192','\x02c6',
			'\x02dc','\x0391','\x0392','\x0393','\x0394','\x0395','\x0396','\x0397','\x0398',
			'\x0399','\x039a','\x039b','\x039c','\x039d','\x039e','\x039f','\x03a0','\x03a1',
			'\x03a3','\x03a4','\x03a5','\x03a6','\x03a7','\x03a8','\x03a9','\x03b1','\x03b2',
			'\x03b3','\x03b4','\x03b5','\x03b6','\x03b7','\x03b8','\x03b9','\x03ba','\x03bb',
			'\x03bc','\x03bd','\x03be','\x03bf','\x03c0','\x03c1','\x03c2','\x03c3','\x03c4',
			'\x03c5','\x03c6','\x03c7','\x03c8','\x03c9','\x03d1','\x03d2','\x03d6','\x2002',
			'\x2003','\x2009','\x200c','\x200d','\x200e','\x200f','\x2013','\x2014','\x2018',
			'\x2019','\x201a','\x201c','\x201d','\x201e','\x2020','\x2021','\x2022','\x2026',
			'\x2030','\x2032','\x2033','\x2039','\x203a','\x203e','\x2044','\x20ac','\x2111',
			'\x2118','\x211c','\x2122','\x2135','\x2190','\x2191','\x2192','\x2193','\x2194',
			'\x21b5','\x21d0','\x21d1','\x21d2','\x21d3','\x21d4','\x2200','\x2202','\x2203',
			'\x2205','\x2207','\x2208','\x2209','\x220b','\x220f','\x2211','\x2212','\x2217',
			'\x221a','\x221d','\x221e','\x2220','\x2227','\x2228','\x2229','\x222a','\x222b',
			'\x2234','\x223c','\x2245','\x2248','\x2260','\x2261','\x2264','\x2265','\x2282',
			'\x2283','\x2284','\x2286','\x2287','\x2295','\x2297','\x22a5','\x22c5','\x2308',
			'\x2309','\x230a','\x230b','\x2329','\x232a','\x25ca','\x2660','\x2663','\x2665',
			'\x2666' };

	}
}
