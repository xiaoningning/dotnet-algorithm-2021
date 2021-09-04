public class Solution {
    // DP v1
    // T: O(m*n) S: O(m*n)
    public int MinPathSum1(int[][] grid) {
        int m = grid.Length, n = grid[0].Length;
        int[,] dp = new int[m+1,n+1];
        for (int i = 0; i <= m; i++) for (int j = 0; j <= n; j++) dp[i,j] = Int32.MaxValue;
        dp[1,1] = grid[0][0];
        for (int i = 1; i <= m; i++)
            for (int j = 1; j <= n; j++) 
                dp[i,j] = grid[i-1][j-1] + (i == 1 && j == 1 ? 0 : Math.Min(dp[i-1,j], dp[i, j-1]));
        return dp[m,n];
    }
    // DP v2
    // T: O(m*n) S: O(1)
    public int MinPathSum2(int[][] grid) {
        int m = grid.Length, n = grid[0].Length;
        for (int i = 0; i < m; i++)
            for (int j = 0; j < n; j++) {
                if (i == 0 && j == 0) continue;
                if (i == 0) grid[i][j] += grid[i][j - 1];
                else if (j == 0) grid[i][j] += grid[i-1][j];
                else grid[i][j] += Math.Min(grid[i-1][j], grid[i][j-1]);
            }
        return grid[m-1][n-1];
    }
    // recursion + memo
    public int MinPathSum(int[][] grid) {
        int m = grid.Length, n = grid[0].Length;
        int[,] memo = new int[m+1,n+1];
        Func<int,int,int> DFS = null;
        DFS = (i,j) => {
            if (i < 0 || j < 0) return Int32.MaxValue;
            if (i == 0 && j == 0) return grid[0][0];
            if (memo[i,j] > 0) return memo[i,j];
            return memo[i,j] = grid[i][j] + Math.Min(DFS(i-1,j), DFS(i, j-1));
        };
        return DFS(m-1, n-1);
    }
}
