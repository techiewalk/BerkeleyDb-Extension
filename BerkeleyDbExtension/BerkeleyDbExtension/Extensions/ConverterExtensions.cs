using ProtoBuf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BerkeleyDbExtension.Extensions
{
    public static class ConverterExtensions
    {
        /// <summary>
        /// The valuetype converters
        /// </summary>
        private static readonly Dictionary<Type, Func<object, byte[]>> ValuetypeConverters = new Dictionary<Type, Func<object, byte[]>>
        {
            {typeof(string),o => Encoding.ASCII.GetBytes((string) o)},
            {typeof(bool),o => BitConverter.GetBytes((bool) o)},
            {typeof(char),o => BitConverter.GetBytes((char) o)},
            {typeof(int),o => BitConverter.GetBytes((int) o)},
            {typeof(long),o => BitConverter.GetBytes((long) o)},
            {typeof(uint),o => BitConverter.GetBytes((uint) o)},
            {typeof(short),o => BitConverter.GetBytes((short) o)},
            {typeof(double),o => BitConverter.GetBytes((double) o)},
            {typeof(float),o => BitConverter.GetBytes((float) o)},
            {typeof(ulong),o => BitConverter.GetBytes((ulong) o)}            
        };

        /// <summary>
        /// The byte converters
        /// </summary>
        private static readonly Dictionary<Type, Func<byte[], object>> ByteConverters = new Dictionary<Type, Func<byte[], object>>
        {
            {typeof(string),o => Encoding.ASCII.GetString(o)},
            {typeof(bool),o => BitConverter.ToBoolean(o,0)},
            {typeof(char),o => BitConverter.ToChar( o,0)},
            {typeof(int),o => BitConverter.ToInt32( o,0)},
            {typeof(long),o => BitConverter.ToInt64( o,0)},
            {typeof(uint),o => BitConverter.ToUInt32(o,0)},
            {typeof(short),o => BitConverter.ToInt16( o,0)},
            {typeof(double),o => BitConverter.ToDouble( o,0)},
            {typeof(float),o => BitConverter.ToSingle( o,0)},
            {typeof(ulong),o => BitConverter.ToUInt64( o,0)}
        };

        /// <summary>
        /// Converts to byte.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static byte[] ConvertToByte<T>(this T value)
        {
            if (typeof(T).IsValueType || typeof(T) == typeof(string))
            {
                Func<object, byte[]> converter;

                if (ValuetypeConverters.TryGetValue(typeof(T), out converter))
                {
                    return converter(value);
                }
            }
            using (var ms = new MemoryStream())
            {
                Serializer.Serialize(ms, value);
                return ms.ToArray();
            }
        }

        /// <summary>
        /// Converts to type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static object ConvertToType<T>(this byte[] value)
        {
            if (typeof(T).IsValueType || typeof(T) == typeof(string))
            {
                Func<byte[], object> converter;

                if (ByteConverters.TryGetValue(typeof(T), out converter))
                {
                    return (T)converter(value);
                }
            }

            if (typeof(T) == value.GetType()) return value;
            return Serializer.Deserialize<T>(new MemoryStream(value));
        }
    }
}
