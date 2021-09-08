public class Solution {
    // DP 
    // T: O(n^2)
    public int FindNumberOfLIS(int[] nums) {
        int n = nums.Length;
        if (n == 0) return 0;
        // len: length of LIS at i
        // cnt: # of length of LIS at i
        int[] len = new int[n], cnt = new int[n];
        Array.Fill(len, 1); Array.Fill(cnt, 1);
        for (int i = 1; i < n; i++) {
            for (int j = 0; j < i; j++) {
                if (nums[i] <= nums[j]) continue; 
                if (len[i] == len[j] + 1) cnt[i] += cnt[j];
                else if (len[i] < len[j] + 1) {
                    len[i] = len[j] + 1;
                    cnt[i] = cnt[j];
                }
            }
        }
        int ans = 0, mx = len.Max();
        for (int i = 0; i < n; i++) if (mx == len[i]) ans += cnt[i];
        return ans;
    }
}
