public class Solution {
    // BFS
    // T: O(n*2^n) S: O(n*2^n)
    public int ShortestPathLength2(int[][] graph) {
        int n = graph.Length;
        int MAX = n * n;
        int ans = 0;
        // n bit => visit state of each node
        // 1: visited, 0: not visited
        // 1 << n == 2^n
        int cnt = (int) Math.Pow(2,n);
        // (state, i)
        var visited = new HashSet<(int, int)>();
        var q = new Queue<(int, int)>();
        for (int i = 0; i < n; i++) q.Enqueue((1 << i, i));
        // start each node itself BFS to visist all nodes
        while (q.Any()) {
            int size = q.Count;
            // BFS to visit other nodes
            // no need to track min since BFS
            while (--size >= 0) {
                var src = q.Dequeue();
                // first to visit all nodes is the shortest
                // (1 << n) - 1: all nodes being visited
                if (src.Item1 == (1 << n) - 1) return ans;
                foreach (int dst in graph[src.Item2]) {
                    int nx = src.Item1 | 1<< dst;
                    if (visited.Contains((nx, dst))) continue;
                    q.Enqueue((nx, dst));
                    visited.Add((nx, dst));
                }
            }
            ans++; // cur level done, step + 1
        }
        return ans;
    }
    
    // Floyd-Warshall + DP
    // Floyd-Warshall : dist[i, j] > dist[i, x] + dist[x, j]
    public int ShortestPathLength(int[][] graph) { 
        int n = graph.Length;
        int MAX = n * n;
        int[,] dist = new int[n,n];
        for (int i = 0; i < n; i++) 
            for (int j = 0; j < n; j++) dist[i,j] = MAX;
        for (int i = 0; i < n; i++) 
            foreach (int j in graph[i] ) 
                dist[i, j] = 1;
        // Floyd: calculate all distiance
        for (int k = 0; k < n; k++) 
            for (int i = 0; i < n; i++) 
                for (int j = 0; j < n; j++) 
                    dist[i,j] = Math.Min(dist[i,j], dist[i,k] + dist[k,j]);
        // n bit => visit state of each node
        // 1 << n == 2^n
        int cnt = (int) Math.Pow(2,n);
        // DP to find min value
        int[,] dp = new int[cnt,n];
        for (int i = 0; i < cnt; i++) 
            for (int j = 0; j < n; j++) dp[i,j] = MAX;
        // state : start from any node to visit all nodes
        // itself i to i dp = 0
        for (int i = 0; i < n; i++) dp[1 << i, i] = 0;
        for (int cur = 0; cur < (1 << n); cur++) {
            for (int i = 0; i < n; i++) {
                for (int j = 0; j < n; j++) {
                    int next = cur | 1 << j;
                    // cur state must has i and go to next next with j
                    if ((cur & 1 << i) > 0 && (cur & 1 << j) == 0) {
                        dp[next, j] = Math.Min(dp[next, j], dp[cur, i] + dist[i,j]);
                    }
                }
            }   
        }
        int ans = MAX;
        for (int j = 0; j < n; j++) {
            // (1 << n) - 1: the state of all nodes being visited
            ans = Math.Min(ans, dp[(1 << n) - 1, j]);
        }
        return ans;
    }
    
    // DP
    public int ShortestPathLength1(int[][] graph) {
        int n = graph.Length;
        int MAX = n * n;
        // n bit => visit state of each node
        // 1 << n == 2^n
        int cnt = (int) Math.Pow(2,n);
        // dist as DP to find min value
        int[,] dist = new int[cnt,n];
        for (int i = 0; i < cnt; i++) 
            for (int j = 0; j < n; j++) dist[i,j] = MAX;
        // itself i to i dist = 0
        for (int i = 0; i < n; i++) dist[1 << i, i] = 0;
        // try to start cur point from each node to visit all nodes
        // update dist as DP to find min value
        for (int cur = 0; cur < (1 << n); cur++) {
            bool repeat = true;
            // if cur state == next state, it needs to re-update all dist
            while (repeat) {
                repeat = false;
                for (int i = 0; i < n; ++i) {
                    int d = dist[cur,i];
                    foreach (int next in graph[i]) {
                        int path = cur | (1 << next);
                        if (d + 1 < dist[path,next]) {
                            dist[path,next] = d + 1;
                            if (path == cur) repeat = true;
                        }
                    }
                }
            }
        }
        int ans = MAX;
        for (int j = 0; j < n; j++) {
            // (1 << n) - 1: the state of all nodes being visited
            ans = Math.Min(ans, dist[(1 << n) - 1, j]);
        }
        return ans;
    }
}
