using System;
using System.Collections.Concurrent;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace RIATLab1
{
    
        public class XmlSerialiizer:ISerializer
        {
            static XmlSerialiizer()
            {
                XmlSettings = new XmlWriterSettings {
                Indent = false,
                IndentChars = "\t",
                OmitXmlDeclaration = true
            };

            XmlNamespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            XmlDictionary = new ConcurrentDictionary<Type, XmlSerializer>();
            }
             private static readonly XmlWriterSettings XmlSettings;
             private static readonly XmlSerializerNamespaces XmlNamespaces;
             private static readonly ConcurrentDictionary<Type, XmlSerializer> XmlDictionary;

            public bool CanSerialize(string serializeFormat)
            {
                return serializeFormat == "Xml" ? true : false;
            }

            public byte[] Serialize<T>(T obj)
            {
                var xmlSerializer = XmlDictionary.GetOrAdd(typeof(T), type => new XmlSerializer(type));

                var stringBuilder = new StringBuilder();
                using (var xmlWriter = XmlWriter.Create(stringBuilder, XmlSettings))
                {
                    xmlSerializer.Serialize(xmlWriter, obj, XmlNamespaces);
                    return Encoding.UTF8.GetBytes(stringBuilder.ToString());
                }
            }

            public T Deserialize<T>(byte[] serializedObj)
            {
                var xmlSerializer = XmlDictionary.GetOrAdd(typeof(T), type => new XmlSerializer(type));
                return (T)xmlSerializer.Deserialize(new MemoryStream(serializedObj));
            }
        }
    
}