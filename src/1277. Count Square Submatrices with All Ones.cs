public class Solution {
    // DP
    public int CountSquares(int[][] matrix) {
        int ans = 0, m = matrix.Length, n = matrix[0].Length;
        // edge len of square bottom right at (i,j)
        int[,] dp = new int[m,n];
        for (int i = 0; i < m; i++) 
            for (int j = 0; j < n; j++) {
                dp[i,j] = matrix[i][j];
                if (i > 0 && j > 0 && matrix[i][j] == 1) dp[i,j] = new int[]{dp[i-1,j-1],dp[i-1,j],dp[i,j-1]}.Min() + 1;
                // # of sqaures = sum(dp[i])
                ans += dp[i,j];
            }
        return ans;
    }
}
