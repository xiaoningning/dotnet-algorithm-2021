public class Solution {
    // DP 
    // Time Complexity: O(k*n^2)
    // Space Complexity: O(n^2)
    int[,] dirs = new int[8,2]{{-1,-2},{-2,-1},{-2,1},{-1,2},{1,2},{2,1},{2,-1},{1,-2}};
    public double KnightProbability1(int n, int k, int row, int column) {
        // # of steps at (i,j) after k moves
        // use double to avoid int32 overflow
        double[,] dp = new double[n,n];
        dp[row, column] = 1;
        for (int m = 0; m < k; m++) {
            double[,] t = new double[n,n];
            for (int i = 0; i < n; i++) 
                for (int j = 0; j < n; j++) {
                    for (int d = 0; d < 8; d++) {
                        int x = i + dirs[d,0], y = j + dirs[d,1];
                        if (x < 0 || x >= n || y < 0 || y >= n) continue;
                        t[x,y] += dp[i,j];
                    }
                }
            dp = t;
        }
        double cnt = 0;
        for (int i = 0; i < n; i++) for (int j = 0; j < n; j++) cnt += dp[i,j];
        return cnt / Math.Pow(8, k);
    }
    // recursion + memo without memo, TLE
    public double KnightProbability(int n, int k, int row, int column) {
        double[,,] memo = new double[n, n, k + 1];
        Func<int,int,int,double> f = null;
        f = (i, j, k) => {
            if (k == 0) return 1;
            if (memo[i, j, k] > 0) return memo[i, j, k];
            for (int d = 0; d < 8; d++) {
                int x = i + dirs[d,0], y = j + dirs[d,1];
                if (x < 0 || x >= n || y < 0 || y >= n) continue;
                memo[i, j, k] += f(x, y, k - 1);
            }
            return memo[i, j, k];
        };
        return f(row, column, k) / Math.Pow(8, k);
    }
}
