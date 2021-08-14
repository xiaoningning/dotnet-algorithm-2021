public class Solution {
    // TLE
    int[] B = new int[2];
    int[] T = new int[2];
    int[] P = new int[2];
    public int MinPushBox(char[][] grid) {
        int m = grid.Length, n = grid[0].Length;
        int[,] dirs = new int[4,2]{{0,1},{0,-1},{1,0},{-1,0}};
        var q = new Queue<int[]>();
        for (int i = 0; i < m; i++) {
            for (int j = 0; j < n; j++) {
                if (grid[i][j] == 'S') P = new int[]{i,j};
                if (grid[i][j] == 'B') B = new int[]{i,j};
                if (grid[i][j] == 'T') T = new int[]{i,j};
            }
        }
        // player can only push box along the same direction
        // track box and player position
        // check if player can reach the node of the same directino along with target
        q.Enqueue(new int[]{B[0], B[1], P[0], P[1]});     
        int ans = 0;
        // track box and player position
        var visited = new int[m, n, m, n];
        while(q.Any()) {
            int size = q.Count;
            while (--size >= 0) {
                var c = q.Dequeue();
                int bx = c[0], by = c[1], px = c[2], py = c[3];
                B = new int[]{bx, by};
                visited[bx, by, px, py] = 1;
                for (int d = 0; d < 4; d++) {
                    int nbx = bx + dirs[d, 0], nby = by + dirs[d, 1];
                    int npx = bx - dirs[d, 0], npy = by - dirs[d, 1];
                    var nx = new int[]{nbx, nby, npx, npy};
                    if (!canPush(grid, c, nx) || visited[nbx, nby, bx, by] == 1) continue;
                    if (grid[nbx][nby] == 'T') return ans + 1;
                    q.Enqueue(new int[]{nbx, nby, bx, by});
                }
            }
            ans++;
        }
        return -1;
    }
    bool canPush(char[][] grid, int[] s, int[] nx) {
        int m = grid.Length, n = grid[0].Length;
        int i = nx[0], j = nx[1];
        if (i < 0 || i >= m || j < 0 || j >= n || grid[i][j] == '#') return false;
        int[,] seen = new int[m,n];
        //Console.WriteLine($"{x} - {dirx}: {y} - {diry}");
        return DFS(grid, new int[]{s[2],s[3]}, new int[]{nx[2],nx[3]}, seen);
    }
    
    bool DFS(char[][] grid, int[] s, int[] nx, int[,] seen) {
        int m = grid.Length, n = grid[0].Length;
        int x = s[0], y = s[1];
        // Console.WriteLine($"{x}:{y} {grid[x][y]}");
        if (x < 0 || x >= m || y < 0 || y >= n || grid[x][y] == '#' || seen[x, y] == 1) return false;
        if (x == B[0] && y == B[1]) return false;
        if (x == nx[0] && y == nx[1]) return true;
        seen[x,y] = 1;
        return DFS(grid, new int[]{x + 1, y}, nx, seen) 
            || DFS(grid, new int[]{x - 1, y}, nx, seen) 
            || DFS(grid, new int[]{x, y + 1}, nx, seen) 
            || DFS(grid, new int[]{x, y - 1}, nx, seen);
    }
}
