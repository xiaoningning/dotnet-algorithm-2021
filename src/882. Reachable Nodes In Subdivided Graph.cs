public class Solution {
    // a single source shortest path problem
    // new nodes of subdivided edge is weight
    // BFS
    public int ReachableNodes(int[][] edges, int maxMoves, int n) {
        var g = new Dictionary<int, List<(int, int)>>();
        for (int i = 0; i < n; i++) g[i] = new List<(int, int)>();
        foreach (var e in edges) {
            g[e[0]].Add((e[1], e[2]));
            g[e[1]].Add((e[0], e[2]));
        }
        // track the max # of new nodes
        var nodes = new Dictionary<int, int>();
        var q = new Queue<(int,int)>();
        // (node, the leftover # of moves)
        q.Enqueue((0, maxMoves));
        while (q.Any()) {
            var t = q.Dequeue();
            int u = t.Item1, w = t.Item2;
            if (nodes.ContainsKey(u) && w < nodes[u]) continue; 
            nodes[u] = w;
            foreach (var e in g[u]) {
                int nx = e.Item1;
                int nw = w - e.Item2 - 1;
                // nw < 0: not reachable
                // nw < moves[nx]: other path is better => pruning
                if (nw < 0 || (nodes.ContainsKey(nx) && nw < nodes[nx])) continue;
                q.Enqueue((nx, nw));
            }
        }
        // original nodes
        int ans = nodes.Keys.Count;
        // add reachable new nodes
        foreach (var e in edges) {
            // it could reach only some of new nodes
            int wu = nodes.ContainsKey(e[0]) ? nodes[e[0]] : 0;
            int wv = nodes.ContainsKey(e[1]) ? nodes[e[1]] : 0;
            ans += Math.Min(e[2], wu + wv);
        }
        return ans;
    }
}
