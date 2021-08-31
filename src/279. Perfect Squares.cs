public class Solution {
    // recursion
    public int NumSquares1(int n) {
        // worst case: 1+1+...+1 = n
        int ans = n, num = 2;
        while (num * num <= n) {
            int a = n / (num * num), b = n % (num * num);
            ans = Math.Min(ans, a + NumSquares(b));
            num++;
        }
        return ans;
    }
    // DP
    // T: O(n * sqrt(n))
    // S: O(n)
    public int NumSquares(int n) {
        // dp[i] = min(dp[i-j*j] + 1) 1<=j*j<=i
        int[] dp = new int[n+1];
        Array.Fill(dp, Int32.MaxValue);
        dp[0] = 0;
        for (int i = 1; i <= n; i++)
            for (int j = 1; j*j <= i; j++)
                dp[i] = Math.Min(dp[i], dp[i - j*j] + 1);
        return dp[n];
    }
}
