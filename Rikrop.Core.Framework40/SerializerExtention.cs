using System;
using System.Collections.Concurrent;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;

namespace Rikrop.Core.Framework
{
    public static class SerializerExtention
    {
        private static readonly ConcurrentDictionary<Type, DataContractSerializer> CachedSerializers = new ConcurrentDictionary<Type, DataContractSerializer>();

        public static string Serialize<T>(this T obj)
        {
            using (var stream = new MemoryStream())
            {
                var serializer = GetSerializer(obj.GetType());
                serializer.WriteObject(stream, obj);
                stream.Seek(0, SeekOrigin.Begin);
                using(var reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        public static T Deserialize<T>(this string xmlString)
        {
            return (T) Deserialize(xmlString, typeof (T));
        }

        public static object Deserialize(this string xmlString, Type toType)
        {
            using (var stringReader = new StringReader(xmlString))
            {
                using (var textReader = new XmlTextReader(stringReader))
                {
                    var serializer = GetSerializer(toType);
                    return serializer.ReadObject(textReader, true);
                }
            }
        }

        private static DataContractSerializer GetSerializer(Type type)
        {
            return CachedSerializers.GetOrAdd(type, o => new DataContractSerializer(o));
        }
    }
}