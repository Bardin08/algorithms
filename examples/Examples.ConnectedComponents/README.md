# Finding Connected Components

## Problem Definition

In an **undirected graph**, a **connected component** is a maximal set of vertices such that any two vertices are
connected to each other by a path, and no vertex in the set is connected to any vertex outside of it.

The goal is to identify all such connected components in a given graph.

### Formal Definition:

Given an undirected graph \( G = (V, E) \), where \( V \) is the set of vertices and \( E \) is the set of edges, a *
*connected component** is a subgraph \( C = (V_C, E_C) \) of \( G \) such that:

1. \( V_C \subseteq V \)
2. \( E_C \subseteq E \)
3. For every pair of vertices \( u, v \in V_C \), there exists a path from \( u \) to \( v \).

The problem is to identify all such connected components in the graph.

### Example

Consider the following undirected graph:

```mermaid
graph TD;
    A -- B;
    A -- C;
    D -- E;
    E -- F;
    G;
```

The graph has 3 connected components:

1. \( \{A, B, C\} \)
2. \( \{D, E, F\} \)
3. \( \{G\} \) (a single isolated node)

---

## Real-World Applications

**Finding connected components** has various practical applications, including:

1. **Social Networks**: Finding clusters of people who are directly or indirectly connected (e.g., groups of friends in
   a social network).
2. **Image Processing**: Identifying connected regions of pixels with the same color or intensity in an image.
3. **Network Design**: Finding isolated parts of a network (e.g., disconnected routers or computers in a distributed
   system).
4. **Biology**: Identifying isolated sub-networks of proteins or genes in biological networks.

---

## Algorithmic Approaches

There are two common approaches to finding connected components in a graph:

1. **Depth-First Search (DFS)**: Using DFS, you can explore all reachable vertices from any unvisited vertex and mark
   them as part of the same component.
2. **Breadth-First Search (BFS)**: BFS can also be used to explore the graph level by level and identify connected
   components.

Both algorithms work by traversing the graph, marking visited vertices, and starting a new traversal whenever an
unvisited vertex is encountered.

### Time Complexity:

- **DFS/BFS**: \( O(V + E) \), where \( V \) is the number of vertices and \( E \) is the number of edges in the graph.

---

## Visual Example

Let’s consider an example graph and show how DFS identifies the connected components:

```mermaid
graph TD;
    A -- B;
    A -- C;
    D -- E;
    E -- F;
    G;
```

- Start with vertex \( A \):
    - DFS marks \( A, B, C \) as visited (first connected component).
- Move to vertex \( D \):
    - DFS marks \( D, E, F \) as visited (second connected component).
- Move to vertex \( G \):
    - DFS marks \( G \) as visited (third connected component).

At the end of the DFS, we have the following connected components:

- \( \{A, B, C\} \)
- \( \{D, E, F\} \)
- \( \{G\} \)

---

## Tasks

1. **Implement DFS to find connected components**.
2. **Implement BFS to find connected components**.
3. **Compare performance and behavior between DFS and BFS** in identifying connected components.

---

## Conclusion

Finding connected components is a crucial graph problem with numerous real-world applications. Whether you're analyzing
social networks, processing images, or designing robust networks, understanding how to find and work with connected
components is essential. Both DFS and BFS offer efficient ways to solve this problem, and choosing between them depends
on the specific context of the application.