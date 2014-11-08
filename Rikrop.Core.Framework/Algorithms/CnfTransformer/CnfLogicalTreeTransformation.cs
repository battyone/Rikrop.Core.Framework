namespace Rikrop.Core.Framework.Algorithms.CnfTransformer
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// �������������, ���������� ������ � ��� �����
    /// ������ - ����������
    /// ���� ����� - ������������ ����������
    /// </summary>
    public class CnfLogicalTreeTransformation : ILogicalTreeTransformation
    {
        private readonly TreeHeightReductionTransformation _treeHeightReductionTransformation;

        private readonly DeMorganNegationPuller _deMorganNegationPuller;

        public CnfLogicalTreeTransformation()
        {
            _treeHeightReductionTransformation = new TreeHeightReductionTransformation();
            _deMorganNegationPuller = new DeMorganNegationPuller();
        }

        public void Transform(LogicalTreeNode root)
        {
            // ������� ����������� �� ���� ��������� � ���������
            // ����� ���������� ������ �� ������ � ��� �����
            PullNegationsDown(root);

            ReductHeight(root);
        }

        private void PullNegationsDown(LogicalTreeNode root)
        {
            _deMorganNegationPuller.Transform(root);
        }

        private void ReductHeight(LogicalTreeNode root)
        {
            // �.�. ������ ������-���� �������� ������ �� ����� �����������,
            // � ������ ������� ����� �������� ������� �� ��������� 3 (������ ���)
            // �� ������� �� ������ "�������" �������� � ������ "��������",
            // �������������� � ����� ������� ���������� ���������.
            var heightOrderedNodes = GetNodesOrderedByHeight(root).ToArray();

            foreach (var node in heightOrderedNodes)
            {
                _treeHeightReductionTransformation.Transform(node);
            }
        }

        /// <summary>
        /// ���������� ������ ������ ������, ������������� �� ������ 
        /// </summary>
        /// <param name="root">������ ������, ��� �������� ����������� ������������� ������ ��� ������</param>
        /// <returns>������ ������ ������, ������������� �� ������</returns>
        private IEnumerable<LogicalTreeNode> GetNodesOrderedByHeight(LogicalTreeNode root)
        {
            var nodes = GetNodes(root);

            return nodes.OrderBy(x => x.Height);
        }
        
        /// <summary>
        /// ���������� ��� ������� ������, ������� ������
        /// </summary>
        /// <param name="root">������ ������, ��� �������� ����������� ������ ��� ������</param>
        /// <returns>������ ������ ������</returns>
        private IEnumerable<LogicalTreeNode> GetNodes(LogicalTreeNode root)
        {
            var result = new List<LogicalTreeNode>();
            
            result.Add(root);
            
            foreach (var node in root.Children)
            {
                result.AddRange(GetNodes(node));
            }

            return result;
        }
    }
}