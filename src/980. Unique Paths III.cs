public class Solution {
    // DFS
    // T: O(4^(m*n))
    int[,] dirs = new int[4,2]{{1,0},{-1,0},{0,1},{0,-1}};
    int ans = 0;
    public int UniquePathsIII(int[][] grid) {
        int m = grid.Length, n = grid[0].Length;
        int cnt = 0;
        int sx = -1, sy = -1;
        
        for (int i = 0; i < m; i++) {
            for (int j = 0; j < n; j++) {
                if (grid[i][j] == 1) {sx = i; sy = j;}
                if (grid[i][j] == 0) cnt++;
            }
        }
        
        Action<int, int, int> DFS = null;
        DFS = (x, y, target) => {
            if (grid[x][y] == 2) {
                if (target == 0) ans++; 
            }
            else {
                grid[x][y] = -1;
                int m = grid.Length, n = grid[0].Length;
                for (int d = 0; d < 4; d++) {
                    int i = x + dirs[d, 0], j = y + dirs[d, 1];
                    if (i < 0 || i >= m || j < 0 || j >= n || grid[i][j] == -1) continue;
                    DFS(i, j, target - 1);
                }
                grid[x][y] = 0;
            }
        };
        
        DFS(sx, sy, cnt + 1);
        return ans;
    }
    void DFSx(int[][] grid, int x, int y, int target) {
        if (grid[x][y] == 2) {
            if (target == 0) ans++; 
            return;
        }
        grid[x][y] = -1;
        int m = grid.Length, n = grid[0].Length;
        for (int d = 0; d < 4; d++) {
            int i = x + dirs[d, 0], j = y + dirs[d, 1];
            if (i < 0 || i >= m || j < 0 || j >= n || grid[i][j] == -1) continue;
            DFSx(grid, i, j, target - 1);
        }
        grid[x][y] = 0;
    }
    
    // DP + bit mask + inline c# func
    // T: O(m*n* 2^(m* n)) is much slower than DFS
    public int UniquePathsIII2(int[][] grid) {
        int m = grid.Length, n = grid[0].Length;
        int sx = -1, sy = -1;
        // (i, j, state) => # of path
        // state => 1 : visited at (i, j), 0: not at (i, j)
        int[,,] memo = new int[m, n, 1 << m*n]; 
        for (int i = 0; i < m; i++) 
            for (int j = 0; j < n; j++)
                for (int k = 0; k < 1 << m*n; k++) memo[i,j,k] = -1;
        
        int state = 0;
        for (int i = 0; i < m; i++) {
            for (int j = 0; j < n; j++) {
                if (grid[i][j] == 1) {sx = i; sy = j;}
                if (grid[i][j] == 0 || grid[i][j] == 2) state |= 1 << (i * n + j);
            }
        }
        
        // c# inline Func recursion call
        Func<int, int, int, int> DFS = null;
        DFS = (x, y, state) => {
            if (memo[x, y, state] != -1) return memo[x, y, state];
            if (grid[x][y] == 2) return state == 0 ? 1 : 0;
            int path = 0;
            for (int d = 0; d < 4; d++) {
                int i = x + dirs[d, 0], j = y + dirs[d, 1];
                if (i < 0 || i >= m || j < 0 || j >= n || grid[i][j] == -1) continue;
                // (i, j) visited
                if ((state & (1 << (i * n + j))) == 0) continue;
                path += DFS(i, j, state ^ (1 << (i * n + j)));
            }
            return memo[x,y,state] = path;
        };
        return DFS(sx, sy, state);
    }
}
