public class Solution {
    // BFS
    public int NumIslands(char[][] grid) {
        if (grid.Length == 0) return 0;
        int m = grid.Length, n = grid[0].Length;
        int ans = 0;
        var dirs = new int[4,2] {{0,1},{0,-1},{1,0},{-1,0}};
        for (int i = 0; i < m; i++) {
            for (int j = 0; j < n; j++) {
                if (grid[i][j] == '0') continue;
                ans++;
                var q = new Queue<(int, int)>();
                q.Enqueue((i,j));
                while (q.Any()) {
                    var t = q.Dequeue();
                    int a = t.Item1, b = t.Item2;
                    grid[a][b] = '0'; // visited
                    for (int d = 0; d < 4; d++) {
                        int x = a + dirs[d, 0], y = b + dirs[d, 1];
                        if (x < 0 || x >= grid.Length || y < 0 || y >= grid[0].Length || grid[x][y] == '0') continue;
                        grid[x][y] = '0';
                        q.Enqueue((x,y));
                    }
                }
            }
        }
        // T: O(m*n)
        return ans;
    }
    // DFS
    public int NumIslands1(char[][] grid) {
        if (grid.Length == 0) return 0;
        int m = grid.Length, n = grid[0].Length;
        int ans = 0;
        for (int i = 0; i < m; i++) {
            for (int j = 0; j < n; j++) {
                if (grid[i][j] == '0') continue;
                ans++;
                DFS(grid, i, j);
            }
        }
        // T: O(m*n)
        return ans;
    }
    void DFS (char[][] grid, int i, int j) {
        if (i < 0 || i >= grid.Length || j < 0 || j >= grid[0].Length || grid[i][j] == '0') return;
        grid[i][j] = '0'; // visited
        // four directions
        DFS(grid, i+1, j);
        DFS(grid, i-1, j);
        DFS(grid, i, j+1);
        DFS(grid, i, j-1);
    }
}
