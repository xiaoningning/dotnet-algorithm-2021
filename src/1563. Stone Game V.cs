public class Solution {
    // DP 
    // T: O(n^3) S: O(n^2)
    public int StoneGameV(int[] stoneValue) {
        int n = stoneValue.Length;
        int[] sum = new int[n+1];
        for (int i = 0; i < n; i++) sum[i+1] = sum[i] + stoneValue[i];
        int[,] memo = new int[n,n];
        for (int i = 0; i < n; i++) for (int j = 0; j < n; j++) memo[i,j] = -1;
        Func<int,int,int> f = null;
        f = (l,r) => {
            if (l == r) return 0;
            if (memo[l,r] != -1) return memo[l,r];
            int ans = Int32.MinValue;
            for (int k = l; k < r; k++) {
                // left: [l, k], right: [k + 1, r]
                int sumL = sum[k+1] - sum[l];
                int sumR = sum[r+1] - sum[k+1];
                if (sumL < sumR) ans = Math.Max(ans, sumL + f(l,k));
                else if (sumR < sumL) ans = Math.Max(ans, sumR + f(k+1,r));
                else ans = Math.Max(ans, sumL + Math.Max(f(l,k), f(k+1,r)));
            }
            return memo[l,r] = ans;
        };
        return f(0, n-1);
    }
}
