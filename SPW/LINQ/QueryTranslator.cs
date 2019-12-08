using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Microsoft.SharePoint;

namespace SPW.LINQ
{
    internal class QueryTranslator : ExpressionVisitor
    {
        private StringBuilder _query;
        private SPQuery _spQuery;

        internal IEnumerable<SPListItem> Translate(Expression expression)
        {
            _query = new StringBuilder();
            _spQuery = new SPQuery();
            Visit(expression);
            var spQuery = new SPQuery {Query = _query.ToString()};
            return new List<SPListItem>();
        }

        private static Expression StripQuotes(Expression e)
        {
            while (e.NodeType == ExpressionType.Quote)
            {
                e = ((UnaryExpression) e).Operand;
            }

            return e;
        }

        protected override Expression VisitMethodCall(MethodCallExpression m)
        {
            if (m.Method.DeclaringType == typeof(Queryable) && m.Method.Name == "Where")
            {
                _query.Append("<Where>");
                Visit(m.Arguments[0]);
                LambdaExpression lambda = (LambdaExpression) StripQuotes(m.Arguments[1]);
                Visit(lambda.Body);
                _query.Append("</Where>");
                return m;
            }

            return base.VisitMethodCall(m);
        }

        protected override Expression VisitUnary(UnaryExpression u)
        {
            switch (u.NodeType)
            {
                case ExpressionType.Not:
                    _query.Append(" NOT ");
                    Visit(u.Operand);
                    break;
                default:
                    throw new NotSupportedException(
                        string.Format("The unary operator '{0}' is not supported", u.NodeType)
                    );
            }

            return u;
        }

        protected override Expression VisitBinary(BinaryExpression b)
        {
            _query.Append("(");
            Visit(b.Left);
            switch (b.NodeType)
            {
                case ExpressionType.And:
                    _query.Append(" AND ");
                    break;
                case ExpressionType.Or:
                    _query.Append(" OR");
                    break;
                case ExpressionType.Equal:
                    _query.Append(" = ");
                    break;
                case ExpressionType.NotEqual:
                    _query.Append(" <> ");
                    break;
                case ExpressionType.LessThan:
                    _query.Append(" < ");
                    break;
                case ExpressionType.LessThanOrEqual:
                    _query.Append(" <= ");
                    break;
                case ExpressionType.GreaterThan:
                    _query.Append(" > ");
                    break;
                case ExpressionType.GreaterThanOrEqual:
                    _query.Append(" >= ");
                    break;
                default:
                    throw new NotSupportedException(
                        string.Format("The binary operator '{0}' is not supported", b.NodeType)
                    );
            }

            Visit(b.Right);
            _query.Append(")");
            return b;
        }

        protected override Expression VisitConstant(ConstantExpression c)
        {
            IQueryable q = c.Value as IQueryable;
            if (q != null)
            {
            }
            else if (c.Value == null)
            {
                _query.Append("NULL");
            }
            else
            {
                switch (Type.GetTypeCode(c.Value.GetType()))
                {
                    case TypeCode.Boolean:
                        _query.Append((bool) c.Value ? 1 : 0);
                        break;
                    case TypeCode.String:
                        _query.Append("'");
                        _query.Append(c.Value);
                        _query.Append("'");
                        break;
                    case TypeCode.Object:
                        throw new NotSupportedException(
                            string.Format("The constant for '{0}' is not supported", c.Value)
                        );
                    default:
                        _query.Append(c.Value);
                        break;
                }
            }

            return c;
        }

        /// <inheritdoc />
        protected override Expression VisitMember(MemberExpression m)
        {
            if (m.Expression != null && m.Expression.NodeType == ExpressionType.Parameter)
            {
                _query.Append(m.Member.Name);
                return m;
            }

            throw new NotSupportedException(string.Format("The member '{0}' is not supported", m.Member.Name));
        }
    }
}