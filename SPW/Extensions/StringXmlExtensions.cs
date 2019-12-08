namespace SPW.Extensions
{
	using System.IO;
	using System.Xml;
	using System.Xml.Serialization;

	/// <summary>
	///   Utilities for XML
	/// </summary>
	internal static class StringXmlExtensions
	{
		/// <summary>
		/// Parses the XML.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="this">The this.</param>
		/// <returns></returns>
		public static T ParseXML<T>(this string @this)
			where T : class
		{
			var reader = XmlReader.Create(
				@this.Trim().ToStream(),
				new XmlReaderSettings { ConformanceLevel = ConformanceLevel.Document });
			return new XmlSerializer(typeof(T)).Deserialize(reader) as T;
		}

		/// <summary>
		///   Converts to stream.
		/// </summary>
		/// <param name="currentString">Current string.</param>
		/// <returns></returns>
		public static Stream ToStream(this string currentString)
		{
			var stream = new MemoryStream();
			var writer = new StreamWriter(stream);
			writer.Write(currentString);
			writer.Flush();
			stream.Position = 0;
			return stream;
		}
	}
}