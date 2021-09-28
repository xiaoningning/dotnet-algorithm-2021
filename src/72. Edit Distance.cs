public class Solution {
    // recursion + memo
    // T: O(wl1 * wl2) S: O(min(wl1, wl2))
    public int MinDistance2(string word1, string word2) {
        int wl1 = word1.Length, wl2 = word2.Length;
        int[,] memo = new int[wl1, wl2];
        Func<int, int, int> f = null;
        f = (l1, l2) => {
            if (l1 == wl1) return wl2 - l2; // rest of word2
            if (l2 == wl2) return wl1 - l1; // rest of word1
            if (memo[l1, l2] > 0) return memo[l1, l2];
            if (word1[l1] == word2[l2]) return memo[l1,l2] = f(l1 + 1, l2 + 1);
            // only count operation of insert/delete/replace, no need to match char at (l1, l2)
            int insertCnt = f(l1, l2 + 1);
            int deleteCnt = f(l1 + 1, l2);
            int replaceCnt = f(l1 + 1, l2 + 1);
            return memo[l1,l2] = new int[]{insertCnt, deleteCnt, replaceCnt}.Min() + 1;
        };
        return f(0, 0);
    }
    // DP
    public int MinDistance(string word1, string word2) {
        int wl1 = word1.Length, wl2 = word2.Length;
        int[,] dp = new int[wl1 + 1, wl2 + 1];
        // base case
        for (int i = 0; i <= wl1; i++) dp[i,0] = i;
        for (int j = 0; j <= wl2; j++) dp[0,j] = j;
        for (int i = 1; i <= wl1; i++) {
            for (int j = 1; j <= wl2; j++){
                if (word1[i - 1] == word2[j - 1]) dp[i, j] = dp[i - 1, j - 1];
                else {
                    // insert/delete/replace
                    dp[i, j] = new int[]{dp[i-1,j], dp[i, j-1], dp[i-1,j-1]}.Min() + 1;
                }
            }
        }
        return dp[wl1, wl2];
    }
}
