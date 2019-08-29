using System.Collections.Generic;
using System.Linq;

namespace TeachStar.Net.Diagnosis.Core.Helper
{
    /// <summary>
    /// 集合类型的扩展：IEnumerable；IList
    /// </summary>
    public static partial class TypeExtension
    {
        #region IEnumerable<T>：IsValid：验证集合是否有效
        /// <summary>
        /// 验证集合是否有效(是否为空，是否包含元素，有效返回true)
        /// 注意：判断是否包含元素，尽量使用Enumerable.Any，而不要用Linq中的Count
        /// </summary>
        public static bool IsValid<T>(this IEnumerable<T> source)
        {
            return source != null && source.Any();
        }
        #endregion

        #region IEnumerable<T>：IsInvalid：验证集合是否无效
        /// <summary>
        /// 验证集合是否无效(是否为空，是否包含元素，若无效返回true)
        /// </summary>
        public static bool IsInvalid<T>(this IEnumerable<T> source)
        {
            return !source.IsValid();
        }
        #endregion

    }
}
