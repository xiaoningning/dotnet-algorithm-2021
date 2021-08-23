public class Solution {
    // DFS
    public IList<IList<int>> Combine1(int n, int k) {
        var ans = new List<IList<int>>();
        Action<int, List<int>> DFS = null;
        DFS = (start, tmp) => {
            if (tmp.Count == k) { ans.Add(new List<int>(tmp)); return; }
            for (int i = start; i <= n; i++) {
                tmp.Add(i);
                DFS(i + 1, tmp);
                tmp.Remove(i);
            }
        };
        DFS(1, new List<int>());
        return ans;
    }
    // Recursion
    // C(n, k) = C(n-1, k-1) + C(n-1, k)
    public IList<IList<int>> Combine(int n, int k) {
        var tmp = new List<IList<int>>();
        if (k > n || k < 0) { return tmp; }
        // base case
        if (k == 0) { tmp.Add(new List<int>()); return tmp; }
        // C(n-1, k-1)
        var ans = Combine(n - 1, k - 1);
        // C(n, k) must has n
        foreach (var a in ans) a.Add(n);
        // C(n-1,k) must not have n
        foreach (var x in Combine(n - 1, k)) ans.Add(x);
        return ans;
    }
}
