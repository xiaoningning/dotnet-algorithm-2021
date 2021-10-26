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
    // map to dictionary count as priority queue := T: O(nk)
    // priority queue := T: O(nklogk), S: O(k)
    public ListNode MergeKLists(ListNode[] lists) {
        var d = new Dictionary<int, int>();
        foreach (var l in lists) {
            var t = l;
            while (t != null) {
                if (!d.ContainsKey(t.val)) d[t.val] = 0;
                d[t.val]++;
                t = t.next;
            }
        }
        if (!d.Any()) return null; // [] or [[]]
        var ans = new ListNode();
        var ptr = ans;
        int mn = d.Keys.Min(), mx = d.Keys.Max();
        for (int i = mn; i <= mx; i++) {
            if (!d.ContainsKey(i)) continue;
            while (--d[i] >=0) {
                ptr.next = new ListNode(i);
                ptr = ptr.next;
            }
        }
        return ans.next;
    }
    // divide conqur
    public ListNode MergeKLists1(ListNode[] lists) {
        Func<ListNode[],int,int,ListNode> merge = null;
        merge = (lst, l, r) => {
            if (l > r) return null;
            if (l == r) return lst[l];
            int m = l + (r - l) / 2;
            var left = merge(lst, l, m);
            var right = merge(lst, m + 1, r);
            return MergeTwoLists(left, right);
        };
        return merge(lists, 0, lists.Length - 1);
    }
    // merge two sorted ListNode
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
