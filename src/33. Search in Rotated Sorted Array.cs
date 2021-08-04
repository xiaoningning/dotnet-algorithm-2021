public class Solution {
    // find the ordered side to search
    public int Search1(int[] nums, int target) {
        int l = 0, r = nums.Length - 1;
        while (l <= r) {
            int m = l + (r - l) / 2;
            //Console.WriteLine(nums[m]);
            if (target == nums[m]) return m;
            // m can = left, never = right due to int / 2
            if (nums[l] <= nums[m]) {
                // change r, so = on left
                if (nums[l] <= target && target < nums[m]) r = m - 1;
                else l = m + 1;
            }
            else {
                // change l, so = on right
                if (nums[m] < target && target <= nums[r]) l = m + 1;
                else r = m - 1;
            }
        }
        return -1;
    }
    
    public int Search(int[] nums, int target) {
        int l = 0, r = nums.Length - 1;
        while (l <= r) {
            int m = l + (r - l) / 2;
            //Console.WriteLine(nums[m]);
            if (target == nums[m]) return m;
            // m can = left, never = right due to int / 2
            if (nums[m] < nums[r]) {
                // change l, so = on right
                if (nums[m] < target && target <= nums[r]) l = m + 1;
                else r = m - 1;
            }
            else {
                // change r, so = on left
                if (nums[l] <= target && target < nums[m]) r = m - 1;
                else l = m + 1;
            }
        }
        return -1;
    }
}
