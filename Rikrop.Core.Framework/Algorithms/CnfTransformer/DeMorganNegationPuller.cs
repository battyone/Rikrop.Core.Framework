namespace Rikrop.Core.Framework.Algorithms.CnfTransformer
{
    /// <summary>
    /// Трансформация дерева, "опускающая" отрицание вниз, к листьям,
    /// с использованием законов Де-Моргана
    /// </summary>
    public class DeMorganNegationPuller : ILogicalTreeTransformation
    {
        /// <summary>
        /// Рекурсивно "опускает" отрицание вплоть до листьев
        /// На выходе в дереве отрицание есть только у листовых вершин
        /// </summary>
        /// <param name="root">Корень дерева, для которого производится трансформация</param>
        public void Transform(LogicalTreeNode root)
        {
            ReverseNegation(root);

            foreach (var childNode in root.Children)
            {
                Transform(childNode);
            }
        }

        /// <summary>
        /// Применяет правило Де-Моргана к конкретной вершине, в случае если она содержит отрицание.
        /// Если вершина не содержит отрицания - она не изменяется.
        /// </summary>
        /// <param name="node">Вершина, к которой применяется правило де-моргана</param>
        private static void ReverseNegation(LogicalTreeNode node)
        {
            if (!node.Negated)
            {
                return;
            }

            switch (node.Type)
            {
                case NodeType.Conjunction:
                    node.Type = NodeType.Disjunction;
                    node.Negated = false;
                    break;

                case NodeType.Disjunction:
                    node.Type = NodeType.Conjunction;
                    node.Negated = false;
                    break;
            }
        }
    }
}