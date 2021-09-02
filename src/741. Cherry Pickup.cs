public class Solution {
    // can not do two round trips seperatelly
    // => picked grid is empty in return trip.
    // => too many grid state to track
    // => track both direction in one round
    
    // DP v1.1 extra cache to track previous round => easy to understand
    // T: O(n^3) S: O(n^2)
    public int CherryPickup1(int[][] grid) {
        // grid : N x N
        // mxPath: steps from (0,0) to (n-1,n-1) to (0,0)
        int n = grid.Length, mxPath = 2 * n -1;
        // (x1, x2) : # of cherries from (0,0) to (x1,x2) and back (0.0)
        int[,] dp = new int[n+1,n+1];
        for (int i = 0; i <= n; i++) for (int j = 0; j <= n; j++) dp[i,j] = -1;
        dp[1,1] = grid[0][0];
        // dp[i,j] in loop k: when the length of path is k
        for (int k = 1; k < mxPath; k++) {
            // t to track previous round
            // without t, then need to x1/x2 from n-1 to 0
            int[,] t = new int[n+1,n+1];
            for (int i = 0; i <= n; i++) for (int j = 0; j <= n; j++) t[i,j] = -1;
            for (int x1 = 0; x1 <= Math.Min(k, n -1); x1++) {
                for (int x2 = 0; x2 <= Math.Min(k, n -1); x2++) {
                    int y1 = k - x1, y2 = k - x2;
                    if (y1 < 0 || y1 >= n || y2 < 0 || y2 >= n || grid[x1][y1] == -1 || grid[x2][y2] == -1) continue;
                    // dp (i,j) is 1-based
                    int mx = dp[x1+1,x2+1];
                    // 1 right, 2 right; 1 down, 2 right; 1 right, 2 down; 1 down, 2 down
                    mx = new int[]{mx, dp[x1,x2+1], dp[x1+1,x2], dp[x1,x2]}.Max();
                    if (mx < 0) continue; // invalid path
                    t[x1+1,x2+1] = mx + grid[x1][y1] + (x1 != x2 ? grid[x2][y2] : 0);
                }
            }
            dp = t;
        }
        return Math.Max(0, dp[n,n]);
    }
    
    // DP v1 => hard to understand this one
    // T: O(n^3) S: O(n^2)
    public int CherryPickup(int[][] grid) {
        // grid : N x N
        // mxPath: steps from (0,0) to (n-1,n-1)
        int n = grid.Length, mxPath = 2 * n -1;
        // (x1, x2) : # of cherries from (0,0) to (x1,x2) and back (0.0)
        int[,] dp = new int[n,n];
        for (int i = 0; i < n; i++) 
            for (int j = 0; j < n; j++) 
                dp[i,j] = -1;
        dp[0,0] = grid[0][0];
        // walk from (0,0) to (n-1,n-1) for both trips
        for (int k = 1; k < mxPath; k++) {
            // it must from n- 1 to 0, avoid re-updating dp[x1-1,x2]/dp[x1, x2-1] !!!
            for (int x1 = n - 1; x1 >= 0; x1--) {
                for (int x2 = n - 1; x2 >= 0; x2--) {
                    // x1<x2 && y1>y2 || x1==x2 && y1==y2 || x1>x2 && y1<y2
                    int y1 = k - x1, y2 = k - x2;
                    if (y1 < 0 || y1 >= n || y2 < 0 || y2 >= n || grid[x1][y1] == -1 || grid[x2][y2] == -1) {
                        dp[x1, x2] = -1; continue;
                    }
                    if (x1 > 0) dp[x1, x2] = Math.Max(dp[x1, x2], dp[x1 - 1, x2]);
                    if (x2 > 0) dp[x1, x2] = Math.Max(dp[x1, x2], dp[x1, x2 - 1]);
                    if (x1 > 0 && x2 > 0) dp[x1,x2] = Math.Max(dp[x1,x2], dp[x1 - 1, x2 - 1]);
                    // dp[x1,x2] < 0 is invalid
                    if (dp[x1, x2] >= 0) dp[x1, x2] += grid[x1][y1] + (x1 != x2 ? grid[x2][y2] : 0);
                }
            }
        }
        return Math.Max(0, dp[n - 1, n - 1]);
    }
    // DP v2
    // T: O(n^3) S: O(n^3)
    public int CherryPickup2(int[][] grid) {
        // grid : N x N
        int n = grid.Length;
        int[,,] dp = new int[n+1,n+1,n+1];
        for (int i = 0; i <= n; i++) 
            for (int j = 0; j <= n; j++)
                for (int k = 0; k <= n; k++) dp[i,j,k] = -1;
        dp[1,1,1] = grid[0][0];
        // walk from (0,0) to (n-1,n-1) for both trips with A and B
        for (int x1 = 1; x1 <= n; x1++) {
            for (int y1 = 1; y1 <= n; y1++) {
                for (int x2 = 1; x2 <= n; x2++) {
                    // only right and down
                    // x1<x2 && y1>y2 || x1==x2 && y1==y2 || x1>x2 && y1<y2
                    int y2 = x1 + y1 - x2;
                    // visited || out of boundary || no access
                    if (dp[x1,y1,x2] > 0 || y2 < 1 || y2 > n || grid[x1-1][y1-1] == -1 || grid[x2-1][y2-1] == -1) continue;
                    // case1 dp[x1 - 1][y1][x2] : A down, B right
                    // case2 dp[x1 - 1][y1][x2 - 1]: A down, B down
                    // case3 dp[x1][y1 - 1][x2] : A right, B right
                    // case4 dp[x1][y1 - 1][x2 - 1]: A right, B down
                    // because A and B could not across each other to pick max # of cherries
                    int mx = new int[]{dp[x1-1, y1, x2], dp[x1-1, y1, x2-1],
                                     dp[x1,y1-1,x2], dp[x1, y1-1, x2-1]}.Max();
                    if (mx < 0) continue; // invalid path
                    dp[x1,y1,x2] = mx + grid[x1-1][y1-1] + (x1 != x2 ? grid[x2-1][y2-1] : 0);
                }
            }
        }
        return Math.Max(0, dp[n, n, n]);
    }
    // DFS recursion + memo
    public int CherryPickup3(int[][] grid) {
        int n = grid.Length;
        int[,,] memo = new int[n,n,n];
        for (int i = 0; i < n; i++) 
            for (int j = 0; j < n; j++)
                for (int k = 0; k < n; k++) memo[i,j,k] = Int32.MinValue;
        Func<int,int,int,int> DFS = null;
        DFS = (x1, y1, x2) => {
            int y2 = x1 + y1 - x2;
            if (x1 < 0 || y1 < 0 || x2 < 0 || y2 < 0 || grid[x1][y1] == -1 || grid[x2][y2] == -1) return -1;
            // base case of DFS
            if (x1 == 0 && y1 == 0) return grid[0][0];
            if (memo[x1,y1,x2] != Int32.MinValue) return memo[x1,y1,x2];
            int mx = new int[]{DFS(x1-1, y1, x2), DFS(x1-1, y1, x2-1),
                            DFS(x1,y1-1,x2), DFS(x1, y1-1, x2-1)}.Max();
            if (mx < 0) return memo[x1,y1,x2] = -1; // invalid path
            else return memo[x1,y1,x2] = mx + grid[x1][y1] + (x1 != x2 ? grid[x2][y2] : 0);
        };
        return Math.Max(0, DFS(n - 1, n - 1, n - 1));
    }
}
