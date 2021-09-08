public class Solution {
    // recursion
    // T: O((|s| + |p|) * 2 ^ (|s| + |p|))
    // S: O(|s| + |p|)
    public bool IsMatch1(string s, string p) {
        if (p.Length == 0) return s.Length == 0;
        // p: a*
        if (p.Length >= 2 && p[1] == '*') return IsMatch(s, p.Substring(2)) || (s.Length != 0 && (s[0] == p[0] || p[0] == '.')) && IsMatch(s.Substring(1), p);
        // p: .x | xy but p.Length >= 1
        else return (s.Length != 0 && (s[0] == p[0] || p[0] == '.')) && IsMatch(s.Substring(1), p.Substring(1));
    }
    // DP T: O(m*n) S: O(m*n)
    public bool IsMatch(string s, string p) {
        int m = s.Length, n = p.Length;
        bool[,] dp = new bool[m+1,n+1];
        dp[0,0] = true;
        // i start with 0 for the base s empty case
        for (int i = 0; i <= m; i++) {
            for (int j = 1; j <= n; j++) {
                if (j >= 2 && p[j - 1] == '*') dp[i,j] = dp[i,j-2] || (i > 0 && (s[i-1] == p[j-2] || p[j-2] == '.') && dp[i-1,j]);
                else dp[i,j] = i > 0 && (s[i-1] == p[j-1] || p[j-1] == '.') && dp[i-1,j-1];
            }
        }
        return dp[m,n];
    }
}
