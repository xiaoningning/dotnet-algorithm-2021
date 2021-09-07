public class Solution {
    // DP v1
    // T: O(n) S: O(n)
    public int MinSwap2(int[] nums1, int[] nums2) {
        int n = nums1.Length;
        int[] noSwap = new int[n], swap = new int[n];
        Array.Fill(noSwap, n+1);
        Array.Fill(swap, n+1);
        noSwap[0] = 0; swap[0] = 1;
        for (int i = 1; i < n; i++) {
            if (nums1[i] > nums1[i-1] && nums2[i] > nums2[i-1]) {
                noSwap[i] = noSwap[i-1];
                swap[i] = swap[i-1] + 1;
            }
            if (nums1[i] > nums2[i-1] && nums2[i] > nums1[i-1]) {
                noSwap[i] = Math.Min(swap[i-1], noSwap[i]);
                swap[i] = Math.Min(noSwap[i-1] + 1, swap[i]);
            }
        }
        return Math.Min(noSwap[n-1], swap[n-1]);
    }
    // DP v2 save space
    // T: O(n) S: O(1)
    public int MinSwap(int[] nums1, int[] nums2) {
        int n = nums1.Length, noSwap = 0, swap = 1;
        for (int i = 1; i < n; i++) {
            // need to reset cur swap/noswap to max
            // both case 1/2 could happen at the same i
            // or just one of them happens
            int curSwap = n+1, curNoSwap = n+1;
            if (nums1[i] > nums1[i-1] && nums2[i] > nums2[i-1]) {
                curNoSwap = noSwap;
                curSwap = swap + 1;
            }
            if (nums1[i] > nums2[i-1] && nums2[i] > nums1[i-1]) {
                curNoSwap = Math.Min(swap, curNoSwap);
                curSwap = Math.Min(noSwap + 1, curSwap);
            }
            noSwap = curNoSwap; swap = curSwap;
        }
        return Math.Min(noSwap, swap);
    }
    // DFS => TLE
    // T: O(2^n)
    public int MinSwap1(int[] nums1, int[] nums2) {
        int n = nums1.Length, ans = n + 1; 
        Action<int[], int[], int> Swap = (a1, a2, i) => {int t = a1[i]; a1[i] = a2[i]; a2[i] = t;};
        Action<int, int> DFS = null;
        DFS = (i, cnt) => {
            if (cnt >= ans) return;
            if (i == n) { ans = Math.Min(ans, cnt); return; }
            if (i == 0 || (nums1[i] > nums1[i-1] && nums2[i] > nums2[i-1])) DFS(i+1, cnt);
            if (i == 0 || (nums1[i] > nums2[i-1] && nums2[i] > nums1[i-1]))  { 
                Swap(nums1, nums2, i); 
                DFS(i+1, ++cnt);
                Swap(nums1, nums2, i); // swap back to search all
            }
        };
        DFS(0,0);
        return ans;
    }
}
