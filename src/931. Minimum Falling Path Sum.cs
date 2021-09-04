public class Solution {
    // DP v2
    // T: O(m*n) S: O(1)
    public int MinFallingPathSum2(int[][] matrix) {
        int m = matrix.Length, n = matrix[0].Length;
        for (int i = 1; i < m; i++)
            for (int j = 0; j < n; j++) {
                int sum = matrix[i-1][j];
                if ( j > 0) sum = Math.Min(sum, matrix[i-1][j-1]);
                if ( j < m - 1) sum = Math.Min(sum, matrix[i-1][j+1]);
                matrix[i][j] += sum;
            }
        return matrix[m-1].Min();
    }
    // DP v1
    // T: O(m*n) S: O(m*n)
    public int MinFallingPathSum(int[][] matrix) {
        int m = matrix.Length, n = matrix[0].Length, ans = Int32.MaxValue;
        int[,] dp = new int[m + 1, n + 2];
        for (int i = 1; i <= m; i++) {
            dp[i-1,0] = dp[i-1,n+1] = Int32.MaxValue;
            for (int j = 1; j <= n; j++) {
                int sum = new int[]{dp[i-1,j], dp[i-1,j+1], dp[i-1,j-1]}.Min();
                dp[i,j] = sum + matrix[i-1][j - 1];
                if (i == m) ans = Math.Min(ans, dp[i,j]);
            }
        }
        return ans;
    }
}
