public class Solution {
    // Union Find
    public int[] FindRedundantConnection1(int[][] edges) {
        int n = edges.Length;
        int[] ans = new int[2];
        int[] roots = new int[n + 1];
        for (int i = 1; i <= n; i++) roots[i] = i;
        foreach (int[] e in edges) {
            int px = UnionFind(roots, e[0]);
            int py = UnionFind(roots, e[1]);
            // remove the last redundant edge
            if (px == py) ans = e;
            else roots[py] = px;  
        }
        // T: O(nlogn)
        return ans;
    }
    int UnionFind(int[] roots, int x) {
        return roots[x] == x ? x : roots[x] = UnionFind(roots, roots[x]);
    }
    // DFS
    Dictionary<int, List<int>> g = new Dictionary<int, List<int>>();
    public int[] FindRedundantConnection(int[][] edges) {
        int n = edges.Length;
        for (int i = 1; i <= n; i++) g[i] = new List<int>();
        int[] ans = new int[2];
        foreach (int[] e in edges) {
            // each graph needs one visited to check
            HashSet<int> visited = new HashSet<int>();
            if (HasCycle(e[0], e[1], edges, visited)) ans = e;
            // undirected graph
            g[e[0]].Add(e[1]);
            g[e[1]].Add(e[0]);
        }
        // T: O(n^2)
        return ans;
    }
    bool HasCycle(int start, int end, int[][] edges, HashSet<int> visited) {
        if (start == end) return true;
        visited.Add(start);
        foreach (int nx in g[start]) {
            if (visited.Contains(nx)) continue;
            if (HasCycle(nx, end, edges, visited)) return true;
        }
        return false;
    }
}
