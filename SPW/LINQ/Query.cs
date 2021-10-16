using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SPW.LINQ
{
	public class Query<T>
				: IQueryable<T>, IQueryable, IEnumerable<T>, IEnumerable, IOrderedQueryable<T>, IOrderedQueryable
	{
		QueryProvider provider;
		Expression expression;

		public Query(QueryProvider provider)
		{
			if (provider == null)
			{
				throw new ArgumentNullException(nameof(provider));
			}

			this.provider = provider;
			this.expression = Expression.Constant(this);
		}

		public Query(QueryProvider provider, Expression expression)
		{
			if (provider == null)
			{
				throw new ArgumentNullException(nameof(provider));
			}

			if (expression == null)
			{
				throw new ArgumentNullException(nameof(expression));
			}

			if (!typeof(IQueryable<T>).IsAssignableFrom(expression.Type))
			{
				throw new ArgumentOutOfRangeException(nameof(expression));
			}


			this.provider = provider;
			this.expression = expression;
		}


		Expression IQueryable.Expression => this.expression;

		Type IQueryable.ElementType => typeof(T);

		IQueryProvider IQueryable.Provider => this.provider;

		public IEnumerator<T> GetEnumerator()
		{
			return ((IEnumerable<T>)this.provider.Execute(this.expression)).GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable)this.provider.Execute(this.expression)).GetEnumerator();
		}

	}
}