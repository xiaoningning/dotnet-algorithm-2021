public class Solution {
    // DP
    // T: O(m*n) S: O(m*n)
    public int MinimumMoves1(int[][] grid) {
        int m = grid.Length, n = grid[0].Length;
        // (i,j) tail pos, 0: h, 1: v
        int[,,] dp = new int[m+1,n+1,2];
        // min() + 1 => init as Max / 2 to avoid overlfow
        for (int i = 0; i <= m; i++) for (int j = 0; j <= n; j++) dp[i,j,0] = dp [i,j,1] = Int32.MaxValue / 2;
        // init pos: (0,0), (0,1) step should be 0
        // => down / right should be inited as -1
        dp[0,1,0] = dp[1,0,0] = -1;
        for (int i = 1; i <= m; i++) 
            for (int j = 1; j <= n; j++) {
                bool v = false, h = false;
                // horizontal
                if (grid[i-1][j-1] == 0 && j < n && grid[i-1][j] == 0) {
                    h = true;
                    dp[i,j,0] = Math.Min(dp[i-1,j,0], dp[i,j-1,0]) + 1;
                }
                // verical
                if (grid[i-1][j-1] == 0 && i < m && grid[i][j-1] == 0) {
                    v = true;
                    dp[i,j,1] = Math.Min(dp[i-1,j,1], dp[i,j-1,1]) + 1;
                }
                // horizontal vs clockwise
                if (h && i < n && grid[i][j] == 0) dp[i,j,0] = Math.Min(dp[i,j,0], dp[i,j,1] + 1);
                // vertical vs counter clockwise
                if (v && j < n && grid[i][j] == 0) dp[i,j,1] = Math.Min(dp[i,j,1], dp[i,j,0] + 1);
        }
        // plus + 1 on each round => >= MaxValue/2
        return dp[m, n-1,0] >= Int32.MaxValue / 2 ? -1 : dp[m, n-1, 0];
    }
    // BFS + bit mask as val + direction key
    public int MinimumMoves(int[][] grid) {
        int m = grid.Length, n = grid[0].Length, ans = 0;
        // tail is init pos
        var q = new Queue<(int,int,int)>();
        q.Enqueue((0,0,0)); // 0: h, 1: v
        Func<int,int,int,bool> goDown = (x,y,dir) => {
            if (dir == 0) return x+1 < m && y+1 < n && grid[x+1][y] == 0 && grid[x+1][y+1] == 0;
            else return x+2 < m && grid[x+2][y] == 0;
        };
        Func<int,int,int,bool> goRight = (x,y,dir) => {
            if (dir == 0) return y+2 < n && grid[x][y+2 ] == 0;
            else return x+1 < m && y+1 < n && grid[x][y+1] == 0 && grid[x+1][y+1] == 0;
        };
        Func<int,int,int,bool> canRotate = (x,y,dir) => {
            return x+1 < m && y+1 < n && grid[x][y+1] == 0 && grid[x+1][y] == 0 && grid[x+1][y+1] == 0;
        };
        while(q.Any()) {
            int size = q.Count;
            while (--size >=0) {
                var t = q.Dequeue();
                int i = t.Item1, j = t.Item2, dir = t.Item3;
                if (i == m - 1 && j == n - 2 && dir == 0) return ans;
                // grid val|dir as visited
                // encode grid.val as 1|1|val, 2 bit: v, 1 bit: h, 0 bit: val
                if ((grid[i][j] & (1 << (dir + 1))) != 0) continue;
                grid[i][j] |= 1 << (dir + 1); // visited
                if(goDown(i,j,dir)) q.Enqueue((i+1,j,dir));
                if(goRight(i,j,dir)) q.Enqueue((i,j+1,dir));
                if(canRotate(i,j,dir)) q.Enqueue((i,j,dir^1));
            }
            ans++;
        }
        return -1;
    }
}
