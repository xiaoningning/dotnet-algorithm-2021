public class Solution {
    // O(n*m)
    // Check 1 area first and find 0 being neighbored by 1
    public int LargestIsland(int[][] grid) {
        int m = grid.Length, n = grid[0].Length;
        int ans = -1;
        // color -> area
        int[] colors = new int[(m +1)*(n + 1)];
        int clr = 1;
        // find 1 area
        for (int i = 0; i < m; i++) {
            for (int j = 0; j < n; j++) {
                if (grid[i][j] == 1) {
                    clr++;
                    colors[clr] = DFSColor(grid, i, j, clr);
                    ans = Math.Max(ans, colors[clr]);
                }    
            }
        }
        // find 0 to be changed 
        for (int i = 0; i < m; i++) {
            for (int j = 0; j < n; j++) {
                if (grid[i][j] == 0) {
                    int cnt = 1;
                    int c1 = GetColor(grid, i + 1, j); 
                    int c2 = GetColor(grid, i - 1, j); 
                    int c3 = GetColor(grid, i, j + 1); 
                    int c4 = GetColor(grid, i, j - 1);
                    foreach (int c in new HashSet<int>(){c1, c2, c3, c4}) cnt += colors[c];
                    ans = Math.Max(ans, cnt);
                }    
            }
        }
        return ans;
    }
    int GetColor(int[][] grid, int i, int j) {
        int m = grid.Length, n = grid[0].Length;
        return (i < 0 || i >= m || j < 0 || j >= n) ? 0 : grid[i][j]; 
    }
    int DFSColor (int[][] grid, int i, int j, int color) {
        int m = grid.Length, n = grid[0].Length;
        // grid[i][j] != 1 => not 0 , not color
        if (i < 0 || i >= m || j < 0 || j >= n || grid[i][j] != 1) return 0;
        grid[i][j] = color;
        return DFSColor(grid, i + 1, j, color)
                + DFSColor(grid, i - 1, j, color)
                + DFSColor(grid, i, j + 1, color)
                + DFSColor(grid, i, j - 1, color)
                + 1;
    }
    
    // TLE due to check 0 one by one
    public int LargestIsland1(int[][] grid) {
        int m = grid.Length, n = grid[0].Length;
        int ans = -1;
        for (int i = 0; i < m; i++) {
            for (int j = 0; j < n; j++) {
                if (grid[i][j] == 1) continue;
                grid[i][j] = 1;
                ans = Math.Max(ans, DFS(grid, i, j, new int[m, n]));
                if (ans == m * n) return ans;
                grid[i][j] = 0;
            }
        }
        // it could have no 0
        return ans < 0 ? m * n : ans;
    }
    int DFS (int[][] grid, int i, int j, int[,] visited) {
        int m = grid.Length, n = grid[0].Length;
        if (i < 0 || i >= m || j < 0 || j >= n || grid[i][j] == 0 || visited[i, j] == 1) return 0;
        visited[i,j] = 1;
        return DFS(grid, i + 1, j, visited)
                + DFS(grid, i - 1, j, visited)
                + DFS(grid, i, j + 1, visited)
                + DFS(grid, i, j - 1, visited)
                + 1;
    }
}
