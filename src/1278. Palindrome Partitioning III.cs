public class Solution {
    // Recursion + memo v1
    Dictionary<(string,int), int> memo = new Dictionary<(string,int),int>();
    Dictionary<string, int> c = new Dictionary<string,int>();
    public int PalindromePartition(string s, int k) {
        Func<string, int> toPalindromeCnt = (str) => {
            if (c.ContainsKey(str)) return c[str];
            int cnt = 0;
            for (int l = 0, r = str.Length - 1; l < r; l++, r--) if (str[l] != str[r]) cnt++;
            return c[str] = cnt;
        };
        if (s.Length == 0 || s.Length == 1) return 0;
        if (memo.ContainsKey((s, k))) return memo[(s,k)];
        if (k == 1) return memo[(s, k)] = toPalindromeCnt(s);
        int ans = s.Length;
        for (int i = 1; i <= s.Length - (k - 1); i++) {
            int t = PalindromePartition(s.Substring(i), k-1);
            ans = Math.Min(ans, toPalindromeCnt(s.Substring(0, i)) + t);
        }
        return memo[(s, k)] = ans;
    }
    // Recursion + memo v2
    public int PalindromePartition1(string s, int k) {
        int n = s.Length;
        Func<int,int,int> f = null;
        f = (i,j) => {
            if (i >= j) return 0;
            return f(i+1,j-1) + Convert.ToInt32(s[i] != s[j]);
        };
        int[,] dp = new int[n, k+1];
        for (int i = 0; i < n; i++) 
            for (int j = 0; j <= k; j++) dp[i,j] = -1;
        Func<int,int,int> DFS = null;
        DFS = (i,m) => {
            if (m == 1) return dp[i,1] = f(0,i);
            if (m == i + 1) return 0; // one char as one partition
            if (m > i+1) return Int32.MaxValue / 2;
            if (dp[i,k] != -1) return dp[i,k];
            int ans = Int32.MaxValue / 2;
            for (int j = 0; j < i; j++)
                ans = Math.Min(ans, DFS(j, m - 1) + f(j+1, i));
            return dp[i,m] = ans;
        };
        return DFS(n-1, k);
    }
    // DP
    public int PalindromePartition2(string s, int k) {
        int n = s.Length;
        // DP to calculate cost of toPalindrome
        int[,] cost = new int[n,n];
        // len == 1, cost[i,i] = 0;
        /**
        for (int len = 2; len <= s.Length; len++)
            for (int i = 0, j = len - 1; j < n; i++, j++)
                cost[i,j] = Convert.ToInt32(s[i] != s[j]) + cost[i+1,j-1];
        */
        for (int i = n-1; i >= 0; i--) 
            for (int j = i+1; j < n; j++) 
                cost[i,j] = Convert.ToInt32(s[i] != s[j]) + cost[i+1,j-1];
        int[,] dp = new int[n, k+1];
        for (int i = 0; i < n; i++) 
            for (int m = 0; m <= k; m++) dp[i,m] = Int32.MaxValue / 2; // avoid overflow
        
        for (int i = 0; i < n; i++) {
            dp[i,1] = cost[0,i]; // base case
            for (int m = 2; m <= k; m++) {
                for (int j = 0; j < i; j++)
                    dp[i, m] = Math.Min(dp[i,m], dp[j, m - 1] + cost[j+1,i]);
            }
        }
        return dp[n-1,k];
    }
}
