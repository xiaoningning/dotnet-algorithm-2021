/**
 * Definition for singly-linked list.
 * public class ListNode {
 *     public int val;
 *     public ListNode next;
 *     public ListNode(int x) {
 *         val = x;
 *         next = null;
 *     }
 * }
 */
public class Solution {
    public bool HasCycle(ListNode head) {
        var seen = new HashSet<ListNode>();
        while (head != null) {
            if (seen.Contains(head)) return true;
            seen.Add(head);
            head = head.next;
        }
        // T: O(n), S: O(n)
        return false;
    }
    public bool HasCycle1(ListNode head) {
        ListNode slow = head;
        ListNode fast = head;
        while (fast != null && fast.next != null) {
            slow = slow.next;
            fast = fast.next.next;
            // it can have duplicated value
            if (fast == slow) return true;
        }
        // T: O(n), S: O(1)
        return false;
    }
}
