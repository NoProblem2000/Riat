using System.Runtime.InteropServices.ComTypes;
using System.Text;
using Newtonsoft.Json;

namespace RIATLab1
{
        public class JsonSerializer:ISerializer
        {
            public bool CanSerialize(string serializeFormat)
            {
                return serializeFormat == "Json" ? true : false;
            }

            public byte[] Serialize<T>(T obj)
            {
                return Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(obj));
            }

            public T Deserialize<T>(byte[] serializedObj)
            {
                return JsonConvert.DeserializeObject<T>(Encoding.UTF8.GetString(serializedObj));
            }
        }
   }
