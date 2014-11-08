namespace Rikrop.Core.Framework.Algorithms.CnfTransformer
{
    /// <summary>
    /// ��� ���� ������ ���������� ���������
    /// </summary>
    public enum NodeType
    {
        /// <summary>
        /// ���������� (A & B & ...)
        /// </summary>
        Conjunction,

        /// <summary>
        /// ���������� (A | B | ...)
        /// </summary>
        Disjunction,

        /// <summary>
        /// �������� �������,
        /// ������������ ����� ���� ���������� ����������.
        /// </summary>
        Leaf,
    }
}