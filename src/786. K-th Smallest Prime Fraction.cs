public class Solution {
    public int[] KthSmallestPrimeFraction(int[] arr, int k) {
        int n = arr.Length;
        double l = (double) arr[0] / arr[n -1], r = 1;
        int x = 0, y = 1;
        while (l < r) {
            double m = l + (r - l) / 2.0;
            int cnt = 0;
            x = 0;
            for (int i = 0; i < n; i++) {
                int j = i + 1;
                // j: [i, n) big -> small
                // avoid missing any small
                // if j: [n-1, i), it could miss small one.
                while (j < n && arr[i] > arr[j] * m) j++;
                cnt += n - j;
                // x, y is the last one when cnt is biggest
                if (j < n && x * arr[j] < arr[i] * y ) {
                    x = arr[i]; y = arr[j];
                }
            }
            if (cnt == k) return new int[]{x, y};
            else if (cnt < k) l = m; // m is double
            else r = m;
        }
        return new int[]{};
    }
}
