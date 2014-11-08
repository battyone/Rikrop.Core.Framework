namespace Rikrop.Core.Framework.Algorithms.CnfTransformer
{
    /// <summary>
    /// Тип узла дерева логических выражений
    /// </summary>
    public enum NodeType
    {
        /// <summary>
        /// Конъюнкция (A & B & ...)
        /// </summary>
        Conjunction,

        /// <summary>
        /// Дизъюнкция (A | B | ...)
        /// </summary>
        Disjunction,

        /// <summary>
        /// Листовая вершина,
        /// представляет собой саму логическую переменную.
        /// </summary>
        Leaf,
    }
}