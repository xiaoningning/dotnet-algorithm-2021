public class Solution {
    // DP T: O(m*n*min(m,n))
    public int Largest1BorderedSquare1(int[][] grid) {
        int m = grid.Length, n = grid[0].Length, mx = 0;
        // # of 1 on left and top at (i, j)
        int[,] left = new int[m,n], top = new int[m,n];
        for (int i = 0; i < m; i++) {
            for (int j = 0; j < n; j++) {
                if (grid[i][j] == 0) continue;
                left[i, j] = (j == 0 ? 0: left[i, j - 1]) + 1;
                top[i, j] = (i == 0 ? 0 : top[i - 1, j]) + 1;
            }
        }
        for (int i = m - 1; i >= 0; i--) {
            for (int j = n - 1; j >= 0; j--) {
                int mn = Math.Min(top[i, j], left[i, j]);
                while (mn > mx) {
                    // check if the board is 1 on righ-top and left-buttom corner
                    if (left[i - mn + 1,j] >= mn && top[i,j - mn + 1] >= mn) mx = mn;
                    mn--;
                }
            }
        }
        return mx * mx;
    }
    // similar to 304. Range Sum Query 2D - Immutable
    public int Largest1BorderedSquare(int[][] grid) {
        int m = grid.Length, n = grid[0].Length;
        // # of 1 on the area at left button corner (i, j)
        int[,] dp = new int[m + 1,n + 1];
        for (int i = 1; i <= m; i++) {
            for (int j = 1; j <= n; j++) {
                dp[i, j] = dp[i-1,j] + dp[i, j-1] + grid[i-1][j-1] - dp[i-1,j-1];
            }
        }
        Func<int,int,int,int,int> sumRegion = (x1,y1,x2,y2) => {
            return dp[x2,y2] - dp[x2,y1-1] - dp[x1-1,y2] + dp[x1-1,y1-1];
        };
        for (int len = Math.Min(m,n); len > 0; len--) {
            for (int x1 = 1, x2 = x1 + len - 1; x2 <= m; x1++, x2++) {
                for (int y1 = 1, y2 = y1 + len - 1; y2 <= n; y1++, y2++) {
                    if (sumRegion(x1, y1, x2, y1) == len
                       && sumRegion(x1, y1, x1, y2) == len
                       && sumRegion(x1, y2, x2, y2) == len
                       && sumRegion(x2, y1, x2, y2) == len) return len * len;
                }
            }
        }
        return 0;
    }
}
