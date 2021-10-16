using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace SPW.LINQ
{
	public class SwQueryProvider : QueryProvider
	{
		public SwQueryProvider()
		{
		}

		public override object Execute(Expression expression)
		{
			var items = Translate(expression);
			Type elementType = TypeSystem.GetElementType(expression.Type);
			return Activator.CreateInstance(
					typeof(SwObjectCreator<>).MakeGenericType(elementType),
					BindingFlags.Instance | BindingFlags.NonPublic,
					null,
					new object[] { items },
					null
			);
		}


		private IEnumerable<SPListItem> Translate(Expression expression)
		{
			expression = Evaluator.PartialEval(expression);
			var request = new QueryTranslator().Translate(expression);
			return request;
		}
	}
}