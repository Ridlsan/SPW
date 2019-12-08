namespace SPW.SwListItemAttributes
{
	using System;

	/// <summary>
	///   Attribute that says the property of class is SP field
	/// </summary>
	/// <seealso cref="System.Attribute" />
	[AttributeUsage(AttributeTargets.Property)]
	public class SwField : Attribute
	{
		/// <summary>
		///   Gets or sets the name of the field.
		/// </summary>
		/// <value>
		///   The internal name of the field.
		/// </value>
		public string InternalName { get; set; }
	}
}