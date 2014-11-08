namespace Rikrop.Core.Framework.Algorithms.CnfTransformer
{
    /// <summary>
    /// ��������� �������������, ������������ (���� ��� ��������) ������ ������ �� 1,
    /// ����� �������� ������ ������ � ���������� �� ������� ����������������.
    /// �������������� ���������� ����� ������ ������ 4 � ������ ������ 3.
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
            // ��� ���������� ��������� ������ �������� ������ ������
            if (root.Type == NodeType.Disjunction)
            {
                _disjunctionTransformation.Transform(root);
            }
            // ���� ������� �� ����������, �� ���������������� ���������, ����� �� �������� ����������������
            else if (ApplicableToDistributiveTransformation(root))
            {
                _distributiveTransformation.Transform(root);
            }
            // ���� �������� ������ - �������� ������� ������ ������ ��� ����������
            else if (root.Type == NodeType.Conjunction)
            {
                _conjunctionTransformation.Transform(root);
            }

            // � ��������� ������ ������� - ����, � ��� ��� ������� ������������� �����������
        }

        private static bool ApplicableToDistributiveTransformation(LogicalTreeNode root)
        {
            var isConjunction = root.Type == NodeType.Conjunction;
            var hasDisjunctionChild = root.HasChild(x => x.Type == NodeType.Disjunction);

            return isConjunction && hasDisjunctionChild;
        }
    }
}