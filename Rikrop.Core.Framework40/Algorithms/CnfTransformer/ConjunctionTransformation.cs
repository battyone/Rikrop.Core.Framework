namespace Rikrop.Core.Framework.Algorithms.CnfTransformer
{
    using System;
    using System.Linq;

    /// <summary>
    /// ������������� ������, ��������� ������ ������ � ������,
    /// ������� ������������� �������� (�� ��������� �� ��������� ����������� ���������)
    /// ������� � ��������� ����������.
    /// </summary>
    public class ConjunctionTransformation : ILogicalTreeTransformation
    {
        public void Transform(LogicalTreeNode root)
        {
            ValidateRootIsConjunction(root);
            
            // ���������� ��������� ��������������� ���������� (.ToArray()),
            // �.�. ����� �������������� ���������, �� ������� ���������� �������
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
                throw new ArgumentException("��� ���������� ������������� �������� ������ ������ ��� ����������, ������ ������ ���� �����������", "root");
            }
        }
    }
}