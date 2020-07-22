using System.Collections.Generic;

namespace FlatObject
{
    internal class ObjectFlattener
    {
        private readonly List<Node> _nodes = new List<Node>();

        public Node[] FlattenTreeFromRoot(Node root)
        {
            AddNodeToList(root);
            return _nodes.ToArray();
        }

        private void AddNodeToList(Node node)
        {
            _nodes.Add(node);
            node.Children.ForEach(AddNodeToList);
        }
    }
}