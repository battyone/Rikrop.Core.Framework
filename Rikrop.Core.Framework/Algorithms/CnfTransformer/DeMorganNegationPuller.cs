namespace Rikrop.Core.Framework.Algorithms.CnfTransformer
{
    /// <summary>
    /// ������������� ������, "����������" ��������� ����, � �������,
    /// � �������������� ������� ��-�������
    /// </summary>
    public class DeMorganNegationPuller : ILogicalTreeTransformation
    {
        /// <summary>
        /// ���������� "��������" ��������� ������ �� �������
        /// �� ������ � ������ ��������� ���� ������ � �������� ������
        /// </summary>
        /// <param name="root">������ ������, ��� �������� ������������ �������������</param>
        public void Transform(LogicalTreeNode root)
        {
            ReverseNegation(root);

            foreach (var childNode in root.Children)
            {
                Transform(childNode);
            }
        }

        /// <summary>
        /// ��������� ������� ��-������� � ���������� �������, � ������ ���� ��� �������� ���������.
        /// ���� ������� �� �������� ��������� - ��� �� ����������.
        /// </summary>
        /// <param name="node">�������, � ������� ����������� ������� ��-�������</param>
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