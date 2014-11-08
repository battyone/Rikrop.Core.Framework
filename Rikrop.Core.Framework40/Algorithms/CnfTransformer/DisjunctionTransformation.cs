namespace Rikrop.Core.Framework.Algorithms.CnfTransformer
{
    using System;
    using System.Linq;

    /// <summary>
    /// Трансформация дерева, удаляющая лишние уровни в дереве,
    /// которые соответствуют ненужным (не влияющими на результат логического выражения)
    /// скобкам в выражении дизъюнкции.
    /// </summary>
    public class DisjunctionTransformation : ILogicalTreeTransformation
    {
        public void Transform(LogicalTreeNode root)
        {
            ValidateRootIsDisjunction(root);

            // Необходимо выполнить предварительную энумерацию (.ToArray()),
            // т.к. далее модифицируется коллекция, по которой происходит выборка
            var disjunctions = root.Children.Where(x => x.Type == NodeType.Disjunction).ToArray();
            var conjunctions = root.Children.Where(x => x.Type == NodeType.Conjunction).ToArray();
            var leafs = root.Children.Where(x => x.Type == NodeType.Leaf).ToArray();

            root.ClearChildren();

            root.AddNodes(conjunctions);
            root.AddNodes(leafs);

            root.AddNodes(disjunctions.SelectMany(x => x.Children));
        }

        private void ValidateRootIsDisjunction(LogicalTreeNode root)
        {
            if (root.Type != NodeType.Disjunction)
            {
                throw new ArgumentException("При применении трансформации удаления лишних скобок для дизъюнкции, корень должен быть дизъюнкцией", "root");
            }
        }
    }
}