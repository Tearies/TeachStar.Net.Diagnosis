using System;

namespace TeachStar.Net.Diagnosis.Core.Helper
{
    public static class TypeExtensionCoverter
    {
        #region ToDecimal
        /// <summary>
        /// 转换为Decimal
        /// </summary>
        public static Decimal ToDecimal(this string value)
        {
            Decimal result;
            if (Decimal.TryParse(value, out result))
            {
                return result;
            }
            throw new InvalidCastException("不能将字符串\"" + value + "\"转换为Decimal数字。");
        }
        #endregion

        #region ToSafeDecimal
        /// <summary>
        /// 转换为安全的Decimal，默认为0
        /// </summary>
        public static Decimal ToSafeDecimal(this string value)
        {
            Decimal result;
            if (Decimal.TryParse(value, out result))
            {
                return result;
            }
            else
            {
                return 0;
            }
        }
        #endregion

        #region ToDouble
        /// <summary>
        /// 转换为double
        /// </summary>
        public static double ToDouble(this string value)
        {
            if (value.IsInvalid()) return 0.0;
            double result;
            if (double.TryParse(value, out result))
            {
                return result;
            }
            throw new InvalidCastException("不能将字符串\"" + value + "\"转换为double数字。");
        }
        #endregion

        #region ToSafeDouble
        /// <summary>
        /// 转换为安全的Double，默认为0
        /// </summary>
        public static double ToSafeDouble(this string value)
        {
            double result;
            if (double.TryParse(value, out result))
            {
                return result;
            }
            else
            {
                return 0;
            }
        }
        #endregion

        #region ToSafeInt
        /// <summary>
        /// 转换为安全的Int，默认为0
        /// </summary>
        public static int ToSafeInt(this string value)
        {
            int result;
            if (int.TryParse(value, out result))
            {
                return result;
            }
            else
            {
                return 0;
            }
        }
        #endregion
    }
}
