public class Solution {
    public int[][] FloodFill(int[][] image, int sr, int sc, int newColor) {
        int m = image.Length, n = image[0].Length;
        int[,] dirs = new int[4,2]{{0,1}, {1,0}, {-1,0}, {0,-1}};
        var q = new Queue<int>();
        int color = image[sr][sc];
        // handle no color change case
        if (color != newColor) q.Enqueue(sr * n + sc);
        while (q.Any()) {
            var t = q.Dequeue();
            int tr = t / n, tc = t % n;
            image[tr][tc] = newColor;
            for (int d = 0; d < 4; d++) {
                int nr = tr + dirs[d,0], nc = tc + dirs[d,1];
                if (nr < 0 || nr >= m || nc < 0 || nc >= n || image[nr][nc] != color) continue;
                q.Enqueue(nr * n + nc);
            }
        }
        return image;
    }
}
