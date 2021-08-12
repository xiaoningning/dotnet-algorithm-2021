public class Solution {
    // DFS
    // state: 0: unknown, 1: safe, 2: unsafe
    int[] state;
    public IList<int> EventualSafeNodes1(int[][] graph) {
        int n = graph.Length;
        state = new int[n];
        var ans = new List<int>();
        for (int i = 0; i < n; i++) {
            if (DFS(graph, i) == 1) ans.Add(i);
        }
        // T: O(V+E)
        return ans;
    }
    int DFS(int[][] graph, int i) {
        if (state[i] != 0) return state[i];
        state[i] = 2; // we can add visiting state as well
        foreach (int j in graph[i])  {
            if (DFS(graph, j) == 2) return state[i];
        }
        return state[i] = 1;
    }
    // BFS in/out degree of graph node
    public IList<int> EventualSafeNodes(int[][] graph) {
        int n = graph.Length;
        int[] ins = new int[n];
        var q = new Queue<int>();
        var ans = new List<int>();
        var rev = new Dictionary<int, List<int>>(); 
        for (int i = 0; i < n; i++) {
            ins[i] = graph[i].Length;
            rev[i] = new List<int>();
            if (ins[i] == 0) q.Enqueue(i);
        }
        for (int i = 0; i < n; i++) {
            foreach (int t in graph[i]) rev[t].Add(i);
        }
        while (q.Any()) {
            var t = q.Dequeue();
            ans.Add(t);
            foreach (var x in rev[t]) {
                if (--ins[x] == 0) q.Enqueue(x);
            }
        }
        // T: O(V+E)
        return ans.OrderBy(x => x).ToList();
    }
}
