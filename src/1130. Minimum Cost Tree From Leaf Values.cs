public class Solution {
    // recursion + memo
    public int MctFromLeafValues(int[] arr) {
        int n = arr.Length;
        int[,] memo = new int[n,n];
        int[,] max = new int[n,n];
        for (int i = 0; i < n; i++) {
            max[i,i] = arr[i];
            for (int j = i + 1; j < n; j++) max[i,j] = Math.Max(max[i, j - 1], arr[j]);
        }
        Func<int,int,int> f = null;
        f = (i,j) => {
            if (i == j) return 0;
            if (memo[i,j] != 0) return memo[i,j];
            int ans = Int32.MaxValue;
            for (int k = i; k < j; k++)
                ans = Math.Min(ans, max[i,k] * max[k+1,j] + f(i,k) + f(k+1,j));
            return memo[i,j] = ans;
        };
        return f(0, n-1);
    }
}
