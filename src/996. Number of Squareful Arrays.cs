public class Solution {
    // DFS
    // T: O(n!)
    public int NumSquarefulPerms1(int[] nums) {
        int n = nums.Length;
        int[] used = new int[n];
        // avoid duplication
        // perm1[i] != perm2[i]
        Array.Sort(nums);
        int ans = 0;
        
        Action<List<int>> DFS = null;
        DFS = (lst) => {
            if (lst.Count == nums.Length) { ans++; return;}
            for (int i = 0; i < n; i++) {
                if (used[i] == 1) continue;
                // avoid duplication
                if (i > 0 && used[i - 1] == 0 && nums[i - 1] == nums[i]) continue;
                // prune invalid permutation
                if (lst.Any() && !isSquareful(lst.Last(), nums[i])) continue;
                used[i] = 1;
                lst.Add(nums[i]);
                DFS(lst);
                used[i] = 0;
                lst.RemoveAt(lst.Count - 1);
            }
        };
        
        DFS(new List<int>());
        return ans;
    }
    bool isSquareful(int x, int y) {
        int s = (int)Math.Sqrt(x + y);
        return s * s == x + y;
    }
    /** 
    dp[s][i] := # of ways to reach state s (binary mask of nodes visited) that ends with node i
    dp[s | (1 << j)][j] += dp[s][i] if g[i][j]
    Time complexity: O(n^2*2^n)
    Space complexity: O(2^n)
    */
    // DP
    public int NumSquarefulPerms(int[] nums) {
        int n = nums.Length;
        // avoid duplication
        // perm1[i] != perm2[i]
        Array.Sort(nums);
        // g[i,j] = 1 if squareful
        int[,] g = new int[n,n];
        for (int i = 0; i < n; i++) 
            for (int j = 0; j < n; j++)
                if (i != j && isSquareful(nums[i], nums[j])) g[i,j] = 1;
        
        int[,] dp = new int[1 << n, n];
        // itself as the start point
        // For the same numbers, only the first one can be the starting point.
        for (int i = 0; i < n; i++) 
            if (i == 0 || nums[i] != nums[i - 1]) 
                dp[1 << i, i] = 1;
        
        for (int s = 0; s < 1 << n; s++) {
            for (int i = 0; i < n; i++) {
                if (dp[s, i] == 0) continue;
                for (int j = 0; j < n; j++) {
                    // prune invalid or visted j
                    if (g[i,j] != 1 || (s & (1 << j)) != 0) continue;
                    // avoid duplication
                    // Only the first one can be used as the dest.
                    if (j > 0 && (s & (1 << (j - 1))) == 0 && nums[j] == nums[j -1]) continue;
                    dp[s | (1 << j), j] += dp[s, i];
                }
            }
        }
        // final state: (1 << n) - 1
        // all nums are used
        int ans = 0;
        for (int i = 0; i < n; i++) ans += dp[(1 << n) - 1, i];
        return ans;
    }
}
