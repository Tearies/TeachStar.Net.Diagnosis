using System;

namespace TeachStar.Net.Diagnosis.Core.Helper
{
    /// <summary>
    /// (n. 守卫)提供全局通用验证
    /// </summary>
    public static class Guard
    {
        #region ArgumentNotNull：验证参数是否为NULL
        /// <summary>
        /// 验证参数是否为NULL
        /// </summary>
        public static void ArgumentNotNull(object value, string argumentName)
        {
            if (value == null)
            {
                throw new ArgumentNullException(argumentName,
                    string.Format("参数[{0}]为null", argumentName));
            }
        }
        #endregion

        #region ArgumentNotNullOrEmpty：验证参数是否为NULL或空字符串
        /// <summary>
        /// 验证参数是否为NULL或空字符串
        /// </summary>
        public static void ArgumentNotNullOrEmpty(string value, string argumentName)
        {
            if (value.IsInvalid())
            {
                throw new ArgumentNullException(argumentName,
                    string.Format("参数[{0}]为null或空字符", argumentName));
            }
        }
        #endregion
    }
}
