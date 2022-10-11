using AD_Manager.Layers.Middleware;
using AD_Manager.Layers.Model;
using System.Data;

namespace AD_Manager.Layers
{
    public static class MyExtentions
    {
        public static bool IsnotEmpty(this string str)
        {
            if (str == null || str.Length == 0) return false;
            return true;
        }
        public static long ToLong(this string str)
        {
            if (str == null) return 0;
            _ = long.TryParse(str, out long l);
            return l;
        }
        public static long ToLong(this object str)
        {
            if (str == null) return 0;
            return str.ToString().ToLong();
        }
        public static int ToInt(this string str)
        {
            if (str == null) return 0;
            _ = int.TryParse(str, out int l);
            return l;
        }
        public static int ToInt(this object str)
        {
            if (str == null) return 0;
            return str.ToString().ToInt();
        }
        public static DateTime? ToDateTime(this string str)
        {
            if (str == null) return null;
            _ = DateTime.TryParse(str, out DateTime l);
            return l;
        }
        public static DateTime? ToDateTime(this object str)
        {
            if (str == null) return null;
            return str.ToString().ToDateTime();
        }
        public static bool ToBool(this string str)
        {
            if (str == null) return false;
            _ = bool.TryParse(str, out bool b);
            return b;
        }
        public static bool ToBool(this object str)
        {
            if (str == null) return false;
            return  str.ToString().ToBool();
        }
        public static void SetPropertyValue(object instance, string strPropertyName, object newValue)
        {
            try
            {
                Type type = instance.GetType();
                System.Reflection.PropertyInfo propertyInfo = type.GetProperty(strPropertyName);
                propertyInfo.SetValue(instance, newValue, null);
            }
            catch { }
        }
        public static IApplicationBuilder UseCorsMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CorsMiddleware>();
        }
    }


}
