public class Solution {
    // DP v1
    public int LongestPalindromeSubseq1(string s) {
        int n = s.Length;
        int[,] dp = new int[n,n];
        // dp[i,j] := dp[i+1,j-1] => i starts with n - 1, j with 0 
        for (int i = n-1; i >= 0; i--) {
            dp[i,i] = 1; // "a" or "bab"
            for (int j = i+1; j < n; j++)
               dp[i,j] = s[i] == s[j] ? 2 + dp[i+1,j-1] : Math.Max(dp[i+1,j], dp[i, j-1]); 
        }
        return dp[0,n-1];
    }
    // DP v2 save space
    public int LongestPalindromeSubseq(string s) {
        int n = s.Length, ans = 0;
        int[] dp = new int[n];
        Array.Fill(dp, 1); // "a" or "bab"
        // dp[i,j] := dp[i+1,j-1] => i starts with n - 1, j with 0 
        for (int i = n-1; i >= 0; i--) {
            int prev = 0;
            for (int j = i+1; j < n; j++) {
                int t = dp[j];
                // dp[j] is prev i; dp[j-1] is curr i
                dp[j] = s[i] == s[j] ? 2 + prev : Math.Max(dp[j], dp[j-1]); 
                prev = t;
            }
        }
        foreach (int cnt in dp) ans = Math.Max(ans, cnt);
        return ans;
    }
    // recursion + memo
    public int LongestPalindromeSubseq3(string s) {
        int n = s.Length;
        int[,] memo = new int[n,n];
        for (int i = 0; i < n; i++) for (int j = 0; j < n; j++) memo[i,j] = -1;
        Func<int,int,int> DFS = null;
        DFS = (i,j) => {
            if (i > j) return 0;
            if (i == j) return 1; // "a", "bab"
            if (memo[i,j] >= 0) return memo[i,j];
            return memo[i,j] = s[i] == s[j] ? 2 + DFS(i+1,j-1) : Math.Max(DFS(i+1,j), DFS(i,j-1));
        };
        return DFS(0, n-1);
    }
}
