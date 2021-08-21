public class Solution {
    // DFS
    // Time complexity: O(n!)
    // Space complexity: O(n)
    public IList<IList<int>> Permute1(int[] nums) {
        int n = nums.Length;
        var ans = new List<IList<int>>();
        var seen = new HashSet<int>();
        Action<List<int>> DFS = null;
        DFS = (tmp) => {
            if (tmp.Count == n) { ans.Add(new List<int>(tmp));  return; }
            foreach (int n in nums) {
                if (seen.Contains(n)) continue;
                seen.Add(n);
                tmp.Add(n);
                DFS(tmp);
                seen.Remove(n);
                tmp.Remove(n);
            }
        };
        
        DFS(new List<int>());
        return ans;
    }
    // Recursion
    public IList<IList<int>> Permute(int[] nums) {
        var ans = new List<IList<int>>();
        int n = nums.Length;
        if (n == 0)  { ans.Add(new List<int>()); return ans; }
        int first = nums[0];
        int[] tmp = new int[n-1];
        Array.Copy(nums, 1, tmp, 0, n - 1);
        var res = Permute(tmp);
        foreach (var lst in res) {
            for (int i = 0; i <= lst.Count; i++) {
                lst.Insert(i, first);
                ans.Add(new List<int>(lst));
                lst.RemoveAt(i);
            }
        }
        return ans;
    }
}
