using Microsoft.SharePoint;
using System;

namespace SPW.Extensions
{
	/// <summary>
	///     Расширение для списков.
	/// </summary>
	public static class ListItemExtension
	{
		/// <summary>
		///     Return item value with specified type.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="item">The item.</param>
		/// <param name="fieldName">Name of the field.</param>
		/// <returns></returns>
		public static T Val<T>(this SPListItem item, string fieldName) where T : class
		{
			var value = item[fieldName];

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
				return (T)item.Fields[fieldName].GetFieldValue(strValue);
			}

			throw new ArgumentOutOfRangeException(nameof(fieldName));
		}

		/// <summary>
		///     Return item value of string.
		/// </summary>
		/// <param name="item">The item.</param>
		/// <param name="fieldName">Name of the field.</param>
		/// <returns></returns>
		public static string Val(this SPListItem item, string fieldName)
		{
			if (item[fieldName] == null)
			{
				return null;
			}

			return item[fieldName] + "";
		}

		/// <summary>
		///     Return item value of string.
		/// </summary>
		/// <param name="item">The item.</param>
		/// <param name="fieldName">Name of the field.</param>
		/// <returns></returns>
		public static SPFieldUserValue ValUser(this SPListItem item, string fieldName)
		{
			if (item[fieldName] == null)
			{
				return null;
			}

			return new SPFieldUserValue(item.Web, item[fieldName] + "");
		}

		/// <summary>
		///     Return item value of string.
		/// </summary>
		/// <param name="item">The item.</param>
		/// <param name="fieldName">Name of the field.</param>
		/// <returns></returns>
		public static bool? ValBool(this SPListItem item, string fieldName)
		{
			if (item[fieldName] == null)
			{
				return null;
			}

			if (item[fieldName] is bool boolVal)
			{
				return boolVal;
			}

			if (item[fieldName].ToString() == "")
			{
				return null;
			}

			return bool.Parse(item[fieldName].ToString());
		}

		/// <summary>
		///     Return item value of string.
		/// </summary>
		/// <param name="item">The item.</param>
		/// <param name="fieldName">Name of the field.</param>
		/// <returns></returns>
		public static int? ValInt(this SPListItem item, string fieldName)
		{
			if (item[fieldName] == null)
			{
				return null;
			}

			if (item[fieldName] is int boolVal)
			{
				return boolVal;
			}

			if (item[fieldName].ToString() == "")
			{
				return null;
			}

			return int.Parse(item[fieldName].ToString());
		}

		/// <summary>
		///     Return item value of string.
		/// </summary>
		/// <param name="item">The item.</param>
		/// <param name="fieldName">Name of the field.</param>
		/// <returns></returns>
		public static DateTime? ValDate(this SPListItem item, string fieldName)
		{
			if (item[fieldName] == null)
			{
				return null;
			}

			if (item[fieldName] is DateTime val)
			{
				return val;
			}

			if (item[fieldName].ToString() == "")
			{
				return null;
			}

			return DateTime.Parse(item[fieldName].ToString());
		}

		/// <summary>
		///     Возвращает урл формы на текущий элемент.
		/// </summary>
		/// <param name="item">Элемент.</param>
		/// <param name="pagetype">Тип формы.</param>
		/// <returns></returns>
		public static string Url(this SPListItem item, PAGETYPE pagetype = PAGETYPE.PAGE_DISPLAYFORM)
		{
			return $"{item.Web.Url}/{item.ParentList.Forms[pagetype].Url}?ID={item.ID}";
		}

		/// <summary>
		///     Возвращает HTML ссылку формы на текущий элемент.
		/// </summary>
		/// <param name="item">Элемент.</param>
		/// <param name="pagetype">Тип формы.</param>
		/// <returns></returns>
		public static string Link(this SPListItem item, PAGETYPE pagetype = PAGETYPE.PAGE_DISPLAYFORM)
		{
			return $"<a href=\"{item.Web.Url}/{item.ParentList.Forms[pagetype].Url}?ID={item.ID}\">{item.Title}</a>";
		}
	}
}