namespace SPW.Utils
{
	using System;

	/// <summary>
	///   Singleton mapper for fields
	/// </summary>
	internal class FieldTypeMapper
	{
		/// <summary>
		///   Instance
		/// </summary>
		private static FieldTypeMapper fieldTypeMapper;

		/// <summary>
		///   Prevents a default instance of the <see cref="FieldTypeMapper" /> class from being created.
		/// </summary>
		private FieldTypeMapper()
		{
		}

		/// <summary>
		///   Returns single instance
		/// </summary>
		public static FieldTypeMapper Instance => fieldTypeMapper ?? (fieldTypeMapper = new FieldTypeMapper());

		/// <summary>
		/// Converts the value of SharePoint Item to class property type
		/// </summary>
		/// <param name="fieldValue">Item field value.</param>
		/// <param name="resultType">Type of the result.</param>
		/// <returns>converted object</returns>
		public object ConvertValue(object fieldValue, Type resultType)
		{
			if (fieldValue == null)
			{
				return null;
			}

			if (resultType == fieldValue.GetType())
			{
				return fieldValue;
			}

			if (resultType == typeof(string))
			{
				return fieldValue.ToString();
			}

			if (resultType == typeof(DateTime) || resultType == typeof(DateTime?))
			{
				return DateTime.Parse(fieldValue.ToString());
			}

			return null;

			// throw new Exception($"Не удалось преобразовать значение к указанному типу {fieldValue} {resultType}");
		}
	}
}