public class Solution {
    HashSet<string> visited = new HashSet<string>();
    // BFS, shortest path
    public int MinMutation1(string start, string end, string[] bank) {
        if (!bank.Contains(end)) return -1;
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
    // DFS
    public int MinMutation(string start, string end, string[] bank) {
        if (!bank.Contains(end)) return -1;
        return DFS(start, end, bank);
    }
    int DFS(string start, string end, string[] bank) {
        if (start == end) return 0;
        visited.Add(start);
        int ans = bank.Length + 1;
        foreach (var nx in bank) {
            if (!visited.Contains(nx) && IsMutation(start, nx)) {
                int cnt = DFS(nx, end, bank);
                // DFS needs to check min
                if (cnt != -1) ans = Math.Min(ans, cnt);
            }
        }
        // DFS must remove prev one
        visited.Remove(start);
        // add 1 for start itself
        return ans == bank.Length + 1 ? -1 : ans + 1;
    }
}
