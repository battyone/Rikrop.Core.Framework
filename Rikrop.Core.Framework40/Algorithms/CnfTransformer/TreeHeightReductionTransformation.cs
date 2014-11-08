namespace Rikrop.Core.Framework.Algorithms.CnfTransformer
{
    /// <summary>
    /// Сценарций трансформации, редуцирующий (если это возможно) высоту дерева на 1,
    /// путем удаления лишних скобок и разложения по правилу дистрибутивности.
    /// Гарантированно редуцирует любое дерево высоты 4 в дерево высоты 3.
    /// </summary>
    public class TreeHeightReductionTransformation : ILogicalTreeTransformation
    {
        private readonly DisjunctionTransformation _disjunctionTransformation;

        private readonly ConjunctionTransformation _conjunctionTransformation;

        private readonly DistributiveTransformation _distributiveTransformation;

        public TreeHeightReductionTransformation()
        {
            _disjunctionTransformation = new DisjunctionTransformation();
            _conjunctionTransformation = new ConjunctionTransformation();
            _distributiveTransformation = new DistributiveTransformation();
        }

        public void Transform(LogicalTreeNode root)
        {
            // Для дизъюнкции применимо только удаление лишних скобок
            if (root.Type == NodeType.Disjunction)
            {
                _disjunctionTransformation.Transform(root);
            }
            // Если вершина не дизъюнкция, то первоприоритетно проверяем, можно ли раскрыть дистрибутивность
            else if (ApplicableToDistributiveTransformation(root))
            {
                _distributiveTransformation.Transform(root);
            }
            // Если раскрыть нельзя - пытаемся удалить лишние скобки для конъюнкции
            else if (root.Type == NodeType.Conjunction)
            {
                _conjunctionTransformation.Transform(root);
            }

            // В противном случае вершниа - лист, и для нее никакие трансформации неприменимы
        }

        private static bool ApplicableToDistributiveTransformation(LogicalTreeNode root)
        {
            var isConjunction = root.Type == NodeType.Conjunction;
            var hasDisjunctionChild = root.HasChild(x => x.Type == NodeType.Disjunction);

            return isConjunction && hasDisjunctionChild;
        }
    }
}