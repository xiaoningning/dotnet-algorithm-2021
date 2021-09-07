public class Solution {
    // DP similar to LC 198. House Robber
    // T: O(n) S: O(r) r: [min,...max]
    public int DeleteAndEarn2(int[] nums) {
        int take = 0, notTake = 0;
        int[] points = new int[nums.Max() + 1];
        // take n => points
        foreach (int n in nums) points[n] += n;
        for (int i = 1; i < nums.Max() + 1; i++) {
            // take i, then i-1/i+1 not take
            int takei = notTake + points[i];
            int notTakei = Math.Max(notTake, take);
            take = takei; notTake = notTakei;
        }
        return Math.Max(take, notTake);
    }
    // DP reduce to LC 198. house robber
    public int DeleteAndEarn(int[] nums) {
        int mn = nums.Min(), mx = nums.Max();
        int[] points = new int[mx - mn + 1];
        // take n => points
        foreach (int n in nums) points[n - mn] += n;
        Func<int[],int> f = null; // house robber
        f = (a) => {
            int take = 0, notTake = 0;
            for (int i = 0; i < a.Length; i++) {
                int preTake = take, preNotTake = notTake;
                take = preNotTake + a[i];
                notTake = Math.Max(preTake, preNotTake);
            }
            return Math.Max(take,notTake);
        };
        return f(points);
    }
    // DFS brute force => TLE
    public int DeleteAndEarn1(int[] nums) {
        if (nums.Length == 0) return 0;
        int ans = 0;
        foreach (int n in nums) {
            var t = new List<int>(nums);
            t.Remove(n);
            t.RemoveAll(x => x == n-1); t.RemoveAll(x => x == n+1);
            int v = DeleteAndEarn(t.ToArray());
            ans = Math.Max(ans, n + v);
        }
        return ans;
    }
}
