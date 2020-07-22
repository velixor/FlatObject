using System.Collections.Generic;

namespace FlatObject
{
    internal class Node
    {
        public Node(object value, string name)
        {
            Value = value;
            Name = name;
        }

        public Node Parent { get; private set; }
        public object Value { get; }
        public string Name { get; }
        public List<Node> Children { get; } = new List<Node>();

        public bool IsRoot => Parent == null;

        private void AddChild(Node child)
        {
            Children.Add(child);
            child.Parent = this;
        }

        public void AddChildren(List<Node> children)
        {
            children.ForEach(AddChild);
        }
    }
}