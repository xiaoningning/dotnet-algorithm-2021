public class Solution {
    // DP similar to 322. Coin Change 494. Target Sum
    public bool CanPartition1(int[] nums) {
        if (nums.Sum() % 2 != 0) return false;
        int target = nums.Sum() / 2;
        bool[] dp = new bool[target+1];
        dp[0] = true;
        foreach (int n in nums)
            // need to start with target, 
            // otherwise dp[1] = true, => all dp[i] = true
            for (int i = target; i >= n; i--)
                // i could be true already
                dp[i] = dp[i-n] || dp[i]; 
        return dp[target];
    } 
    // recursion + memo
    public bool CanPartition(int[] nums) {
        if (nums.Sum() % 2 != 0) return false;
        int target = nums.Sum() / 2, n = nums.Length;
        var memo = new HashSet<(int,int)>();
        Func<int, int, bool> DFS = null;
        DFS = (start, sum) => {
            if (start == n) return sum == target;
            if (memo.Contains((start,sum)) || sum > target) return false;
            // two cases: pick it or not 
            bool ans = DFS(start + 1, sum + nums[start]) || DFS (start + 1, sum);
            memo.Add((start, sum));
            return ans;
        };
        return DFS(0,0);
    }
}
