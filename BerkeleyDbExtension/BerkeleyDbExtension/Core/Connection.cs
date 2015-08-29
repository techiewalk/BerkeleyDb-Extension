using BerkeleyDbExtension.Intefaces;
using System;
using System.Collections.Generic;
using BerkeleyDbExtension.Configuration;
using BerkeleyDB;
using System.Collections;
using BerkeleyDbExtension.Extensions;
using BerkeleyDbExtension.Translators;

namespace BerkeleyDbExtension.Core
{

    /// <summary>
    /// Represents an open connection to a berkeley database. This class cannot be inherited.
    /// </summary>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    public sealed class Connection<TKey, TValue> : IConnection<TKey, TValue>
    {
        private readonly string _sourcefile;
        private readonly ITranslator<TKey, TValue> _translator;

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <value>
        /// The configuration.
        /// </value>
        public BdbConfig Configuration { get; private set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="Connection{TKey, TValue}"/> class.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="translator">The translator.</param>
        public Connection(string filename, BdbConfig configuration, ITranslator<TKey, TValue> translator)
        {
            _sourcefile = filename;
            Configuration = configuration ?? DefaultCofiguration;
            _translator = translator ?? ((ITranslator<TKey, TValue>)new GenericTranslator<string, string>());
        }

        /// <summary>
        /// Gets the default cofiguration.
        /// </summary>
        /// <value>
        /// The default cofiguration.
        /// </value>
        public BdbConfig DefaultCofiguration
        {
            get { return GenerarateConfig(); }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Connection{TKey, TValue}"/> class.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <param name="translator">The translator.</param>
        public Connection(string filename, ITranslator<TKey, TValue> translator)
            : this(filename, null, translator)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Connection{TKey, TValue}"/> class.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <param name="confifugaration">The confifugaration.</param>
        public Connection(string filename, BdbConfig confifugaration)
            : this(filename, confifugaration, null)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Connection{TKey, TValue}"/> class.
        /// </summary>
        /// <param name="filename">The filename.</param>
        public Connection(string filename)
            : this(filename, null, null)
        {

        }


        /// <summary>
        /// Gets or sets the <see cref="TValue"/> with the specified key.
        /// </summary>
        /// <value>
        /// The <see cref="TValue"/>.
        /// </value>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public TValue this[TKey key]
        {
            get
            {
                DatabaseEntry dbKey = _translator.BuildKey(key);
                return _translator.Value(Database.Get(dbKey).Value);
            }
            set { Database.Put(_translator.BuildKey(key), _translator.BuildValue(value)); }
        }

        /// <summary>
        /// Gets the database.
        /// </summary>
        /// <value>
        /// The database.
        /// </value>
        public Database Database
        {
            get;
            private set;
        }

        /// <summary>
        /// Opens this instance.
        /// </summary>
        /// <exception cref="Exception">Connection already opened.</exception>
        public void Open()
        {
            if (!IsOpen)
            {
                Database = BTreeDatabase.Open(_sourcefile, Configuration);
            }
            else throw new Exception("Connection already opened.");
        }

        /// <summary>
        /// Closes this instance.
        /// </summary>
        public void Close()
        {
            if (Database != null)
            {
                Database.Close();
                Database = null;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is open.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is open; otherwise, <c>false</c>.
        /// </value>
        public bool IsOpen
        {
            get { return Database != null; }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Close();
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            Cursor cursor = Database.Cursor();

            while (cursor.MoveNext())
                yield return _translator.Translate(cursor.Current.Key, cursor.Current.Value);
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }


        /// <summary>
        /// Adds the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public void Add(TKey key, TValue value)
        {
            Database.Put(_translator.BuildKey(key), _translator.BuildValue(value));
        }

        /// <summary>
        /// Adds the specified collection.
        /// </summary>
        /// <param name="collection">The collection.</param>
        public void Add(IEnumerable<KeyValuePair<TKey, TValue>> collection)
        {
            foreach (var item in collection)
            {
                Database.Put(_translator.BuildKey(item.Key), _translator.BuildValue(item.Value));
            }
        }

        /// <summary>
        /// The _collection
        /// </summary>
        private Dictionary<TKey, TValue> _collection;

        /// <summary>
        /// Gets the collection.
        /// </summary>
        /// <value>
        /// The collection.
        /// </value>
        public Dictionary<TKey, TValue> Collection
        {
            get
            {
                if (_collection != null)
                {
                    _collection = new Dictionary<TKey, TValue>();

                    using (var cursor = Database.Cursor())
                    {
                        foreach (var kv in cursor)
                        {
                            var key = (TKey)kv.Key.Data.ConvertToType<TKey>();
                            var value = (TValue)kv.Value.Data.ConvertToType<TValue>();
                            _collection.Add(key, value);
                        }
                    }
                }
                return _collection;
            }
        }

        /// <summary>
        /// Generarates the configuration.
        /// </summary>
        /// <returns></returns>
        private static BdbConfig GenerarateConfig()
        {
            var config = new BdbConfig();
            if (BerkeleyDbExtension.Configuration.Configuration.Settings != null)
            {
                // Define all configurations user mentioned in the config file.
                if (BerkeleyDbExtension.Configuration.Configuration.Settings.Creation.HasValue)
                {
                    config.Creation = BerkeleyDbExtension.Configuration.Configuration.Settings.Creation.Value;
                }
            }
            else
            {
                config.Creation = CreatePolicy.IF_NEEDED;
                config.UseRecordNumbers = true;
                config.ErrorPrefix = "Reader :";
            }
            return config;
        }
    }
}
