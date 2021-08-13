public class Solution {
    // BFS
    // search over bus stop TLE
    // search over bus route 
    // T: O(m*n)
    public int NumBusesToDestination(int[][] routes, int source, int target) {
        if (source == target) return 0;
        int n = routes.Length;
        int ans = 0;
        // visited route
        int[] visited = new int[n];
        var m = new Dictionary<int, List<int>>();
        for (int i = 0; i < routes.Length; i++) {
            foreach (int bus in routes[i]) {
                if (!m.ContainsKey(bus)) m[bus] = new List<int>();
                m[bus].Add(i);
            }
        }
            
        var q = new Queue<int>();
        q.Enqueue(source);
        while (q.Any()) {
            int size = q.Count;
            ans++;
            while (--size >= 0) {
                var t = q.Dequeue();
                foreach (int r in m[t]) {
                    if (visited[r] == 1) continue;
                    visited[r] = 1;
                    foreach(int nx in routes[r]) {
                        if (nx == target) return ans;
                        q.Enqueue(nx);
                    }
                }
            }
        }
        return -1;
    }
}
