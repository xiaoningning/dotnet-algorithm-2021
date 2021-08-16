public class Solution {
    // BFS
    // T: O(m*n*(2^6) at most 6 keys
    public int ShortestPathAllKeys(string[] grid) {
        int m = grid.Length, n = grid[0].Length, allKeys = 0;
        int[,] dirs = new int[4,2]{{1,0},{-1,0},{0,1},{0,-1}};
        var q = new Queue<(int, int, int)>();
        var seen = new HashSet<(int, int, int)>();
        for (int i = 0; i < m; i++) {
            for (int j = 0; j < n; j++) {
                if (grid[i][j] == '@') { 
                    q.Enqueue((i, j, 0)); 
                    seen.Add((i, j, 0));
                }
                if (grid[i][j] >= 'a' && grid[i][j] <= 'z') allKeys |= 1 << (grid[i][j] - 'a');
            }
        }
        int steps = 0;
        while (q.Any()) {
            int size = q.Count;
            while (--size >= 0) {
                var t = q.Dequeue();
                int x = t.Item1, y = t.Item2, k = t.Item3;
                if (k == allKeys) return steps;
                for (int d = 0; d < 4; d++) {
                    int i = x + dirs[d,0], j = y + dirs[d,1], nk = k;
                    if (i < 0 || i >= m || j < 0 || j >= n || grid[i][j] == '#') continue;
                    if (grid[i][j] >= 'A' && grid[i][j] <= 'Z' && (nk & 1 << (grid[i][j] - 'A')) == 0) continue;
                    if (grid[i][j] >= 'a' && grid[i][j] <= 'z') nk |= 1 << (grid[i][j] - 'a');
                    if (seen.Contains((i, j, nk))) continue;
                    seen.Add((i, j, nk));
                    q.Enqueue((i, j, nk));
                }
            }
            steps++;
        }
        return -1;
    }
}
