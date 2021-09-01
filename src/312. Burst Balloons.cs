public class Solution {
    // DP
    // T: O(n^3), S: O(n^2)
    public int MaxCoins1(int[] nums) {
        int n = nums.Length;
        int[] vals = new int[n+2];
        vals[0] = 1; vals[n+1] = 1;
        // add the boarder vals
        for (int i = 0; i < n; i++) vals[i+1] = nums[i];
        // dp[i,j]: sums of coins from i to j.
        int[,] dp = new int[n+2, n+2];
        for (int len = 1; len <= n; len++) {
            for (int i = 1; i + len <= n + 1; i++) {
                int j = i + len - 1;
                // brute force to search all k in [i..j]
                for (int k = i; k <= j; k++)
                    // collect at k
                    // i,k-1,k+1,j already collected since dp[i,k-1] and dp[k+1,j]
                    dp[i,j] = Math.Max(dp[i,j], 
                                       dp[i,k-1] + vals[i-1] * vals[k] * vals[j+1] + dp[k+1,j]);
            }
        }
        return dp[1,n];
    }
    // recursion + memo
    public int MaxCoins(int[] nums) {
        int n = nums.Length;
        int[] vals = new int[n+2];
        vals[0] = 1; vals[n+1] = 1;
        // add the boarder vals
        for (int i = 0; i < n; i++) vals[i+1] = nums[i];
        int[,] memo = new int[n+2, n+2];
        Func<int,int,int> f = null;
        f = (i,j) =>{
            // never hit this actually since the caller f(1,n)
            if (i > j) return 0; 
            if (memo[i,j] > 0) return memo[i,j];
            if (i == j) return memo[i,j] = vals[i-1] * vals[i] * vals[j+1];
            int ans = 0;
            for (int k = i; k <= j; k++)
                ans = Math.Max(ans, f(i,k-1) + vals[i-1] * vals[k] * vals[j+1] + f(k+1,j));
            return memo[i,j] = ans;
        };
        f(1,n);
        return memo[1,n];
    }
}
