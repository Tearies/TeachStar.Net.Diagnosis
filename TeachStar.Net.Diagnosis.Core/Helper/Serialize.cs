using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace TeachStar.Net.Diagnosis.Core.Helper
{
    /// <summary>
    /// 序列化的辅助操作类
    /// </summary>
    public static class Serialize
    {
        #region 将对象序列化到XML文件中
        /// <summary>
        /// 将对象序列化到XML文件中
        /// </summary>
        /// <typeparam name="T">要序列化的类，即instance的类名</typeparam>
        /// <param name="instance">要序列化的对象</param>
        /// <param name="xmlFile">Xml文件名，表示保存序列化数据的位置</param>
        public static void SerializeToXML<T>(T instance, string xmlFile)
        {
            //创建XML序列化对象
            var serializer = new XmlSerializer(typeof(T));

            //创建文件流
            using (FileStream fs = new FileStream(xmlFile, FileMode.Create))
            {
                //开始序列化对象
                serializer.Serialize(fs, instance);
            }
        }
        #endregion

        #region 将XML文件反序列化为对象
        /// <summary>
        /// 将XML文件反序列化为对象
        /// </summary>
        /// <typeparam name="T">要获取的类</typeparam>
        /// <param name="xmlFile">Xml文件名，即保存序列化数据的位置</param>        
        public static T DeSerializeFromXML<T>(string xmlFile) where T : class
        {
            //创建XML序列化对象
            var serializer = new XmlSerializer(typeof(T));

            //创建文件流
            using (FileStream fs = new FileStream(xmlFile, FileMode.Open, FileAccess.Read))
            {
                //开始反序列化对象
                return serializer.Deserialize(fs) as T;
            }
        }
        #endregion

        #region 将对象序列化到二进制文件中
        /// <summary>
        /// 将对象序列化到二进制文件中
        /// </summary>
        /// <param name="instance">要序列化的对象</param>
        /// <param name="fileName">文件名，保存二进制序列化数据的位置,后缀一般为.bin</param>
        public static void SerializeToBinary(object instance, string fileName)
        {
            //创建二进制序列化对象
            BinaryFormatter serializer = new BinaryFormatter();
            //创建文件流
            using (FileStream fs = new FileStream(fileName, FileMode.Create))
            {
                //开始序列化对象
                serializer.Serialize(fs, instance);
            }
        }
        #endregion

        #region 对象序列化二进制流及反序列化
        /// <summary>  
        /// 返回对象序列化为byte数组  
        /// </summary>  
        /// <typeparam name="T">对象类型</typeparam>  
        /// <param name="t">对象</param>  
        /// <param name="compress">是否压缩(默认压缩)</param>  
        /// <returns></returns>  
        public static byte[] SerializeToBytes<T>(T t)
        {
            var memory = new MemoryStream();
            var formatter = new BinaryFormatter();
            formatter.Serialize(memory, t);
            memory.Position = 0;
            var read = new byte[memory.Length];
            memory.Read(read, 0, read.Length);
            memory.Close();
            return read;
        }

        /// <summary>  
        /// 返回对象的反序列化  
        /// </summary>  
        /// <typeparam name="T">对象类型</typeparam>  
        /// <param name="pBytes">byte数组</param>  
        /// <param name="decompress">是否先进行解压操作</param>  
        /// <returns></returns>  
        public static T DeSerializeFromBytes<T>(byte[] pBytes) where T : class
        {
            if (pBytes == null)
                return default(T);
            var memory = new MemoryStream(pBytes) { Position = 0 };
            var formatter = new BinaryFormatter();
            object newOjb = formatter.Deserialize(memory);
            memory.Close();
            return (T)newOjb;
        }

        #endregion

        #region 将二进制文件反序列化为对象
        /// <summary>
        /// 将二进制文件反序列化为对象
        /// </summary> <typeparam name="T">要获取的类</typeparam>
        /// <param name="fileName">文件名，保存二进制序列化数据的位置</param>        
        public static T DeSerializeFromBinary<T>(string fileName) where T : class
        {
            //创建二进制序列化对象
            BinaryFormatter serializer = new BinaryFormatter();

            //创建文件流
            using (FileStream fs = new FileStream(fileName, FileMode.Open))
            {
                //开始反序列化对象-
                return serializer.Deserialize(fs) as T;
            }
        }
        #endregion

        #region 将对象序列化成字符串
        /// <summary>
        /// 将对象序列化
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static string SerializeToXML<T>(T instance)
        {
            using (var sw = new StringWriter())
            {
                var xs = new XmlSerializer(instance.GetType());
                xs.Serialize(sw, instance);
                return sw.ToString();
            }
        }
        #endregion

        #region 将字符串范序列化成对象
        /// <summary>
        /// 将字符串范序列化成对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="s"></param>
        /// <returns></returns>
        public static T DeSerializeToObject<T>(string s) where T : class
        {
            using (var sr = new StringReader(s))
            {
                var xz = new XmlSerializer(typeof(T));
                return xz.Deserialize(sr) as T;
            }
        }
        #endregion
    }
}
