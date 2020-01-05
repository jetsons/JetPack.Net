using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

namespace Jetsons.JetPack {

	/// <summary>
	/// A simple class used to generate and parse binary streams consisting of integers, floating points and strings.
	/// </summary>
	public class JetArray {

		/// <summary>
		/// Set the endianness of this JetArray. All multi-byte methods honor this parameter.
		/// </summary>
		public bool BigEndian = true;

		/// <summary>
		/// The underlying stream used for reading and writing data.
		/// </summary>
		private MemoryStream stream;

		/// <summary>
		/// The underlying BinaryReader used for reading data from the stream.
		/// </summary>
		private BinaryReader reader;

		/// <summary>
		/// The underlying BinaryWriter used for writing data to the stream.
		/// </summary>
		private BinaryWriter writer;

		/// <summary>
		/// If reading bytes by index should directly read the original byte array used to create this object.
		/// </summary>
		public bool ReadRawStream = false;

		/// <summary>
		/// The original byte array used to create this JetArray, if any.
		/// </summary>
		private byte[] Bytes;


		/// <summary>
		/// Create a new JetArray.
		/// </summary>
		public JetArray() {
			stream = new MemoryStream();
			Setup();
		}

		/// <summary>
		/// Create a new JetArray from the given MemoryStream.
		/// </summary>
		public JetArray(MemoryStream ms) {
			stream = ms;
			Setup();
		}

		/// <summary>
		/// Create a new JetArray from the given byte array.
		/// </summary>
		public JetArray(byte[] buffer) {
			Bytes = buffer;
			stream = new MemoryStream(buffer);
			Setup();
		}

		/// <summary>
		/// Create a new JetArray from the given JetArray.
		/// </summary>
		public JetArray(JetArray buffer) {
			stream = new MemoryStream(buffer.ToArray());
			Setup();
		}

		/// <summary>
		/// Prints this byte stream to a string
		/// </summary>
		/// <returns></returns>
		public override string ToString() {
			return BitConverter.ToString(ToArray()).Replace("-", " ");
		}

		/// <summary>
		/// Creates the underlying binary reader and writer
		/// </summary>
		private void Setup() {
			reader = new BinaryReader(stream);
			writer = new BinaryWriter(stream);
		}


		/// <summary>
		/// Append a JetArray to the end of the stream, returning a new JetArray object
		/// </summary>
		public JetArray Append(JetArray add, bool posToEnd) {
			return Append(add.ToArray(), posToEnd);
		}

		/// <summary>
		/// Append a JetArray to the end of the stream, returning a new JetArray object
		/// </summary>
		public JetArray Append(byte[] add, bool posToEnd) {

			// save
			uint curPos = Position;

			byte[] resultBuf = new byte[Length + add.Length];
			byte[] mainBuf = ToArray();
			byte[] addBuf = add;

			// do a fast copy
			Buffer.BlockCopy(mainBuf, 0, resultBuf, 0, mainBuf.Length);
			Buffer.BlockCopy(addBuf, 0, resultBuf, mainBuf.Length, addBuf.Length);

			// restore
			JetArray resultBuffer = new JetArray(resultBuf);
			resultBuffer.Position = posToEnd ? resultBuffer.Length : curPos;
			return resultBuffer;
		}

		/// <summary>
		/// Returns a new JetArray, the result of A ^ B
		/// </summary>
		public JetArray Xor(JetArray oprand) {

			// create merged buffer
			JetArray bytes = new JetArray();
			Position = 0;
			oprand.Position = 0;
			for (uint b = 0, bl = Length; b < bl; b = (b + 1)) {
				bytes.WriteByte((byte)(ReadByte() ^ oprand.ReadByte()));
			}
			return bytes;
		}



		/// <summary>
		/// Gets the length of the array in bytes.
		/// </summary>
		public uint Length {
			get { return (uint)stream.Length; }
		}
		/// <summary>
		/// Gets or sets the current position, in bytes, of the file pointer into the JetArray object.
		/// </summary>
		public uint Position {
			get { return (uint)stream.Position; }
			set { stream.Position = value; }
		}

		/// <summary>
		/// The number of bytes of data available for reading from the current position in the byte array to the end of the array.
		/// </summary>
		/// <value>The number of bytes of data available for reading from the current position.</value>
		public uint BytesAvailable {
			get { return Length - Position; }
		}
		/// <summary>
		/// Returns the array of unsigned bytes from which this JetArray was created.
		/// </summary>
		/// <returns>The byte array from which this JetArray was created, or the underlying array if a byte array was not provided to the JetArray constructor during construction of the current instance.</returns>
		public byte[] GetBuffer() {
			return stream.GetBuffer();
		}

		/// <summary>
		/// Writes the JetArray contents to a byte array, regardless of the Position property.
		/// </summary>
		/// <returns>A new byte array.</returns>
		/// <remarks>
		/// This method omits unused bytes in the underlying MemoryStream from the JetArray.
		/// This method returns a copy of the contents of the underlying MemoryStream as a byte array. 
		/// </remarks>
		public byte[] ToArray() {
			return stream.ToArray();
		}

		/// <summary>
		/// Gets the MemoryStream from which this JetArray was created.
		/// </summary>
		internal MemoryStream Stream { get { return stream; } }


		/// <summary>
		/// Gets or sets a specific byte in the stream.
		/// </summary>
		/// <param name="i"></param>
		/// <returns></returns>
		public byte this[uint i] {
			get {
				if (ReadRawStream) {
					return Bytes[i];
				}
				long pos = stream.Position;
				stream.Position = i;
				byte val = reader.ReadByte();
				stream.Position = pos;
				return val;
			}
		}


		/// <summary>
		/// Reads a Boolean from the stream. 
		/// </summary>

		public bool ReadBoolean() {
			return reader.ReadBoolean();
		}


		/// <summary>
		/// Reads a certain number of bytes of data from the stream. 
		/// </summary>
		/// <param name="bytes"></param>
		/// <param name="offset"></param>
		/// <param name="length"></param>
		public void ReadBytes(byte[] bytes, uint offset, uint length) {
			byte[] tmp = reader.ReadBytes((int)length);
			for (int i = 0; i < tmp.Length; i++)
				bytes[i + offset] = tmp[i];
		}

		/// <summary>
		/// Reads all available bytes from the current position of the stream. 
		/// </summary>
		public byte[] ReadBytesArray() {
			return reader.ReadBytes((int)BytesAvailable);
		}

		/// <summary>
		/// Reads a certain number of bytes of data from the stream. 
		/// </summary>
		public byte[] ReadBytesArray(int length) {
			return reader.ReadBytes((int)length);
		}

		/// <summary>
		/// Reads a certain portion of bytes of data from the stream. 
		/// </summary>
		public byte[] ReadBytesArray(uint offset, uint length) {
			byte[] bytesBuf = new byte[Math.Min(length, BytesAvailable)];
			ReadBytes(bytesBuf, offset, length);
			return bytesBuf;
		}
		private byte[] ReadBytesBE(int length) {
			byte[] bytes = reader.ReadBytes(length);
			Array.Reverse(bytes, 0, length);
			return bytes;
		}
		private byte[] ReadBytes(int length) {
			return reader.ReadBytes(length);
		}

		/// <summary>
		/// Reads an IEEE 754 double-precision floating point number in Big Endian. 
		/// </summary>

		public double ReadDouble() {

			if (BigEndian) {
				return BitConverter.ToDouble(ReadBytesBE(8), 0);
			}
			else {
				// Read using LE
				return reader.ReadDouble();
			}
		}
		private double ReadDoubleBE() {
			return BitConverter.ToDouble(ReadBytesBE(8), 0);
		}

		/// <summary>
		/// Reads an IEEE 754 single-precision floating point number in Big Endian. 
		/// </summary>

		public float ReadFloat() {

			if (BigEndian) {
				return BitConverter.ToSingle(ReadBytesBE(4), 0);
			}
			else {
				// Read using LE
				return reader.ReadSingle();
			}
		}

		/// <summary>
		/// Reads a signed 32-bit integer from the stream. 
		/// </summary>

		public int ReadInt() {

			if (BigEndian) {
				return BitConverter.ToInt32(ReadBytesBE(4), 0);
			}
			else {
				// Read using LE
				return reader.ReadInt32();
			}
		}
		private int ReadIntBE() {
			return BitConverter.ToInt32(ReadBytesBE(4), 0);
		}

		/// <summary>
		/// Reads an unsigned 32-bit integer from the stream. 
		/// </summary>

		public uint ReadUInt() {

			if (BigEndian) {
				return BitConverter.ToUInt32(ReadBytesBE(4), 0);
			}
			else {
				// Read using LE
				return reader.ReadUInt32();
			}
		}
		private uint ReadUnsignedIntBE() {
			return BitConverter.ToUInt32(ReadBytesBE(4), 0);
		}

		/// <summary>
		/// Reads a signed 16-bit integer from the stream. 
		/// </summary>

		public short ReadShort() {

			if (BigEndian) {
				return BitConverter.ToInt16(ReadBytesBE(2), 0);
			}
			else {
				return reader.ReadInt16();
			}
		}

		/// <summary>
		/// Reads an unsigned 16-bit integer from the stream. 
		/// </summary>

		public ushort ReadUShort() {

			if (BigEndian) {
				return BitConverter.ToUInt16(ReadBytesBE(2), 0);
			}
			else {
				return reader.ReadUInt16();
			}
		}

		/// <summary>
		/// Reads a signed byte from the stream. 
		/// </summary>

		public sbyte ReadSByte() {
			return reader.ReadSByte();
		}

		/// <summary>
		/// Reads an unsigned byte from the stream. 
		/// </summary>

		public byte ReadByte() {
			return reader.ReadByte();
		}

		/// <summary>
		/// Reads an unsigned byte from the stream. 
		/// </summary>

		public char ReadChar() {
			return (char)reader.ReadByte();
		}


		/// <summary>
		/// Reads a 64-bit signed integer to the stream.
		/// </summary>
		/// <param name="value">A 64-bit signed integer.</param>
		/// <remarks>No type marker is written in the stream.</remarks>
		public long ReadLong() {
			if (BigEndian) {
				return BitConverter.ToInt64(ReadBytesBE(8), 0);
			}
			else {
				return BitConverter.ToInt64(ReadBytes(8), 0);
			}
		}
		/// <summary>
		/// Reads a 64-bit unsigned integer to the stream.
		/// </summary>
		/// <param name="value">A 64-bit unsigned integer.</param>
		/// <remarks>No type marker is written in the stream.</remarks>
		public ulong ReadULong() {
			if (BigEndian) {
				return BitConverter.ToUInt64(ReadBytesBE(8), 0);
			}
			else {
				return BitConverter.ToUInt64(ReadBytes(8), 0);
			}
		}



		/// <summary>
		/// Writes an unsigned byte to the stream.
		/// </summary>
		/// <param name="value">A byte to write to the stream.</param>
		public void WriteByte(byte value) {
			writer.BaseStream.WriteByte(value);
		}

		/// <summary>
		/// Writes an signed byte to the stream.
		/// </summary>
		/// <param name="value">A byte to write to the stream.</param>
		public void WriteSByte(sbyte value) {
			writer.BaseStream.WriteByte((byte)(value + 127));
		}

		/// <summary>
		/// Writes an unsigned byte (char) to the stream.
		/// </summary>
		/// <param name="value">A byte to write to the stream.</param>
		public void WriteChar(char value) {
			writer.BaseStream.WriteByte((byte)value);
		}

		/// <summary>
		/// Writes a stream of bytes to the stream.
		/// </summary>
		/// <param name="buffer">The memory buffer containing the bytes to write to the AMF stream</param>
		public void WriteBytes(byte[] buffer) {
			for (int i = 0; buffer != null && i < buffer.Length; i++)
				writer.BaseStream.WriteByte(buffer[i]);
		}

		/// <summary>
		/// Writes a stream of bytes to the stream.
		/// </summary>
		public void WriteBytes(JetArray buffer) {
			WriteBytes(buffer.ToArray());
		}

		/// <summary>
		/// Writes a 16-bit unsigned integer to the stream.
		/// </summary>
		/// <param name="value">A 16-bit unsigned integer.</param>
		public void WriteUShort(int value) {
			if (BigEndian) {
				WriteBigEndian(BitConverter.GetBytes((ushort)value));
			}
			else {
				writer.Write((ushort)value);
			}
		}

		/// <summary>
		/// Writes a 16-bit signed integer to the stream.
		/// </summary>
		/// <param name="value">A 16-bit signed integer.</param>
		public void WriteShort(int value) {
			if (BigEndian) {
				WriteBigEndian(BitConverter.GetBytes((short)value));
			}
			else {
				writer.Write((short)value);
			}
		}

		/// <summary>
		/// Writes a single-precision floating point number in Big Endian, to the stream.
		/// </summary>
		/// <param name="value">A single-precision floating point number.</param>
		/// <remarks>No type marker is written in the stream.</remarks>
		public void WriteFloat(float value) {
			if (BigEndian) {
				WriteBigEndian(BitConverter.GetBytes(value));
			}
			else {
				writer.Write(value);
			}
		}
		/// <summary>
		/// Writes a double-precision floating point number in Big Endian, to the stream.
		/// </summary>
		/// <param name="value">A double-precision floating point number.</param>
		/// <remarks>No type marker is written in the stream.</remarks>
		public void WriteDouble(double value) {
			if (BigEndian) {
				WriteBigEndian(BitConverter.GetBytes(value));
			}
			else {
				writer.Write(value);
			}
		}

		/// <summary>
		/// Writes a 32-bit signed integer to the stream.
		/// </summary>
		/// <param name="value">A 32-bit signed integer.</param>
		/// <remarks>No type marker is written in the stream.</remarks>
		public void WriteInt(int value) {
			if (BigEndian) {
				WriteBigEndian(BitConverter.GetBytes(value));
			}
			else {
				writer.Write(value);
			}
		}

		/// <summary>
		/// Writes a 32-bit unsigned integer to the stream.
		/// </summary>
		/// <param name="value">A 32-bit signed integer.</param>
		/// <remarks>No type marker is written in the stream.</remarks>
		public void WriteUInt(uint value) {
			if (BigEndian) {
				WriteBigEndian(BitConverter.GetBytes(value));
			}
			else {
				writer.Write(value);
			}
		}
		/// <summary>
		/// Writes a 32-bit signed integer to the stream using variable length unsigned 29-bit integer encoding.
		/// </summary>
		/// <param name="value">A 32-bit signed integer.</param>
		/// <remarks>No type marker is written in the stream.</remarks>
		public void WriteUInt24(int value) {
			byte[] bytes = new byte[3];
			bytes[0] = (byte)(0xFF & (value >> 16));
			bytes[1] = (byte)(0xFF & (value >> 8));
			bytes[2] = (byte)(0xFF & (value >> 0));
			writer.BaseStream.Write(bytes, 0, bytes.Length);
		}

		/// <summary>
		/// Writes a 64-bit signed integer to the stream.
		/// </summary>
		/// <param name="value">A 64-bit signed integer.</param>
		/// <remarks>No type marker is written in the stream.</remarks>
		public void WriteLong(long value) {
			if (BigEndian) {
				WriteBigEndian(BitConverter.GetBytes(value));
			}
			else {
				writer.Write(BitConverter.GetBytes(value));
			}
		}
		/// <summary>
		/// Writes a 64-bit unsigned integer to the stream.
		/// </summary>
		/// <param name="value">A 64-bit unsigned integer.</param>
		/// <remarks>No type marker is written in the stream.</remarks>
		public void WriteULong(ulong value) {
			if (BigEndian) {
				WriteBigEndian(BitConverter.GetBytes(value));
			}
			else {
				writer.Write(BitConverter.GetBytes(value));
			}
		}


		/// <summary>
		/// Writes an unsigned byte to a 2-byte hex string (or 3-byte with seperator).
		/// </summary>
		public void WriteByteHex(byte value, bool seperator = false, char sep = ',') {
			WriteCharHex(value >> 4);
			WriteCharHex(value);
			if (seperator) {
				WriteChar(sep);
			}
		}

		/// <summary>
		/// Writes an unsigned 2-byte integer to a 4-byte hex string (or 5-byte with seperator).
		/// </summary>
		public void WriteUShortHex(ushort value, bool seperator = false, char sep = ',') {
			WriteCharHex(value >> 12);
			WriteCharHex(value >> 8);
			WriteCharHex(value >> 4);
			WriteCharHex(value);
			if (seperator) {
				WriteChar(sep);
			}
		}

		/// <summary>
		/// Writes an unsigned byte integer to a 1-byte hex string.
		/// </summary>
		private void WriteCharHex(int num) {

			byte value = (byte)(num & 0xF);

			if (value >= 10) {
				value += 55;
			}
			else {
				value |= 0x30;
			}

			WriteByte(value);
		}



		/// <summary>
		/// Writes a Boolean value to the stream.
		/// </summary>
		/// <param name="value">A Boolean value.</param>
		/// <remarks>No type marker is written in the stream.</remarks>
		public void WriteBoolean(bool value) {
			writer.BaseStream.WriteByte(value ? ((byte)1) : ((byte)0));
		}

		/// <summary>
		/// Writes the given bytes into the stream using big-endian encoding
		/// </summary>
		/// <param name="bytes"></param>
		private void WriteBigEndian(byte[] bytes) {
			if (bytes == null) {
				return;
			}
			for (int i = bytes.Length - 1; i >= 0; i--) {
				writer.BaseStream.WriteByte(bytes[i]);
			}
		}

		/// <summary>
		/// Writes a sequence of length bytes from the specified byte array, bytes, starting at offset (zero-based index) bytes into the byte stream.
		/// </summary>
		public void WriteBytes(byte[] bytes, int offset, int length) {
			for (int i = offset; i < offset + length; i++) {
				writer.BaseStream.WriteByte(bytes[i]);
			}
		}
		/// <summary>
		/// Writes all bytes from the specified byte array, into the byte stream.
		/// </summary>
		public void WriteBytesArray(byte[] bytes) {
			int length = bytes.Length;
			for (int i = 0; i < length; i++) {
				writer.BaseStream.WriteByte(bytes[i]);
			}
		}
		


		/// <summary>
		/// Reads a UTF-8 string from the stream. 
		/// </summary>
		public string ReadString(int knownLength = Int32.MinValue) {
			if (knownLength != Int32.MinValue) {
				return ReadUTFBytes(knownLength);
			}
			else {
				return ReadUTF();
			}
		}

		/// <summary>
		/// Reads a UTF-8 string from the stream. 
		/// </summary>

		private string ReadUTF() {
			//Get the length of the string (first 2 bytes).
			int length = ReadUShort();
			return ReadUTFBytes(length);
		}

		/// <summary>
		/// Reads a sequence of length UTF-8 bytes from the stream, and returns a string. 
		/// </summary>
		private string ReadUTFBytes(int length) {
			if (length == 0) {
				return string.Empty;
			}
			UTF8Encoding utf8 = new UTF8Encoding(false, true);
			byte[] encodedBytes = reader.ReadBytes(length);

			// skip NULL byte if NULL terminated string
			int len = encodedBytes.Length;
			if (len > 0 && encodedBytes[len - 1] == 0) {
				len--;
			}
			if (len == 0) {
				return string.Empty;
			}

			// convert bytes to UTF
			return utf8.GetString(encodedBytes, 0, len);
		}

		/// <summary>
		/// Reads a sequence of ASCII bytes from the stream. 
		/// </summary>
		public string ReadASCIIBytes(int length) {
			return Encoding.ASCII.GetString(ReadBytesArray(length));
		}

		/// <summary>
		/// Writes a UTF-8 string from the stream. 
		/// </summary>
		public void WriteString(string text, bool knownLength = false) {
			if (knownLength) {
				if (text != null && text.Length > 0) {
					WriteUTFBytes(text);
				}
			}
			else {
				if (text != null && text != "") {
					WriteUTF(text);
				}
				else {
					WriteUShort(0);
				}
			}
		}

		/// <summary>
		/// Writes a UTF-8 string to the stream.
		/// The length of the UTF-8 string in bytes is written first, as a 16-bit integer, followed by the bytes representing the characters of the string.
		/// </summary>
		/// <param name="value">The UTF-8 string, must be at most 65536 chars long.</param>
		/// <remarks>Standard or long string header is not written.</remarks>
		private void WriteUTF(string value) {
			UTF8Encoding utf8Encoding = new UTF8Encoding();
			int byteCount = utf8Encoding.GetByteCount(value);
			byte[] buffer = utf8Encoding.GetBytes(value);
			this.WriteUShort(byteCount);
			if (buffer.Length > 0) {
				writer.Write(buffer);
			}
		}

		/// <summary>
		/// Writes a UTF-8 string to the stream.
		/// Similar to WriteUTF, but does not prefix the string with a 16-bit length word.
		/// </summary>
		/// <param name="value">The UTF-8 string, must be at most 65536 chars long.</param>
		/// <remarks>Standard or long string header is not written.</remarks>
		private void WriteUTFBytes(string value) {
			UTF8Encoding utf8Encoding = new UTF8Encoding();
			byte[] buffer = utf8Encoding.GetBytes(value);
			if (buffer.Length > 0) {
				writer.Write(buffer);
			}
		}


		/// <summary>
		/// Writes a bit array of any length into the given stream.
		/// The bit array cannot contain nulls, and can be any length.
		/// The length of the array is not written - write it manually.
		/// </summary>
		public void WriteBitArray(List<int> bits) {

			int numOfBits = bits.Count;
			int totalBits = (int)Math.Ceiling(((double)numOfBits / 8d)) * 8;

			// ensure all ending bits filled
			while (bits.Count < totalBits) {
				bits.Add(0);
			}

			// write bit array bytes
			for (int b = 0; b < totalBits; b += 8) {
				WriteByte((byte)(bits[b] | (bits[b + 1] << 1) | (bits[b + 2] << 2) | (bits[b + 3] << 3) | (bits[b + 4] << 4) | (bits[b + 5] << 5) | (bits[b + 6] << 6) | (bits[b + 7] << 7)));
			}

		}

		/// <summary>
		/// Writes a bit array of any length into the given stream.
		/// The bit array cannot contain nulls, and can be any length.
		/// The length of the array is not written - write it manually.
		/// </summary>
		public void WriteBitArray(List<bool> bits) {

			int numOfBits = bits.Count;
			int totalBits = (int)Math.Ceiling(((double)numOfBits / 8d)) * 8;

			// ensure all ending bits filled
			while (bits.Count < totalBits) {
				bits.Add(false);
			}

			// write bit array bytes
			for (int b = 0; b < totalBits; b += 8) {
				WriteByte((byte)((bits[b] ? 1 : 0) | ((bits[b + 1] ? 1 : 0) << 1) | ((bits[b + 2] ? 1 : 0) << 2) | ((bits[b + 3] ? 1 : 0) << 3) | ((bits[b + 4] ? 1 : 0) << 4) | ((bits[b + 5] ? 1 : 0) << 5) | ((bits[b + 6] ? 1 : 0) << 6) | ((bits[b + 7] ? 1 : 0) << 7)));
			}

		}

		/// <summary>
		/// Reads a bit array of any length from the given stream.
		/// The number of bits must be provided - the length of the array was not written
		/// </summary>
		public List<int> ReadBitArray(int numOfBits) {

			// calc num of final bits
			int totalBits = (int)Math.Ceiling(((double)numOfBits / 8d)) * 8;

			// read bit array bytes
			List<int> bits = new List<int>(new int[totalBits]);
			for (int b = 0; b < totalBits; b += 8) {
				int flags = ReadByte();
				bits[b] = flags & 1;
				bits[b + 1] = (flags & 2) >> 1;
				bits[b + 2] = (flags & 4) >> 2;
				bits[b + 3] = (flags & 8) >> 3;
				bits[b + 4] = (flags & 16) >> 4;
				bits[b + 5] = (flags & 32) >> 5;
				bits[b + 6] = (flags & 64) >> 6;
				bits[b + 7] = (flags & 128) >> 7;
			}

			// remove extra bits
			while (bits.Count > numOfBits) {
				bits.RemoveLast();
			}

			return bits;
		}


		/// <summary>
		/// Write a single byte of 8 flags.
		/// </summary>
		public void WriteFlags(List<int> bits) {

			// get bit value
			int value = 0;
			for (int b = 0; b < 8; b++) {
				if (bits[b] == 1) {
					value |= (1 << b);
				}
			}

			// write bits
			WriteByte((byte)value);

		}

		/// <summary>
		/// Write a single byte of 8 flags.
		/// </summary>
		public void WriteFlags(List<bool> bits) {

			// get bit value
			int value = 0;
			for (int b = 0; b < 8; b++) {
				if (bits[b]) {
					value |= (1 << b);
				}
			}

			// write bits
			WriteByte((byte)value);

		}

		/// <summary>
		/// Read a single byte of 8-flags.
		/// </summary>
		/// <returns></returns>
		public List<int> ReadFlags() {
			int flags = ReadByte();
			if (flags == 0) {
				return new List<int> { 0, 0, 0, 0, 0, 0, 0, 0 };
			}
			return new List<int> { flags & 1, (flags & 2) >> 1, (flags & 4) >> 2, (flags & 8) >> 3, (flags & 16) >> 4, (flags & 32) >> 5, (flags & 64) >> 6, (flags & 128) >> 7 };
		}


		/// <summary>
		/// Reads a variable length 32-bit unsigned integer from the stream.
		/// The number will use 1 to 5 bytes depending on its length.
		/// </summary>
		public uint ReadVarUInt32() {
			uint result = ReadByte();
			if ((result & 0x80) > 0) {
				result = (result & 0x7f) | ((uint)ReadByte() << 7);
				if ((result & 0x4000) > 0) {
					result = (result & 0x3fff) | ((uint)ReadByte() << 14);
					if ((result & 0x200000) > 0) {
						result = (result & 0x1fffff) | ((uint)ReadByte() << 21);
						if ((result & 0x10000000) > 0) {
							result = (result & 0xfffffff) | ((uint)ReadByte() << 28);
						}
					}
				}
			}
			return result;
		}

		/// <summary>
		/// Reads a variable length 32-bit integer from the stream.
		/// The number will use 1 to 5 bytes depending on its length.
		/// </summary>
		public int ReadVarInt32() {

			uint u = ReadVarUInt32();
			bool isNeg = ((u & 1) == 1);    /// check 1st bit (bit 0)
			u = (u & 0xFFFFFFFE) >> 1;      /// ignore 1st bit (bit 0)
			if (isNeg) {
				return -((int)(u + 1));
			}
			else {
				return (int)(u);
			}

		}


		/// <summary>
		/// Writes a variable length 32-bit unsigned integer to the stream.
		/// The number will use 1 to 5 bytes depending on its length.
		/// </summary>
		public void WriteVarUInt32(uint number) {

			if (number < 128) {
				WriteByte((byte)number);
				return;
			}

			uint v = number & 0x7f;
			if ((number >>= 7) != 0) {
				WriteByte((byte)(v | 0x80));
				v = number & 0x7f;

				if ((number >>= 7) != 0) {
					WriteByte((byte)(v | 0x80));
					v = number & 0x7f;

					if ((number >>= 7) != 0) {
						WriteByte((byte)(v | 0x80));
						v = number & 0x7f;

						if ((number >>= 7) != 0) {
							WriteByte((byte)(v | 0x80));
							v = number & 0x7f;
						}
					}
				}
			}

			WriteByte((byte)v);

		}

		/// <summary>
		/// Writes a variable length 32-bit integer to the stream.
		/// The number will use 1 to 5 bytes depending on its length.
		/// </summary>
		public void WriteVarInt32(int number) {
			uint u = 0;
			if (number < 0) {
				u = ((uint)(-number)) - 1;
				u = (u << 1) | 1;       /// set 1st bit if negative (bit 0)
			}
			else {
				u = ((uint)number) << 1;    /// clear 1st bit if negative
			}
			WriteVarUInt32(u);

		}

		/// <summary>
		/// Reads a variable length floating point value from the stream.
		/// The number will use 2 to 9 bytes depending on its length.
		/// </summary>
		public double ReadVarDouble(bool signed) {
			int length = ReadByte();
			if (length == 1) {
				return signed ? (double)ReadSByte() : (double)ReadByte();
			}
			if (length == 2) {
				return VariableLength.DecodeDouble16(ReadUShort(), signed);
			}
			if (length == 4) {
				return VariableLength.DecodeDouble32(ReadUShort(), signed);
			}
			else {
				return ReadDouble();
			}
		}

		/// <summary>
		/// Writes a variable length floating point value to the stream.
		/// The number will use 2 to 9 bytes depending on its length.
		/// </summary>
		public void WriteVarDouble(double number, bool signed) {
			var length = VariableLength.GetVarDoubleLength(number, signed);
			WriteByte((byte)length);
			if (length == 1) {
				if (signed) {
					WriteSByte((sbyte)number);
				}
				else {
					WriteByte((byte)number);
				}
			}
			if (length == 2) {
				WriteUShort(VariableLength.EncodeDouble16(ReadUShort(), signed));
			}
			if (length == 4) {
				WriteUInt(VariableLength.EncodeDouble32(ReadUShort(), signed));
			}
			else {
				ReadDouble();
			}
		}
		

	}
	
}