public class Solution {
    // DP v1
    public int MaxProfit(int[] prices) {
        int n = prices.Length;
        // profit at i day of either buy or sell
        int[] buy = new int[n], sell = new int[n];
        for (int i = 0; i < n; i++) {
            // one day cool down for buy
            buy[i] = Math.Max((i > 1 ? sell[i-2] : 0) - prices[i], i > 0 ? buy[i-1] : Int32.MinValue);
            sell[i] = Math.Max((i > 0 ? buy[i-1] : Int32.MinValue) + prices[i], i > 0 ? sell[i-1] : 0);
        }
        // init as Int32.MinValue for prices: [1,0] or [1]
        return Math.Max(buy[n-1], sell[n-1]);
    }
    // DP v2 S: O(1)
    public int MaxProfit2(int[] prices) {
        int buy = Int32.MinValue, preBuy = 0, sell = 0, preSell = 0;
        foreach (int p in prices) {
            preBuy = buy; 
            // one day cool down for buy
            // preSell was 2 days back sell
            buy = Math.Max(preSell - p, preBuy);
            // preSell was 1 day back sell
            preSell = sell;
            sell = Math.Max(preBuy + p, preSell);
        }
        return Math.Max(buy, sell);
    }
}
