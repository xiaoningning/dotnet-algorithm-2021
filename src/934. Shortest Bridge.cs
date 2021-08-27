public class Solution {
    // multi source, multi destination, shortest path problem
    // BFS (shortest path) DFS (find an island)
    // T: O(mn)
    public int ShortestBridge(int[][] grid) {
        var dirs = new int[,]{{1,0},{-1,0},{0,1},{0,-1}};
        int m = grid.Length, n = grid[0].Length;
        var q = new Queue<(int, int)>();
        
        Action<int,int> DFS = null;
        DFS = (sx, sy) => {
            if (sx < 0 || sx >= m || sy < 0 || sy >= n || grid[sx][sy] != 1) return;
            grid[sx][sy] = 2;
            q.Enqueue((sx, sy));
            for (int d = 0; d < 4; d++) DFS(sx + dirs[d,0], sy + dirs[d,1]);
        };
        bool found = false;
        for (int i = 0; i < m && !found; i++) for (int j = 0; j < n && !found; j++) if (grid[i][j] == 1) { DFS(i,j); found = true; }
        int steps = 0;
        // BFS
        while (q.Any()) {
            int size = q.Count;
            while (--size >= 0) {
                var t = q.Dequeue();
                int x = t.Item1, y = t.Item2;
                for (int d = 0; d < 4; d++) {
                    int i = x + dirs[d,0], j = y + dirs[d,1];
                    if (i < 0 || i >= m || j < 0 || j >= n || grid[i][j] == 2) continue;
                    if (grid[i][j] == 1) return steps;
                    q.Enqueue((i,j));
                    grid[i][j] = 2;
                }
            }
            steps++;
        }
        return -1;
    }
}
