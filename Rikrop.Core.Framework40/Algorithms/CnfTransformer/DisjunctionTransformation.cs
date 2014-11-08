namespace Rikrop.Core.Framework.Algorithms.CnfTransformer
{
    using System;
    using System.Linq;

    /// <summary>
    /// ������������� ������, ��������� ������ ������ � ������,
    /// ������� ������������� �������� (�� ��������� �� ��������� ����������� ���������)
    /// ������� � ��������� ����������.
    /// </summary>
    public class DisjunctionTransformation : ILogicalTreeTransformation
    {
        public void Transform(LogicalTreeNode root)
        {
            ValidateRootIsDisjunction(root);

            // ���������� ��������� ��������������� ���������� (.ToArray()),
            // �.�. ����� �������������� ���������, �� ������� ���������� �������
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
                throw new ArgumentException("��� ���������� ������������� �������� ������ ������ ��� ����������, ������ ������ ���� �����������", "root");
            }
        }
    }
}