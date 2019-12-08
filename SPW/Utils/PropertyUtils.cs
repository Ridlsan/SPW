using System.Reflection;

namespace SPW.Utils
{
    public class PropertyUtils
    {
        public static PropertyInfo[] GetProperties<T>() where T : SwListItem
        {
            return typeof(T)
                .GetProperties(
                    BindingFlags.Public |
                    BindingFlags.Instance |
                    BindingFlags.DeclaredOnly
                );
        }
    }
}