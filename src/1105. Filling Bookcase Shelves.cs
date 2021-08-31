public class Solution {
    // DP T: O(n^2)
    public int MinHeightShelves(int[][] books, int shelfWidth) {
        int n = books.Length;
        // min height of book i
        int[] dp = new int[n + 1];
        for (int i = 1; i <= n; i++) {
            int w = books[i - 1][0], h = books[i - 1][1];
            // worst case for book[i - 1]: start a new level
            dp[i] = dp[i - 1] + h;
            // add previous book into the i level to min total height
            for (int j = i - 1; j >= 1; j--) {
                if ((w += books[j - 1][0]) > shelfWidth) break;
                h = Math.Max(h, books[j - 1][1]);
                dp[i] = Math.Min(dp[i], dp[j - 1] + h);
            }
        } 
        return dp[n];
    }
    // DP v2
    public int MinHeightShelves2(int[][] books, int shelfWidth) {
        int n = books.Length;
        // min height of book i
        int[] dp = new int[n];
        // MaxValue /2 to avoid overflow
        Array.Fill(dp, Int32.MaxValue / 2);
        for (int i = 0; i < n; i++) {
            int w = 0,  h = 0;
            // add book into the same level to min total height
            for (int j = i ; j < n; j++) {
                if ((w += books[j][0]) > shelfWidth) break;
                h = Math.Max(h, books[j][1]);
                dp[j] = Math.Min(dp[j], (i > 0 ? dp[i - 1] : 0) + h);
            }
        } 
        return dp[n - 1];
    }
}
