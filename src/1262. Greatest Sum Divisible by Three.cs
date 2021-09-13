public class Solution {
    // DP
    // T: O(N) S: O(1)
    public int MaxSumDivThree(int[] nums) {
        // dp[i] := max sum that has a remainder i when mod 3.
        // dp[(i + num) % 3] = max( dp[(i + num) % 3] , dp[i] + num)
        int[] dp = new int[3];
        foreach (int n in nums) {
            int[] t = (int[])dp.Clone();
            foreach (int sum in t) 
                dp[(sum + n) % 3] = Math.Max(dp[(sum + n) % 3], sum + n);
        }
        return dp[0];
    }
    // brute force => TLE
    public int MaxSumDivThree1(int[] nums) {
        Func<int,bool> f = null;
        f = (t) => {
            if (t % 3 != 0) return false;
            bool[] dp = new bool[t+1];
            dp[0] = true;
            foreach (int n in nums) 
                for (int j = t; j >= n; j--) dp[j] |= dp[j-n];
            return dp[t];
        };
        int ans = nums.Sum() - (nums.Sum() % 3);
        while (ans > 0) { 
            if (f(ans)) break;
            else ans -= 3;
        }
        return ans;
    }
}
