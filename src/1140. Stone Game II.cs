public class Solution {
    // DP top down
    // T: O(n^3) S: O(n^2)
    public int StoneGameII(int[] piles) {
        int n = piles.Length;
        int[] sum = (int[])piles.Clone();
        for (int i = n - 2; i >= 0; i--) sum[i] += sum[i+1];
        // memo := # of stones at i starts with m
        // if player takes all, them dp[n,m] = 0
        // => dp needs to n+1, n+1
        int[,] dp = new int[n+1,n+1];
        for (int i = 0; i < n; i++) dp[i,n] = sum[i];
        for (int i = n - 1; i >= 0; i--)
            for (int m = n; m >= 1; m--)
                for (int x = 1; x <= 2 * m && i + x <= n; x++)
                    dp[i, m] = Math.Max(dp[i,m], sum[i] - dp[i+x,Math.Max(m, x)]); 
        return dp[0,1];
    }
    // recursion + memo
    public int StoneGameII2(int[] piles) {
        int n = piles.Length;
        int[] sum = (int[])piles.Clone();
        for (int i = n - 2; i >= 0; i--) sum[i] += sum[i+1];
        // memo := # of stones at i starts with m
        int[,] memo = new int[n,n];
        Func<int,int,int> f = null;
        f = (i, m) => {
            if (i + 2 * m >= n) return sum[i];
            if (memo[i,m] != 0) return memo[i,m];
            int ans = 0;
            for (int x = 1; x <= 2 * m; x++) {
                // 1st player takes all except 1nd player f(i+x, max(x,m))
                ans = Math.Max(ans, sum[i] - f(i+x, Math.Max(x,m)));
            }
            return memo[i,m] = ans;
        };
        return f(0, 1);
    }
}
