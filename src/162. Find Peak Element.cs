public class Solution {
    public int FindPeakElement(int[] nums) {
        // peak can be last one, avoid out of boarder
        int l = 0, r = nums.Length - 1;
        while (l < r) {
            int peak = l + (r - l) / 2;
            if (nums[peak] <= nums[peak + 1]) l = peak +1;
            else r = peak;
        }
        return l;
    }
}
