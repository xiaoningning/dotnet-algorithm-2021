public class Solution {
    // DP
    public bool IsMatch(string s, string p) {
        int m = s.Length, n = p.Length;
        // dp[i,j] == 1 => i of s, j of p match
        // [m+1, n+1] needs base match if s & p is both empty
        bool[,] dp = new bool[m+1,n+1];
        dp[0,0] = true; // base is match.
        for (int i = 1; i <= n; i++) if (p[i-1] == '*') dp[0,i] = dp[0, i-1];
        for (int i = 1; i <= m; i++) {
            for (int j = 1; j <= n; j++) {
                // '*' any sequence of char, or empty
                if (p[j-1] == '*') dp[i,j] = dp[i-1,j] || dp[i, j-1];
                // '?' a single char
                else dp[i,j] = (s[i-1] == p[j-1] || p[j-1] == '?') & dp[i-1, j-1];
            }
        }
        return dp[m,n];
    }
}
