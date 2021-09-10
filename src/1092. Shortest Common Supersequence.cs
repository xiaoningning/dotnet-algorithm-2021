public class Solution {
    // Similar to LC 1143. Longest Common Subsequence
    public string ShortestCommonSupersequence(string str1, string str2) {
        int l1 = str1.Length, l2 = str2.Length;
        string[,] dp = new string[l1+1,l2+1];
        for (int i = 0; i <= l1; i++) for (int j = 0; j <= l2; j++) dp[i,j] = "";
        for (int i = 1; i <= l1; i++) 
            for (int j = 1; j <= l2; j++)
                dp[i,j] = str1[i-1] == str2[j-1] ? 
                    dp[i-1,j-1] + str1[i-1] : 
                    (dp[i-1,j].Length > dp[i, j-1].Length ? dp[i-1,j] : dp[i,j-1]);
        // dp[l1,l2] = LCS string
        int x = 0, y = 0;
        string ans = "";
        foreach (char c in dp[l1,l2]) {
            while (str1[x] != c) ans += str1[x++];
            while (str2[y] != c) ans += str2[y++];
            ans += c;
            x++; y++;
        }
        return ans + str1.Substring(x) + str2.Substring(y);
    }
}
