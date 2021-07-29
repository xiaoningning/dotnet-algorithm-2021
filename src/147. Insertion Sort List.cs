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
    // insert sort O(n^2), S: O(1)
    // merge sort O(nlogn), S: O(logn)
    public ListNode InsertionSortList(ListNode head) {
        ListNode ans = new ListNode();
        ListNode ptr = ans;
        while (head != null) {
            var t = head.next;
            ptr = ans;
            while (ptr.next != null && ptr.next.val <= head.val) ptr = ptr.next;
            head.next = ptr.next; 
            ptr.next = head;
            head = t;
        }
        //T: O(n^2) S: O(1)
        return ans.next;
    }
}
