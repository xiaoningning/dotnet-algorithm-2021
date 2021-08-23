public class Solution {
    // DFS
    // no duplicated subsets
    public IList<IList<int>> SubsetsWithDup(int[] nums) {
        var ans = new List<IList<int>>();
        Array.Sort(nums);
        Action<int, List<int>> DFS = null;
        DFS = (start, tmp) => {
            // subsets => any size of tmp
            ans.Add(new List<int>(tmp));
            for (int i = start; i < nums.Length; i++)  {
                if (i > start && nums[i - 1] == nums[i]) continue;
                tmp.Add(nums[i]);
                DFS(i + 1, tmp);
                tmp.Remove(nums[i]);
            }
        };
        DFS(0, new List<int>());
        return ans;
    }
}
