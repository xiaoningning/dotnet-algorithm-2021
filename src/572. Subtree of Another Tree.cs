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
    public bool IsSubtree(TreeNode root, TreeNode subRoot) {
        // null is subtree of any tree
        if (subRoot == null) return true;
        if (root == null) return false;
        if (IsSameTree(root, subRoot)) return true;
        else return IsSubtree(root.left, subRoot) || IsSubtree(root.right, subRoot);
    }
    // exactly same
    bool IsSameTree(TreeNode root, TreeNode subRoot) { 
        if (root == null && subRoot == null) return true;
        if (root == null || subRoot == null) return false;
        return (root.val == subRoot.val) && IsSameTree(root.left, subRoot.left) && IsSameTree(root.right, subRoot.right);
    }
}
