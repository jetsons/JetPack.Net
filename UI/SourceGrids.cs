using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jetsons.JetPack {
	public static class SourceGrids {

#if UI

		/// <summary>
		/// Populate this SourceGrid object with the list of typed objects given.
		/// Specific properties are fetched from each object and displayed in the relevant columns.
		/// </summary>
		/// <param name="headers">The column headers to display in the first row. Array length must match rowProps</param>
		/// <param name="rows">The list of typed objects to display</param>
		/// <param name="rowProps">The names of properties or paths to fetch from each object. Array length must match headers</param>
		public static void SetCells<T>(this SourceGrid.Grid grid, IList<string> headers, IList<T> rows, IList<string> rowProps) {

			// clear prev data
			grid.Rows.Clear();

			// configure grid
			grid.ColumnsCount = headers.Count;
			grid.FixedRows = 1;
			grid.Rows.Insert(0);
			int frc = grid.FixedRows;

			// set headers
			int h = 0;
			foreach (var header in headers) {
				grid[0, h++] = new SourceGrid.Cells.ColumnHeader(header);
			}

			// set cells
			if (rows != null) {
				for (int y = 0; y < rows.Count; y++) {
					grid.Rows.Insert(y + 1);
					T row = rows[y];
					for (int x = 0; x < headers.Count; x++) {
						var value = (row.GetPropValue(rowProps[x]) ?? "").ToString();
						grid[y + frc, x] = new SourceGrid.Cells.Cell(value);
					}
				}
			}

			// autosize all columns & rows
			grid.AutoSizeCells();

		}

#endif

	}
}
