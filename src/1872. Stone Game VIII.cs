public class Solution {
    // Naive DP (min-max) takes O(n2) which leads to TLE.
    // The key of this problem is that each player takes k stones, 
    // but put their sum back as a new stone, 
    // so you can assume all the original stones are still there, 
    // but opponent has to start from the k+1 th stone.
    // dp[i] = sum[0,i] - max(dp[i+1], dp[i+2], dp[n-1]);
    // T: O(n) S: O(1)
    public int StoneGameVIII(int[] stones) {
        int n = stones.Length;
        for (int i = 1; i < n; i++) stones[i] += stones[i-1];
        // compute dp in reverse order
        // last one is to take the whole array
        int mx = stones.Last();
        // x > 1 => i >= 1
        for (int i = n - 2; i >= 1; i--)
            mx = Math.Max(mx, stones[i] - mx);
        return mx;
    }
    // recursion + memo => TLE
    public int StoneGameVIII1(int[] stones) {
        int n = stones.Length;
        for (int i = 1; i < n; i++) stones[i] += stones[i-1];
        int[] memo = new int[n];
        Array.Fill(memo, Int32.MinValue);
        Func<int,int> f = null;
        f = (i) => {
            if (i == n-1) return memo[i] = stones[i];
            if (memo[i] != Int32.MinValue) return memo[i];
            int ans = Int32.MinValue;
            for (int j = i + 1; j < n; j++) ans = Math.Max(ans, f(j));
            return memo[i] = stones[i] - ans;
        };
        // x > 1
        f(1);
        return memo.Max();
    }
}
