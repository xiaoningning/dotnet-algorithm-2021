public class Solution {
    // DP T: O(mn) S: O(mn)
    public bool IsInterleave(string s1, string s2, string s3) {
        int m = s1.Length, n = s2.Length, k = s3.Length;
        if (m + n != k) return false;
        bool[,] dp = new bool[m+1, n+1];
        dp[0,0] = true;
        // s2 is empty base case
        for (int i = 1; i <= m; i++) dp[i,0] = dp[i-1,0] && s1[i-1] == s3[i-1];
        // s1 is empty base case
        for (int j = 1; j <= n; j++) dp[0,j] = dp[0,j-1] && s2[j-1] == s3[j-1];
        for (int i = 1; i <= m; i++)
            for (int j = 1; j <= n; j++)
                dp[i,j] = (dp[i-1,j] && s1[i-1] == s3[i - 1 + j]) || 
                    (dp[i,j-1] && s2[j-1] == s3[j - 1 + i]);
        return dp[m,n];
    }
    // recursion + memo
    public bool IsInterleave1(string s1, string s2, string s3) {
        if (s1.Length + s2.Length != s3.Length) return false;
        int m = s1.Length, n = s2.Length;
        var memo = new Dictionary<(int,int), bool>();
        Func<int,int,int,bool> f = null;
        f = (i,j,k) => {
            if (memo.ContainsKey((i,j))) return memo[(i,j)];
            if (i == s1.Length) return memo[(i,j)] = s2.Substring(j) == s3.Substring(k);
            if (j == s2.Length) return memo[(i,j)] = s1.Substring(i) == s3.Substring(k);
            if ((s1[i] == s3[k] && f(i+1,j,k+1)) ||
                (s2[j] == s3[k] && f(i,j+1,k+1))) return memo[(i,j)] = true;
            return memo[(i,j)] = false;
        };
        return f(0,0,0);
    }
}
