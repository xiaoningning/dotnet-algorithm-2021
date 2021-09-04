public class Solution {
    // DP v1
    // T: O(n), S: O(1)
    public int MaxProfit1(int[] prices) {
        int n = prices.Length;
        if (n <= 1) return 0;
        int cost = prices[0];
        int profit = 0;
        foreach (int p in prices) {
            cost = Math.Min(cost, p);
            profit = Math.Max(profit, p - cost);
        }
        return profit;
    }
    // DP v2 similar to LC 53. Maximum Subarray
    // T: O(n) + O(n), S: O(n)
    public int MaxProfit(int[] prices) {
        int n = prices.Length;
        if (n <= 1) return 0;
        int[] profit = new int[n];
        for (int i = 1; i < n; i++) profit[i] = prices[i] - prices[i - 1];
        
        int ans = Int32.MinValue, mx = 0;
        // p[i] = prices[i] - prices[i-1]
        // p[i+1] = prices[i+1] - prices[i]
        // p[i+2] = prices[i+2] - prices[i+1]
        // p[i] + p[i+1] + p[i+2] = prices[i+2] - price[i-1]
        for (int i = 0; i < profit.Length; i++) {
            // subarray is contiguous
            mx = Math.Max(mx + profit[i], profit[i]);
            ans = Math.Max(ans, mx);
        }
        return ans;
    }
}
