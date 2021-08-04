public class Solution {
    // media = (max(L1, L2) + min(R1,R2)) / 2
    // T: O(log(min(n1, n2)))
    public double FindMedianSortedArrays(int[] nums1, int[] nums2) {
        int n1 = nums1.Length, n2 = nums2.Length;
        // the longer as nums1
        if (n1 < n2) return FindMedianSortedArrays(nums2, nums1);
        // nums2 is empty, media must be in the longer nums
        if (n2 == 0) return (nums1[(n1 - 1) / 2] + nums1[n1 / 2]) * 0.5;
        int l = 0, r = n2 * 2;
        // find (L2, R2) in nums2
        while (l <= r) {
            int m2 = l + (r - l) / 2;
            int m1 = n1 + n2 - m2;
            int L1 = m1 <= 0 ? Int32.MinValue : nums1[(m1 - 1) / 2];
            int L2 = m2 <= 0 ? Int32.MinValue : nums2[(m2 - 1) / 2];
            int R1 = m1 == n1 * 2 ? Int32.MaxValue : nums1[m1 / 2];
            int R2 = m2 == n2 * 2 ? Int32.MaxValue : nums2[m2 / 2]; 
            if (L1 > R2) l = m2 + 1;
            else if (R1 < L2) r = m2 - 1;
            else return (Math.Max(L1, L2) + Math.Min(R1, R2)) * 0.5;
        }
        return -1;
    }
    
    public double FindMedianSortedArrays1(int[] nums1, int[] nums2) {
        int n1 = nums1.Length, n2 = nums2.Length;
        // n1 should <= n2 => idx of median should be in nums2
        // the shorter as nums1
        if (n1 > n2) return FindMedianSortedArrays(nums2, nums1);
        int k = (n1 + n2 + 1) / 2;
        int l = 0, r = n1;
        // find R1 in nums1
        while (l < r) {
            int mid1 = l + (r - l) / 2;
            int mid2 = k - mid1;
            // R1 < L2
            if (nums1[mid1] < nums2[mid2 - 1]) l = mid1 + 1;
            else r = mid1;
        }
        int m1 = l;
        int m2 = k - m1;
        Console.WriteLine($"{m1} {m2}");
        int c1 = Math.Max(m1 <= 0 ? Int32.MinValue : nums1[m1 - 1], 
                         m2 <= 0 ? Int32.MinValue : nums2[m2 - 1]);
        if ((n1 + n2) % 2 == 1) return c1;
        int c2 = Math.Min(m1 >= n1 ? Int32.MaxValue : nums1[m1], 
                         m2 >= n2 ? Int32.MaxValue : nums2[m2]);
        return (c1 + c2) / 2.0;
    }
}
