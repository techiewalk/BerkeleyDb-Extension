using BerkeleyDB;
using System.Collections.Generic;

namespace BerkeleyDbExtension.Intefaces
{
    /// <summary>
    /// Provides an interface to translate berkeley db content.
    /// </summary>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    public interface ITranslator<TKey, TValue>
    {
        KeyValuePair<TKey, TValue> Translate(DatabaseEntry key, DatabaseEntry value);
        TKey Key(DatabaseEntry key);
        TValue Value(DatabaseEntry value);
        DatabaseEntry BuildKey(TKey key);
        DatabaseEntry BuildValue(TValue value);
    }
}
