public class Solution {
    // DP
    // Time complexity: O(n*6*6*max(rollMax[]))
    // Space complexity: O(n*6*max(rollMax[])) -> O(6*max(rollMax[]))
    public int DieSimulator1(int n, int[] rollMax) {
        int maxRoll = rollMax.Max();
        int M = (int)Math.Pow(10,9) + 7;
        // dp[i,j,k] : # of sequeneces ends with j of k consecutive of j after i rolls 
        int[,,] dp = new int[n + 1,6,maxRoll + 1];
        // base case only roll j for 1 roll, 1 dice, 1 way
        for (int j = 0; j < 6; j++) dp[1,j,1] = 1;
        for (int i = 2; i <= n; i++)
            for (int j = 0; j < 6; j++)
                for (int p = 0; p < 6; p++) // p: previous dice
                    for (int k = 1; k <= maxRoll; k++)
                        // not the same dice of previous roll
                        if (p != j) dp[i,j,1] = (dp[i,j,1] + dp[i-1,p,k]) % M;
                        // the same dice of previous roll but k <= rollmax[j]
                        else if (k <= rollMax[j]) dp[i,j,k] = (dp[i,j,k] + dp[i-1,p,k-1]) % M; 
        
        int ans = 0;
        for (int j = 0; j < 6; j++) 
            for (int k = 1; k <= rollMax[j]; k++) 
                ans = (ans + dp[n,j,k]) % M;
        return ans;
    }
    // DP with compressed state
    // Time complexity: O(n*6)
    // Space complexity: O(n*6)
    public int DieSimulator(int n, int[] rollMax) {
        int maxRoll = rollMax.Max();
        int M = (int)Math.Pow(10,9) + 7;
        // dp[i,j] =: # of sequeneces ends with j after i rolls 
        int[,] dp = new int[n + 1,6];
        // sums[i] := sum(dp[i, j])
        int[] sum = new int[n + 1];
        // base case only roll j for 1 roll, 1 dice, 1 way
        for (int j = 0; j < 6; j++) sum[1] += dp[1,j] = 1;
        for (int i = 2; i <= n; i++)
            for (int j = 0; j < 6; j++) {
                int k = i - rollMax[j];
                // k == 1: just one extra roll of j => invalid = 1;
                // k > 1: k - 1 rolls of all p != j dice plus 1 roll of j 
                // => k - 1 rolls of all dices extract k-1 rolls of j => invalid = sum[k-1] - dp[k-1,j] 
                int invalidRoll = k <= 1 ? Math.Max(k,0) : sum[k-1] - dp[k - 1, j];
                // dp[i,j] = sum[i-1] - (extra invalid roll) + 1 roll of j
                dp[i,j] = ((sum[i - 1] - invalidRoll) % M + M) % M;
                sum[i] = (sum[i] + dp[i,j]) % M;
            }
        
        return sum[n];
    }
}
