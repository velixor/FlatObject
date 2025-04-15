# FlatObject

FlatObject is a C# library that provides a flattened representation of complex object hierarchies. It allows you to easily traverse and analyze object graphs by turning nested structures into flat collections while maintaining relationship information.

## Features

- **Object Flattening**: Convert complex nested object hierarchies into flat collections
- **Relationship Tracking**: Maintain parent-child relationships between objects
- **Path Information**: Get path information from root to any object in the hierarchy
- **Hierarchical Querying**: Query objects by their position in the object graph
- **Collection Support**: Works with arrays, lists, and other enumerable types
- **Recursive Property Analysis**: Analyzes properties and their properties recursively

## Usage

### Basic Usage

```csharp
using FlatObject;

// Create an instance of the factory
var factory = new FlatObjectFactory();

// Flatten a complex object
var myObject = new MyComplexClass();
IFlatObject flat = factory.Flatten(myObject);

// Access all descendant objects
foreach (var descendant in flat.Descendants)
{
    Console.WriteLine($"Found descendant: {flat.GetDescendantName(descendant)}");
}
```

### Getting Object Paths

```csharp
// Get the path to a specific object in the hierarchy
var someNestedObject = myObject.SomeProperty.NestedProperty;
string[] path = flat.GetDescendantPath(someNestedObject, includeRoot: true);

// Print the full path
Console.WriteLine("Path to object: " + string.Join(".", path));
```

### Working with Hierarchies

```csharp
// Check if an object is the root
bool isRoot = flat.IsRootObject(someObject);

// Get just the name of a descendant object
string name = flat.GetDescendantName(someDescendant);
```

## How It Works

FlatObject traverses the object graph starting from the provided root object. It:
1. Explores all public properties of the object
2. Follows reference-type properties recursively
3. Handles collections by examining their elements
4. Builds a tree structure representing the object hierarchy
5. Provides a flattened view of this hierarchy through the IFlatObject interface
