using System;
using System.Collections.Generic;
using Microsoft.SharePoint;

namespace SPW
{
    /// <summary>
    ///     Параметры для полей
    /// </summary>
    public class SwFieldTemplate
    {
        /// <summary>
        ///     Для полей типа Note. Режим только добавления текста
        /// </summary>
        public bool? AppendOnly { get; set; }

        /// <summary>
        ///     Варианты для полей типа выбор
        /// </summary>
        public IEnumerable<string> Choices { get; set; }

        /// <summary>
        ///     Тип поля в который нужно сконвертировать
        /// </summary>
        public string ConvertFieldType { get; set; }

        /// <summary>
        ///     У полей унаследованных от HackedField есть свойство Data. Значение этого поля заполнится
        /// </summary>
        public object DataProperty { get; set; }

        /// <summary>
        ///     Формат для дат
        /// </summary>
        public SPDateTimeFieldFormatType? DateTimeFormat { get; set; }

        /// <summary>
        ///     Значение по-умолчанию
        /// </summary>
        public string DefaultValue { get; set; }

        /// <summary>
        /// Минимальное значение для числовых полей
        /// </summary>
        public double? MinimumValue { get; set; }

        /// <summary>
        /// Максимальное значение для числовых полей
        /// </summary>
        public double? MaximumValue { get; set; }

        /// <summary>
        ///     Комментарий к полю
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///     Отображаемое имя поля
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        ///     Требуются ли уникальные поля
        /// </summary>
        public bool? EnforceUniqueValues { get; set; }

        /// <summary>
        ///     Тип поля
        /// </summary>
        public SPFieldType? FieldType { get; set; }

        /// <summary>
        ///     Скрыто ли поле
        /// </summary>
        public bool? Hidden { get; set; }

        /// <summary>
        ///     Внутреннее имя поля
        /// </summary>
        public string InternalName { get; set; }

        /// <summary>
        ///     Для полей типа Note, можно ли вводить HTML
        /// </summary>
        public bool? IsHtmlEnabled { get; set; }

        /// <summary>
        /// Формула для вычисляемых полей
        /// </summary>
        public string Formula { get; set; }

        /// <summary>
        /// Возвращаемое значение для поля 
        /// </summary>
        public SPFieldType OutputType { get; set; } = SPFieldType.Text;

        /// <summary>
        ///     Внутреннее имя поля для лукапа
        /// </summary>
        public string LookupField { get; set; }

        /// <summary>
        ///     ИД списка для поля Lookup
        /// </summary>
        public Guid LookupListId { get; set; } = Guid.Empty;

        /// <summary>
        ///     Если поле user или lookup, то возможен ли множественный выбор
        /// </summary>
        public bool? Multiple { get; set; }

        /// <summary>
        ///     Обязательно ли поле
        /// </summary>
        public bool? Required { get; set; }

        /// <summary>
        ///     Если поле типа User, то определяет тип выбора
        /// </summary>
        public SPFieldUserSelectionMode? UserSelectionMode { get; set; }

        /// <summary>
        ///     ИД узла для поля Lookup
        /// </summary>
        public Guid WebId { get; set; } = Guid.Empty;

        /// <summary>
        /// Максимальная длинна
        /// </summary>
        public int MaxLength { get; set; } = 0;

        /// <summary>
        /// Разрешить добавлять поля
        /// </summary>
        public bool? AllowAddValues { get; set; }

        #region Отображение на формах

        /// <summary>
        ///     Отображение в форме редактирования
        /// </summary>
        public bool? ShowInEditForm { get; set; }

        /// <summary>
        ///     Отображение на форме просмотра
        /// </summary>
        public bool? ShowInDisplayForm { get; set; }

        /// <summary>
        ///     Отображение на новой форме
        /// </summary>
        public bool? ShowInNewForm { get; set; }

        /// <summary>
        ///     Отображать на формах просмотра
        /// </summary>
        public bool? ShowInViewForms { get; set; }

        #endregion
    }
}