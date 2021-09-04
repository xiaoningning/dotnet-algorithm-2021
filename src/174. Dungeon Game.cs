public class Solution {
    // DP v1
    // T: O(m*n) S: O(m*n)
    public int CalculateMinimumHP1(int[][] dungeon) {
        int m = dungeon.Length, n = dungeon[0].Length;
        // min health required to reach (i,j)
        int[,] dp = new int[m + 1, n + 1];
        for (int i = 0; i <= m; i++) for (int j = 0; j <= n; j++) dp[i,j] = Int32.MaxValue;
        // min health after the exit of (m-1,n-1) is 1 
        dp[m, n - 1] = dp[m - 1, n] = 1;
        for (int i = m - 1; i >= 0; i--) {
            for (int j = n - 1; j >= 0; j--) {
                // if dungeon[i][j] > 0, health sore required < 1
                // => at most need 1 for [i][j]
                dp[i,j] = Math.Max(1, Math.Min(dp[i+1,j], dp[i, j+1]) - dungeon[i][j]);
            }
        }
        return dp[0,0];
    }
    // DP v2 reduce space
    // T: O(m*n) S: O(n)
    public int CalculateMinimumHP(int[][] dungeon) {
        int m = dungeon.Length, n = dungeon[0].Length;
        // min health required to reach (i,j)
        int[] dp = new int[n + 1];
        for (int j = 0; j <= n; j++) dp[j] = Int32.MaxValue;
        // min health after the exit of (m-1,n-1) is 1 
        dp[n-1] = 1;
        for (int i = m - 1; i >= 0; i--)
            for (int j = n - 1; j >= 0; j--)
                // dp[j] = dp[i+1,j]
                dp[j] = Math.Max(1, Math.Min(dp[j], dp[j+1]) - dungeon[i][j]);
        return dp[0];
    }
}
