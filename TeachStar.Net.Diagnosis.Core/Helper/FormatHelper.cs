using System;

namespace TeachStar.Net.Diagnosis.Core.Helper
{
    public static class FormatHelper
    {
        #region FormatSecond
        /// <summary>
        /// 把秒格式成{0}时{0}分{1}秒
        /// </summary>
        public static string FormatSecond(int second)
        {
            if (second < 60)
            {
                return string.Format("{0}秒", second);
            }
            if (second < 3600)
            {
                return string.Format("{0}分{1}秒", second / 60, second % 60);
            }
            else
            {
                var h = second / 3600;
                var m = (second % 3600) / 60;
                var s = (second % 3600) % 60;
                return string.Format("{0}时{1}分{2}秒", h, m, s);
            }
        }
        #endregion

        /// <summary>
        /// 格式化文件大小（参数：文件大小，字节数）
        /// </summary>
        public static string FormatFileSize(long len, string format = "F2")
        {
            if (len <= 0)
            {
                return "0 KB";
            }

            string unit = " B";
            double res = len, rule = 1024D;
            //KB
            if (len >= rule)
            {
                res = len / rule;
                unit = " KB";
            }
            //M
            if (res > rule)
            {
                res = res / rule;
                unit = " MB";
            }
            //G
            if (res > rule)
            {
                res = res / rule;
                unit = " GB";
            }
            //去掉多余的0
            if (res - Math.Truncate(res) == 0)
            {
                return string.Concat(res.ToString("F2"), unit);
            }
            return string.Concat(res.ToString("F2"), unit);
        }
    }
}
