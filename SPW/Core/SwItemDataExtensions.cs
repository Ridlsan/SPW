namespace SPW
{
	using System;
	using System.Collections.Generic;
	using Microsoft.SharePoint;
	using Newtonsoft.Json;


	/// <summary>
	///		Расширения для SwItemData
	/// </summary>
	internal static class SwItemDataExtensions
	{
		public static T FieldGetter<T>(this SwItemData data, string fieldName)
		{
			return (T)data[fieldName];
		}

		public static void FieldSetter<T>(this SwItemData data, string fieldName, T value)
		{
			data[fieldName] = value;
		}
	}
}