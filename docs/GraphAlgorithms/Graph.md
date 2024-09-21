### Design of `Graph` Class

#### Key Features:

- **Support for Directed and Undirected Graphs**: The class should work for both types, allowing flexibility depending
  on the problem.
- **Weighted and Unweighted Graphs**: The graph can store weights on edges if required.
- **Adjacency List Representation**: This is the most memory-efficient representation, especially for sparse graphs.
- **Generic Type for Vertices**: The graph should work with any type of vertex, allowing maximum flexibility (e.g., int,
  string, custom classes).
- **Edge Class**: A separate class to define edges, which might include source, destination, and weight.
- **Graph Operations**: Common graph operations such as adding/removing edges, checking adjacency, retrieving neighbors,
  etc.
- **Traversal Methods**: Functions for DFS and BFS might be part of this class for convenience.

---

### 1. **Graph Properties**

The graph class should have properties to store vertices and edges. We use an adjacency list to represent the graph.

- **Vertices**: A `HashSet` or `List` of vertices for quick lookups.
- **Adjacency List**: A `Dictionary` where the key is a vertex, and the value is a `List` of adjacent vertices or `Edge`
  objects (if weighted).

#### Example Properties:

- `IsDirected`: Boolean flag to indicate whether the graph is directed.
- `AdjacencyList`: Dictionary of vertices and their corresponding edges.
- `Vertices`: A collection of vertices (optional since you can derive it from the keys of `AdjacencyList`).

---

### 2. **Edge Class (for Weighted Graphs)**

If the graph supports weighted edges, we can define an `Edge` class that stores the source, destination, and weight.

#### Example Properties for Edge Class:

- `Source`: The starting vertex of the edge.
- `Destination`: The ending vertex of the edge.
- `Weight`: The weight of the edge (can be optional for unweighted graphs).

```plaintext
class Edge<T>
    - Source: T (vertex)
    - Destination: T (vertex)
    - Weight: double (optional, for weighted graphs)
```

---

### 3. **Methods for Graph Class**

#### Basic Methods:

- **AddVertex(T vertex)**: Adds a vertex to the graph.
- **AddEdge(T source, T destination, double? weight = null)**: Adds an edge between two vertices. If the graph is
  undirected, it adds edges in both directions.
- **RemoveVertex(T vertex)**: Removes a vertex and all associated edges.
- **RemoveEdge(T source, T destination)**: Removes an edge between two vertices.
- **GetNeighbors(T vertex)**: Retrieves the list of adjacent vertices.
- **ContainsVertex(T vertex)**: Checks if the graph contains a given vertex.

#### Example Methods:

```plaintext
- AddVertex(T vertex): Adds a new vertex to the graph.
- AddEdge(T source, T destination, double? weight = null): Adds an edge from source to destination (optionally with a weight).
- RemoveVertex(T vertex): Removes the vertex and all its edges.
- RemoveEdge(T source, T destination): Removes the edge between two vertices.
- GetNeighbors(T vertex): Returns a list of adjacent vertices or edges for the given vertex.
- ContainsVertex(T vertex): Checks if a vertex exists in the graph.
```

---

### 4. **Traversal Algorithms**

For convenience, we can implement common graph traversal algorithms directly within the graph class.

- **Depth-First Search (DFS)**: A method that performs a DFS traversal starting from a given vertex. It can return all
  reachable vertices or simply print the order of traversal.

- **Breadth-First Search (BFS)**: Similar to DFS but uses a queue to explore the graph level by level.

```plaintext
- DFS(T start): Traverses the graph using depth-first search.
- BFS(T start): Traverses the graph using breadth-first search.
```

---

### 5. **Graph Representation**

The adjacency list should be represented as a `Dictionary<T, List<Edge<T>>>` where `T` is the type of the vertices. This
allows quick lookups for neighbors and edges, and the `List` structure ensures efficient iteration through neighbors.

#### For Unweighted Graph:

- Use `Dictionary<T, List<T>>` where each key is a vertex and its value is a list of adjacent vertices.

#### For Weighted Graph:

- Use `Dictionary<T, List<Edge<T>>>` where `Edge<T>` stores both the destination and the weight of the edge.

---

### 6. **Example Structure**

Here’s a basic outline of how the structure of the `Graph` class could look.

```plaintext
class Graph<T>
    - IsDirected: bool (determines if the graph is directed or undirected)
    - AdjacencyList: Dictionary<T, List<Edge<T>>> (stores the vertices and their neighbors)
    
    Methods:
    - AddVertex(T vertex)
    - AddEdge(T source, T destination, double? weight = null)
    - RemoveVertex(T vertex)
    - RemoveEdge(T source, T destination)
    - GetNeighbors(T vertex)
    - ContainsVertex(T vertex)
    - DFS(T start)
    - BFS(T start)

class Edge<T>
    - Source: T (vertex)
    - Destination: T (vertex)
    - Weight: double (optional for weighted graphs)
```

---

### 7. **Design Considerations**

- **Performance**: Using an adjacency list ensures efficient memory usage and quick lookups for sparse graphs.
- **Generics**: The class should be generic (`T` for vertices), so the user can define graphs with different types of
  vertices.
- **Directed/Undirected Flexibility**: By using the `IsDirected` flag, the same graph class can handle both directed and
  undirected graphs.
- **Modularity**: Separate the `Edge` class to allow easier handling of weighted edges without complicating the main
  graph logic.

---

### Conclusion

This `Graph` class design provides a clean, efficient, and flexible way to represent and manipulate both
directed/undirected and weighted/unweighted graphs in C#. The class is structured to promote readability and
scalability, with essential methods like adding/removing vertices and edges, and performing traversals (DFS, BFS). This
design ensures the graph remains modular and easy to extend as new algorithms or functionalities are added.