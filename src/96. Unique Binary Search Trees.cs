public class Solution {
    // DP
    // T: O(n^2)
    // S: O(n)
    public int NumTrees(int n) {
        int[] dp = new int[n + 1];
        // empty node is BST as well
        dp[0] = dp[1] = 1;
        for (int i = 2; i <= n; i++)
            for (int j = 0; j < i; j++)
                // root: 1 node; 
                // left: j nodes; right: i – j – 1 nodes
                dp[i] += dp[j] * dp[i - j - 1];
        return dp[n];
    }
}
