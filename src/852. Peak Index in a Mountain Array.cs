public class Solution {
    public int PeakIndexInMountainArray(int[] arr) {
        // guarantee a peak and go down, never out of boarder
        int l = 0, r = arr.Length;
        while (l < r) {
            int peak = l + (r - l) / 2;
            if (arr[peak] <= arr[peak + 1]) l = peak + 1;
            else r = peak;
        }
        return l;
    }
}
