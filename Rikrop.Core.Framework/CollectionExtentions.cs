using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using JetBrains.Annotations;

namespace Rikrop.Core.Framework
{
    /// <summary>
    /// Методы-расширения для контейнеров.
    /// </summary>
    public static class CollectionExtentions
    {
        /// <summary>
        /// Добавление в контейнер элементов коллекцию элементов.
        /// </summary>
        /// <remarks>Элементы будут добавлены по одному.</remarks>
        /// <typeparam name="T">Тип элементов контейнера.</typeparam>
        /// <param name="collection">Контейнер, в который будут добавляться элементы.</param>
        /// <param name="items">Коллекция элементов для добавления.</param>
        public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                collection.Add(item);
            }
        }

        /// <summary>
        /// Вставка в контейнер элементов коллекцию элементов.
        /// </summary>
        /// <remarks>Коллекция будет очищена перед вставкой.</remarks>
        /// <typeparam name="T">Тип элементов контейнера.</typeparam>
        /// <param name="collection">Контейнер, в который будут вставляться элементы.</param>
        /// <param name="items">Коллекция элементов для вставки.</param>
        public static void SetRange<T>(this ICollection<T> collection, IEnumerable<T> items)
        {
            collection.Clear();
            AddRange(collection, items);
        }

        /// <summary>
        /// Поиск элемента в словаре по ключу.
        /// </summary>
        /// <typeparam name="TKey">Тип записей ключа.</typeparam>
        /// <typeparam name="TValue">Тип записей элементов.</typeparam>
        /// <param name="dictionary">Словарь для поиска.</param>
        /// <param name="key">Ключ элемента для поиска.</param>
        /// <returns>Элемент с заданным ключом или null, если элемента с таким ключом нет в коллекции.</returns>
        public static TValue FindValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
        {
            TValue value;
            dictionary.TryGetValue(key, out value);
            return value;
        }

        /// <summary>
        /// Перебор элементов коллекции с обработкой каждого элемента.
        /// </summary>
        /// <typeparam name="T">Тип элементов.</typeparam>
        /// <param name="items">Коллекция элементов для перебора.</param>
        /// <param name="itemAction">Действие-обработчик элемента.</param>
        public static void Foreach<T>(this IEnumerable<T> items, [NotNull] Action<T> itemAction)
        {
            Contract.Requires<ArgumentNullException>(items != null);
            Contract.Requires<ArgumentNullException>(itemAction != null);

            foreach (var item in items)
            {
                itemAction(item);
            }
        }

        /// <summary>
        /// Слияние двух коллекций с разным типом элементов по заданному критерию сравнения.
        /// </summary>
        /// <typeparam name="TSource">Тип элементов исходной коллекции.</typeparam>
        /// <typeparam name="TTarget">Тип элементов целевой коллекции.</typeparam>
        /// <param name="sources">Исходная коллекция.</param>
        /// <param name="targets">Целевая коллекция.</param>
        /// <param name="comparer">Критерий сравнения элементов коллекций.</param>
        /// <returns>Информация о слиянии коллекций.</returns>
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