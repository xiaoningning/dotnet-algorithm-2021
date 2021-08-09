public class Solution {
    // 0: not visited, 1: red, -1: blue
    int[] colors;
    // BFS
    public bool IsBipartite1(int[][] graph) {
        int n = graph.Length;
        colors = new int[n];
        // not all nodes being connected
        for (int v = 0; v < n; v++) {
            if (colors[v] != 0) continue;
            colors[v] = 1;
            var q = new Queue<int>();
            q.Enqueue(v);
            while (q.Any()) {
                int t = q.Dequeue();
                foreach (int e in graph[t]) {
                    if (colors[e] == colors[t]) return false;
                    if (colors[e] == 0)  {
                        q.Enqueue(e);
                        colors[e] = -1 * colors[t];
                    }
                }
            }
        }
        // T: O(V+E)
        return true;
    }
    // DFS (recursion)
    public bool IsBipartite2(int[][] graph) {
        int n = graph.Length;
        colors = new int[n];
        for (int v = 0; v < n; v++) {
            if (colors[v] == 0 && !validColors(graph, v, 1)) return false;
        }
        // T: O(V+E)
        return true;
    }
    // check if v is the same as expected color
    bool validColors(int[][] graph, int v, int color) {
        if (colors[v] != 0) return colors[v] == color;
        colors[v] = color;
        foreach (int e in graph[v]) {
            if (!validColors(graph, e, -1 * color)) return false;
        }
        return true;
    }
    // Union find
    public bool IsBipartite(int[][] graph) {
        int n = graph.Length;
        int[] roots = new int[n];
        for (int i = 0; i < n; i++) roots[i] = i;
        for (int i = 0; i < n; i++) {
            if (graph[i].Length == 0) continue;
            foreach (int e in graph[i]) {
                // Bipartite => connected nodes should have different roots
                if (UnionFind(roots, i) == UnionFind(roots, e)) return false;
                // Update roots to its same group, but different from roots[i]
                roots[e] = UnionFind(roots, graph[i][0]);
            }
        }
        // T: O(V+logE)
        return true;
    }
    int UnionFind(int[] roots, int i) {
        return roots[i] == i ? i : UnionFind(roots, roots[i]);
    }
}

