public class Solution {
    // Monotonic stack
    // T: O(n)
    // SL O(n)
    public int SumSubarrayMins1(int[] arr) {
        int M = (int)1e9 + 7, ans = 0, n = arr.Length;
        var st = new Stack<int>();
        for (int i = 0; i <= n; i++) {
            // last num itself
            int cur = i == n ? 0 : arr[i];
            // monotonic stack increasing
            while (st.Any() && arr[st.Peek()] > cur) {
                int idx = st.Pop();
                int left = idx - (st.Any() ? st.Peek() : -1);
                int right = i - idx;
                ans = (int)(ans + (long)arr[idx] * left * right % M) % M;
            }
            st.Push(i);
        }
        return ans;
    }
    /*
    e.g. given [3,1,2,4],
    For 3, the left and right boudary is: | 3 | ...
    For 1, the left and right boudray is: | 3 1 2 4 |
    For 2, the left and right boudray is: ... | 2 4 |
    For 4, the left and right boudary is: ... | 4 |
    */
    public int SumSubarrayMins2(int[] arr) {
        int M = (int)1e9 + 7, ans = 0, n = arr.Length;
        int[] left = new int[n], right = new int[n];
        // (num, len)
        var st = new Stack<(int,int)>();
        for (int i = 0; i < n; i++) {
            int len = 1;
            while (st.Any() && st.Peek().Item1 > arr[i]) {
                (int num , int prevLen) = st.Pop();
                len += prevLen;
            }
            st.Push((arr[i], len));
            left[i] = len;
        }
        st.Clear();
        for (int i = n - 1; i >= 0; i--) {
            int len = 1;
            // it could be equal but only count one side
            while (st.Any() && st.Peek().Item1 >= arr[i]) {
                (int num , int prevLen) = st.Pop();
                len += prevLen;
            }
            st.Push((arr[i], len));
            right[i] = len;
        }
        for (int i = 0; i < n; i++){
            ans = (int)(ans + (long)arr[i] * left[i] * right[i] % M) % M;
        }
        return ans;
    }
    // V2: cleaner code
    public int SumSubarrayMins(int[] arr) {
        int M = (int)1e9 + 7, ans = 0, n = arr.Length;
        int[] left = new int[n], right = new int[n];
        Array.Fill(left, 1); Array.Fill(right, 1);
        
        Action<int[], int, int, int> FindLens = (lens, start, end, step) => {
            var st = new Stack<(int,int)>();   // (num, len)
            int i = start;
            while (i != end) {
                int cur = step == -1 ? arr[i] - 1 : arr[i];
                while (st.Any() && st.Peek().Item1 > cur) lens[i] += st.Pop().Item2;
                st.Push((arr[i], lens[i]));
                i += step;
            }
        };
        FindLens(left, 0, n, 1);
        FindLens(right, n - 1, -1, -1);
        for (int i = 0; i < n; i++){
            ans = (int)(ans + (long)arr[i] * left[i] * right[i] % M) % M;
        }
        return ans;
    }
}
