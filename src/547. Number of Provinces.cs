public class Solution {
    // Union Find
    int[] roots;
    public int FindCircleNum(int[][] isConnected) {
        int n = isConnected.Length;
        roots = new int[n];
        int ans = 0;
        for (int i = 0; i < n; i++) roots[i] = i;
        for (int i = 0; i < n; i++) {
            for (int j = 0; j < n; j++) {
                int ri = UnionFind(roots, i);
                int ry = UnionFind(roots, j);
                if (isConnected[i][j] == 1) roots[ry] = ri;
            }
        }
        for (int i = 0; i < n; i++) if (roots[i] == i) ans++;
        // T: O(n^2 * logn)
        return ans;
    }
    int UnionFind(int[] roots, int x) {
        return roots[x] == x ? x : roots[x] = UnionFind(roots, roots[x]);
    }
    // DFS
    // T: O(n^2)
    int[] visited;
    public int FindCircleNum1(int[][] isConnected) {
        int n = isConnected.Length;
        int ans = 0;
        visited = new int[n];
        for (int i = 0; i < n; i++) {
            if (visited[i] == 0 ) {
                DFS(isConnected, i);
                ans++;
            }
        }
        return ans;
    }
    void DFS(int[][] grid, int i) {
        int n = grid.Length;
        visited[i] = 1;
        for (int j = 0; j < n; j++) {
            if (grid[i][j] == 1 && visited[j] == 0)  {
                visited[j] = 1;
                DFS(grid, j);
            }
        }
    }
}
