namespace Rikrop.Core.Framework.Algorithms.CnfTransformer
{
    /// <summary>
    /// ��������� ������������� ������ ����������� ���������
    /// </summary>
    public interface ILogicalTreeTransformation
    {
        /// <summary>
        /// ��������� ������������� ������ (���������), ������ �������� �������� ���������� <see cref="root"/>
        /// </summary>
        /// <param name="root">������ ����������������� ������ (���������)</param>
        void Transform(LogicalTreeNode root);
    }
}