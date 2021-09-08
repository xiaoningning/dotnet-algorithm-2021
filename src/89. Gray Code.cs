public class Solution {
    // Recursion T: O(n)
    public IList<int> GrayCode(int n) {
        if (n == 0) return new List<int>(){0};
        var ans = new List<int>();
        var t = GrayCode(n-1);
        foreach (int i in t) ans.Add(i<<1|0);
        // minor order => reverse t
        foreach (int i in t.Reverse()) ans.Add(i<<1|1);
        return ans;
    }
    // DP
    // dp[0] = 0
    // dp[i] = dp[i – 1] + [x | 1 << (i – 1) for x in reversed(dp[i – 1])]
    // Time complexity: O(2^n)
    // Space complexity: O(2^n)
    public IList<int> GrayCode2(int n) {
        var dp = new List<int>(){0};
        for (int i = 1; i <= n; i++)
            for (int j = dp.Count-1; j >= 0; j--)
                dp.Add(dp[j] | 1 << (i-1));
        return dp;
    }
}
