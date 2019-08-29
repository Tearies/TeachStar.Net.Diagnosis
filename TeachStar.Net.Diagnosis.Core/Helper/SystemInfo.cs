using System;

namespace TeachStar.Net.Diagnosis.Core.Helper
{
    public static class SystemInfo
    {
        /// <summary>  
        /// 将字节转换为GB
        /// </summary>  
        /// <param name="size">字节值</param>  
        /// <param name="mod">除数，硬盘除以1000，内存除以1024</param>  
        /// <returns></returns>  
        public static double ToGB(double size, double mod)
        {
            size /= mod;
            return Math.Round(size);
        }
    }
}
