using System;
using System.Collections.Generic;
using System.Text;
using Jetsons.JetPack;

namespace Jetsons.CSV {

	/// <summary>
	/// Strongly-typed Fast CSV Decoder
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class CsvDecoder<T> {

		/// <summary>
		/// Parse a CSV file and convert it into a List of strongly typed Objects.
		/// Never returns null.
		/// If the first line is not headers, and you don't supply any columnProps, then the names of the columns are assumed.
		/// </summary>
		/// <param name="csv">CSV-formatted string</param>
		/// <param name="headers">Read the first line as the column headers?</param>
		/// <param name="columnProps">Provide the properties per column, if known</param>
		/// <param name="delimiter">Uses the given delimiter</param>
		/// <returns></returns>
		public static CsvResults<T> Decode(string csv, CsvHeaders headers, List<string> columnProps = null, char delimiter = ',') {

			if (!csv.Exists()) {
				return new CsvResults<T>();
			}

			csv = csv.Trim();

			return new CsvDecoder<T>() {
				Csv = csv,
				Lines = csv.Lines(),
				Header = headers,
				ColumnProps = columnProps,
				Delimiter = delimiter
			}.DecodeString();
		}


		// INPUTS
		private string Csv;
		private CsvHeaders Header;
		private bool FirstLineIsHeaders;
		private List<string> ColumnProps = null;
		private char Delimiter = ',';

		// STATE
		private CsvResults<T> Results = new CsvResults<T>();
		private List<string> Lines;
		private bool NewRecord = true;
		private StringBuilder Value = new StringBuilder();
		private T DataObject = default(T);
		private int PropIndex = 0;
		private string Prop;
		private bool IsHeader;

		// CONSTS
		private const char Quote = '\"';
		private const char Slash = '\\';


		/// <summary>
		/// Parse a CSV file and convert it into a List of strongly typed Objects
		/// </summary>
		private CsvResults<T> DecodeString() {

			// if props given, take those
			if (ColumnProps.Exists()) {
				Results.Headers = ColumnProps;
			}

			// calc header type
			switch (Header) {
				case CsvHeaders.FirstRow:
					FirstLineIsHeaders = true;
					break;
				case CsvHeaders.None:
					FirstLineIsHeaders = false;
					break;
				case CsvHeaders.AutoDetect:
					FirstLineIsHeaders = !Csv.Before(",").IsSingleNumber();
					break;
			}

			// status
			IsHeader = FirstLineIsHeaders;

			// per line
			foreach (var line in Lines) {

				// if this is the start of a new record
				if (NewRecord && !IsHeader) {

					// create a new data object
					DataObject = Activator.CreateInstance<T>();
					Results.Data.Add(DataObject);
					PropIndex = 0;
					Prop = CalcPropName();
				}

				// per char
				for (int x = 0; x < line.Length; x++) {
					char ch = line[x];


					if (NewRecord) {

						if (ch == Delimiter) {

							// end of the previous value
							AddProp();
							continue;

						} else if (ch == '\"') {

							// start of a quoted string value
							NewRecord = false;
							continue;

						} else {

							// VALUE CHARACTER
							Value.Append(ch);

							// end of the line
							if (x == line.Length) {
								AddProp();
							}

						}

					} else {

						if (ch == '\"') {
							HandleQuotedStrings(line, ref x);
							continue;
						} else {

							// VALUE CHARACTER
							Value.Append(ch);
						}

					}
				}

				// at end of line take word
				if (NewRecord && Value.Length > 0) {
					AddProp();
				}

				// state
				IsHeader = false;

			}

			// OK
			Results.Success = true;
			return Results;
		}

		private void HandleQuotedStrings(string line, ref int x) {

			// if its a quote
			if (IsChar(line, x + 1, Quote)) {
				x++;
				Value.Append(Quote);

			// if its a slash
			} else if (IsChar(line, x - 1, Slash)) {
				x++;
				Value.Append(Quote);

			} else {
				NewRecord = true;
			}
		}
		private bool IsChar(string line, int x, char value) {
			return x >= 0 && line.Length > x && line[x] == value;
		}

		private void AddProp() {

			string val = Value.ToString().Trim();

			if (IsHeader) {

				// register this header
				Results.Headers.Add(val);

			} else {

				// fetch or generate prop name for this value
				Prop = CalcPropName();

				// store this value in the obj
				DataObject.SetPropValue(Prop, val);

				PropIndex++;

			}

			Value = new StringBuilder();
		}

		private string CalcPropName() {

			// if the header has not been registered
			if (!Results.Headers.HasSlotAndValue(PropIndex)) {

				// generate column name from column index if none given
				while ((Results.Headers.Count - 1) < PropIndex) {
					Results.Headers.Add("Column" + (Results.Headers.Count + 1));
				}
			}

			// return the registered header
			return Results.Headers[PropIndex];
		}

	}

	public class CsvResults<T> {

		public bool Success;

		/// <summary>
		/// Headers that were picked up from the first row of the CSV, or the headers that were given by the caller via columnProps.
		/// </summary>
		public List<string> Headers = new List<string>();

		/// <summary>
		/// Strongly typed data of the CSV records
		/// </summary>
		public List<T> Data = new List<T>();

	}

	public enum CsvHeaders {

		/// <summary>
		/// The headers are surely on the first row of the CSV
		/// </summary>
		FirstRow,

		/// <summary>
		/// There are surely no headers in the CSV
		/// </summary>
		None,

		/// <summary>
		/// Auto-detect if there are headers on the first row of the CSV
		/// </summary>
		AutoDetect
	}

}