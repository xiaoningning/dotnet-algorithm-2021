public class Solution {
    public bool PossibleBipartition(int n, int[][] dislikes) {
        var g = new Dictionary<int, List<int>>();
        for (int i = 1; i <= n; i++) g[i] = new List<int>();
        foreach (var d in dislikes) {
            g[d[0]].Add(d[1]);
            g[d[1]].Add(d[0]);
        }
        // 0: unknown, 1: red, -1: blue
        int[] colors = new int[n+1];
        for (int i = 1; i <= n; i++) {
            if (colors[i] == 0 && !DFS(g, colors, i, 1)) return false;
        } 
        return true;
    }
    bool DFS(Dictionary<int, List<int>> g, int[] colors, int i, int clr) {
        colors[i] = clr;
        foreach (int c in g[i]) { 
            if (colors[c] == 0 && !DFS(g, colors, c, -1 * clr)) return false;
            if (colors[c] == colors[i]) return false;
        }
        return true;
    }
}
