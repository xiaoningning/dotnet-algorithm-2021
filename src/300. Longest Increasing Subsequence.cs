public class Solution {
    // DP T: O(n^2)
    public int LengthOfLIS1(int[] nums) {
        if (nums.Length == 0) return 0;
        int ans = 0;
        int[] dp = new int[nums.Length];
        Array.Fill(dp, 1);
        for (int i = 1; i < nums.Length; i++) {
            for (int j = 0; j < i; j++) 
                if (nums[i] > nums[j]) dp[i] = Math.Max(dp[i], dp[j] + 1);
            ans = Math.Max(ans, dp[i]);
        }
        return ans;
    }
    // DP + Binary Search / Patience Sort
    // T: O(nlogn)
    public int LengthOfLIS(int[] nums) {
        // dp is an increasing array
        // dp[i] the smallest num of tailing LIS
        var dp = new List<int>();
        foreach (int n in nums) {
            int l = 0, r = dp.Count;
            while (l < r) {
                int mid = l + (r - l) / 2;
                if (dp[mid] < n) l = mid + 1;
                else r = mid;
            }
            if (r == dp.Count) dp.Add(n);
            // update right, not insert
            dp[r] = n;
        }
        return dp.Count;
    }
}
