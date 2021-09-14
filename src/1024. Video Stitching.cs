public class Solution {
    // DP T: O(T*n) S: O(T)
    public int VideoStitching(int[][] clips, int time) {
        int[] dp = new int[time+1]; 
        Array.Fill(dp, time+1);
        dp[0] = 0; // base case
        // no sorting on clip since loop from Time
        for (int i = 1; i <= time; i++)
            foreach (int[] c in clips) 
                if (i >= c[0] && i <= c[1])
                    dp[i] = Math.Min(dp[i], dp[c[0]] + 1);
        return dp[time] >= time + 1 ? -1 : dp[time];
    }
}
