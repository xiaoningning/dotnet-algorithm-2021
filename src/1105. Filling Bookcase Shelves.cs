public class Solution {
    // DP T: O(n^2)
    public int MinHeightShelves1(int[][] books, int shelfWidth) {
        int n = books.Length;
        // min height of book i
        int[] dp = new int[n + 1];
        for (int i = 1; i <= n; i++) {
            int w = books[i - 1][0], h = books[i - 1][1];
            dp[i] = dp[i - 1] + h;
            // add previous book into the same level to min total height
            for (int j = i - 1 - 1; j >= 0 && w + books[j][0] <= shelfWidth; j--) {
                w += books[j][0];
                h = Math.Max(h, books[j][1]);
                dp[i] = Math.Min(dp[i], dp[j] + h);
            }
        } 
        return dp[n];
    }
    // DP v2
    public int MinHeightShelves(int[][] books, int shelfWidth) {
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
