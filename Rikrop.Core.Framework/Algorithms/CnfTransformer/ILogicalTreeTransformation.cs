namespace Rikrop.Core.Framework.Algorithms.CnfTransformer
{
    /// <summary>
    /// Интерфейс трансформации дерева логического выражения
    /// </summary>
    public interface ILogicalTreeTransformation
    {
        /// <summary>
        /// Выполняет трансформацию дерева (поддерева), корнем которого является переменная <see cref="root"/>
        /// </summary>
        /// <param name="root">Корень трансформируемого дерева (поддерева)</param>
        void Transform(LogicalTreeNode root);
    }
}