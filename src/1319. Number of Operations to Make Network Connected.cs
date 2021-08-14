public class Solution {
    // Union find => T: O(V+E)
    public int MakeConnected1(int n, int[][] connections) {
        if (connections.Length < n - 1) return -1;
        int[] roots = new int[n];
        for (int i = 0; i < n; i++) roots[i] = i;
        // Union
        foreach (int[] c in connections) {
            int pu = UnionFind(roots, c[0]);
            int pv = UnionFind(roots, c[1]);
            if (pu != pv) roots[pv] = pu;
        }
        var st = new HashSet<int>();
        for (int i = 0; i < n; i++) st.Add(UnionFind(roots, i));
        return st.Count - 1;
    }
    int UnionFind(int[] roots, int x) {
        return roots[x] == x ? x : roots[x] = UnionFind(roots, roots[x]);
    }
    // DFS => T: O(V+E)
    public int MakeConnected(int n, int[][] connections) {
        if (connections.Length < n - 1) return -1;
        var g = new Dictionary<int, List<int>>();
        for (int i = 0; i < n; i++) g[i] = new List<int>();
        foreach (var c in connections) {
            g[c[0]].Add(c[1]);
            g[c[1]].Add(c[0]);
        }
        int[] seen = new int[n];
        int cnt = 0;
        for (int i = 0; i < n; i++) {
            if (seen[i]++ == 0) {
                DFS(g, i, seen);
                cnt++;
            }
        }
        return cnt - 1;
    }
    void DFS(Dictionary<int, List<int>> g, int x, int[] seen) {
        foreach (int nx in g[x]) if (seen[nx]++ == 0) DFS(g, nx, seen);
    }
}
