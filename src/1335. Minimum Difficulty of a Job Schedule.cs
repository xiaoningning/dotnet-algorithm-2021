public class Solution {
    // DP
    // T: O(n^2*d) S: O(n)
    public int MinDifficulty(int[] jobDifficulty, int d) {
        int n = jobDifficulty.Length;
        if (d > n) return -1;
        int[,] dp = new int[n+1, d+1];
        for (int i = 0; i <= n; i++) 
            for (int j = 0; j <= d; j++) dp[i,j] = jobDifficulty.Sum() + 1;
        dp[0,0] = 0;
        for (int i = 1; i <= n; i++) {
            for (int j = i - 1, mx = 0; j >= 0; j--) {
                mx = Math.Max(mx, jobDifficulty[j]);
                // each day at least one job
                for (int k = 1; k <= Math.Min(i,d); k++)
                    dp[i,k] = Math.Min(dp[i,k], dp[j, k - 1] + mx);
            }
        }
        return dp[n, d];
    }
    // DP v2
    // T: O(n^2*d) S: O(n)
    public int MinDifficulty2(int[] jobDifficulty, int d) {
        int n = jobDifficulty.Length;
        if (d > n) return -1;
        // dp := Difficulty at job i when day d
        int[] dp = new int[n+1];
        // d == 1 case
        for (int i = n - 1; i >= 0; i--) dp[i] = Math.Max(dp[i + 1], jobDifficulty[i]);
        for (int k = 2; k <= d; k++) {
            for (int i = 0; i < n - (k - 1); i++) {
                dp[i] = Int32.MaxValue;
                for (int j = i, mx = 0; j < n - (k -1); j++) {
                    mx = Math.Max(mx, jobDifficulty[j]);
                    // dp[j+1] := k - 1 days
                    dp[i] = Math.Min(dp[i], mx + dp[j+1]);
                }
            }
        }
        return dp[0];
    }
}
