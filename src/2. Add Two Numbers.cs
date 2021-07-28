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
    public ListNode AddTwoNumbers(ListNode l1, ListNode l2) {
        ListNode head = new ListNode();
        ListNode ans = head;
        int carry = 0;
        // if carry > 0, extra node for the carry
        while (l1 != null || l2 != null || carry != 0) {
            carry += l1 != null ? l1.val : 0;
            carry += l2 != null ? l2.val : 0;
            head.next = new ListNode(carry % 10);
            head = head.next;
            carry = carry / 10;
            if (l1 != null) l1 = l1.next;
            if (l2 != null) l2 = l2.next;
        }
        return ans.next;
    }
}
