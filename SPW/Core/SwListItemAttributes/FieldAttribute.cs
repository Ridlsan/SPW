using System;

namespace SPW.SwListItemAttributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class SwField : Attribute
    {
        public string FieldName { get; set; }
    }
}