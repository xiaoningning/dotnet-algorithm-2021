public class Solution {
    // monotonic stack
    // T: O(n * logn)
    public int MaxChunksToSorted(int[] arr) {
        int n = arr.Length;
        var st = new HashSet<int>();
        for (int i = 0; i < n; i++) {
            if (!st.Any() || st.Last() < arr[i]) st.Add(arr[i]);
            else {
                int curMx = st.Last();
                // remove all st.Last() > arr[i] since they must be in one chunk
                while (st.Any() && st.Last() > arr[i]) st.Remove(st.Last());
                st.Add(curMx);
            }
        }
        return st.Count;
    }
    // T: O(n)
    public int MaxChunksToSorted1(int[] arr) {
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
