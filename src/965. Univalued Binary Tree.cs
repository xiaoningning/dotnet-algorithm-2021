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
    public bool IsUnivalTree(TreeNode root) {
        if (root == null) return true;
        bool left = IsUnivalTree(root.left);
        bool right = IsUnivalTree(root.right);
        return left && right 
            && (root.left != null ? root.val == root.left.val : true)
            && (root.right != null ? root.val == root.right.val : true);
    }
}
