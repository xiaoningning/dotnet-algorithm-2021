public class Solution {
    // T: O(logn)
    public int[] SearchRange(int[] nums, int target) {
        int left = FirstPos(nums, target);
        int right = LastPos(nums, target);
        return new int[]{left, right};
    }
    int FirstPos(int[] nums, int t) {
        int l = 0, r = nums.Length;
        while (l < r) {
            int m = l + (r - l) / 2;
            if (nums[m] >= t) r = m;
            else l = m + 1;
        }
        if (l == nums.Length || nums[l] != t) return -1;
        else return l;
    }
    int LastPos(int[] nums, int t) {
        int l = 0, r = nums.Length;
        while (l < r) {
            int m = l + (r - l) / 2;
            if (nums[m] <= t) l = m + 1;
            else r = m ;
        }
        --l;
        if (l < 0 || nums[l] != t) return -1;
        else return l;
    }
    
    // T: O(logn) -> O(n)
    public int[] SearchRange1(int[] nums, int target) {
        int l = 0, r = nums.Length, position = -1;
        while (l < r) {
            int m = l + (r - l) / 2;
            if (nums[m] == target) { 
                position = m; 
                break;
            }
            else if (nums[m] < target) l = m + 1;
            else r = m;
        }
        // not found
        if (position == -1) return new int[]{-1, -1};
        // can be O(n)
        int i = position, j = position;
        while (i >= 0 && nums[i] == target) i--;
        while (j < nums.Length && nums[j] == target) j++;
        
        return new int[]{++i,--j};
    }
}
