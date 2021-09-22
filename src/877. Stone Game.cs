public class Solution {
    // recursion + memo
    // T: O(n^2) S: O(n^2)
    public bool StoneGame1(int[] piles) {
        int n = piles.Length;
        int[,] memo = new int[n,n];
        for (int i = 0; i < n; i++) for (int j = 0; j < n; j++) memo[i,j] = -1;
        Func<int,int,int> f = null;
        f = (l, r) => {
            if (l == r) return piles[l];
            if (memo[l,r] != -1) return memo[l, r];
            return memo[l, r] = Math.Max(piles[l] - f(l+1,r), piles[r] - f(l, r-1));
        };
        // alice first, bob second => f(0,n-1) > 0 => alice win
        return f(0, n-1) > 0;
    }
    // min - max strategy + linear DP
    public bool StoneGame(int[] piles) {
        int n = piles.Length;
        int[,] dp = new int[n,n];
        for (int l = 2; l <= n; l++)
            for (int i = 0, j = i + l - 1; j < n; j++, i++)
                dp[i,j] = Math.Max(piles[i] - dp[i+1, j], piles[j] - dp[i, j-1]);

        return dp[0, n-1] > 0;
    }
}
