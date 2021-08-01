/**
 * Definition for a binary tree node.
 * public class TreeNode {
 *     public int val;
 *     public TreeNode left;
 *     public TreeNode right;
 *     public TreeNode(int val=0, TreeNode left=null, TreeNode right=null) {
 *         this.val = val;
 *         this.left = left;
 *         this.right = right;
 *     }
 * }
 */
public class Solution {
    TreeNode prev;
    int mx = 0, cnt = 1;
    List<int> ans = new List<int>();
    public int[] FindMode(TreeNode root) {
        InOrder(root);
        return ans.ToArray();
    }
    void InOrder(TreeNode root) {
        if (root == null) return;
        InOrder(root.left);
        // reset cnt if prev ! = root
        // the left subtree <= root
        // thr right subtree >= root
        if (prev != null) cnt = prev.val == root.val ? cnt + 1 : 1;
        if (cnt >= mx) {
            if (cnt > mx) ans.Clear();
            mx = cnt;
            ans.Add(root.val);
        }
        prev = root;
        InOrder(root.right);
    }
}
