public class Solution {
    // Floyd-Warshall
    // T: O(n^3)
    public int FindTheCity1(int n, int[][] edges, int distanceThreshold) {
        int[,] dist = new int[n,n];
        for (int i = 0; i < n; i++)
            for (int j = 0; j < n; j++)
                dist[i,j] = Int32.MaxValue / 2;
            
        for (int i = 0; i < n; i++) dist[i,i] = 0;
        foreach (var e in edges) dist[e[0],e[1]] = dist[e[1],e[0]] = e[2];
        
        // Floyd-Warshall dist[i, j] = dist[i, k] + dist[k, j]
        for (int k = 0; k < n; k++) {
            for (int i = 0; i < n; i++) {
                for (int j = 0; j < n; j++) {
                    dist[i,j] = Math.Min(dist[i,j], dist[i,k] + dist[k,j]);
                }
            }
        }
        int minCnt = Int32.MaxValue;
        int ans = -1;
        for (int i = 0; i < n; i++) {
            int cnt = 0;
            for (int j = 0; j < n; j++) {
                if (i != j && dist[i,j] <= distanceThreshold) cnt++;
            }
            if (cnt <= minCnt)  {
                ans = i;
                minCnt = cnt;
            }
        }
        return ans;
    }
    // Dijkstra shortest path: if dist[u] + w[u,v] < dist[v] => dist[v] update
    // BFS
    // T: O(V*ELogV)
    public int FindTheCity(int n, int[][] edges, int distanceThreshold) {
        var g = new Dictionary<int, List<(int,int)>>();
        for (int i = 0; i < n; i++) g[i] = new List<(int, int)>();
        foreach (int[] e in edges) {
            g[e[0]].Add((e[1],e[2]));
            g[e[1]].Add((e[0],e[2]));
        }
        int minCnt = Int32.MaxValue;
        int ans = 0;
        for (int i = 0; i < n; i++) {
            int cnt = DijkstraBFS(g, i, distanceThreshold).Count;
            if (cnt <= minCnt)  {
                ans = i;
                minCnt = cnt;
            }
        }
        return ans;
    }
    List<int> DijkstraBFS(Dictionary<int, List<(int,int)>> g, int s, int T) {
        int n = g.Count;
        int[] dist = new int[n];
        Array.Fill(dist, Int32.MaxValue / 2);
        dist[s] = 0;
        var ans = new List<int>();
        var q = new Queue<int>();
        q.Enqueue(s);
        while (q.Any()) {
            var t = q.Dequeue();
            // prunning
            if (dist[t] > T) continue;
            foreach (var x in g[t]) {
                int nx = x.Item1, w = x.Item2;
                if (dist[t] + w > dist[nx]) continue;
                dist[nx] = dist[t] + w;
                q.Enqueue(nx);
            }
        }
        for (int i = 0; i < n; i++) if (dist[i] <= T) ans.Add(i);
        return ans;
    }
}
