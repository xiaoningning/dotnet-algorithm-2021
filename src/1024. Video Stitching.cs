public class Solution {
    // DP T: O(T*n) S: O(T)
    public int VideoStitching1(int[][] clips, int time) {
        int[] dp = new int[time+1]; 
        Array.Fill(dp, time+1);
        dp[0] = 0; // base case, it must start from 0
        // no sorting on clip since loop from Time
        for (int i = 0; i <= time; i++)
            foreach (int[] c in clips) 
                if (i >= c[0] && i <= c[1])
                    dp[i] = Math.Min(dp[i], dp[c[0]] + 1);
        return dp[time] >= time + 1 ? -1 : dp[time];
    }
    public int VideoStitching(int[][] clips, int time) {
        int[,] dp = new int[time+1, time+1]; 
        for (int i = 0; i <= time; i++) for (int j = 0; j <= time; j++) dp[i,j] = i < j ? time+1 : 0;
        dp[0,0] = 0; // base case, it must start from 0
        foreach (int[] c in clips) {
            int s = Math.Min(c[0], time);
            int e = Math.Min(c[1], time);
            for (int l = 0; l <= time; l++) {
                for (int i = 0, j = l; j <= time; i++, j++) {
                    if (s > j || e < i) continue;
                    dp[i,j] = Math.Min(dp[i,j], dp[i,s] + 1 + dp[e,j]);
                }
            }
        }
        return dp[0,time] >= time + 1 ? -1 : dp[0,time];
    }
}
