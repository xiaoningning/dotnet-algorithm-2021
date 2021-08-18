// Monotonic stack decreasing
public class StockSpanner {
    // (price, span/cnt)
    Stack<(int, int)> q;
    public StockSpanner() { q = new Stack<(int, int)>(); }
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

/**
 * Your StockSpanner object will be instantiated and called as such:
 * StockSpanner obj = new StockSpanner();
 * int param_1 = obj.Next(price);
 */
