using NoisyCowStudios.Bin2Object;
using System.IO;

namespace Eliza.Core
{
    class Save
    {
        static void Read(string path)
        {
            using (var fs = new FileStream(path, FileMode.Open, FileAccess.ReadWrite))
            using (var reader = new BinaryObjectReader(fs, Endianness.Little))
            {
                var save = reader.ReadObject<Model.Save>();
            }
        }
        static void Write()
        {
        }
        static uint Checksum(byte[] buffer)
        {
            uint sum = 0xcbf29ce4;
            uint lo = 0;
            uint running_sum = 0x39;
            for (int index = 0; index < buffer.Length; index++)
            {
                uint value = buffer[index];
                uint delta = value - lo;
                lo = (lo & 0xff) + 0xb2;
                sum = sum * 0x1b3 ^ (delta ^ running_sum) & 0xff;
                running_sum = value;
            }
            return sum;
        }
    }
}
