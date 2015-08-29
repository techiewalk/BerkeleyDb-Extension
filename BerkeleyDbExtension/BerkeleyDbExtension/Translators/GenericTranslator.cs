using BerkeleyDbExtension.Intefaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BerkeleyDbExtension.Extensions;

namespace BerkeleyDbExtension.Translators
{
    /// <summary>
    /// Helps translating information to bdb content and vice versa.
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public class GenericTranslator<TKey,TValue> : ITranslator<TKey,TValue>
    {
        public KeyValuePair<TKey, TValue> Translate(BerkeleyDB.DatabaseEntry key, BerkeleyDB.DatabaseEntry value)
        {
            return new KeyValuePair<TKey, TValue>(Key(key), Value(value));
        }

        public TKey Key(BerkeleyDB.DatabaseEntry key)
        {
            return (TKey)key.Data.ConvertToType<TKey>();
        }

        public TValue Value(BerkeleyDB.DatabaseEntry value)
        {
            return (TValue)value.Data.ConvertToType<TValue>();
        }

        public BerkeleyDB.DatabaseEntry BuildKey(TKey key)
        {
            return new BerkeleyDB.DatabaseEntry { Data = key.ConvertToByte() };
        }

        public BerkeleyDB.DatabaseEntry BuildValue(TValue value)
        {
            return new BerkeleyDB.DatabaseEntry { Data = value.ConvertToByte() };
        }
    }
}
