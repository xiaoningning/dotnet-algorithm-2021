public class Solution {
    // DP
    public bool IsMatch(string s, string p) {
        int m = s.Length, n = p.Length;
        // dp[i,j] == 1 => i of s, j of p match
        // [m+1, n+1] needs base match if s & p is both empty
        bool[,] dp = new bool[m+1,n+1];
        dp[0,0] = true; // base is match.
        for (int j = 1; j <= n; j++) if (p[j-1] == '*') dp[0,j] = dp[0, j-1];
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
    // recursion => TLE
    public bool IsMatch2(string s, string p) {
        if (s.Length == 0) return p.Length == 0 || (p[0] == '*' && IsMatch(s, p.Substring(1)));
        if (p.Length == 0) return s.Length == 0;
        if (p[0] != '*') return (s[0] == p[0] || p[0] == '?') && IsMatch(s.Substring(1), p.Substring(1));
        else return IsMatch(s.Substring(1),p) || IsMatch(s, p.Substring(1));
    }
} 
