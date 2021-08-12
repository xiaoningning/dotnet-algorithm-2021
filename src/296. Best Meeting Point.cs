public class Solution {
    //  Manhattan Distance:  distance(p1, p2) = |p2.x - p1.x| + |p2.y - p1.y|.
    public int MinTotalDistance(int[][] grid) {
        int m = grid.Length, n = grid[0].Length;
        List<int> rows = new List<int>(), cols = new List<int>();
        for (int i = 0; i < m; i++) {
             for (int j = 0; j < n; j++) {
                 if (grid[i][j] == 1)  {
                     rows.Add(i); cols.Add(j);
                 }
             }
        }
        int ans = 0;
        rows.Sort(); cols.Sort();
        int x = 0, y = rows.Count - 1;
        while (x < y) ans += rows[y] - rows[x] + cols[y--] - cols[x++];
        return ans;
    }
}
