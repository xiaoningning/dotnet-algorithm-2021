public class Solution {
    // coloring graph
    // mask bit to set selected color
    public int[] GardenNoAdj1(int n, int[][] paths) {
        int[] ans = new int[n];
        var g = new Dictionary<int, List<int>>();
        for (int i = 0; i < n; i++) g[i]  = new List<int>();
        foreach (var p in paths) {
            g[p[0] - 1].Add(p[1] - 1);
            g[p[1] - 1].Add(p[0] - 1);
        }
        // path only in between two nodes
        // no need to check DFS
        // mask to set selected flower type
        for (int i = 0; i < n; i++) {
            int mask = 0;
            // mask all selected color by neighbors
            foreach (int j in g[i]) mask |= 1 << ans[j];
            for (int c = 1; c <= 4 && ans[i] == 0; c++) {
                // fomd the bit is 0 (not used yet)
                if ((mask & 1 << c) == 0) ans[i] = c;
            } 
        }
        return ans;
    }
    // without mask
    public int[] GardenNoAdj(int n, int[][] paths) {
        int[] ans = new int[n];
        var g = new Dictionary<int, List<int>>();
        for (int i = 0; i < n; i++) g[i]  = new List<int>();
        foreach (var p in paths) {
            g[p[0] - 1].Add(p[1] - 1);
            g[p[1] - 1].Add(p[0] - 1);
        }
        // path only in between two nodes
        // no need to check DFS
        for (int i = 0; i < n; i++) {
            if (ans[i] != 0) continue;
            var st = new HashSet<int>();
            foreach (int j in g[i]) if (ans[j] != 0) st.Add(ans[j]);
            for (int c = 1; c <= 4; c++) if (!st.Contains(c)) ans[i] = c;
        }
        return ans;
    }
}
