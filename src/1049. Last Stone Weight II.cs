public class Solution {
    // similar to LC 322. Coin Change 416. Partition Equal Subset Sum 494. Target Sum
    // recursion + memo
    public int LastStoneWeightII1(int[] stones) {
        int n = stones.Length;
        var memo = new Dictionary<int,Dictionary<int,int>>();
        for (int i = 0; i < n; i++) memo[i] = new Dictionary<int,int>();
        Func<int,int,int> f = null;
        f = (start, sum) => {
            if (start == n) return sum;
            if (memo[start].ContainsKey(sum)) return memo[start][sum];
            // negative:= take stone, positive:= dont take stone
            return memo[start][sum] = Math.Min(Math.Abs(f(start + 1,sum - stones[start])), Math.Abs(f(start+1, sum + stones[start])));
        };
        return f(0,0);
    }
    // DP v1 Similar to LC 494. Target Sum
    public int LastStoneWeightII2(int[] stones) {
        int n = stones.Length;
        var sum = new HashSet<int>();
        sum.Add(stones[0]);
        sum.Add(-stones[0]);
        for (int i = 1; i < n; i++) {
            var t = new HashSet<int>();
            foreach (int s in sum) {
                t.Add(s + stones[i]);
                t.Add(s - stones[i]);
            }
            sum = t;
        }
        int ans = stones.Sum();
        foreach (int s in sum) ans = Math.Min(ans, Math.Abs(s));
        return ans;
    }
    // DP v2 416. Partition Equal Subset Sum
    // split stones into two subset as equal as possible
    // s1 + s2 = s, s1-s2 = diff 
    // ==> 2s1 = s + diff ==> diff = s - 2 * s1
    // ==> diff >= 0 for min diff ==> search max s1 <= s / 2
    public int LastStoneWeightII(int[] stones) {
        int sum = stones.Sum();
        bool[] dp = new bool[sum+1];
        dp[0] = true;
        foreach (int n in stones)
            for (int i = sum; i >= n; i--)
                dp[i] |= dp[i-n];
        int ans = sum;
        for (int i = sum / 2; i >= 1; i--) 
            if (dp[i]) return ans = sum - 2 * i;
        return ans;
    }
}
