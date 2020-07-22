using System.Collections.Generic;
using System.Linq;

namespace FlatObject
{
    internal class FlatObject : IFlatObject
    {
        private readonly Node[] _nodes;

        public FlatObject(Node[] nodes)
        {
            _nodes = nodes;
        }

        public IEnumerable<object> Descendants => _nodes.Select(x => x.Value);

        public string[] GetDescendantPath(object obj, bool includeRoot = false, bool includeLeaf = false)
        {
            var node = _nodes.Single(x => x.Value.Equals(obj));
            var path = new List<string>();

            if (!node.IsRoot)
            {
                if (!includeLeaf) node = node.Parent;

                while (!node.IsRoot)
                {
                    path.Add(node.Name);
                    node = node.Parent;
                }
            }

            if (includeRoot) path.Add(node.Name);

            path.Reverse();
            return path.ToArray();
        }

        public string GetDescendantName(object obj)
        {
            var node = _nodes.Single(x => x.Value.Equals(obj));
            return node.Name;
        }

        public bool IsRootObject(object obj)
        {
            return _nodes.Single(x => x.Value.Equals(obj)).IsRoot;
        }
    }
}