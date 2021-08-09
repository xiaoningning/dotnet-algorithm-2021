public class Solution {
    // directed graph, check in/out degrees of each node
    public int FindJudge1(int n, int[][] trust) {
        int[] ins= new int[n+1], outs = new int[n+1];
        foreach (var t in trust) {
            ins[t[1]]++; outs[t[0]]++;
        }
        for (int i = 1; i <= n; i++) {
            if (outs[i] == 0 && ins[i] == n - 1) return i;
        }
        return -1;
    }
    // HashSet + Map
    public int FindJudge(int n, int[][] trust) {
        var st = new HashSet<int>();
        var m = new Dictionary<int, List<int>>();
        // need to handle empty trust case
        for (int i = 1; i <= n; i++) m[i] = new List<int>();
        foreach (var t in trust) {
            st.Add(t[0]);
            // t[1] is judge candidate
            m[t[1]].Add(t[0]);
        }
        for (int i = 1; i <= n; i++) {
            if (st.Contains(i)) continue;
            if (m.ContainsKey(i) && m[i].Count == n - 1) return i;
        }
        return -1;
    }
}
