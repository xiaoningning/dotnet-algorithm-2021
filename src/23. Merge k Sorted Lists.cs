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
    public ListNode MergeKLists(ListNode[] lists) { 
        var d = new Dictionary<int,int>();
        int mx = Int32.MinValue, mn = Int32.MaxValue;
        foreach (var l in lists) {
            var t = l;
            while (t != null) {
                mx = Math.Max(mx, t.val);
                mn = Math.Min(mn, t.val);
                if (!d.ContainsKey(t.val)) d[t.val] = 0;
                d[t.val]++;
                t = t.next;
            }
        }
        var ans = new ListNode();
        var ptr = ans;
        for (int i = mn; i <= mx; i++) {
            if (d.ContainsKey(i)) {
                while(d[i]-- > 0) {
                    ptr.next = new ListNode(i);
                    ptr = ptr.next;
                }
            }
        }
        // T: O(nklogk), S: O(k)
        return ans.next;
    }
    
    public ListNode MergeKLists1(ListNode[] lists) {
        // base case for split array
        if (!lists.Any()) return null;
        if (lists.Length == 1) return lists[0];
        int k = lists.Length;
        var l = new ListNode[k / 2];
        var r = new ListNode[(k + 1) / 2];
        Array.Copy(lists, 0, l, 0, k / 2);
        Array.Copy(lists, k / 2, r, 0, (k + 1) / 2);
        var l1 = MergeKLists(l);
        var l2 = MergeKLists(r);
        // T: O(nklogk), S: O(logk)
        return MergeTwoLists(l1, l2);
    }
    
    public ListNode MergeKLists2(ListNode[] lists) {
        return MergeKLists(lists, 0, lists.Length - 1);
    }
    public ListNode MergeKLists(ListNode[] lists, int l, int r) {
        if (l > r) return null;
        if (l == r) return lists[l];
        int m = l + (r - l) / 2;
        var left = MergeKLists(lists, l, m);
        var right = MergeKLists(lists, m + 1, r);
        return MergeTwoLists(left, right);
    }
    
    public ListNode MergeTwoLists(ListNode l1, ListNode l2) {
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
