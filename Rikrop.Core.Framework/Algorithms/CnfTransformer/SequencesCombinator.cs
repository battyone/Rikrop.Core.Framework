namespace Rikrop.Core.Framework.Algorithms.CnfTransformer
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// ����� ��� ��������� ���� ��������� ��������� ���������� ��������� �� ���������� ����� ����� ������ ������ �������� �� ������ ������
    /// </summary>
    /// <typeparam name="TElement">��� �������� �����</typeparam>
    public static class SequencesCombinator<TElement>
    {
        /// <summary>
        /// ���������� ��� ��������� ��������� ���������� ���������, ������� �� ������ �������� �� ������ ������
        /// </summary>
        /// <param name="collections">������������ ����� ���������</param>
        /// <returns>������ ���������� ����������</returns>
        public static IEnumerable<IEnumerable<TElement>> Combinate(
            IEnumerable<IEnumerable<TElement>> collections)
        {
            var outerCollection = collections.ToList();

            return (outerCollection.Count > 0)
                ? Combinate(outerCollection, 0, new List<TElement>())
                : new IEnumerable<TElement>[0];
        }

        /// <summary>
        /// ���������� ��� ��������� ��������� ����������, ������� �� ������ �������� �� ������ ������ � ����������� �� � ����� <see cref="buildingChain"/>
        /// </summary>
        /// <param name="collections">������������ ����� ���������</param>
        /// <param name="outerCollectionIndex">������ ������� ������� �� ������� ������ (������ ���������)</param>
        /// <param name="buildingChain">����� ���������, ������� ����� �������� � ������ ������ ��������������� ������������������</param>
        /// <returns>������ ���������� ����������</returns>
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
        /// ���������� ��� ��������� ��������� ����������, ������� �� ������ �������� �� ������ ������
        /// � ����������� �� � ����� <see cref="buildingChain"/>, ������������ � ��������� �� <see cref="tailHeads"/>
        /// </summary>
        /// <param name="outerColletion">������������ ����� ���������</param>
        /// <param name="outerCollectionIndex">������ ������� ������� �� ������� ������ (������ ���������)</param>
        /// <param name="buildingChain">����� ���������, ������� ����� �������� � ������ ������ ��������������� ������������������</param>
        /// <param name="tailHeads">������ ���������, ������� ����� ������������ ����� <see cref="buildingChain"/> � ���������������� ��������������������</param>
        /// <returns>������ ���������� ����������</returns>
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
        /// ���������� ����� ��������� ����������, ����������� ������� �� ������ <see cref="tails"/> � ����� <see cref="buildingChain"/>
        /// </summary>
        /// <param name="buildingChain">����� ���������, ������� ����� �������� � ������ ������ ��������������� ������������������</param>
        /// <param name="tails">����� ��������� ������� �������������������</param>
        /// <returns>������ ���������� ����������</returns>
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