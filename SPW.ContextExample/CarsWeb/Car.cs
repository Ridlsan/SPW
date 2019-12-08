using System;
using SPW.SwListItemAttributes;

namespace SPW.ContextExample.CarsWeb
{
    public class Car : SwListItem
    {
        [SwField(FieldName = "Produced")]
        public DateTime? Produced { get; set; }

        [SwField]
        public string Title { get; set; }
    }
}