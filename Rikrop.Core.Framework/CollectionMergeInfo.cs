using System.Collections.Generic;

namespace Rikrop.Core.Framework
{
    /// <summary>
    /// ���������� � ������� ���������.
    /// </summary>
    /// <typeparam name="TSourceElement">��� ��������� �������� ���������.</typeparam>
    /// <typeparam name="TTargetElement">��� ��������� ������� ���������.</typeparam>
    public class CollectionMergeInfo<TSourceElement, TTargetElement>
    {
        /// <summary>
        /// ��������, ������� ���������� ������ � ������� ���������, �� ����������� � ��������.
        /// </summary>
        public readonly List<TTargetElement> ToAdd = new List<TTargetElement>();

        /// <summary>
        /// ��������, ������� ���������� ������ � �������� ���������, �� ����������� � �������.
        /// </summary>
        public readonly List<TSourceElement> ToDelete = new List<TSourceElement>();

        /// <summary>
        /// ������������� ��������� �������� � ������� ���������, ������� ���������� � ����� ����������.
        /// </summary>
        public readonly Dictionary<TSourceElement, TTargetElement> Equal = new Dictionary<TSourceElement, TTargetElement>();
    }
}
