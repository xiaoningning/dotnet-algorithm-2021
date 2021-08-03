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
    // re-calculate height of nodes
    // T: O(nlogn)
    public bool IsBalanced1(TreeNode root) {
        if (root == null) return true;
        if (Math.Abs(GetHeight(root.left) - GetHeight(root.right)) > 1) return false;
        // every subtree needs to be balance as well
        else return IsBalanced(root.left) && IsBalanced(root.right);
    }
    int GetHeight(TreeNode n) {
        if (n == null) return 0;
        return 1 + Math.Max(GetHeight(n.left), GetHeight(n.right));
    }
    // Check subtree along with height calculation
    // T: O(n)
    public bool IsBalanced(TreeNode root) {
        return CheckBalance(root) != -1;
    }
    int CheckBalance(TreeNode root) {
        if (root == null) return 0;
        int left = CheckBalance(root.left);
        int right = CheckBalance(root.right);
        if (left == -1) return -1;
        if (right == -1) return -1;
        if (Math.Abs(left - right) > 1) return -1;
        else return 1 + Math.Max(left, right); // return height here
    }
}
