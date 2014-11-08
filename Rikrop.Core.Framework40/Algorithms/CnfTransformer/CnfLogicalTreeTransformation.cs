namespace Rikrop.Core.Framework.Algorithms.CnfTransformer
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Трансформация, приводящая дерево в КНФ форму
    /// Корень - дизъюнкция
    /// Дети корня - элементарные конъюнкции
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
            // Вначале избавляемся от всех отрицаний в нелистьях
            // Потом редуцируем дерево по высоте к КНФ форме
            PullNegationsDown(root);

            ReductHeight(root);
        }

        private void PullNegationsDown(LogicalTreeNode root)
        {
            _deMorganNegationPuller.Transform(root);
        }

        private void ReductHeight(LogicalTreeNode root)
        {
            // Т.к. высота какого-либо элемента дерева не может увеличиться,
            // и высота вершины после редукции никогда не превышает 3 (высота КНФ)
            // то проходя от самого "низкого" элемента к самому "высокому",
            // гарантированно в конце получим правильный результат.
            var heightOrderedNodes = GetNodesOrderedByHeight(root).ToArray();

            foreach (var node in heightOrderedNodes)
            {
                _treeHeightReductionTransformation.Transform(node);
            }
        }

        /// <summary>
        /// Возвращает список вершин дерева, упорядоченные по высоте 
        /// </summary>
        /// <param name="root">Корень дерева, для которого вычисляется упорядоченный список его вершин</param>
        /// <returns>Список вершин дерева, упорядоченные по высоте</returns>
        private IEnumerable<LogicalTreeNode> GetNodesOrderedByHeight(LogicalTreeNode root)
        {
            var nodes = GetNodes(root);

            return nodes.OrderBy(x => x.Height);
        }
        
        /// <summary>
        /// Возвращает все вершины дерева, включая корень
        /// </summary>
        /// <param name="root">Корень дерева, для которого вычисляется список его вершин</param>
        /// <returns>Список вершин дерева</returns>
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