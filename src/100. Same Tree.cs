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
    public bool IsSameTree(TreeNode p, TreeNode q) {
        // both null
        if (p == null && q == null) return true;
        // one null, but not another
        if (p == null || q == null) return false;
        // both not null, but val different
        if (p.val != q.val) return false;
        return IsSameTree(p.left, q.left)
            && IsSameTree(p.right, q.right);
    }
}
