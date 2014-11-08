namespace Rikrop.Core.Framework.Algorithms.CnfTransformer
{
    using System;
    using System.Linq;

    /// <summary>
    /// Трансформация дерева, удаляющая лишние уровни в дереве,
    /// которые соответствуют ненужным (не влияющими на результат логического выражения)
    /// скобкам в выражении дизъюнкции.
    /// </summary>
    public class ConjunctionTransformation : ILogicalTreeTransformation
    {
        public void Transform(LogicalTreeNode root)
        {
            ValidateRootIsConjunction(root);
            
            // Необходимо выполнить предварительную энумерацию (.ToArray()),
            // т.к. далее модифицируется коллекция, по которой происходит выборка
            var conjunctions = root.Children.Where(x => x.Type == NodeType.Conjunction).ToArray();
            var leafs = root.Children.Where(x => x.Type == NodeType.Leaf).ToArray();

            root.ClearChildren();

            root.AddNodes(conjunctions.SelectMany(x => x.Children).ToList());
            root.AddNodes(leafs);
        }

        private void ValidateRootIsConjunction(LogicalTreeNode root)
        {
            if (root.Type != NodeType.Conjunction)
            {
                throw new ArgumentException("При применении трансформации удаления лишних скобок для конъюнкции, корень должен быть конъюнкцией", "root");
            }
        }
    }
}