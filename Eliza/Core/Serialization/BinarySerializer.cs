using Eliza.Model;
using MessagePack;
using System;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using static Eliza.Core.Serialization.Attributes;

namespace Eliza.Core.Serialization
{
    class BinarySerializer : BinarySerialization
    {
        public BinaryWriter Writer;

        public BinarySerializer(Stream baseStream) : base(baseStream)
        {
            Writer = new BinaryWriter(baseStream);
        }

        public void Serialize<T>(T obj)
        {
            WriteValue(obj);
        }

        private void WriteValue(object value)
        {
            var type = value.GetType();

            if (type.IsPrimitive)
            {
                WritePrimitive(value);
            }
            else if (IsList(type))
            {
                WriteList((IList)value);
            }
            else if (type == typeof(string))
            {
                WriteString((string)value);
            }
            else if (type == typeof(SaveFlagStorage))
            {
                WriteSaveFlagStorage((SaveFlagStorage)value);
            }
            else
            {
                WriteObject(value);
            }
        }
        
        private void WritePrimitive(object value)
        {
            var type = value.GetType();

            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Boolean: Writer.Write((bool)value); break;
                case TypeCode.Byte: Writer.Write((byte)value); break;
                case TypeCode.Char: Writer.Write((char)value); break;
                case TypeCode.UInt16: Writer.Write((ushort)value); break;
                case TypeCode.UInt32: Writer.Write((uint)value); break;
                case TypeCode.UInt64: Writer.Write((ulong)value); break;
                case TypeCode.SByte: Writer.Write((sbyte)value); break;
                case TypeCode.Int16: Writer.Write((short)value); break;
                case TypeCode.Int32: Writer.Write((int)value); break;
                case TypeCode.Int64: Writer.Write((long)value); break;
                case TypeCode.Single: Writer.Write((float)value); break;
                case TypeCode.Double: Writer.Write((double)value); break;
            }
        }
        
        private void WriteList(IList list, TypeCode lengthType = TypeCode.Int32, int length = 0)
        {
            if (length != 0)
            {
                foreach (object value in list)
                {
                    WriteValue(value);
                }
            }
            else
            {
                //Type type = list.GetType();

                //type = type.IsArray
                //    ? type.GetElementType()
                //    : type.GetGenericArguments()[0];

                switch (lengthType)
                {
                    case TypeCode.Byte: Writer.Write((byte)list.Count); break;
                    case TypeCode.Char: Writer.Write((char)list.Count); break;
                    case TypeCode.UInt16: Writer.Write((ushort)list.Count); break;
                    case TypeCode.UInt32: Writer.Write((uint)list.Count); break;
                    case TypeCode.UInt64: Writer.Write((ulong)list.Count); break;
                    case TypeCode.SByte: Writer.Write((sbyte)list.Count); break;
                    case TypeCode.Int16: Writer.Write((short)list.Count); break;
                    case TypeCode.Int32: Writer.Write((int)list.Count); break;
                    case TypeCode.Int64: Writer.Write((long)list.Count); break;
                }

                foreach (object value in list)
                {
                    WriteValue(value);
                }
            }
        }
        private void WriteString(string value, int max = 0)
        {
            var data = Encoding.Unicode.GetBytes(value);
            if (max != 0)
            {
                Writer.Write(data.Length);
                for (int index = 0; index < max; index++)
                {
                    if (index < data.Length)
                    {
                        Writer.Write(data);
                    }
                    else
                    {
                        Writer.Write((byte)0x0);
                    }
                }
            }
            else
            {
                for (int index = 0; index < data.Length; index++)
                {
                    Writer.Write(data[index]);
                }
            }
        }
        private void WriteSaveFlagStorage(SaveFlagStorage saveFlagStorage)
        {
            Writer.Write(saveFlagStorage.Length);
            Writer.Write(saveFlagStorage.Data);
        }

        private void WriteObject(object objectValue)
        {
            var objectType = objectValue.GetType();

            // MessagePackObject
            if (objectType.IsDefined(typeof(MessagePackObjectAttribute)))
            {
                WriteMessagePackObject(objectValue);
                return;
            }

            int fieldCount = 0;

            foreach (FieldInfo info in GetFieldsSorted(objectType))
            {
                fieldCount++;

                if (!info.IsDefined(typeof(CompilerGeneratedAttribute)))
                {
                    object fieldValue = info.GetValue(objectValue);

                    Type fieldType = info.FieldType;

                    var lengthAttribute = (LengthAttribute)info.GetCustomAttribute(typeof(LengthAttribute));
                    if (lengthAttribute != null)
                    {
                        if (lengthAttribute.Size != 0)
                        {
                            if (IsList(fieldType))
                            {
                                WriteList((IList)fieldValue, length: lengthAttribute.Size);
                            }
                            else if (fieldType == typeof(string))
                            {
                                WriteString((string)fieldValue, lengthAttribute.Size);
                            }
                        }
                        else if (lengthAttribute.Type != TypeCode.Empty)
                        {
                            if (IsList(fieldType))
                            {
                                WriteList((IList)fieldValue, lengthType: lengthAttribute.Type);
                            }
                            else if (fieldType == typeof(string))
                            {
                                //Not supported for strings ATM
                            }
                        }
                        else if (lengthAttribute.Type != TypeCode.Empty)
                        {
                            if (IsList(fieldType))
                            {
                                //Not supported 
                                
                            }
                            else if (fieldType == typeof(string))
                            {
                                WriteString((string)fieldValue, max: lengthAttribute.Max);
                            }
                        }
                    }
                    else
                    {
                        WriteValue(fieldValue);
                    }
                }
            }

        }

        private void WriteMessagePackObject(object value)
        {
            //Not sure if this will put the count
            MessagePackSerializer.Serialize(value);
        }
    }
}
