public class Solution {
    // hashtable
    public int SubarraySum(int[] nums, int k) {
        int sum = 0, ans = 0;
        var d = new Dictionary<int,int>(){ [0] = 1 };
        foreach (int n in nums) {
            sum += n;
            if (d.ContainsKey(sum - k)) ans += d[sum - k];
            if (!d.ContainsKey(sum)) d[sum] = 0;
            d[sum]++;
        }
        // O(n)
        return ans;
    }
    
    // brute force
    public int SubarraySum1(int[] nums, int k) {
        int n = nums.Length, ans = 0;
        int[] sums = new int[n+1];
        for (int i = 1; i < n+1; i++) sums[i] = nums[i-1] + sums[i-1];
        for (int i = 1; i < n+1; i++) {
            for (int j = 0; j < i; j++) {
                if (sums[i] - sums[j] == k) ans++;
            }
        }
        // O(n^2)
        return ans;
    }
}
