namespace SPW
{
	using Newtonsoft.Json;
	using System;
	using System.Collections.Generic;

	/// <summary>
	///   Represents List Item Data in dynamic fashion.
	/// </summary>
	/// <seealso>
	///   <cref>Dictionary{string, object}</cref>
	/// </seealso>
	public class SwItemData : Dictionary<string, object>
	{
		/// <summary>
		///     Gets or sets the <see cref="object" /> with the specified field.
		/// </summary>
		/// <value>
		///     The <see cref="object" />.
		/// </value>
		/// <param name="field">The field.</param>
		/// <returns></returns>
		public new object this[string field]
		{
			get
			{
				if (this.ContainsKey(field))
				{
					return base[field];
				}

				throw new KeyNotFoundException($"{field} in item not found");
			}

			set
			{
				base[field] = value;
			}
		}

		/// <summary>
		/// Real id. Its checked when updating. If not equals to ID then throws error.
		/// </summary>
		internal int SourceId { get; set; }

		/// <summary>
		/// 	Gets or sets id of the item.
		/// </summary>
		public int ID
		{
			get => this.FieldGetter<int>(nameof(this.ID));
			set => this.FieldSetter(nameof(this.ID), value);
		}

		/// <summary>
		/// 	Gets or sets Title of the item.
		/// </summary>
		public string Title
		{
			get => this.FieldGetter<string>(nameof(this.Title));
			set => this.FieldSetter(nameof(this.Title), value);
		}

		/// <summary>
		/// Adds the specified field.
		/// </summary>
		/// <param name="field">The field.</param>
		/// <param name="value">The value.</param>
		public new void Add(string field, object value)
		{
			if (!this.ContainsKey(field))
			{
				base.Add(field, value);
			}
			else
			{
				base[field] = value;
			}
		}

		/// <summary>
		///     Return item value with specified type.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="fieldName">Name of the field.</param>
		/// <returns></returns>
		public T Val<T>(string fieldName)
			where T : class
		{
			var value = this[fieldName];

			if (value == null)
			{
				return default;
			}

			if (value is T tValue)
			{
				return tValue;
			}

			if (value is string strValue)
			{
				return (T)Activator.CreateInstance(typeof(T), strValue);
			}

			throw new ArgumentOutOfRangeException(nameof(fieldName));
		}

		/// <summary>
		///     Return item value of string
		/// </summary>
		/// <param name="fieldName">Name of the field.</param>
		/// <returns></returns>
		public string Val(string fieldName)
		{
			if (this[fieldName] == null)
			{
				return null;
			}

			return this[fieldName] + string.Empty;
		}

		/// <summary>
		///     Return item value of string
		/// </summary>
		/// <param name="fieldName">Name of the field.</param>
		/// <returns></returns>
		public bool? ValBool(string fieldName)
		{
			if (this[fieldName] == null)
			{
				return null;
			}

			if (this[fieldName] is bool boolVal)
			{
				return boolVal;
			}

			if (this[fieldName].ToString() == string.Empty)
			{
				return null;
			}

			return bool.Parse(this[fieldName].ToString());
		}

		/// <summary>
		///     Return item value of string
		/// </summary>
		/// <param name="fieldName">Name of the field.</param>
		/// <returns></returns>
		public int? ValInt(string fieldName)
		{
			if (this[fieldName] == null)
			{
				return null;
			}

			if (this[fieldName] is int boolVal)
			{
				return boolVal;
			}

			if (this[fieldName].ToString() == string.Empty)
			{
				return null;
			}

			return int.Parse(this[fieldName].ToString());
		}

		/// <summary>
		///     Return item value of string
		/// </summary>
		/// <param name="fieldName">Name of the field.</param>
		/// <returns></returns>
		public DateTime? ValDate(string fieldName)
		{
			if (this[fieldName] == null)
			{
				return null;
			}

			if (this[fieldName] is DateTime val)
			{
				return val;
			}

			if (this[fieldName].ToString() == string.Empty)
			{
				return null;
			}

			return DateTime.Parse(this[fieldName].ToString());
		}

		/// <summary>
		///     Return item value with specified type
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="fieldName">Name of the field.</param>
		/// <returns></returns>
		public T Deserialize<T>(string fieldName)
			where T : class
		{
			var value = Val(fieldName);

			if (value == null)
			{
				return null;
			}

			return JsonConvert.DeserializeObject<T>(value);
		}

	}
}