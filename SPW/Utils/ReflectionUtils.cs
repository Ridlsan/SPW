namespace SPW.Utils
{
	using System.Reflection;

	/// <summary>
	///   Utilities for reflection.
	/// </summary>
	public class ReflectionUtils
	{
		/// <summary>
		/// Finds properties in SwListItem that fits some BindingFlags.
		/// </summary>
		/// <typeparam name="T">The SwListItem.</typeparam>
		/// <returns>Property collection.</returns>
		public static PropertyInfo[] GetProperties<T>()
			where T : SwListItem
		{
			return typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
		}
	}
}