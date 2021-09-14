public class Solution {
    // DP buttom up
    public int MinScoreTriangulation1(int[] values) {
        int n = values.Length;
        int[,] dp = new int[n,n];
        for (int i = 0; i < n; i++) for (int j = 0; j < n; j++) dp[i,j] = Int32.MaxValue;
        for (int i = 0; i < n - 1; i++) dp[i, i+1] = 0; // base case
        for (int l = 3; l <= n; l++) {
            for (int i = 0; i + l <= n; i++) {
                int j = i + l - 1;
                for (int k = i + 1; k <= j - 1; k++)
                    dp[i,j] = Math.Min(dp[i,j], values[i] * values[k] * values[j] + dp[i,k] + dp[k,j]);
            }
        }
        return dp[0,n-1];
    }
    // recursion + memo top down
    public int MinScoreTriangulation(int[] values) {
        int n = values.Length;
        int[,] memo = new int[n,n];
        for (int i = 0; i < n; i++) for (int j = 0; j < n; j++) memo[i,j] = Int32.MaxValue;
        Func<int,int,int> f = null;
        f = (i,j) => {
            if (j - i == 1 || i >= j) return 0;
            if (memo[i,j] != Int32.MaxValue) return memo[i,j];
            int ans = Int32.MaxValue;
            for (int k = i + 1; k <= j - 1; k++)
                ans = Math.Min(ans, values[i] * values[j] * values[k] + f(i, k) + f(k, j));
            return memo[i,j] = ans;
        };
        return f(0, n - 1);
    }
}
