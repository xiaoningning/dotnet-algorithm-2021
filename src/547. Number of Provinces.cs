public class Solution {
    int[] visited;
    public int FindCircleNum(int[][] isConnected) {
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
