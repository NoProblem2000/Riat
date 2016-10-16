using System;
using System.Collections.Concurrent;
using System.IO;
using System.Text;
using System.Xml;
using Newtonsoft.Json;
using System.Xml.Serialization;

namespace RIATLab1
{
    class Program
    {
        private static readonly XmlWriterSettings XmlSettings;
        private static readonly XmlSerializerNamespaces XmlNamespaces;
        private static readonly ConcurrentDictionary<Type, XmlSerializer> XmlDictionary;

        public static byte[] SerializeJson<T>(T obj)
        {
            return Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(obj));
        }

        //todo: нуджно сделать такой инотерфейс и всю логику вынести в два наслдженика - JsonSerializer, XmlSerilaizer
        //interface ISerializer
        //{
        //    bool CanSerialize(string serializeFormat);
        //    string Serialize<T>(T obj);
        //    T Deserialize<T>(string serializedObj);
        //}

        public static T DeserializeJson<T>(byte[] bytes)
        {
            return JsonConvert.DeserializeObject<T>(Encoding.UTF8.GetString(bytes));
        }


        static Program() {
            XmlSettings = new XmlWriterSettings {
                Indent = false,
                IndentChars = "\t",
                OmitXmlDeclaration = true
            };

            XmlNamespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            XmlDictionary = new ConcurrentDictionary<Type, XmlSerializer>();
        }

        public static byte[] SerializeXml<T>(T obj) {
            var xmlSerializer = XmlDictionary.GetOrAdd(typeof(T), type => new XmlSerializer(type));

            var stringBuilder = new StringBuilder();
            using (var xmlWriter = XmlWriter.Create(stringBuilder, XmlSettings)) {
                xmlSerializer.Serialize(xmlWriter, obj, XmlNamespaces);
                return Encoding.UTF8.GetBytes(stringBuilder.ToString());
            }
        }

        public static T DeserializeXml<T>(byte[] bytes) {
            var xmlSerializer = XmlDictionary.GetOrAdd(typeof(T), type => new XmlSerializer(type));
            return (T)xmlSerializer.Deserialize(new MemoryStream(bytes));
}

        static void Main()
        {
            var typeOfSerialization = Console.ReadLine();
            var byteInput = Encoding.UTF8.GetBytes(Console.ReadLine());

            if (typeOfSerialization == "Json")
            {
                var input = DeserializeJson<Input>(byteInput);
                var output = input.DoOutPut();
                Console.WriteLine(Encoding.UTF8.GetString(SerializeJson(output)));
            }
            if (typeOfSerialization == "Xml") 
            {
                var input = DeserializeXml<Input>(byteInput);
                var output = input.DoOutPut();
                Console.WriteLine(Encoding.UTF8.GetString(SerializeXml(output)));
            }
        }

    }
}
