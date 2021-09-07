public class Solution {
    // 0 and n-1 are adjancent
    // Recursion + memo
    // T:O(n), S:O(n)
    public int Rob(int[] nums) {
        int n = nums.Length;
        if (n <= 1) return n == 0 ? 0 : nums[0];
        int[] memo = new int[n];
        Array.Fill(memo, -1);
        Func<int,int,int> f = null;
        f = (l,r) => {
            if (l >= r) return 0; // case i+1 / i+2
            if (memo[l] >= 0) return memo[l];
            return memo[l] = Math.Max(nums[l] + f(l+2,r), f(l+1,r));
        };
        int ans1 = f(0, n-1);
        Array.Fill(memo, -1);
        int ans2 = f(1, n);
        return Math.Max(ans1,ans2);
    }
    // DP
    // T:O(n), S:O(1)
    public int Rob1(int[] nums) {
        int n = nums.Length;
        if (n <= 1) return n == 0 ? 0 : nums[0];
        Func<int,int,int> f = null;
        f =(l,r) => {
            int rob = 0, notRob = 0;
            for (int i = l; i < r; i++) {
                int preRob = rob, preNotRob = notRob;
                rob = preNotRob + nums[i];
                notRob = Math.Max(preNotRob, preRob);
            }
            return Math.Max(rob, notRob);
        };
        return Math.Max(f(0, n-1), f(1, n));
    }
}
