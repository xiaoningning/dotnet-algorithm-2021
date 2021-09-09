public class Solution {
    // DP v1
    public int MinimumDeleteSum1(string s1, string s2) {
        int l1 = s1.Length, l2 = s2.Length;
        int[,] dp = new int[l1+1,l2+1];
        // base case
        for (int i = 1; i <= l1; i++) dp[i,0] = dp[i-1,0] + s1[i-1];  
        for (int j = 1; j <= l2; j++) dp[0,j] = dp[0,j-1] + s2[j-1];
        for (int i = 1; i <= l1; i++)
            for (int j = 1; j <= l2; j++)
                dp[i,j] = s1[i-1] == s2[j-1] ? dp[i-1,j-1] : Math.Min(dp[i-1,j] + s1[i-1], dp[i,j-1] + s2[j-1] );
        return dp[l1,l2];
    }
    // DP v2 save space
    public int MinimumDeleteSum2(string s1, string s2) {
        int l1 = s1.Length, l2 = s2.Length;
        int[] dp = new int[l2+1];
        // base case for i = 0
        for (int j = 1; j <= l2; j++) dp[j] = dp[j-1] + s2[j-1];
        for (int i = 1; i <= l1; i++) {
            int ti = dp[0]; // prev i
            dp[0] += s1[i-1]; // base case for j = 0
            for (int j = 1; j <= l2; j++) {
                int tj = dp[j]; // prev j at pre i
                dp[j] = s1[i-1] == s2[j-1] ? ti : Math.Min(dp[j] + s1[i-1], dp[j-1] + s2[j-1] );
                ti = tj;
            }
        }
        return dp[l2];
    }
    // DP v3
    public int MinimumDeleteSum3(string s1, string s2) {
        int l1 = s1.Length, l2 = s2.Length;
        int[,] dp = new int[l1+1,l2+1];
        // calculate min # of common string
        for (int i = 1; i <= l1; i++)
            for (int j = 1; j <= l2; j++)
                dp[i,j] = s1[i-1] == s2[j-1] ? dp[i-1,j-1] + s1[i-1] : Math.Max(dp[i-1,j], dp[i,j-1]);
        int sum1 = 0, sum2 = 0;
        foreach (char c in s1) sum1 += c;
        foreach (char c in s2) sum2 += c;
        return sum1 + sum2 - 2 * dp[l1,l2];
    }
    // recursion + memo
    public int MinimumDeleteSum(string s1, string s2) {
        int l1 = s1.Length, l2 = s2.Length;
        int[,] memo = new int[l1+1,l2+1];
        for (int i = 0; i <= l1; i++)
            for (int j = 0; j <= l2; j++) memo[i,j] = Int32.MinValue;
        Func<int,int,int> f = null;
        f = (i,j) => {
            if (i == l1 && j == l2) return 0;
            if (memo[i,j] != Int32.MinValue) return memo[i,j];
            if (i == l1) return memo[i,j] = f(i,j+1) + s2[j];
            if (j == l2) return memo[i,j] = f(i+1,j) + s1[i];
            return memo[i,j] = s1[i] == s2[j] ? 
                f(i+1,j+1) 
                : Math.Min(f(i+1,j) + s1[i], f(i,j+1) + s2[j]);
        };
        return f(0,0);
    }
}
