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
    // T: O(n), S: O(1)
    public int ClimbStairs(int n) {
        int one = 1, two = 1, cur = 1;
        for (int i = 2; i <= n; i++) {
            cur = one + two;
            two = one;
            one = cur;
        }
        return cur;
    }
}
