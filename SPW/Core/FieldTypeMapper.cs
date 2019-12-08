using System;

namespace SPW
{
    public class FieldTypeMapper
    {
        private static FieldTypeMapper _fieldTypeMapper;

        private FieldTypeMapper()
        {
        }

        public static FieldTypeMapper Instance => _fieldTypeMapper ?? (_fieldTypeMapper = new FieldTypeMapper());

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