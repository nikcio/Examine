﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Examine
{
    /// <summary>
    /// An implementation of a generic ordered dictionary
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TVal"></typeparam>
    public class OrderedDictionary<TKey, TVal> : KeyedCollection<TKey, KeyValuePair<TKey, TVal>>, IDictionary<TKey, TVal>, IReadOnlyDictionary<TKey, TVal>
    {
        public OrderedDictionary()
        {
        }

        public OrderedDictionary(IEqualityComparer<TKey> comparer) : base(comparer)
        {
        }
        
        public TVal GetItem(int index)
        {
            if (index >= Count) throw new IndexOutOfRangeException();

            var found = base[index];

            return base[found.Key].Value;
        }

        public int IndexOf(TKey key)
        {
            if (base.Dictionary == null) return -1;
            if (base.Dictionary.TryGetValue(key, out var found))
            {
                return base.Items.IndexOf(found);
            }
            return -1;
        }

        protected override TKey GetKeyForItem(KeyValuePair<TKey, TVal> item)
        {
            return item.Key;
        }

        public bool ContainsKey(TKey key)
        {            
            return base.Contains(key);
        }

        public void Add(TKey key, TVal value)
        {
            if (base.Contains(key)) throw new ArgumentException("The key " + key + " already exists in this collection");

            base.Add(new KeyValuePair<TKey, TVal>(key, value));
        }

        public bool TryGetValue(TKey key, out TVal value)
        {
            if (base.Dictionary == null)
            {
                value = default(TVal);
                return false;
            }

            if (base.Dictionary.TryGetValue(key, out var found))
            {
                value = found.Value;
                return true;
            }

            value = default(TVal);
            return false;
        }

        TVal IReadOnlyDictionary<TKey, TVal>.this[TKey key] => ((IDictionary<TKey, TVal>)this)[key];

        IEnumerable<TKey> IReadOnlyDictionary<TKey, TVal>.Keys => Keys;

        IEnumerable<TVal> IReadOnlyDictionary<TKey, TVal>.Values => Values;

        TVal IDictionary<TKey, TVal>.this[TKey key]
        {
            get
            {
                if (base.Dictionary != null && 
                    base.Dictionary.TryGetValue(key, out var found))
                {
                    return found.Value;
                }
                return default(TVal);
            }
            set
            {
                if (base.Dictionary != null && 
                    base.Dictionary.TryGetValue(key, out var found))
                {
                    var index = base.Items.IndexOf(found);
                    base.SetItem(index, new KeyValuePair<TKey, TVal>(key, value));
                }
                else
                {
                    base.Add(new KeyValuePair<TKey, TVal>(key, value));
                }
            }
        }

        private static readonly ICollection<TKey> EmptyCollection = new List<TKey>();
        private static readonly ICollection<TVal> EmptyValues = new List<TVal>();

        public ICollection<TKey> Keys => base.Dictionary != null ? base.Dictionary.Keys : EmptyCollection;

        public ICollection<TVal> Values => base.Dictionary != null ? base.Dictionary.Values.Select(x => x.Value).ToArray() : EmptyValues;
    }
}