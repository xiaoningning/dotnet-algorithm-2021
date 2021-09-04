public class Solution {
    // DP
    // T: O(m*n) S: O(m*n)
    public int UniquePathsWithObstacles2(int[][] obstacleGrid) {
        if (obstacleGrid.Length == 0 || obstacleGrid[0].Length == 0 || obstacleGrid[0][0] == 1) return 0;
        int m = obstacleGrid.Length, n = obstacleGrid[0].Length;
        // in case overflow int32
        long[,] dp = new long[m,n];
        // obstacle could be at (0,0)
        dp[0,0] = 1 - obstacleGrid[0][0];
        for (int i = 1; i < m; i++) dp[i,0] = obstacleGrid[i][0] == 0 ? dp[i - 1, 0] : 0;
        for (int j = 1; j < n; j++) dp[0,j] = obstacleGrid[0][j] == 0 ? dp[0, j - 1] : 0;
        for (int i = 1; i < m; i++) {
            for (int j = 1; j < n; j++) {
                if (obstacleGrid[i][j] == 1) continue;
                dp[i,j] = dp[i-1,j] + dp[i, j-1];
            }
        }
        return (int)dp[m-1,n-1];
    }
    // DP
    // T: O(m*n) S: O(n)
    public int UniquePathsWithObstacles1(int[][] obstacleGrid) {
        if (obstacleGrid.Length == 0 || obstacleGrid[0].Length == 0 || obstacleGrid[0][0] == 1) return 0;
        int m = obstacleGrid.Length, n = obstacleGrid[0].Length;
        // in case overflow int32
        long[] dp = new long[n];
        // obstacle could be at (0,0)
        dp[0] = 1 - obstacleGrid[0][0];
        for (int i = 0; i < m; i++) {
            for (int j = 0; j < n; j++) {
                if (obstacleGrid[i][j] == 1) dp[j] = 0;
                // j = 0 => dp[0] is the same as the preivous i
                // => save space
                else if (j > 0) dp[j] += dp[j - 1];
            }
        }
        return (int)dp[n-1];
    }
    // recursion + memo
    public int UniquePathsWithObstacles(int[][] obstacleGrid) {
        if (obstacleGrid.Length == 0 || obstacleGrid[0].Length == 0) return 0;
        int m = obstacleGrid.Length, n = obstacleGrid[0].Length;
        int[,] memo = new int[m,n];
        for (int i = 0; i < m; i++) for (int j = 0; j < n; j++) memo[i, j] = -1;
        Func<int, int, int> f = null;
        f = (i,j) => {
            if (i < 0 || j < 0) return 0;
            if (i == 0 && j == 0) return 1 - obstacleGrid[0][0];
            if (memo[i,j] != -1) return memo[i,j];  
            if (obstacleGrid[i][j] == 1) return memo[i,j] = 0;
            else return memo[i,j] = f(i-1, j) + f(i, j - 1);
        };
        return f(m-1,n-1);
    }
}
