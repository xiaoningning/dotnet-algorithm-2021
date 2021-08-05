public class Solution {
    public int FindMin1(int[] nums) {
        int l = 0, r = nums.Length - 1;
        while (l < r) {
            int m = l + (r - l) / 2;
            if (nums[m] > nums[r]) l = m + 1;
            else if (nums[m] < nums[r]) r = m;
            else r--; // nums[m] == nums[r]
        }
        return nums[r];
    }
    // Divide-Conquer
    // no need to consider duplicated num
    public int FindMin(int[] nums) {
        return FindMin(nums, 0, nums.Length - 1);
    }
    int FindMin(int[] nums, int s, int e) {
        if (s == e) return nums[s];
        // sorted case
        if (nums[s] < nums[e]) return nums[s];
        int m = s + (e - s) / 2;
        return Math.Min(FindMin(nums, s, m), FindMin(nums, m+1, e));
    }
}
