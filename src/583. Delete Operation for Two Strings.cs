public class Solution {
    // DP
    public int MinDistance(string word1, string word2) {
        int l1 = word1.Length, l2 = word2.Length;
        int[,] dp = new int[l1+1,l2+1];
        // base case
        for (int i = 0; i <= l1; i++) dp[i,0] = i;  
        for (int j = 0; j <= l2; j++) dp[0,j] = j;
        for (int i = 1; i <= l1; i++)
            for (int j = 1; j <= l2; j++)
                dp[i,j] = word1[i-1] == word2[j-1] ? dp[i-1,j-1] : 1 + Math.Min(dp[i-1,j], dp[i,j-1]);
        return dp[l1,l2];
    }
    // recursion + memo
    public int MinDistance1(string word1, string word2) {
        int l1 = word1.Length, l2 = word2.Length;
        int[,] memo = new int[l1+1,l2+1];
        Func<int,int,int> f = null;
        f = (i,j) => {
            if (memo[i,j] != 0) return memo[i,j];
            if (i == l1 || j == l2) return memo[i,j] = l1 + l2 - i - j;
            return memo[i,j] = word1[i] == word2[j] ? f(i+1,j+1) : Math.Min(f(i+1,j), f(i,j+1)) + 1;
        };
        return f(0,0);
    }
}
