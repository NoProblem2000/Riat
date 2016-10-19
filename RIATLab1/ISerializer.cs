using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RIATLab1
{
    interface ISerializer
    {
        bool CanSerialize(string serializeFormat);
        byte[] Serialize<T>(T obj);
        T Deserialize<T>(byte[] serializedObj);
    }
}
