using System;
using System.Runtime.Serialization;

namespace P.Common.Exceptions
{
    /// <summary>
    ///  枚举扩展方法类
    /// </summary>
    public static class EnumExtension
    {
        /// <summary>
        /// 获取枚举的说明
        /// </summary>
        /// <typeparam name="T">枚举</typeparam>
        /// <param name="n">枚举int?值</param>
        /// <returns></returns>
        public static string GetDescription<T>(this int? n) where T : struct
        {
            if (n.HasValue)
            {
                return n.Value.GetDescription<T>();
            }
            return string.Empty;
        }

        /// <summary>
        /// 获取枚举的说明
        /// </summary>
        /// <typeparam name="T">枚举</typeparam>
        /// <param name="n">枚举int值</param>
        /// <returns></returns>
        public static string GetDescription<T>(this int n) where T : struct
        {
            try
            {
                Type enumType = typeof(T);
                if (enumType.IsEnum)
                {
                    string name = Enum.GetName(enumType, n);
                    EnumMemberAttribute customAttribute = (EnumMemberAttribute)Attribute.GetCustomAttribute(enumType.GetField(name), typeof(EnumMemberAttribute));
                    return ((customAttribute == null) ? name : customAttribute.Value);
                }
            }
            catch { }
            return string.Empty;
        }

        /// <summary>
        /// 获取枚举的说明
        /// </summary>
        /// <typeparam name="T">枚举</typeparam>
        /// <param name="n">枚举long?值</param>
        /// <returns></returns>
        public static string GetDescription<T>(this long? n) where T : struct
        {
            if (n.HasValue)
            {
                return n.Value.GetDescription<T>();
            }
            return string.Empty;
        }

        /// <summary>
        /// 获取枚举的说明
        /// </summary>
        /// <typeparam name="T">枚举</typeparam>
        /// <param name="n">枚举long值</param>
        /// <returns></returns>
        public static string GetDescription<T>(this long n) where T : struct
        {
            try
            {
                Type enumType = typeof(T);
                if (enumType.IsEnum)
                {
                    string name = Enum.GetName(enumType, n);
                    EnumMemberAttribute customAttribute = (EnumMemberAttribute)Attribute.GetCustomAttribute(enumType.GetField(name), typeof(EnumMemberAttribute));
                    return ((customAttribute == null) ? name : customAttribute.Value);
                }
            }
            catch { }
            return string.Empty;
        }

        /// <summary>
        /// 获取枚举的说明
        /// </summary>
        /// <param name="o">枚举值</param>
        /// <returns></returns>
        public static string GetDescription(this Enum o)
        {
            Type enumType = o.GetType();
            string name = Enum.GetName(enumType, o);
            EnumMemberAttribute customAttribute = (EnumMemberAttribute)Attribute.GetCustomAttribute(enumType.GetField(name), typeof(EnumMemberAttribute));
            return ((customAttribute == null) ? name : customAttribute.Value);
        }

        public static int ToInt(this Enum o)
        {
            return Convert.ToInt32(o);
        }

        public static long ToLong(this Enum o)
        {
            return Convert.ToInt64(o);
        }
    }
}
