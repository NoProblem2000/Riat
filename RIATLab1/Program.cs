using System;
using System.Collections.Concurrent;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml;
using Newtonsoft.Json;
using System.Xml.Serialization;

namespace RIATLab1
{
    partial class Program
    {
        
        //todo: нуджно сделать такой инотерфейс и всю логику вынести в два наслдженика - JsonSerializer, XmlSerilaizer
        //interface ISerializer
        //{
        //    bool CanSerialize(string serializeFormat);
        //    string Serialize<T>(T obj);
        //    T Deserialize<T>(string serializedObj);
        //}
        static void Main()
        {
            var typeOfSerialization = Console.ReadLine();
            var byteInput = Encoding.UTF8.GetBytes(Console.ReadLine());

            ISerializer serializerJ = new JsonSerializer();
            ISerializer serializerX = new XmlSerialiizer();

            if (serializerJ.CanSerialize(typeOfSerialization))
            {
                var input = serializerJ.Deserialize<Input>(byteInput);
                var output = input.DoOutPut();
                Console.WriteLine(Encoding.UTF8.GetString(serializerJ.Serialize(output)));
            }
            if (serializerX.CanSerialize(typeOfSerialization)) 
            {
                var input = serializerX.Deserialize<Input>(byteInput);
                var output = input.DoOutPut();
                Console.WriteLine(Encoding.UTF8.GetString(serializerX.Serialize(output)));
            }

           
        }

    }
}
