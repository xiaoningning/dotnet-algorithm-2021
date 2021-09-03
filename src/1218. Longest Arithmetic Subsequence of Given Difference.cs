public class Solution {
    // DP
    // T: O(n), S: O(n)
    public int LongestSubsequence(int[] arr, int difference) {
        int ans = 0;
        var dp = new Dictionary<int,int>();
        foreach (int n in arr) {
            ans = Math.Max(ans, dp[n] = dp.ContainsKey(n - difference) ? dp[n - difference] + 1 : 1);
        }
        return ans;
    }
}
