public class Solution {
    // monotonic stack
    public int[] NextGreaterElements(int[] nums) {
        int n = nums.Length;
        int[] ans = new int[n];
        Array.Fill(ans, -1);
        var st = new Stack<int>();
        // circular array
        for (int i = 0; i < 2 * n; i++) {
            while(st.Any() && nums[st.Peek()] < nums[i % n]) ans[st.Pop()] = nums[i % n];
            if (i < n) st.Push(i % n);
        }
        return ans;
    }
}
