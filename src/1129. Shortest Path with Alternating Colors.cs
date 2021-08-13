public class Solution {
    // BFS
    // T: O(|V| + |E|)
    public int[] ShortestAlternatingPaths(int n, int[][] red_edges, int[][] blue_edges) {
        int[] ans = new int[n];
        // -1 if not reachable
        Array.Fill(ans, -1);
        var r = new Dictionary<int, List<int>>();
        var b = new Dictionary<int, List<int>>();
        for (int i = 0; i< n; i++) {
            r[i] = new List<int>();
            b[i] = new List<int>();
        }
        foreach (int[] e in red_edges) r[e[0]].Add(e[1]);
        foreach (int[] e in blue_edges) b[e[0]].Add(e[1]);
        // 1: red, -1: blue
        var q = new Queue<(int, int)>();
        q.Enqueue((0, 1)); q.Enqueue((0, -1));
        int steps = 0;
        var visited = new HashSet<(int, int)>();
        // update ans along the path
        // no need to go through each dest node
        while (q.Any()) {
            int size = q.Count;
            while (--size >= 0) {
                var t = q.Dequeue();
                visited.Add(t);
                int node = t.Item1, clr = t.Item2;
                // keep previous steps of each dst node
                ans[node] = ans[node] >= 0 ? Math.Min(ans[node], steps) : steps;
                var edges = clr == 1 ? r[node] : b[node];
                foreach (int nx in edges) {
                    if (visited.Contains((nx, -1 * clr))) continue;
                    q.Enqueue((nx, -1 * clr));
                }
            }
            steps++;
        }
        return ans;
    }
}
