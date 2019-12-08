using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SPW.Deployment.Utils
{
    /// <summary>
    ///     Memos object properties (Not all!) and checks if they changed
    /// </summary>
    public class Mementer
    {
        private readonly object _instance;
        private readonly Dictionary<string, string> _memo = new Dictionary<string, string>();
        private readonly IEnumerable<PropertyInfo> _properties;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Mementer{T}" /> class.
        /// </summary>
        /// <param name="instance">The instance.</param>
        public Mementer(object instance)
        {
            _instance = instance;
            _properties = instance.GetType()
                .GetProperties()
                .Where(
                    p => p.CanRead &&
                         p.CanWrite
                );
            foreach (var property in _properties)
            {
                var value = property.GetValue(instance);
                _memo.Add(property.Name, value?.ToString());
            }
        }

        public bool IsChanged()
        {
            foreach (var property in _properties)
            {
                var oldValue = _memo[property.Name];
                var newValue = property.GetValue(_instance)?.ToString();
                if (newValue != oldValue)
                {
                    return true;
                }
            }

            return false;
        }
    }
}