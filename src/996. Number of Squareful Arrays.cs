public class Solution {
    // DFS
    // T: O(n!)
    public int NumSquarefulPerms(int[] nums) {
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
}
