public class Solution {
    /**
    // 2 factor is due to tromino
    dp[n] = dp[n-1] + dp[n-2] + 2*(dp[n-3] + ... + dp[0])
      = dp[n-1] + dp[n-3] + [dp[n-2] + dp[n-3] + 2*(dp[n-4] + ... + dp[0])]
      = dp[n-1] + dp[n-3] + dp[n-1]
      = 2*dp[n-1] + dp[n-3]
    */
    // DP v1
    public int NumTilings1(int n) {
        if (n <= 1) return 1;
        int M = (int)Math.Pow(10,9) + 7; 
        long[] dp = new long[n+1];
        dp[0] = dp[1] = 1; dp[2] = 2;
        for (int i = 3; i <= n; i++)
            dp[i] = (2 * dp[i-1] + dp[i-3]) % M;
        return (int)dp[n];
    }
    // dp[i][0]: ways to cover i cols, both rows of i-th col are covered
    // dp[i][1]: ways to cover i cols, only top row of i-th col is covered
    // dp[i][2]: ways to cover i cols, only bottom row of i-th col is covered
    // DP v2
    public int NumTilings(int n) {
        if (n <= 1) return 1;
        int M = (int)Math.Pow(10,9) + 7; 
        long[,] dp = new long[n+1,3];
        dp[0,0] = dp[1,0] = 1;
        for (int i = 2; i <= n; i++) {
            dp[i,0] = (dp[i-1,0] + dp[i-2,0] + dp[i-1,1] + dp[i-1,2]) % M;
            // dp[i,1] and dp[i,2] the same, they can be merged
            dp[i,1] = (dp[i-1,2] + dp[i-2,0]) % M;
            dp[i,2] = (dp[i-1,1] + dp[i-2,0]) % M;
        }
        return (int)dp[n,0];
    }
}
