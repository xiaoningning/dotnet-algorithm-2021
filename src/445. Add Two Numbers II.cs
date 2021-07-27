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
        int n1 = GetLength(l1), n2 = GetLength(l2);
        var head = new ListNode(1);
        var ans = n1 > n2 ? AddNums(l1, l2) : AddNums(l2, l1);
        if (ans.carry > 0) {
            head.next = ans.node;
            return head;
        }
        else return ans.node;
    }
    (ListNode node, int carry) AddNums(ListNode l1, ListNode l2) {
        if (l1 == null) return (null, 0);
        int n1 = GetLength(l1), n2 = GetLength(l2);
        var ans = n1 > n2 ? AddNums(l1.next, l2) : AddNums(l1.next, l2.next);
        var v = ans.carry + l1.val + ((n1 > n2) ? 0 : l2.val);
        var node = new ListNode(v % 10);
        node.next = ans.node;
        var carry = v / 10;
        return (node, carry);
    }
    int GetLength(ListNode l) {
        int ans = 0;
        while (l != null) {
            ans++; 
            l = l.next;
        }
        return ans;
    }
}
