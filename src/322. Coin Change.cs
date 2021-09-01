public class Solution {
    // DP
    // Time complexity: O(n*amount)
    // Space complexity: O(amount)
    public int CoinChange(int[] coins, int amount) {
        int[] dp = new int[amount + 1];
        Array.Fill(dp, amount + 1);
        dp[0] = 0;
        for (int i = coins.Min(); i <= amount; i++) {
            foreach (int c in coins) {
                if (i < c) continue;
                dp[i] = Math.Min(dp[i],  dp[i - c] + 1);
            }
        }
        return dp[amount] > amount ? -1 : dp[amount];
    }
    // Recursion + memo
    public int CoinChange2(int[] coins, int amount) {
        int[] memo = new int[amount + 1];
        Array.Fill(memo, amount + 1);
        memo[0] = 0;
        Func<int,int> f = null;
        f = (t) => {
            if (t < 0) return -1;
            if (memo[t] != amount + 1) return memo[t];
            foreach (int c in coins) {
                int ans = f(t - c);
                if (ans >= 0) memo[t] = Math.Min(memo[t], ans + 1);
            }
            return memo[t] = (memo[t] != amount + 1) ? memo[t] : -1;
        };
        return f(amount);
    }
    // DFS + Recursion without memo => TLE
    public int CoinChange3(int[] coins, int amount) {
        // use biggest coin => less change
        Array.Sort(coins);
        int ans = amount + 1;
        Action<int, int, int> f = null;
        f = (start, cnt, target) => {
            if (start < 0) return;
            if (target % coins[start] == 0) {
                ans = Math.Min(ans, cnt + target / coins[start]);
                return;
            }
            int c = coins[start];
            for (int k = target / c; k >= 0 && cnt + k < ans; k--) f(start - 1, cnt + k, target - k * c);
        };
        f(coins.Length - 1, 0, amount);
        return ans > amount ? -1 : ans;
    }
}
