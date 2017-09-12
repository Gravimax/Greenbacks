using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Greenbacks.Helpers
{
    internal class XMLFileIO
    {
        /// <summary>
        /// Reads an xml file and converts it to its correct object type.
        /// </summary>
        /// <typeparam name="T">The object type</typeparam>
        /// <param name="path">The path including the file name.</param>
        /// <returns>Object of type T</returns>
        public static T Load<T>(string path)
        {
            try
            {
                FileInfo fi = new FileInfo(path);
                if (fi.Exists)
                {
                    XmlSerializer xSerializer = new XmlSerializer(typeof(T));
                    using (FileStream fs = new FileStream(path, FileMode.Open))
                    {
                        XmlReader xReader = new XmlTextReader(fs);
                        return (T)xSerializer.Deserialize(xReader);
                    }
                }
                else
                {
                    throw new FileNotFoundException("Not found: " + path);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Takes a serializable object and saves it to an xml file.
        /// </summary>
        /// <typeparam name="T">The object type</typeparam>
        /// <param name="path">The path including the file name.</param>
        /// <param name="obj">The object to serialize.</param>
        public static void Save<T>(string path, T obj)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                using (TextWriter textWriter = new StreamWriter(path))
                {
                    serializer.Serialize(textWriter, obj);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
