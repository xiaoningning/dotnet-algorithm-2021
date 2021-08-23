public class Solution {
    // DFS
    // T: O(2^n), S: O(n)
    public IList<IList<int>> Subsets1(int[] nums) {
        var ans = new List<IList<int>>();
        Action<int, int, List<int>> DFS = null;
        DFS = (start, cnt, tmp) => {
            if (tmp.Count == cnt) { ans.Add(new List<int>(tmp)); return;}
            for (int i = start; i < nums.Length; i++)  {
                tmp.Add(nums[i]);
                DFS(i + 1, cnt, tmp);
                tmp.Remove(nums[i]);
            }
        };
        for (int n = 0; n <= nums.Length; n++) DFS(0, n, new List<int>());
        return ans;
    }
    // DFS v2
    public IList<IList<int>> Subsets2(int[] nums) {
        var ans = new List<IList<int>>();
        Action<int, List<int>> DFS = null;
        DFS = (start, tmp) => {
            // subsets => any size of tmp
            ans.Add(new List<int>(tmp));
            for (int i = start; i < nums.Length; i++)  {
                tmp.Add(nums[i]);
                DFS(i + 1, tmp);
                tmp.Remove(nums[i]);
            }
        };
        DFS(0, new List<int>());
        return ans;
    }
    // bit mask
    // T: O(n* 2^n), S: O(1)
    public IList<IList<int>> Subsets(int[] nums) {
        var ans = new List<IList<int>>();
        int state = 1 << nums.Length;
        for (int s = 0; s < state; s++) {
            var tmp = new List<int>();
            // state has i in bit mask
            for (int i = 0; i < nums.Length; i++) if ((s & 1 << i) > 0) tmp.Add(nums[i]);
            ans.Add(new List<int>(tmp));
        }
        return ans;
    }
}
