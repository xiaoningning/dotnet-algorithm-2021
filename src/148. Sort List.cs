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
    // buttom-up merge sort
    /*
    Progress	Step size	Sublists	Merged
    [3, 45, 2, 15, 37, 19, 39, 20]	1	[3], [45], [2], [15], [37], [19], [39], [20]	[3, 45], [2, 15], [19, 37], [20, 39]
    [3, 45, 2, 15, 19, 37, 20, 39]	2	[3, 45], [2, 15], [19, 37], [20, 39]	[2, 3, 15, 45], [19, 20, 37, 39]
    [2, 3, 15, 45, 19, 20, 37, 39]	4	[2, 3, 15, 45], [19, 20, 37, 39]	[2, 3, 15, 19, 20, 37, 39, 45]
    */
    public ListNode SortList(ListNode head) {
        // base case
        if (head == null || head.next == null) return head;
        int len = 1;
        ListNode ptr = head;
        while (ptr != null) {
            ptr = ptr.next;
            len++;
        }
        ListNode ans = new ListNode();
        ans.next = head;
        ListNode l, r, tail;
        for (int n = 1; n <= len; n *= 2) {
            ptr = ans.next;
            tail = ans;
            while (ptr != null) {
                l = ptr;
                r = SplitList(ptr, n);
                ptr = SplitList(r, n);
                var res = MergeTwo(l, r);
                tail.next = res.Item1;
                tail = res.Item2;
            }
        }
        // T: O(nlogn) S: O(1)
        return ans.next;
    }
    
    // split list int first n nodes and the rest
    ListNode SplitList(ListNode head, int n) {
        while (--n > 0 && head != null) head = head.next;
        ListNode rest = head != null ? head.next : null;
        if (head != null) head.next = null;
        return rest;
    }
    
    (ListNode, ListNode) MergeTwo(ListNode l1, ListNode l2) {
        var ans = new ListNode();
        var ptr = ans;
        while (l1 != null && l2 != null) {
            if (l1.val < l2.val) {
                ptr.next = l1;
                l1 = l1.next;
            }
            else {
                ptr.next = l2;
                l2 = l2.next;
            }
            ptr = ptr.next;
        }
        ptr.next = (l1 != null) ? l1 : l2;
        while (ptr.next != null) ptr = ptr.next;
        return (ans.next, ptr);
    }
    
    // Up-down merge sort
    public ListNode SortList1(ListNode head) {
        // base case
        if (head == null || head.next == null) return head;
        ListNode slow = head, fast = head, ptr = head;
        while (fast != null && fast.next != null) {
            ptr = slow;
            fast = fast.next.next;
            slow = slow.next;
        }
        ptr.next = null; // cut listNode into half;
        // T: O(nlogn) S: O(logn)
        return MergeTwoLists(SortList(head), SortList(slow));
    }
    ListNode MergeTwoLists(ListNode l1, ListNode l2) {
        if (l1 == null) return l2;
        if (l2 == null) return l1;
        if (l1.val < l2.val) {
            l1.next = MergeTwoLists(l1.next, l2);
            return l1;
        }
        else {
            l2.next = MergeTwoLists(l1, l2.next);
            return l2;
        }
    }
}
