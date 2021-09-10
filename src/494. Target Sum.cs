public class Solution {
    // DP v2 save space
    public int FindTargetSumWays1(int[] nums, int target) {
        var dp = new Dictionary<int,int>(){[0] = 1};
        foreach (int n in nums) {
            var t = new Dictionary<int,int>();
            var ks = dp.Keys.ToList();
            foreach (int v in ks) {
                int cnt = dp[v];
                if (!t.ContainsKey(v+n)) t[v+n] = 0;
                if (!t.ContainsKey(v-n)) t[v-n] = 0;
                t[v+n] += cnt;
                t[v-n] += cnt;
            }
            dp = t;
        }
        return dp.ContainsKey(target) ? dp[target] : 0;
    }
    // recursion v1 + memo
    public int FindTargetSumWays2(int[] nums, int target) {
        int n = nums.Length;
        var memo = new Dictionary<int,Dictionary<int,int>>();
        for (int i = 0; i < n; i++) memo[i] = new Dictionary<int,int>();
        Func<int,int,int> f = null;
        f = (start, sum) => {
            if (start == n) return Convert.ToInt32(sum == 0);
            if (memo[start].ContainsKey(sum)) return memo[start][sum];
            return memo[start][sum] = f(start+1, sum - nums[start]) + f(start+1, sum + nums[start]);
        };
        return f(0,target);
    }
    // recursion v2 without memo slower than v1 
    public int FindTargetSumWays3(int[] nums, int target) {
        if (nums.Sum() < Math.Abs(target)) return 0;
        int ans = 0;
        Action<int,int> DFS = null;
        DFS = (start, sum) => {
            if (start == nums.Length) ans += Convert.ToInt32(sum == 0);
            else { 
                DFS(start+1, sum - nums[start]); 
                DFS(start+1, sum + nums[start]);
            }
        };
        DFS(0,target);
        return ans;
    }
    // DP v1 with offset search int[] faster than v2 with dictionary
    // int array is less space with class dictionary
    public int FindTargetSumWays(int[] nums, int target) {
        if (nums.Sum() < Math.Abs(target)) return 0;
        int kOffset = nums.Sum();
        int kMax = 2 * kOffset;
        int[] dp = new int[kMax + 1];
        dp[kOffset] = 1;
        foreach (int n in nums) {
            int[] t = new int[kMax+1];
            for (int i = n; i <= kMax - n; i++) {
                if (dp[i] != 0) {
                    t[i-n] += dp[i];
                    t[i+n] += dp[i];
                }
            }
            dp = t;
        }
        return dp[target + kOffset];
    }
}
