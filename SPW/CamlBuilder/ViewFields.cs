using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace SPW.CamlBuilder
{
	// NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
	/// <remarks />
	[Serializable]
	[DesignerCategory("code")]
	[XmlType(AnonymousType = true)]
	[XmlRoot(Namespace = "", IsNullable = false)]
	public class ViewFields
	{
		/// <remarks />
		[XmlElement("FieldRef")]
		public FieldRef[] FieldRef { get; set; }
	}
}