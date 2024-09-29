# Cycle Detection in Graphs Using Depth-First Search (DFS)

This document describes the solution for detecting **all cycles** in a graph using a DFS-based algorithm. The
implementation utilizes vertex coloring to track the state of each node and reconstructs cycles when back edges are
found.

## Problem Definition

A **cycle** in a graph is a path that starts and ends at the same vertex, with no repeated edges or vertices (except for
the starting/ending vertex).

The goal of this algorithm is to detect all such cycles in a graph.

## Approach: Depth-First Search (DFS) with Vertex Coloring

We use **Depth-First Search (DFS)** to traverse the graph and a **vertex coloring scheme** to detect back edges. The
colors are used to track the state of each vertex:

- **White (0)**: The vertex has not been visited yet.
- **Gray (1)**: The vertex is being visited (currently in the DFS stack).
- **Black (2)**: The vertex and all its neighbors have been fully processed.

Back edges (Gray → Gray) indicate the presence of a cycle. The algorithm reconstructs each cycle by backtracking along
the parent chain.

### Key Data Structures:

- **`_color`**: Dictionary to store the state of each vertex (White, Gray, Black).
- **`_parent`**: Dictionary to store the parent of each vertex, used for reconstructing cycles.
- **`_cycles`**: List of all detected cycles.

---

## Algorithm: Cycle Detection in Undirected Graphs

### Step-by-Step Process

1. **Initialization**:
    - All vertices are marked as **White** (unvisited).
    - The algorithm initializes the parent of each vertex as `null` to prepare for cycle reconstruction.

2. **DFS Traversal**:
    - The DFS is initiated for every unvisited vertex.
    - During DFS, vertices are marked **Gray** to indicate they are in progress.

3. **Cycle Detection**:
    - If a **Gray** vertex is encountered during the DFS (a back edge), a cycle is detected.
    - The algorithm reconstructs the cycle by backtracking along the parent chain.

4. **Cycle Reconstruction**:
    - Once a cycle is detected, the path is reconstructed from the current vertex back to the starting vertex of the
      cycle, and the cycle is added to the result list.

5. **Finish Processing**:
    - After fully processing all neighbors, the vertex is marked **Black**, indicating that it has been completely
      processed.

---

## Code Implementation

- [CycleDetector.cs](../../../src/Examples/Graphs/CycleDetection/CycleDetector.cs)

---

## Usage Example

```csharp
class Program
{
    static void Main()
    {
        var graph = new Graph<int>(isDirected: false);

        // Add vertices and edges
        graph.AddVertex(0);
        graph.AddVertex(1);
        graph.AddVertex(6);
        graph.AddVertex(9);

        graph.AddEdge(0, 1);
        graph.AddEdge(1, 6);
        graph.AddEdge(6, 9);
        graph.AddEdge(9, 0);  // Forms a cycle

        var cycleDetector = new CycleDetector<int>(graph);
        var cycles = cycleDetector.FindAllCycles();

        foreach (var cycle in cycles)
        {
            Console.WriteLine("Cycle detected: " + string.Join(" -> ", cycle));
        }
    }
}
```

### Output:

```
Cycle detected: 0 -> 9 -> 6 -> 1 -> 0
```

The above graph has a cycle formed by the vertices **0 → 1 → 6 → 9 → 0**. The DFS traversal identifies this cycle,
reconstructs it, and outputs it.

- Each cycle is detected by finding **back edges** in the DFS traversal.
- Cycles are reconstructed by backtracking along the parent chain.

---

## Complexity Analysis

- **Time Complexity**: The time complexity of the algorithm is **O(V + E)**, where:
    - **V** is the number of vertices.
    - **E** is the number of edges.
      This is the time complexity of a DFS traversal, as each vertex and edge is visited once.

- **Space Complexity**: The space complexity is also **O(V + E)** due to the storage of the graph, the color dictionary,
  and the parent tracking for cycle reconstruction.

---

## Conclusion

This cycle detection algorithm efficiently finds all cycles in an undirected graph using **DFS** and **vertex coloring**.
By leveraging back edges during traversal, the algorithm ensures that all simple cycles are detected and reported.

This implementation can be further extended to handle various graph structures and traversal strategies.
