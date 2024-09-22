# Connected Components Problem: BFS vs DFS

## Problem Definition

In an **undirected graph**, a **connected component** is a maximal set of vertices such that any two vertices are
connected by a path, and no vertex in the set is connected to any vertex outside of it.

The goal is to identify all such connected components in a given graph. This can be achieved using two common traversal
strategies:

- **Breadth-First Search (BFS)**
- **Depth-First Search (DFS)**

This document provides an overview of solving the connected components problem using both **BFS** and **DFS**, comparing
their implementations and benchmark results.

## Table of Contents

- [Generic Solution Using Different Traversal Strategies](#generic-solution-using-different-traversal-strategies)
- [BFS Approach](#bfs-approach)
- [DFS Approach](#dfs-approach)
- [Benchmarks](#benchmarks)
- [Afterword: Memory Spike in Iterative DFS](#afterword-memory-spike-in-iterative-dfs)
- [Conclusion](#conclusion)

---

## Generic Solution Using Different Traversal Strategies

The solution to the connected components problem is flexible and allows us to provide either **BFS** or **DFS** as the
traversal strategy. This is achieved using a `ConnectedComponentsFinder<T>` class that accepts any implementation of the
`IGraphTraversal<T>` interface. This approach makes it easy to switch between BFS and DFS while keeping the solution
generic.

### ConnectedComponentsFinder<T>

The `ConnectedComponentsFinder<T>` class accepts a graph and a traversal strategy (either **BFS** or **DFS**) and finds
all connected components.

**File Reference**:

- [ConnectedComponentsFinder.cs](../ConnectedComponentsFinder.cs)

---

## BFS Approach

### Overview

**Breadth-First Search (BFS)** explores the graph level by level, ensuring that all nodes connected at a particular
distance from the starting node are processed before moving deeper into the graph. BFS is particularly well-suited for
finding connected components in graphs where the graph depth is moderate, and memory usage is a concern.

### File References:

- [BreadthFirstSearchIterative.cs](../../../src/GraphAlgorithms/Traversals/Bfs/BreadthFirstSearchIterative.cs)
- [BreadthFirstSearchRecursive.cs](../../../src/GraphAlgorithms/Traversals/Bfs/BreadthFirstSearchRecursive.cs)

---

## DFS Approach

### Overview

**Depth-First Search (DFS)** explores the graph as deep as possible along a branch before backtracking. DFS can be
implemented either recursively or iteratively. The recursive version is simpler but may cause stack overflow for deep
graphs, while the iterative version avoids this issue by explicitly managing the call stack.

### File References:

- [DepthFirstSearchIterative.cs](../../../src/GraphAlgorithms/Traversals/Dfs/DepthFirstSearchIterative.cs)
- [DepthFirstSearchRecursive.cs](../../../src/GraphAlgorithms/Traversals/Dfs/DepthFirstSearchRecursive.cs)

---

## Benchmarks

The performance of **BFS** and **DFS** was measured using `BenchmarkDotNet` for two different graph sizes. Below are the
results comparing **Recursive BFS**, **Iterative BFS**, **Recursive DFS**, and **Iterative DFS**.

### Graph with 1000 vertices

- **Number of vertices**: 1000
- **Edge creation probability**: 0.1
- **Is directed**: false

| Method        |       Mean |  StdDev | Allocated Memory |
|---------------|-----------:|--------:|-----------------:|
| Recursive BFS |   651.1 μs | 1.53 μs |        208.15 KB |
| Iterative BFS |   616.2 μs | 1.06 μs |        208.15 KB |
| Recursive DFS |   800.6 μs | 6.73 μs |         168.7 KB |
| Iterative DFS | 1,085.5 μs | 3.84 μs |        681.17 KB |

### Graph with 10,000 vertices

- **Number of vertices**: 10,000
- **Edge creation probability**: 0.1
- **Is directed**: false

| Method        |     Mean |   StdDev | Allocated Memory |
|---------------|---------:|---------:|-----------------:|
| Recursive BFS | 199.8 ms | 34.29 ms |          1.96 MB |
| Iterative BFS | 264.3 ms | 33.40 ms |          1.96 MB |
| Recursive DFS | 139.3 ms |  4.21 ms |          1.53 MB |
| Iterative DFS | 288.2 ms | 20.10 ms |         65.53 MB |

---

## Afterword: Memory Spike in Iterative DFS

A significant memory spike was observed in the **Iterative DFS** implementation for larger graphs, where it used **65.53
MB** of memory compared to **1.96 MB** for BFS and Recursive DFS.

### Explanation:

1. **Explicit Stack Usage in Iterative DFS**:
    - In **Iterative DFS**, an explicit **stack** is used to simulate recursion. This stack grows as the algorithm
      explores deeper paths in the graph, leading to high memory consumption in large or deep graphs.

2. **Queue vs Stack Behavior**:
    - In **Iterative BFS**, a **queue** is used, and nodes are processed level by level, meaning fewer nodes are stored
      in memory at any given time compared to DFS, which explores deeply before backtracking.

3. **Graph Depth and Structure**:
    - The memory spike occurs in deep graphs where **Iterative DFS** stores many vertices in the stack before
      backtracking.

4. **Memory Allocation in .NET**:
    - **Iterative DFS** may trigger more frequent heap allocations as the stack grows, leading to higher memory
      consumption compared to BFS or Recursive DFS, which manage memory more efficiently.

---

## Conclusion

The **ConnectedComponentsFinder<T>** class provides a flexible solution for finding connected components using either *
*BFS** or **DFS**. The choice between BFS and DFS depends on the structure of the graph and the available memory:

- **BFS** is better suited for graphs where memory usage is a concern and where breadth-first exploration is more
  appropriate.
- **DFS** is useful for exploring deep graphs, but the iterative version may consume significantly more memory,
  especially in larger graphs.

The benchmarks show that **Recursive DFS** is generally faster and more memory-efficient, but **Iterative DFS** incurs
higher memory usage due to stack management. **BFS**, on the other hand, maintains a consistent memory footprint but may
be slower depending on the graph size and structure.