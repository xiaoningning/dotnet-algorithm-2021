public class Solution {
    // DP
    public int MaxSubArray1(int[] nums) {
        int[] dp = new int[nums.Length];
        dp[0] = nums[0];
        for (int i = 1; i < nums.Length; i++) {
            // subarray is contiguous
            dp[i] = Math.Max(dp[i - 1] + nums[i], nums[i]);
        }
        return dp.Max();
    }
    // DP + space optimization
    public int MaxSubArray(int[] nums) {
        int ans = Int32.MinValue, mx = 0;
        for (int i = 0; i < nums.Length; i++) {
            // subarray is contiguous
            mx = Math.Max(mx + nums[i], nums[i]);
            ans = Math.Max(ans, mx);
        }
        return ans;
    }
}
