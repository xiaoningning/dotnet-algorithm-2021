public class NumMatrix {
    int[,] dp; // prefix sum of (i,j)
    public NumMatrix(int[][] matrix) {
        if (matrix.Length == 0 || matrix[0].Length == 0) return;
        int m = matrix.Length, n = matrix[0].Length;
        dp = new int[m+1, n+1];
        for (int i = 1; i <= m; i++) {
            for (int j = 1; j <= n; j++) {
                dp[i, j] = dp[i-1,j] + dp[i, j-1] + matrix[i-1][j-1] - dp[i-1,j-1];
            }
        }
    }
    
    public int SumRegion(int row1, int col1, int row2, int col2) {
        return dp[row2+1,col2+1] - dp[row1,col2+1] - dp[row2+1,col1] + dp[row1,col1];
    }
}

/**
 * Your NumMatrix object will be instantiated and called as such:
 * NumMatrix obj = new NumMatrix(matrix);
 * int param_1 = obj.SumRegion(row1,col1,row2,col2);
 */
