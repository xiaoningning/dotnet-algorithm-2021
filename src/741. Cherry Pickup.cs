public class Solution {
    // can not do two round trips seperatelly
    // => picked grid is empty in return trip.
    // => too many grid state to track
    // => track both direction in one round
    // DP v1 => hard to understand this one
    // T: O(n^3) S: O(n^2)
    public int CherryPickup1(int[][] grid) {
        // grid : N x N
        int n = grid.Length, mx = 2 * n -1;
        // (x1, x2) : # of cherries from (0,0) to (x1,x2) and back (0.0)
        int[,] dp = new int[n,n];
        for (int i = 0; i < n; i++) 
            for (int j = 0; j < n; j++) 
                dp[i,j] = -1;
        dp[0,0] = grid[0][0];
        // walk from (0,0) to (n-1,n-1) for both trips
        for (int k = 1; k < mx; k++) {
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
                    if (dp[x1, x2] >= 0) dp[x1, x2] += grid[x1][y1] + (x1 != x2 ? grid[x2][y2] : 0);
                }
            }
        }
        return Math.Max(0, dp[n - 1, n - 1]);
    }
    // DP v2
    // T: O(n^3) S: O(n^3)
    public int CherryPickup(int[][] grid) {
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
                    // must be max(case1, case2), max(case3, case4) 
                    // because A and B could not across each other to pick max # of cherries
                    int mx = Math.Max(Math.Max(dp[x1-1, y1, x2], dp[x1-1, y1, x2-1]),
                                     Math.Max(dp[x1,y1-1,x2], dp[x1, y1-1, x2-1]));
                    if (mx < 0) continue; // invalid path
                    dp[x1,y1,x2] = mx + grid[x1-1][y1-1] + (x1 != x2 ? grid[x2-1][y2-1] : 0);
                }
            }
        }
        return Math.Max(0, dp[n, n, n]);
    }
}
