public class Solution {
    // DP v1
    // T: O(steps * steps) S: O(steps * steps)
    public int NumWays1(int steps, int arrLen) {
        int kM = (int)Math.Pow(10, 9) + 7;
        // if steps smaller, then you can not move more than steps from 0;
        arrLen = Math.Min(arrLen, steps);
        // # of ways back to pos j with steps i
        // dp[i][j] = dp[i-1][j – 1] + dp[i-1][j] + dp[i-1][j+1] // sum of right, stay, left
        int[,] dp = new int[steps + 1, arrLen];
        dp[0,0] = 1;
        for (int i = 1; i <= steps; i++) {
            for (int j = 0; j < arrLen; j++) {
                dp[i,j] = dp[i-1,j]; // stay
                if (j > 0) dp[i,j] = (dp[i,j] + dp[i-1,j-1]) % kM; // right
                if (j < arrLen - 1) dp[i,j] = (dp[i,j] + dp[i-1, j+1]) % kM; // left 
            }
        }
        return dp[steps,0];
    }
    // DP v2 save space
    // T: O(steps * steps) S: O(steps)
    public int NumWays2(int steps, int arrLen) {
        int kM = (int)Math.Pow(10, 9) + 7;
        arrLen = Math.Min(arrLen, steps);
        // # of ways back to pos j with steps i
        int[] dp = new int[arrLen];
        dp[0] = 1; // stay at pos i
        for (int i = 1; i <= steps; i++) {
            int[] t = (int[])dp.Clone();
            for (int j = 0; j < arrLen; j++) {
                t[j] = dp[j]; // stay
                if (j > 0) t[j] = (t[j] + dp[j-1]) % kM; // right
                if (j < arrLen - 1) t[j] = (t[j] + dp[j+1]) % kM; // left 
            }
            dp = t;
        }
        return dp[0];
    }
    // recursion + memo
    public int NumWays(int steps, int arrLen) {
        int kM = (int)Math.Pow(10, 9) + 7;
        int mxPos = Math.Min(steps / 2 + 1, arrLen);
        int[,] memo = new int[mxPos, steps+1];
        for (int i = 0; i < mxPos; i++) for (int j = 0; j <= steps; j++) memo[i,j] = -1;
        Func<int, int, int> f = null;
        f = (i, s) => {
            if ( s == 0 && i == 0) return 1;
            if ( i < 0 || i == mxPos || s == 0) return 0;
            if (memo[i,s] != -1) return memo[i, s];
            // left, stay, right
            int ans = 0;
            ans = (ans + f(i+1, s - 1)) % kM;
            ans = (ans + f(i, s - 1)) % kM;
            ans = (ans + f(i-1, s - 1)) % kM;
            return memo[i,s] = ans;
        };
        return f(0,steps);
    }
}
