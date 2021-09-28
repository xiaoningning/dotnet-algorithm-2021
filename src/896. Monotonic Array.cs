public class Solution {
    public bool IsMonotonic(int[] nums) {
        bool inc = true, dec = true;
        for (int i = 1; i < nums.Length; i++) {
            inc &= (nums[i-1] <= nums[i]);
            dec &= (nums[i-1] >= nums[i]);
            if (!inc && !dec) return false;
        }
        return true;
    }
}
