namespace Rikrop.Core.Framework.Algorithms.CnfTransformer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// �������������, ���������� �� ������ ��������� ���������������� � ���������� ����������
    /// A & (B | C) => A & B | A & C
    /// </summary>
    /// <remarks>
    /// ��� �������� ��������� ���������������� � ����� ������, ����� ������������ ��������� ���������� � ����������, �� ��������� ������ ���� ������� ������
    /// ��������: 
    /// A & (B | C) & (D | E) & F & G => A & F & G & B & D | A & F & G & B & E | A & F & G & C & D | A & F & G & C & E
    /// </remarks>
    public class DistributiveTransformation : ILogicalTreeTransformation
    {
        public void Transform(LogicalTreeNode root)
        {
            ValidateRootIsConjunction(root);

            // ���������� ��������� ��������������� ���������� (.ToArray()),
            // �.�. ����� �������������� ���������, �� ������� ���������� �������
            var disjunctions = root.Children.Where(x => x.Type == NodeType.Disjunction).ToArray();
            var conjunctions = root.Children.Where(x => x.Type == NodeType.Conjunction).ToArray();
            var leafs = root.Children.Where(x => x.Type == NodeType.Leaf).ToArray();

            var conjunctionsDescendants = conjunctions.SelectMany(x => x.Children).ToArray();
            var disjunctionDescendants = disjunctions.Select(x => x.Children).ToArray();
            var disjunctionsCombinations = SequencesCombinator<LogicalTreeNode>.Combinate(disjunctionDescendants);

            // ��������� ������� �� ���������� ������������ ����������
            // ������ ���������� ������� �� ���� �������� ����������, ���������������� ����� �����
            // ���� ����� ����������, ��� ���������� - ���������������� ������� �����
            // ������ �� ��������� ���������� ����� ����������, ��� ���������� - ���������������� ������� �����
            root.Type = NodeType.Disjunction;
            root.ClearChildren();

            foreach (var disjunctionsCombination in disjunctionsCombinations)
            {
                // ���������� ����������, ������� ����� ����������� � ���������� ����� ����������.
                var disjunctionLeafs = FlatInnerConjunctions(disjunctionsCombination);

                var conjunction = new LogicalTreeNode(NodeType.Conjunction);

                conjunction.AddNodes(disjunctionLeafs);
                conjunction.AddNodes(conjunctionsDescendants);
                conjunction.AddNodes(leafs);

                root.AddNode(conjunction);
            }
        }

        private static IEnumerable<LogicalTreeNode> FlatInnerConjunctions(IEnumerable<LogicalTreeNode> disjunctionsCombination)
        {
            var result = new List<LogicalTreeNode>();

            var disjunctionLeafs = disjunctionsCombination.Where(x => x.Type == NodeType.Leaf);

            var disjunctionConjunctionsLeafs = disjunctionsCombination.Where(x => x.Type == NodeType.Conjunction)
                                                                      .SelectMany(x => x.Children);
            
            result.AddRange(disjunctionLeafs);
            result.AddRange(disjunctionConjunctionsLeafs);

            return result;
        }

        private void ValidateRootIsConjunction(LogicalTreeNode root)
        {
            if (root.Type != NodeType.Conjunction)
            {
                throw new ArgumentException("��� ���������� ������������� ��������� ����������������, ������ ������ ���� �����������", "root");
            }
        }
    }
}