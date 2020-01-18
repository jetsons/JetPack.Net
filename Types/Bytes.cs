using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Jetsons.JetPack {
	public static class Bytes {

		/// <summary>
		/// Convert the entire stream into an array of bytes.
		/// Seeks to the start of the stream before reading its bytes.
		/// The stream can be destroyed immediately after this call.
		/// </summary>
		/// <param name="stream">Input stream</param>
		public static byte[] ToBytes(this Stream stream) {

			// if its a memory stream
			if (stream is MemoryStream) {

				// call the platform method
				var mStream = stream as MemoryStream;
				return mStream.ToArray();

			}
			else {

				// rewind to the start of the stream if possible
				if (stream.CanSeek) {
					stream.Seek(0, SeekOrigin.Begin);
				}

				// read from here to the end
				int count = (int)stream.Length;
				var bytes = new byte[count];
				int read, pos = 0;
				while (pos < count && (read = stream.Read(bytes, pos, count - pos)) > 0) {
					pos += read;
				}
				return bytes;

			}
		}

		/// <summary>
		/// Convert the entire stream into an array of bytes, wrapped in a JetArray.
		/// Seeks to the start of the stream before reading its bytes.
		/// The stream can be destroyed immediately after this call.
		/// </summary>
		/// <param name="stream">Input stream</param>
		public static JetArray ToJetArray(this Stream stream) {
			return new JetArray(stream.ToBytes());
		}

		/// <summary>
		/// Wraps this array of bytes into a JetArray.
		/// </summary>
		/// <param name="stream">Input bytes</param>
		public static JetArray ToJetArray(this byte[] stream) {
			return new JetArray(stream);
		}

		/// <summary>
		/// Wraps this array of bytes into a MemoryStream.
		/// </summary>
		/// <param name="stream">Input bytes</param>
		public static MemoryStream ToStream(this byte[] stream) {
			return new MemoryStream(stream);
		}


		/// <summary>
		/// Encode an object into data serialized with the .NET Framework BinaryFormatter.
		/// Supports most object graphs. Returns null if serialization failed.
		/// </summary>
		public static byte[] EncodeBinaryFormatted(this object obj) {
			using (var mem = new MemoryStream()) {
				using (StreamWriter streamWriter = new StreamWriter(mem)) {
					BinaryFormatter binaryFormatter = new BinaryFormatter();
					try {
						binaryFormatter.Serialize(streamWriter.BaseStream, obj);
						return mem.ToBytes();
					}
					catch (Exception ex) {
						return null;
					}
				}
			}
		}

		/// <summary>
		/// Decode data serialized with the .NET Framework BinaryFormatter into an object.
		/// Returns null if deserialization failed.
		/// </summary>
		public static object DecodeBinaryFormatted(this byte[] obj) {
			using (var mem = new MemoryStream(obj)) {
				using (StreamReader streamWriter = new StreamReader(mem)) {
					BinaryFormatter binaryFormatter = new BinaryFormatter();
					try {
						return binaryFormatter.Deserialize(streamWriter.BaseStream);
					}
					catch (Exception ex) {
						return null;
					}
				}
			}
		}

	}
}
