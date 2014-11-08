namespace Rikrop.Core.Framework.Algorithms.CnfTransformer
{
    /// <summary>
    /// ������ ����������� ���������
    /// </summary>
    public class LogicalTree
    {
        /// <summary>
        /// ������ ������
        /// </summary>
        public LogicalTreeNode Root { get; private set; }

        public LogicalTree(LogicalTreeNode root)
        {
            Root = root;
        }
    }
}