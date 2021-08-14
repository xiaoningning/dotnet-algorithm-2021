public class Solution {
    // BFS
    // T : O(n^ (k+1))
    public int FindCheapestPrice(int n, int[][] flights, int src, int dst, int k) {
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
}
