public class Solution {
    // DP
    public int FindPaths1(int m, int n, int maxMove, int startRow, int startColumn) {
        int M = (int)Math.Pow(10,9) + 7, ans = 0;
        int[,] dirs = new int[4,2]{{-1,0},{1,0},{0,1},{0,-1}};
        int[,] dp = new int[m,n];
        dp[startRow, startColumn] = 1;
        for (int k = 0; k < maxMove; k++) {
            int[,] t = new int[m,n];
            for (int i = 0; i < m; i++) 
                for (int j = 0; j < n; j++) {
                    for (int d = 0; d < 4; d++) {
                        int x = i + dirs[d,0], y = j + dirs[d,1];
                        if (x < 0 || x >= m || y < 0 || y >= n) ans = (ans + dp[i,j]) % M;
                        else t[x,y] = (t[x,y] + dp[i,j]) % M;
                    }
                }
            dp = t;
        }
        return ans;
    }
    // recursion + memo
    public int FindPaths(int m, int n, int maxMove, int startRow, int startColumn) {
        int M = (int)Math.Pow(10,9) + 7;
        int[,] dirs = new int[4,2]{{-1,0},{1,0},{0,1},{0,-1}};
        int[,,] memo = new int[m,n,maxMove+1];
        for (int k = 0; k <= maxMove; k++)
            for (int i = 0; i < m; i++) 
                for (int j = 0; j < n; j++) memo[i,j,k] = -1;
        Func<int,int,int,int> f = null;
        f = (x,y,k) => {
            if (x < 0 || x >= m || y < 0 || y >= n) return 1;
            if (k == 0) return 0;
            if (memo[x,y,k] != -1) return memo[x,y,k];
            long ans = f(x-1,y,k-1) % M;
            ans += f(x+1,y,k-1) % M;
            ans += f(x,y-1,k-1) % M;
            ans += f(x,y+1,k-1) % M;
            ans %= M;
            return memo[x,y,k] = (int)ans;
        };
        return f(startRow, startColumn, maxMove);
    }
}
