using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jetsons.JetPack {
	public static class Colors {

		/// <summary>
		/// Return a color linearly blended between the two colors, depending on the blend amount required.
		/// </summary>
		/// <param name="startColor">The source color, will be returned if blend amount is 0</param>
		/// <param name="endColor">The target color, will be returned if blend amount is 1</param>
		/// <param name="blendAmount">Must be in the range of 0 to 1.</param>
		/// <returns></returns>
		public static Color Blend(this Color startColor, Color endColor, double blendAmount) {

			// quickly return if precise amount
			if (blendAmount <= 0) {
				return startColor;
			}
			if (blendAmount >= 1) {
				return endColor;
			}

			// extract the RGB values
			int start = ToInt(startColor);
			int end = ToInt(endColor);
			double blendInversed = (1 - blendAmount);

			// extract the RGB components
			byte startR = (byte)((start >> 16) & 0xFF);
			byte startG = (byte)((start >> 8) & 0xFF);
			byte startB = (byte)(start & 0xFF);
			byte endR = (byte)((end >> 16) & 0xFF);
			byte endG = (byte)((end >> 8) & 0xFF);
			byte endB = (byte)(end & 0xFF);

			// perform component-wise blending
			uint blendR = (uint)(((float)startR * blendInversed) + ((float)endR * blendAmount));
			uint blendG = (uint)(((float)startG * blendInversed) + ((float)endG * blendAmount));
			uint blendB = (uint)(((float)startB * blendInversed) + ((float)endB * blendAmount));

			// merge and return the RGB value
			uint blendedColor = 0xFF000000 | (blendR << 16) | (blendG << 8) | blendB;
			return ToColor(blendedColor);
		}


		/// <summary>
		/// Converts a 32-bit ARGB value into a Color object.
		/// </summary>
		public static Color ToColor(this uint argb) {
			var A = (int)((argb >> 24) & 0xFF);
			var R = (int)((argb >> 16) & 0xFF);
			var G = (int)((argb >> 8) & 0xFF);
			var B = (int)(argb & 0xFF);
			return Color.FromArgb(A, R, G, B);
		}

		/// <summary>
		/// Converts a 24-bit RGB value into a Color object.
		/// </summary>
		public static Color ToColor(this int rgb, int alpha = 255) {
			var R = ((rgb >> 16) & 0xFF);
			var G = ((rgb >> 8) & 0xFF);
			var B = (rgb & 0xFF);
			return Color.FromArgb(alpha, R, G, B);
		}

		/// <summary>
		/// Converts a Color object into a 24-bit RGB value.
		/// </summary>
		public static int ToInt(this Color argb) {
			var value = argb.ToArgb();
			return value & 0xFFFFFF;
		}

		/// <summary>
		/// Converts a Color object into a 32-bit ARGB value.
		/// </summary>
		public static uint ToUInt(this Color argb) {
			var value = (uint)argb.ToArgb();
			return value;
		}

	}
}
