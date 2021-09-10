public class Solution {
    // recursion + memo
    public int LongestCommonSubsequence1(string text1, string text2) {
        int l1 = text1.Length, l2 = text2.Length;
        int[,] memo = new int[l1,l2];
        for (int i = 0; i < l1; i++) for (int j = 0; j < l2; j++) memo[i,j] = -1;
        Func<int,int,int> DFS = null;
        DFS = (i,j) => {
            if (i == l1 || j == l2) return 0;
            if (memo[i,j] >= 0) return memo[i,j];
            return memo[i,j] = text1[i] == text2[j] ? 1 + DFS(i+1,j+1) : Math.Max(DFS(i+1,j), DFS(i, j+1)); 
        };
        return DFS(0,0);
    }
    // DP
    public int LongestCommonSubsequence(string text1, string text2) {
        int l1 = text1.Length, l2 = text2.Length;
        int[,] dp = new int[l1+1,l2+1];
        for (int i = 1; i <= l1; i++) 
            for (int j = 1; j <= l2; j++) 
                dp[i,j] = text1[i-1] == text2[j-1] ? dp[i-1,j-1] + 1 : Math.Max(dp[i, j-1], dp[i-1,j]);
        return dp[l1,l2];
    }
}
