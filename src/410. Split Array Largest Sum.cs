public class Solution {
    // DP similar to LC 813. Largest Sum of Averages
    // T: O(n^2*m) S: O(n*m)
    public int SplitArray1(int[] nums, int m) {
        int n = nums.Length;
        int[,] dp = new int[m+1, n];
        for (int i = 0; i <= m; i++)
            for (int j = 0; j < n; j++) dp[i,j] = Int32.MaxValue;
        int[] sum = new int[n];
        for (int i = 0; i < n; i++) sum[i] = (i == 0 ? 0 : sum[i-1]) + nums[i];
        for (int i = 0; i < n; i++) dp[1,i] = sum[i];
        for (int i = 2; i <= m; i++)
            for (int j = i - 1; j < n; j++)
                for (int k = 0; k < j; k++)
                    dp[i,j] = Math.Min(dp[i,j], Math.Max(sum[j] - sum[k], dp[i-1,k]));
        return dp[m, n-1];
    }
    // recursion + memo
    public int SplitArray2(int[] nums, int m) {
        int n = nums.Length;
        int[,] memo = new int[m+1, n];
        for (int i = 0; i <= m; i++)
            for (int j = 0; j < n; j++) memo[i,j] = Int32.MaxValue;
        int[] sum = new int[n];
        for (int i = 0; i < n; i++) sum[i] = (i == 0 ? 0 : sum[i-1]) + nums[i];
        Func<int,int, int> f = null;
        f = (end, k) => {
            if (k == 1) return sum[end];
            if (end < k - 1) return Int32.MaxValue; // impossible to split
            if (memo[k, end] != Int32.MaxValue) return memo[k, end];
            int ans = Int32.MaxValue;
            for (int i = 0; i < end; i++)
                ans = Math.Min(ans, Math.Max(f(i,k-1), sum[end] - sum[i]));
            return memo[k, end] = ans;
        };
        return f(n-1,m);
    }
    // Binary search to find min sum directly
    // T: O(log(sum(nums))*n) S: O(1)
    public int SplitArray(int[] nums, int m) {
        Func<long, int> splitCnt = (limit) => {
            int cnt = 1; long sum = 0;
            foreach (int n in nums) {
                if (sum + n > limit) {
                    sum = n;
                    cnt++;
                }
                else sum += n;
            }
            return cnt;
        };
        // use long to avoid overflow int32 by sum(nums)
        long l = nums.Max(), r = nums.Sum() + 1;
        while (l < r) {
            long mid = l + (r - l) / 2;
            if (splitCnt(mid) > m) l = mid + 1;
            else r = mid;
        }
        return (int)l;
    }
}
