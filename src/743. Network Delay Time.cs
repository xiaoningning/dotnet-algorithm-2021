public class Solution {
    // single source shorted path graph problem
    // Bellman-Ford v2
    public int NetworkDelayTime2(int[][] times, int n, int k) {
        int MAX = 100001;
        // dist from k to i
        int[] dist = new int[n+1];
        // e: connected graph
        var e = new Dictionary<int, List<(int, int)>>();
        for (int i = 1; i <= n; i++) { 
            dist[i] = MAX;
            e[i] = new List<(int, int)>();
        }
        dist[k] = 0;
        // iterate all edges
        foreach (var t in times) e[t[0]].Add((t[1],t[2]));
        var q = new Queue<int>();
        q.Enqueue(k);
        // iterate V connected with k
        while (q.Any()) {
            int t = q.Dequeue();
            var visited = new HashSet<int>();
            foreach (var nx in e[t]) {
                int v = nx.Item1, w = nx.Item2;
                // t could be not reachable from k
                if (dist[t] != MAX && dist[v] > w + dist[t]) {
                    // update dist[v] when found larger 
                    dist[v] = w + dist[t];
                    // no need to add into q again.
                    if (visited.Contains(v)) continue;
                    visited.Add(v);
                    q.Enqueue(v);
                }
            }
        }
        int ans = dist.Max();
        // T: O(V+E)
        return ans == MAX ? -1 : ans;
    }
    
    // Bellman-Ford v1
    public int NetworkDelayTime3(int[][] times, int n, int k) { 
        // not use MaxValue to avoid overflow
        int MAX = 100001;
        // dist from k to i
        int[] dist = new int[n+1];
        for (int i = 1; i <= n; i++) dist[i] = MAX;
        dist[k] = 0;
        // iterate all V to update dist
        // dist[] is DP to keep min value
        for (int i = 1; i <= n; i++) {
            foreach (var t in times) {
                // min dist update
                // if MAX => t[0] is not reachable from k in this iteration
                if (dist[t[0]] != MAX) dist[t[1]] = Math.Min(dist[t[1]], t[2] + dist[t[0]]);
            }
        }
        int ans = dist.Max();
        // T: O(V*E)
        return ans == MAX ? -1 : ans;
    }
    
    // Floyd-Warshall v1 : dist[i, j] > dist[i, x] + dist[x, j]
    public int NetworkDelayTime1(int[][] times, int n, int k) {
        int[,] dist = new int[n+1, n+1];
        for (int i = 1; i <= n; i++) {
            for (int j = 1; j <= n; j++) dist[i, j] = -1;
        }
        for (int i = 1; i <= n; i++) dist[i,i] = 0;
        foreach (var t in times) dist[t[0], t[1]] = t[2];
        // calculate all dist
        for (int x = 1; x <= n; x++) {
            for (int i = 1; i <= n; i++) {
                for (int j = 1; j <= n; j++) {
                    if (dist[i,x] >= 0 && dist[x,j] >= 0) {
                        if (dist[i,j] < 0 || dist[i, j] > dist[i, x] + dist[x, j]) {
                            dist[i, j] = dist[i, x] + dist[x, j];
                        }
                    }
                }
            }
        }
        int ans = Int32.MinValue;
        for (int i = 1; i <= n; i++) {
            if (dist[k, i] < 0) return -1;
            ans = Math.Max(ans, dist[k, i]);
        }
        // T: O(V^3)
        return ans;
    }
    // Floyd-Warshall v2 : dist[i, j] > dist[i, x] + dist[x, j]
    public int NetworkDelayTime(int[][] times, int n, int k) {
        // not use MaxValue to avoid overflow
        int MAX = 100001;
        int[,] dist = new int[n+1, n+1];
        for (int i = 1; i <= n; i++) {
            for (int j = 1; j <= n; j++) dist[i, j] = MAX;
        }
        foreach (var t in times) dist[t[0], t[1]] = t[2];
        for (int i = 1; i <= n; i++) dist[i,i] = 0;
        // calculate all dist
        for (int x = 1; x <= n; x++) {
            for (int i = 1; i <= n; i++) {
                for (int j = 1; j <= n; j++) {
                    // if dist[x,j] == MAX, it means not reachable of this round
                    dist[i, j] = Math.Min(dist[i, j], dist[i, x] + dist[x, j]);
                }
            }
        }
        int ans = -1;
        for (int i = 1; i <= n; i++) {
            if (dist[k, i] >= MAX) return -1;
            ans = Math.Max(ans, dist[k, i]);
        }
        // T: O(V^3)
        return ans;
    }
}
