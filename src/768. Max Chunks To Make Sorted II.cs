public class Solution {
    // with forward and backward arrays
    // T: O(n)
    public int MaxChunksToSorted1(int[] arr) {
        int n = arr.Length, ans = 1;
        int[] forward = (int[]) arr.Clone();
        int[] backward = (int[]) arr.Clone();
        for (int i = 1; i < n; i++) forward[i] = Math.Max(arr[i], forward[i-1]);
        for (int i = n - 2; i >= 0; i--) backward[i] = Math.Min(arr[i], backward[i+1]);
        // each chunk, max of chunk < min of next chunk
        for (int i = 0; i < n - 1; i++) if (forward[i] <= backward[i+1]) ans++;
        return ans;
    }
    // monotonic stack
    // T: O(n * logn)
    // it can have duplicated ones
    public int MaxChunksToSorted(int[] arr) {
        int n = arr.Length;
        // keep max of each chunk
        var st = new Stack<int>();
        for (int i = 0; i < n; i++) {
            // different from Lc 769 Max Chunks To Make Sorted 
            // it can be the same
            if (!st.Any() || st.Peek() <= arr[i]) st.Push(arr[i]);
            else {
                int curMx = st.Pop();
                // remove all st.Last() > arr[i] since they must be in one chunk
                while (st.Any() && st.Peek() > arr[i]) st.Pop();
                st.Push(curMx);
            }
        }
        return st.Count;
    }
    // T: O(n * logn) // sorting O(nlogn)
    public int MaxChunksToSorted2(int[] arr) {
        int n = arr.Length, ans = 0, sum1 = 0, sum2 = 0;
        int[] expected = (int[]) arr.Clone();
        Array.Sort(expected);
        for (int i = 0; i < n; i++) {
            sum1 += arr[i];
            sum2 += expected[i];
            // each chunk sum should be the same 
            // before or after sort
            // sum of chunk should be increasing as cumulated
            if (sum1 == sum2) ans++;
        }
        return ans;
    }
}
