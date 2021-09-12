public class Solution {
    // DP
    // dp[i][j] := prob of j coins face up after tossing first i coins.
    // dp[i][j] = dp[i-1][j] * (1 – p[i]) + dp[i-1][j-1] * p[i]
    // T: O(n^2) S: O(n^2) -> O(n)
    public double ProbabilityOfHeads1(double[] prob, int target) {
        int n = prob.Length;
        // prob of i coin with j target
        double[] dp = new double[target+1];
        dp[0] = 1.0;
        // toss each coin exactly once
        for (int i = 0; i < n; i++) 
            // need prev i prob[i] at j - 1 coins => start with target to 0
            // 0 <= target <= prob.length
            for (int j = Math.Min(i + 1, target); j >= 0; j--)
                // i coins with j head: i - 1 coins with j head prob * i coin not head prob
                // plus i -1 coins with j - 1 head * i coin head prob
                dp[j] = (1 - prob[i]) * dp[j] + (j > 0 ? dp[j-1] : 0) * prob[i]; 
        return dp[target];
    }
    // recursion + memo
    // T: O(n^2) S: O(n^2)
    public double ProbabilityOfHeads(double[] prob, int target) {
        int n = prob.Length;
        double[,] memo = new double[n+1,target+1];
        for (int i = 0; i <= n; i++) for (int j = 0; j <= target; j++) memo[i,j] = -1;
        Func<int, int, double> f = null;
        f = (i, t) => {
            // 0 <= target <= prob.length
            if (t > i || t < 0) return 0.0;
            if (i == 0) return 1.0; // base case;
            if (memo[i,t] != -1) return memo[i,t];
            double p = prob[i-1];
            return memo[i,t] = p * f(i-1,t-1) + (1- p) * f(i-1, t);
        };
        return f(n, target);
    }
}
