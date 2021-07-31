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
    int ans = Int32.MaxValue;
    TreeNode prev = null;
    public int GetMinimumDifference(TreeNode root) {
        InOrder(root);
        return ans;
    }
    void InOrder(TreeNode root) {
        if (root == null) return;
        InOrder(root.left);
        if (prev != null) ans = Math.Min(ans, root.val - prev.val);
        prev = root;
        InOrder(root.right);
    }
}
