public class Solution {
    // DP
    public int ShortestPathLength(int[][] graph) {
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
