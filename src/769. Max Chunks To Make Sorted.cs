public class Solution {
    public int MaxChunksToSorted(int[] arr) {
        int n = arr.Length;
        int ans = 0;
        int mx = Int32.MinValue;
        for (int i = 0; i < n; i++) {
            mx = Math.Max(mx, arr[i]);
            // arr[i] is in [0... (arr.Length =1)]
            if (mx == i) ans++;
        }
        return ans;
    }
}
