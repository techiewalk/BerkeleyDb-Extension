using BerkeleyDB;
using System;
using System.Collections.Generic;

namespace BerkeleyDbExtension.Intefaces
{
    /// <summary>
    /// Represents an open connection to a data source, and is implemented by BBerkeley databases.
    /// </summary>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    public interface IConnection<TKey, TValue> : IDisposable, IEnumerable<KeyValuePair<TKey, TValue>>
    {
        TValue this[TKey key] { get; set; }
        Database Database { get; }
        void Open();
        void Close();
        bool IsOpen { get; }
    }
}
