public class Solution {
    // Recursion + memo 
    // T: O(nsqrt(n)) S: O(n)
    public bool WinnerSquareGame(int n) {
        // 0: unknown, 1: win, -1: lose
        int[] memo = new int[n+1];
        Func<int,int> f = null;
        f = (i) => {
            // i==0 no more stone => lose
            if (i == 0) return -1;
            if (memo[i] != 0) return memo[i];
            for (int k = (int) Math.Sqrt(i); k >= 1; k--) 
                // f(n-i*i) is another player
                if (f(i-k*k) < 0) return memo[i] = 1;
            return memo[i] = -1;
        };
        return f(n) > 0;
    }
}
