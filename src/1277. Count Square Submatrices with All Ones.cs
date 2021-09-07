public class Solution {
    // DP v2 save space
    // T: O(m*n) S: O(1)
    public int CountSquares(int[][] matrix) {
        int ans = 0, m = matrix.Length, n = matrix[0].Length;
        // matrix track # of square with including (i,j)
        for (int i = 0; i < m; i++) 
            for (int j = 0; j < n; j++) {
                if (i > 0 && j > 0 && matrix[i][j] == 1) 
                    matrix[i][j] = new int[]{matrix[i-1][j-1],matrix[i-1][j],matrix[i][j-1]}.Min() + 1;
                ans += matrix[i][j];
            }
        return ans;
    }
    // DP v1
    public int CountSquares1(int[][] matrix) {
        int ans = 0, m = matrix.Length, n = matrix[0].Length;
        // edge len of square bottom right at (i,j)
        // also, track # of square with including (i,j)
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
