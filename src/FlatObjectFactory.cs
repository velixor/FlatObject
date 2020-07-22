using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace FlatObject
{
    public class FlatObjectFactory : IFlatObjectFactory
    {
        private readonly ObjectFlattener _objectFlattener;
        private readonly List<object> _visited;

        public FlatObjectFactory()
        {
            _visited = new List<object>();
            _objectFlattener = new ObjectFlattener();
        }

        public IFlatObject Flatten(object obj)
        {
            ValidateArgument(obj);

            var root = new Node(obj, obj.GetType().Name);
            AddChildrenToNode(root);

            var nodes = _objectFlattener.FlattenTreeFromRoot(root);
            return new FlatObject(nodes);
        }

        private static void ValidateArgument(object obj)
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));

            if (!obj.GetType().IsClass) throw new ArgumentException("Тип аргумента должен быть классом", nameof(obj));
        }

        private void AddChildrenToNode(Node node)
        {
            var childNodes = GetChildNodes(node);
            node.AddChildren(childNodes.ToList());
            node.Children.ForEach(AddChildrenToNode);
        }

        private IEnumerable<Node> GetChildNodes(Node node)
        {
            var childNodes = GetChildNodesFromObject(node.Value);

            var notVisitedChildNodes = childNodes.Where(x => !_visited.Contains(x.Value)).ToArray();

            _visited.AddRange(notVisitedChildNodes.Select(x => x.Value));

            return notVisitedChildNodes;
        }

        private static IEnumerable<Node> GetChildNodesFromObject(object obj)
        {
            if (obj is IEnumerable enumerable)
                return GetChildNodesFromSequence(enumerable);
            else
                return GetChildNodesFromComplexObject(obj);
        }

        private static IEnumerable<Node> GetChildNodesFromSequence(IEnumerable enumerable)
        {
            var array = enumerable as object[] ?? enumerable.Cast<object>().ToArray();

            if (!IsElementTypeValid(array)) return Enumerable.Empty<Node>();

            var nodes = array
                .Where(item => item != null)
                .Select(item => new Node(item, item.GetType().Name));

            return nodes;
        }

        private static bool IsElementTypeValid(object[] array)
        {
            var elementType = array.All(x => x == null) ? null : array.First().GetType();
            return elementType != null && IsValidType(elementType);
        }

        private static IEnumerable<Node> GetChildNodesFromComplexObject(object obj)
        {
            var type = obj.GetType();
            var properties = GetPropertiesOfType(type);

            var nodes = properties
                .Select(prop => new {value = prop.GetValue(obj), name = prop.Name})
                .Where(prop => prop.value != null)
                .Select(prop => new Node(prop.value, prop.name));

            return nodes;
        }

        private static IEnumerable<PropertyInfo> GetPropertiesOfType(IReflect type)
        {
            return type
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(IsValidProperty).ToArray();
        }

        private static bool IsValidProperty(PropertyInfo property)
        {
            return property.CanRead &&
                IsValidType(property.PropertyType);
        }

        private static bool IsValidType(Type type)
        {
            return type.IsClass;
        }
    }
}