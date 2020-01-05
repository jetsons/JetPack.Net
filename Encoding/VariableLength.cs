using System;

namespace Jetsons.JetPack {
	public static class VariableLength {

		/// <summary>
		/// Calculate the number of bytes required to store this signed integer value.
		/// Returns 1, 2, 4 or 8 depending on the value.
		/// </summary>
		public static int GetVarIntLength(int intVal) {

			if (intVal >= -128 && intVal <= 127) {
				// 1 byte
				return 1;

			}
			else if (intVal >= -32768 && intVal <= 32767) {
				// 2 bytes
				return 2;
			}
			else if (intVal >= -2147483648 && intVal <= 2147483647) {
				// 4 bytes
				return 4;
			}
			else {
				// 8 bytes
				return 8;
			}
		}

		/// <summary>
		/// Calculate the number of bytes required to store this signed integer value.
		/// Returns 1, 2, 4 or 8 depending on the value.
		/// </summary>
		public static int GetVarUIntLength(uint intVal) {

			if (intVal <= 0xFF) {
				// 1 byte
				return 1;
			}
			else if (intVal <= 0xFFFF) {
				// 2 bytes
				return 2;
			}
			else if (intVal <= 0xFFFFFFFF) {
				// 4 bytes
				return 4;
			}
			else {
				// 8 bytes
				return 8;
			}
		}

		/// <summary>
		/// Calculate the number of bytes required to store this floating point value.
		/// Returns 1, 2, 4 or 8 depending on the value.
		/// </summary>
		public static int GetVarDoubleLength(double value, bool signed) {

			if (value == value.Round() && ((signed && value >= -128 && value <= 127) || (!signed && value >= 0 && value <= 255))) {
				// 1 byte
				return 1;
			}
			else if (value == DecodeDouble16(EncodeDouble16(value, signed), signed)) {
				// 2 bytes
				return 2;
			}
			else if (value == DecodeDouble32(EncodeDouble32(value, signed), signed)) {
				// 4 bytes
				return 4;
			}
			else {
				// 8 bytes
				return 8;
			}
		}

		/// <summary>
		/// Accurately encodes a floating-point number into a 2-byte value.
		/// </summary>
		public static int EncodeDouble16(double value, bool sign) {

			// calc sign
			int neg = 0;
			if (sign && value < 0) {
				neg = 0x8000;
				value = -value;
			}

			// find pos of dot
			string valueStr = value.ToString();
			int dot = valueStr.IndexOf(".");
			int valueInt = -1;
			if (dot == -1) {

				// no dot
				dot = 0;
				valueInt = (int)value;

			}
			else {

				// remove dot
				valueInt = (Sub(valueStr, 0, dot) + Sub(valueStr, dot + 1)).ToInt();
			}

			// save sign, dot pos, value
			if (sign) {
				return ((dot & 3) << 13) | (valueInt & 0x1FFF) | neg;
			}
			return ((dot & 3) << 14) | (valueInt & 0x3FFF);
		}


		/// <summary>
		/// Accurately decodes a 2-byte value into a floating-point number.
		/// </summary>
		public static double DecodeDouble16(int value, bool sign) {

			// get pos of dot & value
			int dot = -1;
			string result = "";
			if (sign) {
				dot = (value & 0x6000) >> 13;
				result = (value & 0x1FFF).ToString();
			}
			else {
				dot = (value & 0xC000) >> 14;
				result = (value & 0x3FFF).ToString();
			}

			// if integer
			double resultNum = double.NaN;
			if (dot == 0) {
				resultNum = result.ToDouble();

			}
			else {

				// add dot
				resultNum = (Sub(result, 0, dot) + "." + Sub(result, dot)).ToDouble();
			}

			// return signed value
			return (sign && (value & 0x8000) > 0) ? -resultNum : resultNum;
		}


		/// <summary>
		/// Accurately encodes a floating-point number into a 4-byte value.
		/// </summary>
		public static uint EncodeDouble32(double value, bool sign) {

			// calc sign
			uint neg = 0;
			if (sign && value < 0) {
				neg = 0x40000000;
				value = -value;
			}

			// find pos of dot
			string valueStr = value.ToString();
			int dot = valueStr.IndexOf(".");
			uint idot = 0;
			uint valueInt = 0;
			if (dot == -1) {

				// no dot
				dot = 0;
				valueInt = (uint)value;

			}
			else {

				// remove dot
				string pre = Sub(valueStr, 0, dot);
				valueInt = (pre + Sub(valueStr, dot + 1)).ToUInt();

				// find if dot inverted
				if (pre == "0") {
					idot = 0x80000000;
					dot++;
					while (valueStr[dot] == '0') {
						dot++;
					}
					dot -= 2;
				}
			}

			// save sign, dot pos, value
			if (sign) {
				return ((uint)(dot & 7) << 27) | (valueInt & 0x7FFFFFF) | neg | idot;
			}
			return ((uint)(dot & 7) << 28) | (valueInt & 0xFFFFFFF) | idot;
		}


		/// <summary>
		/// Accurately decodes a 2-byte value into a floating-point number.
		/// </summary>
		public static double DecodeDouble32(uint value, bool sign) {

			// get pos of dot & value
			uint dot = 0;
			string result = "";
			if (sign) {
				dot = (value & 0x38000000) >> 27;
				result = (value & 0x7FFFFFF).ToString();
			}
			else {
				dot = (value & 0x70000000) >> 28;
				result = (value & 0xFFFFFFF).ToString();
			}


			// if inverted dot
			string resultStr = "";
			double resultNum = double.NaN;
			if ((value & 0x80000000) < 0) {
				resultStr = "0.";
				while (dot-- > 0) {
					resultStr += "0";
				}
				resultNum = (resultStr + result).ToDouble();

				// add dot
			}
			else {

				// if integer
				if (dot == 0) {
					resultNum = result.ToDouble();
				}
				else {
					resultNum = (Sub(result, 0, (int)dot) + "." + Sub(result, (int)dot)).ToDouble();
				}
			}

			// return signed value
			return (sign && (value & 0x40000000) > 0) ? -resultNum : resultNum;
		}

		/// <summary>
		/// Extract the given substring using start index & length notation
		/// </summary>
		private static string Sub(string text, int start, int len = 0) {
			int sLen = text.Length;
			if (start >= sLen) {
				start = sLen - 1;
			}
			if (len == 0) {
				return text.Substring(start);
			}
			if (len > sLen) {
				len = sLen;
			}
			return text.Substring(start, len);
		}


	}
}