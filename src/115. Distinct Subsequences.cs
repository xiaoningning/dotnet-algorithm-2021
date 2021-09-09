public class Solution {
    // DP
    public int NumDistinct1(string s, string t) {
        int ls = s.Length, lt = t.Length;
        // # of subseq at t(i) and s(j)
        int[,] dp = new int[lt+1,ls+1];
        dp[0,0] = 1;
        // base case
        for (int j = 1; j <= ls; j++) dp[0,j] = 1;
        // move s[j] first then t[i]
        for (int i = 1; i <= lt; i++)
            for (int j = 1; j <= ls; j++)
                dp[i,j] = dp[i,j-1] + (t[i-1] == s[j-1] ? dp[i-1,j-1] : 0);
        return dp[lt,ls];
    }
    // recursion + memo
    public int NumDistinct(string s, string t) {
        int ls = s.Length, lt = t.Length;
        int[,] memo = new int[lt+1, ls+1];
        for (int i = 0; i <= lt; i++)
            for (int j = 0; j <= ls; j++) memo[i,j] = -1;
        Func<int,int,int> f = null;
        f = (i,j) => {
            if (memo[i,j] >= 0) return memo[i,j];
            if (i == t.Length) return memo[i,j] = 1;
            if (j == s.Length) return memo[i,j] = 0;
            return memo[i,j] = (t[i] == s[j] ? f(i+1,j+1) : 0 ) + f(i,j+1); 
        };
        return f(0,0);
    }
    
}
