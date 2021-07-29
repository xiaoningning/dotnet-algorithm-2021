public class Solution {
    public int[] TwoSum(int[] nums, int target) {
        var d = new Dictionary<int,int>();
        for (int i = 0; i < nums.Length; i++) {
            int t = target - nums[i];
            if (d.ContainsKey(t)) return new int[]{d[t], i};
            d[nums[i]] = i;
        }
        return new int[]{};
    }
}
