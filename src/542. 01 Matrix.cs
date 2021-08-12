public class Solution {
    // matix scan
    public int[][] UpdateMatrix(int[][] mat) {
        int m = mat.Length, n = mat[0].Length;
        int[][] ans = new int[m][];
        for (int i = 0; i < m; i++) { 
            ans[i] = new int[n]; 
            Array.Fill(ans[i], m*n);
        }
        for (int i = 0; i < m; i++) {
            for (int j = 0; j < n; j++) {
                if (mat[i][j] == 0) ans[i][j] = 0;
                else {
                    if (i > 0) ans[i][j] = Math.Min(ans[i][j], ans[i-1][j] + 1);
                    if (j > 0) ans[i][j] = Math.Min(ans[i][j], ans[i][j - 1] + 1);
                }
            }
        }
        // reverse scan
        for (int i = m - 1; i >= 0; i--) {
            for (int j = n - 1; j >= 0; j--) {
                if (ans[i][j] == 0) continue;
                if (i < m - 1) ans[i][j] = Math.Min(ans[i][j], ans[i + 1][j] + 1);
                if (j < n - 1) ans[i][j] = Math.Min(ans[i][j], ans[i][j + 1] + 1);
            }
        }
        return ans;
    }
    // BFS
    public int[][] UpdateMatrix1(int[][] mat) {
        int m = mat.Length, n = mat[0].Length;
        var dirs = new int[4,2]{{1,0},{-1,0},{0,1},{0,-1}};
        var q = new Queue<(int, int)>();
        for (int i = 0; i < m; i++) {
            for (int j = 0; j < n; j++) {
                if (mat[i][j] == 0) q.Enqueue((i,j));
                else mat[i][j] = m*n;
            }
        }
        // BFS from 0 to count
        while (q.Any()) {
            var t = q.Dequeue();
            int x = t.Item1, y = t.Item2;
            for (int d = 0; d < 4; d++) {
                int i = x + dirs[d, 0], j = y + dirs[d, 1];
                if (i < 0 || i >= m || j < 0 || j >= n || mat[x][y] >= mat[i][j]) continue;
                mat[i][j] = mat[x][y] + 1;
                q.Enqueue((i,j));
            }
        }
        return mat;
    }
}
