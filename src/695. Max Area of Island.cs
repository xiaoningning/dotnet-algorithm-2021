public class Solution {
    public int MaxAreaOfIsland(int[][] grid) {
        int m = grid.Length, n = grid[0].Length;
        int ans = 0;
        for (int i = 0; i < m; i++) {
            for (int j = 0; j < n; j++) {
                if (grid[i][j] == 0) continue;
                ans = Math.Max(ans, DFS(grid, i, j));
            }
        }
        return ans;
    }
    int DFS(int[][] grid, int i, int j) {
        if (i < 0 || i >= grid.Length || j < 0 || j >= grid[0].Length || grid[i][j] == 0) return 0;
        grid[i][j] = 0; // visited
        int cnt = 1;
        cnt += DFS(grid, i + 1, j);
        cnt += DFS(grid, i - 1, j);
        cnt += DFS(grid, i, j + 1);
        cnt += DFS(grid, i, j - 1);
        return cnt;
    }
}
