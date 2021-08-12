public class Solution {
    // matrix scan
    // 542. 01 matrix
    public int MaxDistance(int[][] grid) {
        int m = grid.Length, n = grid[0].Length;
        for (int i = 0; i < m; i++) {
            for (int j = 0; j < n; j++) {
                if (grid[i][j] == 1) continue;
                grid[i][j] = m*n;
                // record dist in grid 0 cell
                if (i > 0) grid[i][j] = Math.Min(grid[i][j], grid[i-1][j] + 1);
                if (j > 0) grid[i][j] = Math.Min(grid[i][j], grid[i][j - 1] + 1);
            }
        }
        int ans = 0;
        // reverse scan
        for (int i = m - 1; i >= 0; i--) {
            for (int j = n - 1; j >= 0; j--) {
                if (grid[i][j] == 1) continue;
                if (i < m - 1) grid[i][j] = Math.Min(grid[i][j], grid[i + 1][j] + 1);
                if (j < n - 1) grid[i][j] = Math.Min(grid[i][j], grid[i][j + 1] + 1);
                ans = Math.Max(ans, grid[i][j]);
            }
        }
        // no land case => res == m* n
        // no water case => res == 0
        return ans == m*n ? -1 : ans - 1;
    }
    // BFS
    public int MaxDistance1(int[][] grid) {
        int m = grid.Length, n = grid[0].Length;
        var q = new Queue<(int,int)>();
        for (int i = 0; i < m; i++) {
            for (int j = 0; j < n; j++) {
                if (grid[i][j] == 1) q.Enqueue((i, j)); 
            }
        }
        // no 1 or no 0 case
        if (!q.Any() || q.Count == m*n) return -1;
        var dirs = new int[4,2]{{1,0},{-1,0},{0,1},{0,-1}};
        int ans = 0;
        while (q.Any()) {
            ans++;
            int size = q.Count;
            while (--size >= 0) {
                var t = q.Dequeue();
                int x = t.Item1, y = t.Item2;
                for (int d = 0; d < 4; d++) {
                    int i = x + dirs[d, 0], j = y + dirs[d, 1];
                    if (i < 0 || i >= m || j < 0 || j >= n || grid[i][j] != 0) continue;
                    grid[i][j] = ans; // visited
                    q.Enqueue((i, j));
                }
            }
        }
        return ans - 1;
    }
    // DFS
    public int MaxDistance2(int[][] grid) {
        int m = grid.Length, n = grid[0].Length;
        var q = new Queue<(int,int)>();
        for (int i = 0; i < m; i++) {
            for (int j = 0; j < n; j++) {
                if (grid[i][j] == 1) {
                    // start from edge of island
                    grid[i][j] = 0;
                    DFS(grid, i, j, 1);
                }
            }
        }
        int ans = -1;
        for (int i = 0; i < m; i++)
            for (int j = 0; j < n; j++)
                if (grid[i][j] > 1) ans = Math.Max(ans, grid[i][j] - 1);
        return ans;
    }
    void DFS(int[][] grid, int i, int j, int dist) {
        int m = grid.Length, n = grid[0].Length;
        // only update largest dist or edge of island
        if (i < 0 || i >= m || j < 0 || j >= n || (grid[i][j] != 0 && grid[i][j] <= dist)) return;
        grid[i][j] = dist;
        DFS(grid, i + 1, j, dist + 1);
        DFS(grid, i - 1, j, dist + 1);
        DFS(grid, i, j + 1, dist + 1);
        DFS(grid, i, j - 1, dist + 1);
    }
}
