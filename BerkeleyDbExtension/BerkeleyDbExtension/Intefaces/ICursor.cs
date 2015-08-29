using System;
using System.Collections.Generic;

namespace BerkeleyDbExtension.Intefaces
{
    /// <summary>
    /// Provides an interface to iteratively access berkeley database.
    /// </summary>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    interface ICursor<TKey, TValue> :IDisposable
    {
        KeyValuePair<TKey, TValue> Current { get; }
        void Reset();
        bool Next();
        bool Previous();
        bool MoveToKey();
    }
}
