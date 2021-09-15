public class Solution {
    // recursion + memo
    // T: O(n^3) S: O(n^2)
    public int MctFromLeafValues1(int[] arr) {
        int n = arr.Length;
        // min cnt of [i..j]
        int[,] memo = new int[n,n];
        int[,] max = new int[n,n];
        for (int i = 0; i < n; i++) {
            max[i,i] = arr[i];
            for (int j = i + 1; j < n; j++) max[i,j] = Math.Max(max[i, j - 1], arr[j]);
        }
        Func<int,int,int> f = null;
        f = (i,j) => {
            if (i == j) return 0;
            if (memo[i,j] != 0) return memo[i,j];
            int ans = Int32.MaxValue;
            for (int k = i; k < j; k++)
                ans = Math.Min(ans, max[i,k] * max[k+1,j] + f(i,k) + f(k+1,j));
            return memo[i,j] = ans;
        };
        return f(0, n-1);
    }
    // DP
    // T: O(n^3) S: O(n^2)
    public int MctFromLeafValues2(int[] arr) {
        int n = arr.Length;
        int[,] dp = new int[n,n];
        int[,] max = new int[n,n];
        for (int i = 0; i < n; i++) {
            max[i,i] = arr[i];
            for (int j = i + 1; j < n; j++) max[i,j] = Math.Max(max[i, j - 1], arr[j]);
        }
        for (int len = 2; len <= n; len++) {
            for (int i = 0, j = i + len - 1; j < n; j++, i++) {
                dp[i,j] = Int32.MaxValue;
                for (int k = i; k < j; k++)
                    dp[i,j] = Math.Min(dp[i,j], max[i,k] * max[k+1,j] + dp[i,k] + dp[k+1,j]);
            }
        }
        return dp[0, n-1];
    }
    // greedy search => find the smallest node at each level of tree
    // T: O(n^2) S: O(n)
    public int MctFromLeafValues3(int[] arr) {
        int ans = 0;
        var t = new List<int>(arr);
        while (t.Count > 1) {
            int idx = t.IndexOf(t.Min());
            if (idx > 0 && idx < t.Count - 1) ans += t[idx] * Math.Min(t[idx-1], t[idx+1]);
            else ans += t[idx] * (idx == 0 ? t[idx + 1] : t[idx - 1]);
            t.RemoveAt(idx);
            t = new List<int>(t);
        }
        return ans;
    }
    // DP + Monotonic stack similar idea to greedy search
    // LC 503. Next Greater Element II
    // T: O(n) S: O(n)
    public int MctFromLeafValues(int[] arr) {
        int ans = 0;
        var st = new Stack<int>();
        // int32.maxvalue for picking left boarder node
        st.Push(Int32.MaxValue);
        foreach (int n in arr) {
            while (st.Any() && st.Peek() <= n) {
                // min val of local range
                int mn = st.Pop(); 
                // min cost of node to find min(left of mn, right of mn)
                ans += mn * Math.Min(st.Peek(), n);
            }
            st.Push(n);
        }
        // for picking up right boarder node
        while (st.Count > 2) ans += st.Pop() * st.Peek();
        return ans;
    }
}
