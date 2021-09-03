public class Solution {
    // DP
    public int MinCostClimbingStairs1(int[] cost) {
        int n = cost.Length;
        int[] dp = new int[n];
        dp[0] = cost[0]; dp[1] = cost[1];
        // calculate cost leaving each stair => i < n, min + cost[i]
        for (int i = 2; i < n; i++) dp[i] = Math.Min(dp[i-1], dp[i-2]) + cost[i];
        // climb top from either n - 1, or n - 2
        return Math.Min(dp[n-1], dp[n-2]);
    }
    // DP + space optimization
    public int MinCostClimbingStairs2(int[] cost) {
        int n = cost.Length;
        int c1 = 0, c2 = 0;
        // calculate cost to climb to each stair => i <= n, min + cost[i-1]/cost[i-2]
        for (int i = 2; i <= n; i++) { 
            int cur = Math.Min(c1 + cost[i - 1], c2 + cost[i - 2]);
            c2 = c1;
            c1 = cur;
        }
        // i == n, c1 is min
        return c1;
    }
    // recursion + memo v1
    public int MinCostClimbingStairs3(int[] cost) {
        int n = cost.Length;
        int[] memo = new int[n+1];
        Func<int,int> DFS = null;
        // min cost to climb to i-th step
        DFS = (i) => {
            if (memo[i] > 0) return memo[i];
            if (i <= 1) return 0;
            return memo[i] = Math.Min(DFS(i-1) + cost[i-1], DFS(i-2) + cost[i-2]);
        };
        return DFS(n);
    }
    // recursion + memo v2
    public int MinCostClimbingStairs(int[] cost) {
        int n = cost.Length;
        int[] memo = new int[n];
        Func<int,int> DFS = null;
        // min cost to leaving i-th step
        DFS = (i) => {
            if (memo[i] > 0) return memo[i];
            if (i <= 1) return memo[i] = cost[i];
            return memo[i] = cost[i] + Math.Min(DFS(i-1), DFS(i-2));
        };
        return Math.Min(DFS(n-1), DFS(n-2));
    }
}
