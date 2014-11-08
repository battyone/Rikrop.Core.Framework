namespace Rikrop.Core.Framework.Algorithms.CnfTransformer
{
    /// <summary>
    /// Дерево логического выражения
    /// </summary>
    public class LogicalTree
    {
        /// <summary>
        /// Корень дерева
        /// </summary>
        public LogicalTreeNode Root { get; private set; }

        public LogicalTree(LogicalTreeNode root)
        {
            Root = root;
        }
    }
}