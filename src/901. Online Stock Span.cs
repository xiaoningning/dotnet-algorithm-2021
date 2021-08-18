// Monotonic stack decreasing
public class StockSpanner1 {
    // (price, span/cnt)
    Stack<(int, int)> q;
    public StockSpanner1() { q = new Stack<(int, int)>(); }
    // Next() T: O(1) stack operation is O(1)
    public int Next(int price) {
        int cnt = 1;
        // backward consecutive days
        // pop previous smaller price since cnt is cumulated to current one
        while (q.Any() && q.Peek().Item1 <= price) cnt += q.Pop().Item2;
        q.Push((price, cnt));
        return cnt;
    }
}
// DP
public class StockSpanner {
    List<int> prices = new List<int>();
    List<int> dp = new List<int>(); // span/cnt of price(i)
    public StockSpanner() {}
    // Next() T: O(n) scan dp
    public int Next(int price) {
        if (!prices.Any() || price < prices.Last()) dp.Add(1);
        else {
            // start from the last one
            int j = prices.Count - 1;
            // next to check is the larger than the last one.
            while (j >= 0 && price >= prices[j]) j -= dp[j];
            // j is the idx of price larger than the current one
            // prices.Count - j is the cnt/span of previous consecutive days 
            // of smaller prices
            dp.Add(prices.Count - j);
        }
        prices.Add(price);
        return dp.Last();
    }
}
/**
 * Your StockSpanner object will be instantiated and called as such:
 * StockSpanner obj = new StockSpanner();
 * int param_1 = obj.Next(price);
 */
