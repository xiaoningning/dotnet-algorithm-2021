public class Solution {
    // DFS
    // Time complexity: O(n!)
    // Space complexity: O(n + k)
    public IList<IList<int>> PermuteUnique(int[] nums) {
        int n = nums.Length;
        // sorted first to avoid duplicates
        Array.Sort(nums);
        var ans = new List<IList<int>>();
        int[] used = new int[n];
        Action<List<int>> DFS = null;
        DFS = (tmp) => {
            if (tmp.Count == n) { ans.Add(new List<int>(tmp));  return; }
            for (int i = 0; i < n; i++) {
                if (used[i] == 1) continue;
                // the same number can be only used once at each depth.
                // used[i - 1] is reset as 0 in the previous DFS level  
                if (i > 0 && nums[i] == nums[i - 1] && used[i - 1] == 0) continue;
                used[i] = 1;
                tmp.Add(nums[i]);
                DFS(tmp);
                used[i] = 0;
                tmp.RemoveAt(tmp.Count - 1);
            }
        };
        
        DFS(new List<int>());
        return ans;
    }
}
