public class Solution {
    // DP or DFS to track prev val with swap or no swap
    // DP similar to LC 300. Longest Increasing Subsequence
    public int MakeArrayIncreasing(int[] arr1, int[] arr2) {
        int kInf = (int) Math.Pow(10,9);
        Array.Sort(arr2);
        var a2 = new HashSet<int>(arr2).ToList();
        int l1 = arr1.Length, l2 = a2.Count;
        Func<int, int> binarySearchRight = (t) => {
            int l = 0, r = a2.Count;
            while (l < r) {
                int mid = l + (r - l) / 2;
                if (a2[mid] <= t) l = mid + 1;
                else r = mid;
            }
            return l;
        };
        // k: prev val, val: # of swap op
        var dp = new Dictionary<int,int>(){[-1] = 0};
        foreach (int i in arr1) {
            var t = new Dictionary<int,int>();
            foreach (var kv in dp) {
                int prev = kv.Key, op = kv.Value;
                if (i > prev) t[i] = Math.Min(t.ContainsKey(i) ? t[i] : kInf, op);
                int nxj = binarySearchRight(prev);
                if (nxj != l2) t[a2[nxj]] = Math.Min(t.ContainsKey(a2[nxj]) ? t[a2[nxj]] : kInf, op + 1);
            }
            dp = t;
        }
        return dp.Any() ? dp.Values.Min() : -1;
    }
    // DP 
    public int MakeArrayIncreasing3(int[] arr1, int[] arr2) {
        int kInf = (int) Math.Pow(10,9);
        Array.Sort(arr2);
        var a2 = new HashSet<int>(arr2).ToList();
        int l1 = arr1.Length, l2 = a2.Count;
        Func<int, int> binarySearchRight = (t) => {
            int l = 0, r = a2.Count;
            while (l < r) {
                int mid = l + (r - l) / 2;
                if (a2[mid] <= t) l = mid + 1;
                else r = mid;
            }
            return l;
        };
        // VAL at a1(i) with j swap, j = 0 => no swap
        int[,] dp = new int[l1+1, l2+1];
        for (int i = 0; i <= l1; i++)
            for (int j = 0; j <= l2; j++) dp[i,j] = kInf;
        dp[0,0] = -kInf;
        for (int i = 1; i <= l1; i++) {
            for (int j = 0; j <= l2; j++) {
                // the same j, no swap
                if (i == 0 || arr1[i-1] > dp[i-1,j]) dp[i,j] = arr1[i-1];
                if (j > 0) {
                    // prev j => swap, if no swap at prev i, dp[i-1,j-1] = kInf
                    int nxj = binarySearchRight(dp[i-1,j-1]);
                    if (nxj != l2) dp[i,j] = Math.Min(dp[i,j], a2[nxj]);
                }
                if (i == l1 && dp[i,j] != kInf) return j;
            }
        }
        return -1;
    }
    // DFS + memo => easy to understand, without memo => TLE
    public int MakeArrayIncreasing2(int[] arr1, int[] arr2) {
        int kInf = (int) Math.Pow(10,9);
        Array.Sort(arr2);
        var a2 = new HashSet<int>(arr2).ToList();
        int l1 = arr1.Length, l2 = a2.Count;
        Func<int, int> binarySearchRight = (t) => {
            int l = 0, r = a2.Count;
            while (l < r) {
                int mid = l + (r - l) / 2;
                if (a2[mid] <= t) l = mid + 1;
                else r = mid;
            }
            return l;
        };
        var memo = new Dictionary<(int,int), int>();
        Func<int, int, int> DFS = null;
        DFS = (i, prev) => {
            if (i == l1) return 0;
            if (memo.ContainsKey((i,prev))) return memo[(i,prev)];
            int j = binarySearchRight(prev);
            int noswap = arr1[i] > prev ? DFS(i+1, arr1[i]) : kInf;
            int swap = j < l2 ? DFS(i+1, a2[j]) + 1 : kInf;
            return memo[(i, prev)] = Math.Min(swap, noswap);
        };
        int ans = DFS(0, -1);
        return ans >= kInf ? -1 : ans;
    }
    // DP hard to understand
    public int MakeArrayIncreasing1(int[] arr1, int[] arr2) {
        int kInf = (int) Math.Pow(10,9);
        Array.Sort(arr2);
        var a2 = new HashSet<int>(arr2).ToList();
        int l1 = arr1.Length, l2 = a2.Count;
        // keep: min # of op if keep at a1[i]
        // swap: min # of op if swap at a2[j]
        int[] keep = new int[l1], swap = new int[l2];
        Array.Fill(keep, kInf);
        keep[0] = 0;
        Array.Fill(swap, 1);
        for (int i = 1; i < l1; i++) {
            // need to use kInf ans MAX, in case overflow int32
            int min_keep = kInf, min_swap = kInf;
            int[] t = new int[l2];
            Array.Fill(t, kInf);
            for (int j = 0; j < l2; j++) {
                // if swap is needed, min_swap at least swap[j-1] + 1 at i - 1
                if (j > 0) min_swap = Math.Min(min_swap, swap[j-1] + 1);
                // keep condition a1[i] > a2[j]: min_keep at least swap(j) at i - 1
                // a2 is sorted here
                if (arr1[i] > a2[j]) min_keep = Math.Min(min_keep, swap[j]); 
                if (arr1[i] > arr1[i-1]) keep[i] = keep[i-1];
                if (a2[j] > arr1[i-1]) t[j] = keep[i-1] + 1;
                t[j] = Math.Min(min_swap, t[j]);
                keep[i] = Math.Min(min_keep, keep[i]);
            }
            swap = t;
        }
        int mn = swap.Min();
        int ans = Math.Min(mn, keep.Last());
        // no meet to condistion, ans can be kInf + 1
        return ans >= kInf ? -1 : ans;
    }
}
