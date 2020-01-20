using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jetsons.JetPack {
	public static class Bitwise {

		/// <summary>
		/// Get the value of the given flag in the byte.
		/// </summary>
		/// <param name="number">The byte value you want to read</param>
		/// <param name="flag">Which bit? Measured from 0 to 7</param>
		/// <returns></returns>
		public static bool GetFlag(this byte number, int flag) {
			return (number & (1 << flag)) > 0;
		}

		/// <summary>
		/// Set or clear the given flag in the byte.
		/// </summary>
		/// <param name="number">The byte value you want to modify</param>
		/// <param name="flag">Which bit? Measured from 0 to 7</param>
		/// <param name="flagValue"></param>
		/// <returns></returns>
		public static byte SetFlag(this byte number, int flag, bool flagValue = true) {
			if (flagValue) {
				return (byte)(number | (byte)((1 << flag)));
			}
			else {
				return (byte)(number & (byte)(~(1 << flag)));
			}
		}

		/// <summary>
		/// Clear the given flag in the byte.
		/// </summary>
		/// <param name="number">The byte value you want to modify</param>
		/// <param name="flag">Which bit? Measured from 0 to 7</param>
		/// <returns></returns>
		public static byte ClearFlag(this byte number, int flag) {
			return (byte)(number & (byte)(~(1 << flag)));
		}

		/// <summary>
		/// Get the value of the given flag in the short.
		/// </summary>
		/// <param name="number">The byte value you want to read</param>
		/// <param name="flag">Which bit? Measured from 0 to 15</param>
		/// <returns></returns>
		public static bool GetFlag(this ushort number, int flag) {
			return (number & (1 << flag)) > 0;
		}

		/// <summary>
		/// Set or clear the given flag in the short.
		/// </summary>
		/// <param name="number">The byte value you want to modify</param>
		/// <param name="flag">Which bit? Measured from 0 to 15</param>
		/// <param name="flagValue"></param>
		/// <returns></returns>
		public static ushort SetFlag(this ushort number, int flag, bool flagValue = true) {
			if (flagValue) {
				return (ushort)(number | (ushort)((1 << flag)));
			}
			else {
				return (ushort)(number & (ushort)(~(1 << flag)));
			}
		}

		/// <summary>
		/// Clear the given flag in the short.
		/// </summary>
		/// <param name="number">The byte value you want to modify</param>
		/// <param name="flag">Which bit? Measured from 0 to 15</param>
		/// <returns></returns>
		public static uint ClearFlag(this ushort number, int flag) {
			return (ushort)(number & (ushort)(~(1 << flag)));
		}

		/// <summary>
		/// Get the value of the given flag in the integer.
		/// </summary>
		/// <param name="number">The byte value you want to read</param>
		/// <param name="flag">Which bit? Measured from 0 to 31</param>
		/// <returns></returns>
		public static bool GetFlag(this uint number, int flag) {
			return (number & (1 << flag)) > 0;
		}

		/// <summary>
		/// Set or clear the given flag in the integer.
		/// </summary>
		/// <param name="number">The byte value you want to modify</param>
		/// <param name="flag">Which bit? Measured from 0 to 31</param>
		/// <param name="flagValue"></param>
		/// <returns></returns>
		public static uint SetFlag(this uint number, int flag, bool flagValue = true) {
			if (flagValue) {
				return (uint)(number | (uint)((1 << flag)));
			}
			else {
				return (uint)(number & (uint)(~(1 << flag)));
			}
		}

		/// <summary>
		/// Clear the given flag in the integer.
		/// </summary>
		/// <param name="number">The byte value you want to modify</param>
		/// <param name="flag">Which bit? Measured from 0 to 31</param>
		/// <returns></returns>
		public static uint ClearFlag(this uint number, int flag) {
			return (uint)(number & (uint)(~(1 << flag)));
		}


	}
}
