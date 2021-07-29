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
    public ListNode SwapPairs1(ListNode head) {
        if (head == null || head.next == null) return head;
        var ans = head.next;
        head.next = SwapPairs(head.next.next);
        ans.next = head;
        return ans;
    }
    public ListNode SwapPairs2(ListNode head) {
        ListNode ans = new ListNode();
        ans.next = head;
        // create a object as pointer in c# or java
        ListNode ptr = ans;
        while (ptr.next != null && ptr.next.next != null) {
            var t = ptr.next.next;
            ptr.next.next = t.next;
            t.next = ptr.next;
            ptr.next = t;
            ptr = t.next;
        }
        return ans.next;
    }
    // two tmp nodes. easy to read
    public ListNode SwapPairs(ListNode head) {
        ListNode ans = new ListNode();
        ans.next = head;
        // create a object as pointer in c# or java
        ListNode ptr = ans;
        while (ptr.next != null && ptr.next.next != null) {
            var first = ptr.next;
            var second = ptr.next.next;
            first.next = second.next;
            second.next = first;
            ptr.next = second;
            ptr.next.next = first;
            ptr = first;
        }
        return ans.next;
    }
}
