public class Solution {
    // DP
    // T: O(m*n) S: O(m*n)
    public int MinimumMoves(int[][] grid) {
        int m = grid.Length, n = grid[0].Length;
        // (i,j) tail pos, 0: h, 1: v
        int[,,] dp = new int[m+1,n+1,2];
        // min() + 1 => init as Max / 2 to avoid overlfow
        for (int i = 0; i <= m; i++) for (int j = 0; j <= n; j++) dp[i,j,0] = dp [i,j,1] = Int32.MaxValue / 2;
        // init pos: (0,0), (0,1) step should be 0
        // => down / right should be inited as -1
        dp[0,1,0] = dp[1,0,0] = -1;
        for (int i = 1; i <= m; i++) 
            for (int j = 1; j <= n; j++) {
                bool v = false, h = false;
                // horizontal
                if (grid[i-1][j-1] == 0 && j < n && grid[i-1][j] == 0) {
                    h = true;
                    dp[i,j,0] = Math.Min(dp[i-1,j,0], dp[i,j-1,0]) + 1;
                }
                // verical
                if (grid[i-1][j-1] == 0 && i < m && grid[i][j-1] == 0) {
                    v = true;
                    dp[i,j,1] = Math.Min(dp[i-1,j,1], dp[i,j-1,1]) + 1;
                }
                // horizontal vs clockwise
                if (h && i < n && grid[i][j] == 0) dp[i,j,0] = Math.Min(dp[i,j,0], dp[i,j,1] + 1);
                // vertical vs counter clockwise
                if (v && j < n && grid[i][j] == 0) dp[i,j,1] = Math.Min(dp[i,j,1], dp[i,j,0] + 1);
        }
        // plus + 1 on each round => >= MaxValue/2
        return dp[m, n-1,0] >= Int32.MaxValue / 2 ? -1 : dp[m, n-1, 0];
    }
}
