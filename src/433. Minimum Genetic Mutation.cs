public class Solution {
    // BFS, shortest path
    public int MinMutation(string start, string end, string[] bank) {
        if (!bank.Contains(end)) return -1;
        var visited = new HashSet<string>();
        visited.Add(start);
        var q = new Queue<string>();
        q.Enqueue(start);
        int ans = 0;
        while (q.Any()) {
            int size = q.Count;
            while (--size >= 0) {
                var t = q.Dequeue();
                if (t == end) return ans;
                foreach (string nx in bank) {
                    if (!visited.Contains(nx) && IsMutation(t, nx)) {
                        q.Enqueue(nx);
                        visited.Add(nx);
                    }
                }
            }
            ans++;
        }
        return -1;
    }
    bool IsMutation(string x, string y) {
        int cnt = 0;
        for (int i = 0; i < x.Length; i++) {
            if (x[i] != y[i]) cnt++;
        }
        return cnt == 1;
    }
}
