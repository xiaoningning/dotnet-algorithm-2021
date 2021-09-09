public class Solution {
    // DP v1
    public int FindLength1(int[] nums1, int[] nums2) {
        int n1 = nums1.Length, n2 = nums2.Length, ans = 0;
        int[,] dp = new int[n1+1,n2+1];
        for (int i = 1; i <= n1; i++)
            for (int j = 1; j <= n2; j++) {
                dp[i,j] = nums1[i-1] == nums2[j-1] ? dp[i-1,j-1] + 1 : 0;
                ans = Math.Max(ans, dp[i,j]);
            }
        return ans;
    }
    // DP v2
    public int FindLength2(int[] nums1, int[] nums2) {
        int n1 = nums1.Length, n2 = nums2.Length, ans = 0;
        if (n1 < n2) return FindLength(nums2, nums1);
        int[] dp = new int[n2+1];
        for (int i = 1; i <= n1; i++) {
            int[] t = (int[])dp.Clone();
            for (int j = 1; j <= n2; j++) {
                dp[j] = nums1[i-1] == nums2[j-1] ? t[j-1] + 1 : 0;
                ans = Math.Max(ans, dp[j]);
            }
        }
        return ans;
    }
    // DP v2.1 save space
    public int FindLength(int[] nums1, int[] nums2) {
        int n1 = nums1.Length, n2 = nums2.Length, ans = 0;
        if (n1 < n2) return FindLength(nums2, nums1);
        int[] dp = new int[n2+1];
        for (int i = 1; i <= n1; i++) {
            // reverse search j to avoid overwriting prev dp[j-1] at i -1
            // otherwise it needs temp [] to cache prev dp at i - 1
            for (int j = n2; j >= 1; j--) {
                dp[j] = nums1[i-1] == nums2[j-1] ? dp[j-1] + 1 : 0;
                ans = Math.Max(ans, dp[j]);
            }
        }
        return ans;
    }
}
