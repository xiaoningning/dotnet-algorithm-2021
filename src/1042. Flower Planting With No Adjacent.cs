public class Solution {
    // coloring graph
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
        // mask to set selected flower type
        for (int i = 0; i < n; i++) {
            int mask = 0;
            // mask all selected color by neighbors
            foreach (int j in g[i]) mask |= 1 << ans[j];
            for (int c = 1; c <= 4 && ans[i] == 0; c++) {
                // if mask == 0, that bit is not used yet
                if ((mask & 1 << c) == 0) ans[i] = c;
            } 
        }
        return ans;
    }
}
