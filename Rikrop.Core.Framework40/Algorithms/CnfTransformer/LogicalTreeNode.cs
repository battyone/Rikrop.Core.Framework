namespace Rikrop.Core.Framework.Algorithms.CnfTransformer
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    /// <summary>
    /// Вершина дерева логического выражения
    /// </summary>
    public class LogicalTreeNode
    {
        private readonly List<LogicalTreeNode> _children;

        private NodeType _type;

        public string Name { get; set; }

        public bool Negated { get; set; }

        public NodeType Type
        {
            get
            {
                return _type;
            }

            set
            {
                if (_type == NodeType.Leaf || value == NodeType.Leaf)
                {
                    throw new ArgumentException("Невозможно изменение типа вершины на лист, если она не была листом, или на любой другой, если она была листом", "value");
                }

                _type = value;
            }
        }

        public int Height { get; private set; }

        public LogicalTreeNode Parent { get; private set; }

        public ReadOnlyCollection<LogicalTreeNode> Children
        {
            get
            {
                return _children.AsReadOnly();
            }
        }

        public bool HasParent
        {
            get
            {
                return Parent != null;
            }
        }

        public LogicalTreeNode(NodeType type)
        {
            _children = new List<LogicalTreeNode>();

            _type = type;

            ResetHeight();
        }

        private LogicalTreeNode Clone()
        {
            var clone = new LogicalTreeNode(Type);
            clone.Name = Name;
            clone.Negated = Negated;

            foreach (var node in _children)
            {
                clone.AddNode(node.Clone());
            }

            return clone;
        }

        public void AddNodes(IEnumerable<LogicalTreeNode> nodes)
        {
            foreach (var node in nodes)
            {
                AddNode(node.Clone());
            }

            UpdateParentHeight();
        }

        public void AddNode(LogicalTreeNode node)
        {
            _children.Add(node);

            node.SetParent(this);

            UpdateHeightUsingChild(node);
        }

        public void RemoveNode(LogicalTreeNode node)
        {
            _children.Remove(node);
            node.Parent = null;

            UpdateHeight();
        }

        public void ClearChildren()
        {
            _children.ForEach(x => x.Parent = null);

            _children.Clear();

            ResetHeight();

            UpdateParentHeight();
        }

        public bool HasChild(Predicate<LogicalTreeNode> predicate)
        {
            return _children.Exists(predicate);
        }

        private void DelinkFromParent()
        {
            if (HasParent)
            {
                Parent.RemoveNode(this);
            }
        }

        private void SetParent(LogicalTreeNode newParent)
        {
            DelinkFromParent();

            Parent = newParent;
        }

        private void UpdateHeight()
        {
            ResetHeight();

            foreach (var child in _children)
            {
                UpdateHeightUsingChild(child);
            }
        }

        private void UpdateHeightUsingChild(LogicalTreeNode node)
        {
            if (Height < node.Height + 1)
            {
                Height = node.Height + 1;
            }
        }

        private void UpdateParentHeight()
        {
            if (HasParent)
            {
                Parent.Height = Height + 1;

                Parent.UpdateParentHeight();
            }
        }

        private void ResetHeight()
        {
            Height = 1;
        }
    }
}