public class Solution {
    // O(n^2)
    public int SmallestDistancePair(int[] nums, int k) { 
        int n = nums.Length;
        int[] cnt = new int[1000000];
        for (int i = 0; i < n; ++i) {
            for (int j = i + 1; j < n; ++j) {
                ++cnt[Math.Abs(nums[i] - nums[j])];
            }
        }
        for (int i = 0; i < 1000000; i++) {
            if (cnt[i] >= k) return i;
            k -= cnt[i];
        }
        return -1;
    }
    
    // T: O(nlog(max(nums)))
    public int SmallestDistancePair1(int[] nums, int k) {
        int l = 0, r = nums.Max() - nums.Min() + 1, n = nums.Length;
        // sorted nums avoid to duplicate distance calculation
        Array.Sort(nums);
        while (l < r) {
            int m = l + (r - l) / 2;
            int cnt = 0;
            int t = 0;
            for (int i = 0; i < n; i++) {
                int j = i+1;
                while (j < n && nums[j] - nums[i] <= m) {
                    j++;
                    cnt++;
                }
            }
            if (cnt < k) l = m + 1;
            else r = m;
        }
        return r;
    }
}
