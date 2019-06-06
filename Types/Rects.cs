using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jetsons.JetPack {
	public static class Rects {
		
		/// <summary>
		/// Resize this rect to fit within the new size, while maintaining aspect ratio.
		/// 
		/// Passing 0 for either maxWidth or maxHeight maintains aspect ratio while
		///        the other non-null parameter is guaranteed to be constrained to
		///        its maximum value.
		///        
		/// Taken from Stackoverflow and improved.
		/// 
		/// @url		https://stackoverflow.com/a/5367895/11170692
		/// @author		Brian Chavez
		/// </summary>
		public static Rectangle ResizeToFitSize(this Rectangle original, int maxWidth, int maxHeight, bool tightFit = true) {

			if (maxWidth <= 0 && maxHeight <= 0) {
				throw new ArgumentException("At least one scale factor (maxWidth or maxHeight) must be non-zero.");
			}
			if (original.Height <= 0 || original.Width <= 0) {
				return new Rectangle(0, 0, 0, 0);
			}

			double widthScale = 0;
			double heightScale = 0;
			double scale = 0;

			if (maxWidth > 0) {
				widthScale = maxWidth / (double)original.Width;
				scale = widthScale;
			}
			if (maxHeight > 0) {
				heightScale = maxHeight / (double)original.Height;
				scale = heightScale;
			}
			if (maxWidth > 0 && maxHeight > 0) {
				if (tightFit) {
					scale = Math.Max((double)widthScale, (double)heightScale);
				} else {
					scale = Math.Min((double)widthScale, (double)heightScale);
				}
			}

			// scale it
			var newWidth = (int)Math.Floor(original.Width * scale);
			var newHeight = (int)Math.Ceiling(original.Height * scale);

			// calculate x/y offset in order to keep the image in the center
			var x = (int)(((double)(original.Width - newWidth)) / 2);
			var y = (int)(((double)(original.Height - newHeight)) / 2);

			return new Rectangle(x, y, newWidth, newHeight);
		}
	}
}
