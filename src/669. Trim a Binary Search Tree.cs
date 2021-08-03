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
    // T: O(n), S: O(1)
    public TreeNode TrimBST1(TreeNode root, int low, int high) {
        if (root == null) return null;
        if (root.val < low) return TrimBST(root.right, low, high);
        else if (root.val > high) return TrimBST(root.left, low, high);
        else {
            root.left = TrimBST(root.left, low, root.val);
            root.right = TrimBST(root.right, root.val, high);
            return root;
        }
    }
    public TreeNode TrimBST(TreeNode root, int low, int high) {
        if (root == null) return null;
        // it is while to trim all
        while (root != null && (root.val < low || root.val > high)) {
            root = root.val < low ? root.right : root.left;
        }
        var ptr = root;
        while (ptr != null) {
            while (ptr.left != null && ptr.left.val < low) ptr.left = ptr.left.right;
            ptr = ptr.left;
        }
        ptr = root;
        while (ptr != null) {
            while (ptr.right != null && ptr.right.val > high) ptr.right = ptr.right.left;
            ptr = ptr.right;
        }
        return root;
    }
}
