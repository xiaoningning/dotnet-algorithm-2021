public class Solution {
    // min - max strategy
    // T: O(n) S: O(n)
    public string StoneGameIII(int[] stoneValue) {
        int n = stoneValue.Length;
        int[] memo = new int[n];
        Array.Fill(memo, Int32.MinValue);
        Func<int,int> f = null;
        f = (i) => {
            if (i >= n) return 0;
            if (memo[i] != Int32.MinValue) return memo[i];
            for (int j = 0, s = 0; j < 3 && i + j < n; j++) {
                s += stoneValue[i + j];
                memo[i] = Math.Max (memo[i], s - f(i + j + 1));
            } 
            return memo[i];
        };
        int ans = f(0);
        return ans > 0 ? "Alice" : ans == 0 ? "Tie" : "Bob";
    }
}
