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
    TreeNode prev, x, y;
    public void RecoverTree(TreeNode root) {
        InOrder(root);
        SwapVal(x, y);
        //T: O(n) S: O(3)
    }
    void InOrder(TreeNode root) {
        if (root == null) return;
        InOrder(root.left);
        if (prev != null && prev.val > root.val) {
            if (x == null) x = prev;
            y = root;
        }
        prev = root;
        InOrder(root.right);
    }
    void SwapVal(TreeNode x, TreeNode y) {
        int t = x.val; x.val = y.val; y.val = t;
    }
}
