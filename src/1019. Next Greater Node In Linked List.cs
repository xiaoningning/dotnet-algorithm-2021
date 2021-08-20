/**
 * Definition for singly-linked list.
 * public class ListNode {
 *     public int val;
 *     public ListNode next;
 *     public ListNode(int val=0, ListNode next=null) {
 *         this.val = val;
 *         this.next = next;
 *     }
 * }
 */
public class Solution {
    // Monotonic stack
    // O(n)
    public int[] NextLargerNodes1(ListNode head) {
        int n = 0;
        var p = head;
        while ( p != null) { n++; p = p.next; }
        int[] ans = new int[n];
        var st = new Stack<(int,int)>();
        int i = 0;
        p = head;
        while (p != null) {
            while (st.Any() && p.val > st.Peek().Item1) {
                var pre = st.Pop();
                ans[pre.Item2] = p.val;
            }
            st.Push((p.val, i));
            p = p.next;
            i++;
        }
        while (st.Any()) ans[st.Pop().Item2] = 0;
        return ans;
    }
    // reverse + monotonic stack
    public int[] NextLargerNodes2(ListNode head) {
        var nums = new List<int>();
        var p = head;
        while (p != null) { nums.Add(p.val); p = p.next; }
        int[] ans = new int[nums.Count];
        var st = new Stack<int>();
        for (int i = nums.Count - 1; i >= 0; --i) {
            // >= since 0 case is handle here.  
            // it needs to pop all, include itself
            while (st.Any() && nums[i] >= st.Peek()) st.Pop();
            ans[i] = st.Any() ? st.Peek() : 0;
            st.Push(nums[i]);
        }
        return ans;
    }
    // V2 without reverse
    public int[] NextLargerNodes(ListNode head) {
        var nums = new List<int>();
        int[] ans = new int[1];
        var st = new Stack<int>();
        int i = 0;
        while (head != null) {
            nums.Add(head.val);
            while (st.Any() && head.val > nums[st.Peek()]) ans[st.Pop()] = head.val;
            st.Push(i);
            Array.Resize(ref ans, ++i);
            head = head.next;
        }
        return ans;
    }
}
