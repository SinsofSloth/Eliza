using System.IO;
using System.Runtime.InteropServices;
using BinarySerialization;

namespace Eliza.Core.Serialization
{
    class Deserializer
    {
        private BinaryReader reader;

        Deserializer(BinaryReader reader)
        {
            this.reader = reader;
        }
        T Deserialize<T>()
        {
            var buffer = new byte[Marshal.SizeOf(typeof(T))];
            reader.Read(
                buffer, 0, buffer.Length
            );
            var deserializer = new BinarySerializer();
            return deserializer.Deserialize<T>(buffer);
        }
    }
}
