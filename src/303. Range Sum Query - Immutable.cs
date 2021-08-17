public class NumArray1 {
    int[] dp; // Cumulative sum of nums;
    public NumArray1(int[] nums) {
        dp = (int[])nums.Clone();
        for (int i = 1; i < nums.Length; i++) dp[i] += dp[i-1];
    }
    // assume left, right within [0,n)
    public int SumRange(int left, int right) {
         // inclusive of left and right
        return left > 0 ? dp[right] - dp[left - 1] : dp[right];   
    }
}
// resize dp to avoid left == 0 for inclusive sum
public class NumArray {
    int[] dp; // Cumulative sum of nums;
    public NumArray(int[] nums) {
        int n = nums.Length;
        dp = new int[n + 1];
        for (int i = 1; i <= n; i++) dp[i] = dp[i-1] + nums[i-1];
    }
    // assume left, right within [0,n)
    public int SumRange(int left, int right) {
         // dp is n+1 => inclusive of left and right 
        // left can be 0
        return dp[right+1] - dp[left];   
    }
}

/**
 * Your NumArray object will be instantiated and called as such:
 * NumArray obj = new NumArray(nums);
 * int param_1 = obj.SumRange(left,right);
 */
