public class Solution {
    // similar to 139. word break
    // DP
    // T: O(kn^2)
    // S: O(kn)
    public double LargestSumOfAverages1(int[] nums, int k) {
        int n = nums.Length;
        // (i, k) average of nums[0,..i] with k sub array
        double[,] dp = new double[n + 1, k + 1];
        // easy to calculate average
        double[] sum = new double[n + 1];
        for (int i = 1; i <= n; i++)  {
            sum[i] = sum[i - 1] + nums[i - 1];
            dp[i,1] = sum[i] / i;
        }
        for (int x = 2; x <= k; x++)
            for (int i = x; i <= n; i++)
                for (int j = x - 1 ; j < i; j++) 
                    dp[i, x] = Math.Max(dp[i, x], (sum[i] - sum[j]) / (i - j) + dp[j, x - 1]);
                
        return dp[n, k];
    }
    // DP v2
    // T: O(kn^2)
    // S: O(n)
    public double LargestSumOfAverages2(int[] nums, int k) {
        int n = nums.Length;
        // (i, k) average of nums[0,..i] with k sub array
        double[] dp = new double[n + 1];
        // easy to calculate average
        double[] sum = new double[n + 1];
        for (int i = 1; i <= n; i++)  {
            sum[i] = sum[i - 1] + nums[i - 1];
            dp[i] = sum[i] / i;
        }
        for (int x = 2; x <= k; x++) {
            double[] t = new double[n + 1];
            for (int i = x; i <= n; i++) {
                for (int j = x - 1 ; j < i; j++) {
                    t[i] = Math.Max(t[i], (sum[i] - sum[j]) / (i - j) + dp[j]);
                }
            }
            dp = t;
        }
                
        return dp[n];
    }
    // DFS (recursion) + memo
    public double LargestSumOfAverages(int[] nums, int k) {
        int n = nums.Length;
        double[,] memo = new double[n+1, k+1];
        double[] sum = new double[n + 1];
        for (int i = 1; i <= n; i++) sum[i] = sum[i - 1] + nums[i - 1];
        Func<int,int,double> f = null;
        f = (end, x) => {
            if (memo[end, x] > 0) return memo[end, x];
            if (x == 1) return memo[end, 1] = sum[end] / end;
            for (int j = 1; j < end; j++)
                memo[end, x] = Math.Max(memo[end, x], f(j, x - 1) + (sum[end] - sum[j]) / (end - j));
            return memo[end, x];
        };
        f(n,k);
        return memo[n, k];
    }
}
