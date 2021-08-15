public class Solution {
    // BFS
    // T : O(n^ (k+1))
    public int FindCheapestPrice1(int n, int[][] flights, int src, int dst, int k) {
        var g = new Dictionary<int, List<(int, int)>>();
        for (int i = 0; i < n; i++) g[i] = new List<(int, int)>();
        foreach (var f in flights) g[f[0]].Add((f[1], f[2]));
        int ans = Int32.MaxValue;
        var q = new Queue<(int, int)>();
        q.Enqueue((src,0));
        while (q.Any()) {
            int size = q.Count;
            while (--size >= 0) {
                var t = q.Dequeue();
                if (t.Item1 == dst) ans = Math.Min(ans, t.Item2);
                foreach (var nx in g[t.Item1]) {
                    // IMPORTANT!!! prunning to save time
                    if ((t.Item2 + nx.Item2) > ans) continue;
                    q.Enqueue((nx.Item1, t.Item2 + nx.Item2));
                }
            }
            // at most k
            if (k-- < 0) break;
        }
        return ans == Int32.MaxValue ? -1 : ans;
    }
    // Bellman-Ford v2
    // T : O(k * n) S: O(n)
    public int FindCheapestPrice(int n, int[][] flights, int src, int dst, int k) {
        // cost from src to i
        int[] cost = new int[n];
        int MAX = 10000 * n;
        Array.Fill(cost, MAX);
        // init src cost as 0
        cost[src] = 0;
        // at most k stop, from src to i and keep min cost
        for (int i = 0; i <= k; i++) {
            // int[] tmp = new int[n];
            // Array.Copy(cost, tmp, n);
            // previous round i - 1
            int[] tmp = (int[]) cost.Clone(); // clone back to just an object
            foreach (var f in flights) tmp[f[1]] = Math.Min(tmp[f[1]], cost[f[0]] +f[2]);
            cost = tmp;
        }
        return cost[dst] == MAX ? -1 : cost[dst];
    }
    // Bellman-Ford v1
    // similar to 743. network delay time
    // T : O(k * n) S: O(n)
    public int FindCheapestPrice2(int n, int[][] flights, int src, int dst, int k) {
        // dp cost from src to i at UP to k stops
        // at most k stops, src + dst + k
        int[,] dp = new int[k+2,n];
        int MAX = 10000 * n;
        for (int i = 0; i <= k+1; i++) for (int j = 0; j < n; j++) dp[i,j] = MAX;
        dp[0,src] = 0;
        for (int i = 1; i <= k + 1; ++i) {
            dp[i,src] = 0; // src is cost 0
            foreach (int[] f in flights) dp[i,f[1]] = Math.Min(dp[i, f[1]], dp[i - 1, f[0]] + f[2]);
        }
        return dp[k+1, dst] >= MAX ? -1 : dp[k+1, dst];
    }
}
