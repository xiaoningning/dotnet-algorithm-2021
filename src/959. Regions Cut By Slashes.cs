public class Solution {
    // each unit is divided into 3
    // 1: \\ or /; 0: ""
    // number of islands: 0 is island
    public int RegionsBySlashes(string[] grid) {
        int n = grid.Length;
        int[,] nums = new int[3*n, 3*n];
        for (int r = 0; r < n; r++) {
            for (int c = 0; c < n; c++) {
                if (grid[r][c] == '/') {
                    nums[3 * r + 0, 3 * c + 2] = 1;
                    nums[3 * r + 1, 3 * c + 1] = 1;
                    nums[3 * r + 2, 3 * c + 0] = 1;
                }
                else if (grid[r][c] == '\\') {
                    nums[3 * r + 0, 3 * c + 0] = 1;
                    nums[3 * r + 1, 3 * c + 1] = 1;
                    nums[3 * r + 2, 3 * c + 2] = 1;
                }
                // "" is 0
            }
        }
        int ans = 0;
        // "0" is island
        for (int i = 0; i < nums.GetLength(0); i++) {
            for (int j = 0; j < nums.GetLength(1); j++) {
                if (nums[i,j] == 1) continue;
                DFS(nums, i, j);
                ans++;
            }
        }
        return ans;
    }
    void DFS(int[,] nums, int i, int j) {
        if (i < 0 || i >= nums.GetLength(0) || j < 0 || j >= nums.GetLength(1) || nums[i,j] == 1) return;
        nums[i, j] = 1;
        DFS(nums, i + 1, j);
        DFS(nums, i - 1, j);
        DFS(nums, i, j + 1);
        DFS(nums, i, j - 1);
    }
}
