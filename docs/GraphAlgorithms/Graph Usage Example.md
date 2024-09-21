## Graph Class Usage Example

This document provides a simple usage example for the `Graph<T>` class, demonstrating how to create a graph, add
vertices and edges, and print the resulting graph. The test output is also included for clarity.

### 1. Graph Creation Example

Below is a C# program that demonstrates how to use the `Graph<T>` class and the `PrintGraph` extension method.

#### C# Code Example:

```csharp
using System;
using Algorithms.Graphs;

class Program
{
    static void Main()
    {
        // Create an undirected graph of integers
        var graph = new Graph<int>(isDirected: false);

        // Add vertices and edges
        graph.AddVertex(1);
        graph.AddVertex(2);
        graph.AddVertex(3);
        graph.AddVertex(4);

        // Add edges with weights
        graph.AddEdge(1, 2, 4.5);  // Edge from 1 to 2 with weight 4.5
        graph.AddEdge(1, 3, 2.0);  // Edge from 1 to 3 with weight 2.0
        graph.AddEdge(3, 4, 1.5);  // Edge from 3 to 4 with weight 1.5
        graph.AddEdge(2, 4, 3.0);  // Edge from 2 to 4 with weight 3.0

        // Print the graph
        graph.PrintGraph();
    }
}
```

### 2. Output

When you run the above code, the graph will be printed in the console, showing each vertex and its connected neighbors
along with the weight of each edge.

#### Test Output:

```
1:  -> 2 (Weight: 4.5) -> 3 (Weight: 2)
2:  -> 1 (Weight: 4.5) -> 4 (Weight: 3)
3:  -> 1 (Weight: 2) -> 4 (Weight: 1.5)
4:  -> 3 (Weight: 1.5) -> 2 (Weight: 3)
```

### 3. Explanation of Output:

- **Vertex 1** is connected to:
    - **Vertex 2** with a weight of `4.5`.
    - **Vertex 3** with a weight of `2.0`.

- **Vertex 2** is connected to:
    - **Vertex 1** with a weight of `4.5`.
    - **Vertex 4** with a weight of `3.0`.

- **Vertex 3** is connected to:
    - **Vertex 1** with a weight of `2.0`.
    - **Vertex 4** with a weight of `1.5`.

- **Vertex 4** is connected to:
    - **Vertex 3** with a weight of `1.5`.
    - **Vertex 2** with a weight of `3.0`.

### 4. Notes

- The graph is **undirected**, so each edge is represented twice (once for each direction). For example, the edge
  between vertex 1 and vertex 2 appears under both vertex 1 and vertex 2.
- The weights of the edges are printed alongside each connection.

---

### 5. How to Run the Example

1. Clone the repository or create a new C# project.
2. Add the `Graph<T>` class and `GraphExtensions` class from the repository.
3. Copy the above usage example into your `Main` method.
4. Run the program to see the printed graph output.

### 6. Conclusion

This example demonstrates how to create a graph, add vertices and edges, and print the graph's structure using a simple,
flexible C# implementation. The output reflects the structure and weights of the edges in the undirected graph.