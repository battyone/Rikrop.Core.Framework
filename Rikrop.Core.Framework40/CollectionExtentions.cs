using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using JetBrains.Annotations;

namespace Rikrop.Core.Framework
{
    public static class CollectionExtentions
    {
        public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                collection.Add(item);
            }
        }

        public static void SetRange<T>(this ICollection<T> collection, IEnumerable<T> items)
        {
            collection.Clear();
            AddRange(collection, items);
        }

        public static TValue FindValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
        {
            TValue value;
            dictionary.TryGetValue(key, out value);
            return value;
        }

        public static void Foreach<T>(this IEnumerable<T> items, [NotNull] Action<T> itemAction)
        {
            Contract.Requires<ArgumentNullException>(items != null);
            Contract.Requires<ArgumentNullException>(itemAction != null);

            foreach (var item in items)
            {
                itemAction(item);
            }
        }

        public static CollectionMergeInfo<TSource, TTarget> Merge<TSource, TTarget>(this IEnumerable<TSource> sources, IEnumerable<TTarget> targets, Func<TTarget, TSource, bool> comparer)
        {
            var info = new CollectionMergeInfo<TSource, TTarget>();
            var targetsar = targets.ToArray();
            info.ToAdd.AddRange(targetsar);

            foreach (var source in sources)
            {
                var element = source;
                var target = targetsar.FirstOrDefault(o => comparer(o, element));

                if (!Equals(target, default(TTarget)))
                {
                    info.ToAdd.Remove(target);
                    info.Equal.Add(source, target);
                }
                else
                {
                    info.ToDelete.Add(source);
                }
            }

            return info;
        }
    }
}