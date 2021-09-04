public class Solution {
    // DP
    // T: O(m*n) S: O(m*n)
    public int UniquePaths1(int m, int n) {
        int[,] dp = new int[m,n];
        dp[0,0] = 1;
        for (int i = 0; i < n; i++) dp[0,i] = 1;
        for (int i = 0; i < m; i++) dp[i,0] = 1;
        for (int i = 1; i < m; i++) {
            for (int j = 1; j < n; j++)
                dp[i,j] = dp[i-1,j] + dp[i, j-1];
        }
        return dp[m-1,n-1];
    }
    // DP v2
    // T: O(m*n) S: O(n)
    public int UniquePaths(int m, int n) {
        int[] dp = new int[n];
        Array.Fill(dp, 1);
        for (int i = 1; i < m; i++)
            for (int j = 1; j < n; j++)
                dp[j] += dp[j-1];
        return dp[n-1];
    }
    // recursion + memo
    public int UniquePaths2(int m, int n) {
        int[,] memo = new int[m,n];
        Func<int, int, int> f = null;
        f = (i,j) => {
            if (i == 0 || j == 0) return 1;
            if (memo[i,j] > 0) return memo[i,j];  
            int leftPaths = f(i - 1, j);
            int topPaths = f(i, j - 1);
            return memo[i,j] = leftPaths + topPaths;
        };
        return f(m-1,n-1);
    }
}
