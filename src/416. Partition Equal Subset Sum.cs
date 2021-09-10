public class Solution {
    // DP similar to 322. Coin Change
    public bool CanPartition(int[] nums) {
        if (nums.Sum() % 2 != 0) return false;
        int target = nums.Sum() / 2;
        bool[] dp = new bool[target+1];
        dp[0] = true;
        foreach (int n in nums)
            // need to start with target, 
            // otherwise dp[1] = true, => all dp[i] = true
            for (int i = target; i >= n; i--)
                dp[i] = dp[i-n] || dp[i];
        return dp[target];
    }
}
