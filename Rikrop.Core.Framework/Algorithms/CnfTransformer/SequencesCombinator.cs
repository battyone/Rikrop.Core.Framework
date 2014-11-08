namespace Rikrop.Core.Framework.Algorithms.CnfTransformer
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Класс для генерации всех возможных различных комбинаций элементов из нескольких групп путем выбора одного элемента из каждой группы
    /// </summary>
    /// <typeparam name="TElement">Тип элемента групп</typeparam>
    public static class SequencesCombinator<TElement>
    {
        /// <summary>
        /// Генерирует все возможные различные комбинации элементов, выбирая по одному элементу из каждой группы
        /// </summary>
        /// <param name="collections">Перечисление групп элементов</param>
        /// <returns>Список полученных комбинаций</returns>
        public static IEnumerable<IEnumerable<TElement>> Combinate(
            IEnumerable<IEnumerable<TElement>> collections)
        {
            var outerCollection = collections.ToList();

            return (outerCollection.Count > 0)
                ? Combinate(outerCollection, 0, new List<TElement>())
                : new IEnumerable<TElement>[0];
        }

        /// <summary>
        /// Генерирует все возможные различные комбинаций, выбирая по одному элементу из каждой группы и присоединяя их в хвост <see cref="buildingChain"/>
        /// </summary>
        /// <param name="collections">Перечисление групп элементов</param>
        /// <param name="outerCollectionIndex">Индекс текущей позиции во внешнем списке (группы элементов)</param>
        /// <param name="buildingChain">Набор элементов, который будет добавлен в голову каждой сгенерированной последовательности</param>
        /// <returns>Список полученных комбинаций</returns>
        private static IEnumerable<IEnumerable<TElement>> Combinate(
            List<IEnumerable<TElement>> collections,
            int outerCollectionIndex,
            List<TElement> buildingChain)
        {
            var result = new List<IEnumerable<TElement>>();

            var currentStepElements = collections[outerCollectionIndex];
            bool lastStep = (collections.Count - 1) == outerCollectionIndex;

            var concatenations = lastStep
                                     ? GetAllShortings(buildingChain, currentStepElements)
                                     : GetDeeperConcatenations(collections, outerCollectionIndex, buildingChain, currentStepElements);

            result.AddRange(concatenations);

            return result;
        }

        /// <summary>
        /// Генерирует все возможные различные комбинаций, выбирая по одному элементу из каждой группы
        /// и присоединяя их в хвост <see cref="buildingChain"/>, соединенного с элементом из <see cref="tailHeads"/>
        /// </summary>
        /// <param name="outerColletion">Перечисление групп элементов</param>
        /// <param name="outerCollectionIndex">Индекс текущей позиции во внешнем списке (группы элементов)</param>
        /// <param name="buildingChain">Набор элементов, который будет добавлен в голову каждой сгенерированной последовательности</param>
        /// <param name="tailHeads">Список элементов, которые будут присоеденены между <see cref="buildingChain"/> и сгенерированными последовательностями</param>
        /// <returns>Список полученных комбинаций</returns>
        private static List<IEnumerable<TElement>> GetDeeperConcatenations(
            List<IEnumerable<TElement>> outerColletion,
            int outerCollectionIndex,
            List<TElement> buildingChain,
            IEnumerable<TElement> tailHeads)
        {
            var result = new List<IEnumerable<TElement>>();

            foreach (var tail in tailHeads)
            {
                var tailedChain = new List<TElement>(buildingChain);
                tailedChain.Add(tail);

                result.AddRange(Combinate(outerColletion, outerCollectionIndex + 1, tailedChain));
            }

            return result;
        }

        /// <summary>
        /// Генерирует набор различных комбинаций, присоединяя элемент из списка <see cref="tails"/> в хвост <see cref="buildingChain"/>
        /// </summary>
        /// <param name="buildingChain">Набор элементов, который будет добавлен в голову каждой сгенерированной последовательности</param>
        /// <param name="tails">Набор различных хвоство последовательностей</param>
        /// <returns>Список полученных комбинаций</returns>
        private static List<IEnumerable<TElement>> GetAllShortings(
            List<TElement> buildingChain,
            IEnumerable<TElement> tails)
        {
            var result = new List<IEnumerable<TElement>>();

            foreach (var tail in tails)
            {
                var chain = new List<TElement>(buildingChain);
                chain.Add(tail);

                result.Add(chain);
            }

            return result;
        }
    }
}