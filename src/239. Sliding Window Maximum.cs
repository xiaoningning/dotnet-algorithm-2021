public class Solution {
    // Monotonic Queue
    // T: O(n)
    public int[] MaxSlidingWindow(int[] nums, int k) {
        if (k == 0) return nums;
        var ans = new List<int>();
        var q = new List<int>();
        for (int i = 0; i < nums.Length; i++) {
            // keep the same max value in the monotonic queue
            while (q.Any() && nums[q.Last()] < nums[i]) q.RemoveAt(q.Count - 1);
            q.Add(i);
            if (i >= k - 1) ans.Add(nums[q.First()]);
            // q.Add() first, no need to check q.Any()
            if (i >= q.First() + k - 1) q.RemoveAt(0);
            
        }
        return ans.ToArray();
    }
    public int[] MaxSlidingWindow1(int[] nums, int k) {
        var mq = new MonotonicQueue();
        var ans = new List<int>();
        for (int i = 0; i < nums.Length; i++) {
            mq.Push(nums[i]);
            if (i >= k - 1) {
                ans.Add(mq.Max());
                if (nums[i - (k - 1)] == mq.Max()) mq.Pop();
            }
        }
        return ans.ToArray();
    }
}
// value decreasing monotonic queue
public class MonotonicQueue {
    List<int> q;
    public MonotonicQueue() { q = new List<int>();}
    public void Push(int e) {
        // "<" since remove 1st of q based on max value
        while (q.Any() && q.Last() < e) q.RemoveAt(q.Count - 1);
        q.Add(e);
    }
    public void Pop() { q.RemoveAt(0); }
    public int Max() { return q.First();}
}
