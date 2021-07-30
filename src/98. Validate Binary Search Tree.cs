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
    // in order should be sorted
    TreeNode prev = null;
    public bool IsValidBST(TreeNode root) {
        return InOrder(root);
    }
    bool InOrder(TreeNode root) {
        if (root == null) return true;
        if (!InOrder(root.left)) return false;
        if (prev != null && root.val <= prev.val) return false;;
        prev = root;
        return InOrder(root.right);
    }
    public bool IsValidBST1(TreeNode root) {
        // -2^31 <= Node.val <= 2^31 - 1
        return IsValidBST(root, Int64.MinValue, Int64.MaxValue);
    }
    bool IsValidBST(TreeNode root, long mn, long mx) {
        if (root == null) return true;
        Console.WriteLine(root.val);
        if (root.val <= mn || root.val >= mx) return false;
        else return IsValidBST(root.left, mn, root.val)
                    && IsValidBST(root.right, root.val, mx);
    }
}
