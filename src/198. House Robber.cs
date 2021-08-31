public class Solution {
    // recursion + memo if without memo, then TLE
    // T: O(n)
    // S: O(n)
    public int Rob1(int[] nums) {
        int[] memo = new int[nums.Length];
        Array.Fill(memo, -1);
        Func<int, int> f = null;
        f = (i) => {
            if (i >= nums.Length) return 0;
            if (memo[i] >= 0) return memo[i];
            // two case: rob i or not
            return memo[i] = Math.Max(f(i+2) + nums[i], f(i+1));
        };
        return f(0);
    }
    // DP
    // T: O(n)
    // S: O(1)
    public int Rob2(int[] nums) {
        if (nums.Length == 0) return 0;
        int rob = 0, notRob = 0;
        for (int i = 0; i < nums.Length; i++) {
            int preRob = rob, preNotRob = notRob;
            rob = preNotRob + nums[i];
            notRob = Math.Max(preRob, preNotRob);
        }
        return Math.Max(rob, notRob);
    }
    // DP
    // T: O(n)
    // S: O(n)
    public int Rob(int[] nums) {
        if (nums.Length == 0) return 0;
        int[] dp = new int[nums.Length];
        for (int i = 0; i < nums.Length; i++) 
            dp[i] = Math.Max((i > 1 ? dp[i - 2] : 0) + nums[i], 
                             i > 0 ? dp[i - 1] : 0);
        return dp.Last();
    }
}
