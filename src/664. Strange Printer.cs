public class Solution {
    // recursion + memo
    // T: O(n^3) S: O(n^2)
    public int StrangePrinter1(string s) {
        int n = s.Length;
        int[,] memo = new int[n,n];
        Func<int, int, int> DFS = null;
        DFS = (i,j) => {
            if (i > j) return 0;
            if (memo[i,j] != 0) return memo[i,j];
            /**
            // k loop covers this case
            int x = i;
            while (x < n && s[i] == s[x]) x++;
            i = x - 1;
            */
            int ans = 1 + DFS(i+1,j);
            for (int k = i + 1; k <= j; k++)
                // since s[i] == s[k], i can be ignored for print turn
                if (s[i] == s[k]) ans = Math.Min(ans, DFS(i+1,k-1) + DFS(k,j));
            return memo[i,j] = ans;
        };
        return DFS(0, n-1);
    }
    // DP
    public int StrangePrinter(string s) {
        int n = s.Length;
        int[,] dp = new int[n,n];
        for (int i = n - 1; i >= 0; i--)
            for (int j = i; j < n; j++) {
                dp[i,j] = 1 + (i < n - 1 ? dp[i+1, j] : 0);
                for (int k = i + 1; k <= j; k++)
                    if (s[i] == s[k]) dp[i,j] = Math.Min(dp[i,j], dp[i + 1, k -1] + dp[k, j]);
            }
        return dp[0, n - 1];
    }
}
