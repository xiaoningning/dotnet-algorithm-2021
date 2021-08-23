public class Solution {
    // DFS + memo
    // T: O(sum(target / num_i))
    public int CombinationSum41(int[] nums, int target) {
        int[] memo = new int[target + 1];
        Array.Fill(memo, -1);
        Func<int, int> DFS = null;
        DFS = (sum) => {
            if (sum == 0) return 1;
            if (sum < 0) return 0;
            if (memo[sum] != -1) return memo[sum];
            int ans = 0;
            foreach (int n in nums) ans += DFS(sum - n);
            return memo[sum] = ans;
        };
        return DFS(target);
    }
    // DP
    public int CombinationSum4(int[] nums, int target) {
        // dp[i]: # of combinations sum up to i
        int[] dp = new int[target + 1];
        dp[0] = 1; // base case
        for (int sum = 1; sum <= target; sum++) {
            foreach (int n in nums) if (sum - n >= 0) dp[sum] += dp[sum - n];
        }
        return dp.Last();
    }
}
