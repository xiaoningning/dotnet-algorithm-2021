public class Solution {
    // Tarjan Algorithm: stong connected components (SCC) graph
    Dictionary<int, List<int>> g = new Dictionary<int, List<int>>();
    List<IList<int>> ans = new List<IList<int>>();
    int[] low; // the lowest dfn of subtree of node being searched
    int[] dfn; // the sequence (timestamp) order of node being searched
    int timer = 0;
    int MAX;
    public IList<IList<int>> CriticalConnections(int n, IList<IList<int>> connections) {
        low = new int[n]; dfn = new int[n]; MAX = n;
        for (int i = 0; i < n; i++) g[i] = new List<int>();
        for (int i = 0; i < n; i++) low[i] = MAX;
        foreach (var c in connections)  {
            g[c[0]].Add(c[1]);
            g[c[1]].Add(c[0]);
        }
        DFS(0, 0);
        // T: O(V+E)
        return ans;
    }
    void DFS(int u, int parent) {
        low[u] = ++timer;
        dfn[u] = low[u];
        foreach (int v in g[u]) {
            if (v == parent) continue;
            if (low[v] == MAX) {
                DFS(v, u);
                low[u] = Math.Min(low[u], low[v]);
                if (low[v] > dfn[u]) ans.Add(new List<int>(){u, v});
            }
            // if v is searched before u and not a parent of u,
            // low[u] should be min (low[u], dfn[v])
            else low[u] = Math.Min(low[u], dfn[v]);
        }
    }
}
