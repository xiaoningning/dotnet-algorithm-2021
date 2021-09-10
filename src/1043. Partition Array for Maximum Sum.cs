public class Solution {
    // DP
    // T: O(n*k) S: O(n)
    public int MaxSumAfterPartitioning(int[] arr, int k) {
        int n = arr.Length;
        // max sum of after partition at i-1
        int[] dp = new int[n + 1];
        for (int i = 1; i <= n; i++) {
            int mx = 0;
            // including itself at arr[i-1]
            for (int j = 1; j <= k && i - j >= 0; j++) {
                mx = Math.Max(mx, arr[i - j]);
                dp[i] = Math.Max(dp[i], dp[i - j] + mx * j);
            }
        }
        return dp[n];
    }
}
