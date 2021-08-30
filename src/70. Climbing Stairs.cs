public class Solution {
    // DP T: O(n), S: O(n)
    public int ClimbStairs1(int n) {
        // dp[i] = climbStairs(i)
        int[] dp = new int[n+1];
        dp[0] = dp[1] = 1;
        // dp[i] = dp[i-1] + dp[i-2]
        for (int i = 2; i <= n; i++) dp[i] = dp[i - 1] + dp[i - 2];
        return dp[n];
    }
    // DP + optimization space T: O(n), S: O(1)
    public int ClimbStairs2(int n) {
        int one = 1, two = 1, cur = 1;
        for (int i = 2; i <= n; i++) {
            cur = one + two;
            two = one;
            one = cur;
        }
        return cur;
    }
    // recursion + memo
    public int ClimbStairs(int n) {
        int[] memo = new int[n+1];
        Func<int, int> f = null;
        f = (n) => {
            if (n <= 1) return 1;
            if (memo[n] > 0) return memo[n];
            return memo[n] = f(n-1) + f(n-2);
        };
        return f(n);
    }
}
