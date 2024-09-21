# Graph Algorithms in C#

This document provides an in-depth overview of the graph algorithms implemented in this repository. Each section includes an explanation of the algorithm, its time complexity, use cases, and important notes about the implementation in C#.

## Table of Contents

1. [Introduction to Graphs](#introduction-to-graphs)
2. [Depth-First Search (DFS)](#depth-first-search-dfs)
3. [Breadth-First Search (BFS)](#breadth-first-search-bfs)
4. [Connected Components](#connected-components)
5. [Topological Sort](#topological-sort)
6. [Dijkstra’s Algorithm](#dijkstras-algorithm)
7. [Bellman-Ford Algorithm](#bellman-ford-algorithm)
8. [Minimum Spanning Trees (Kruskal's & Prim's)](#minimum-spanning-trees)
9. [Graph Representation](#graph-representation)

---

## 1. Introduction to Graphs

Graphs are a fundamental data structure in computer science, used to model relationships between entities. A graph consists of **vertices** (or nodes) connected by **edges**. Graphs can be **directed** or **undirected**, and edges can be **weighted** or **unweighted**.

- **Applications**:
    - Social networks (e.g., modeling connections between users)
    - GPS navigation systems (finding optimal routes)
    - Network routing protocols (optimizing data flow)

---

## 2. Depth-First Search (DFS)

### Description
Depth-First Search (DFS) is an algorithm used for traversing or searching tree or graph data structures. It explores as far as possible along each branch before backtracking.

### Time Complexity
- **Time Complexity**: O(V + E) where V is the number of vertices and E is the number of edges.
- **Space Complexity**: O(V) due to the recursion stack or explicit stack in the iterative version.

### Use Cases
- Finding connected components
- Detecting cycles in a graph
- Solving maze problems

### C# Implementation Notes
- DFS can be implemented recursively or iteratively using a stack.
- For large graphs, consider handling recursion depth limits carefully.

### Example
Refer to the source code in `src/Algorithms/GraphAlgorithms/DFS.cs`.

---

## 3. Breadth-First Search (BFS)

### Description
Breadth-First Search (BFS) explores all the nodes at the present depth before moving on to nodes at the next depth level. It is commonly used to find the shortest path in an unweighted graph.

### Time Complexity
- **Time Complexity**: O(V + E)
- **Space Complexity**: O(V) due to the queue used for traversal.

### Use Cases
- Finding the shortest path in unweighted graphs
- Level-order traversal in trees
- Solving puzzles like the shortest path in a maze

### C# Implementation Notes
- BFS is usually implemented using a queue.
- Ensure all nodes are marked as visited to avoid infinite loops.

### Example
Refer to the source code in `src/Algorithms/GraphAlgorithms/BFS.cs`.

---

## 4. Connected Components

### Description
A connected component is a subgraph in which any two vertices are connected to each other by paths. In undirected graphs, a connected component is simply a maximal set of vertices where every vertex is reachable from every other vertex in the same set.

### Time Complexity
- **Time Complexity**: O(V + E)

### Use Cases
- Identifying clusters in social networks
- Analyzing connected subgraphs in biological networks

### C# Implementation Notes
- Connected components can be identified using DFS or BFS. Mark all visited nodes and move to the next unvisited node to start the next component.

### Example
Refer to the source code in `src/Algorithms/GraphAlgorithms/ConnectedComponents.cs`.

---

## 5. Topological Sort

### Description
Topological sort is an ordering of the vertices in a directed acyclic graph (DAG) such that for every directed edge u → v, vertex u comes before vertex v in the ordering.

### Time Complexity
- **Time Complexity**: O(V + E)

### Use Cases
- Task scheduling with dependencies
- Ordering of compilation tasks
- Resolving symbol dependencies in linkers

### C# Implementation Notes
- Topological sort can be implemented using DFS. Ensure the graph is a DAG before applying the algorithm.

### Example
Refer to the source code in `src/Algorithms/GraphAlgorithms/TopologicalSort.cs`.

---

## 6. Dijkstra’s Algorithm

### Description
Dijkstra’s algorithm finds the shortest path between two vertices in a weighted graph. It works by exploring all possible paths and updating the shortest known path to each vertex.

### Time Complexity
- **Time Complexity**: O((V + E) log V) with a priority queue (e.g., using a binary heap).

### Use Cases
- GPS navigation systems
- Network routing protocols
- Shortest path in game maps

### C# Implementation Notes
- Use a priority queue to keep track of the minimum distance vertex to explore next.
- The algorithm only works for graphs with non-negative edge weights.

### Example
Refer to the source code in `src/Algorithms/GraphAlgorithms/Dijkstra.cs`.

---

## 7. Bellman-Ford Algorithm

### Description
Bellman-Ford is a dynamic programming algorithm used to find the shortest path in graphs with negative edge weights. Unlike Dijkstra’s algorithm, Bellman-Ford can handle negative weights but is slower.

### Time Complexity
- **Time Complexity**: O(V * E)

### Use Cases
- Graphs with negative edge weights
- Financial applications (e.g., arbitrage opportunities)

### C# Implementation Notes
- The algorithm iterates over all edges V-1 times to update distances and detects negative cycles.

### Example
Refer to the source code in `src/Algorithms/GraphAlgorithms/BellmanFord.cs`.

---

## 8. Minimum Spanning Trees (Kruskal's & Prim's)

### Kruskal's Algorithm
Kruskal’s algorithm builds the minimum spanning tree by sorting all edges and adding them one by one, ensuring no cycles are formed.

### Prim's Algorithm
Prim’s algorithm starts with a single vertex and grows the minimum spanning tree by adding the lowest-cost edge that connects a new vertex.

### Time Complexity
- **Kruskal's**: O(E log E)
- **Prim's**: O((V + E) log V) with a priority queue.

### Use Cases
- Network design (e.g., minimizing cable length)
- Approximation algorithms for NP-complete problems

### C# Implementation Notes
- Use Union-Find (Disjoint Set) to efficiently implement Kruskal’s algorithm.
- Prim’s algorithm is typically implemented with a priority queue.

### Example
Refer to the source code in `src/Algorithms/GraphAlgorithms/MinimumSpanningTree.cs`.

---

## 9. Graph Representation

### Description
Graphs can be represented in various ways, each with its trade-offs in terms of memory and performance.

- **Adjacency List**: A list where each vertex has a collection of its neighbors.
- **Adjacency Matrix**: A 2D matrix where the rows and columns represent vertices, and the cells represent edges.

### C# Implementation Notes
- Adjacency lists are usually more memory-efficient for sparse graphs, while adjacency matrices are useful for dense graphs.

### Example
Refer to the source code in `src/DataStructures/Graph.cs`.

---

## Conclusion

These graph algorithms form the backbone of many applications across different industries. Implementing them efficiently in C# and understanding their trade-offs is key to solving complex real-world problems.
