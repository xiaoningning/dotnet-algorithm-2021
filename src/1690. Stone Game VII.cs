public class Solution {
    // DP top down
    public int StoneGameVII2(int[] stones) {
        int n = stones.Length;
        int[,] memo = new int[n,n];
        for (int i = 0; i < n; i++) for (int j = 0; j < n; j++) memo[i,j] = Int32.MaxValue;
        Func<int,int,int,int> f = null;
        f = (l,r,s) => {
            if (l >= r) return 0;
            if (memo[l,r] != Int32.MaxValue) return memo[l,r];
            return memo[l,r] = Math.Max(s - stones[l] - f(l+1,r, s - stones[l]), s - stones[r] - f(l, r -1, s - stones[r]));
        };
        return f(0, n-1, stones.Sum());
    }
    // DP buttom up
    public int StoneGameVII(int[] stones) {
        int n = stones.Length;
        int[] sum = new int[n+1];
        for (int i = 1; i <= n; i++) sum[i] = sum[i-1] + stones[i-1];
        int[,] dp = new int[n,n];
        for (int c = 2; c <= n; c++)
            for (int i = 0, j = i + c - 1; j < n; j++, i++)
                dp[i,j] = Math.Max(sum[j+1] - sum[i+1] - dp[i+1,j], sum[j] - sum[i] - dp[i, j-1]);
     
        return dp[0, n-1];
    }
}
