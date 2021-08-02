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
    // Path is a sequence of adjacent nodes
    public int MaxPathSum(TreeNode root) {
        int ans = Int32.MinValue;
        MaxPathSum(root, ref ans);
        return ans;
    }
    int MaxPathSum(TreeNode root, ref int ans) {
        if (root == null) return 0;
        int left = Math.Max(0, MaxPathSum(root.left, ref ans));
        int right = Math.Max(0, MaxPathSum(root.right, ref ans));
        ans = Math.Max(ans, left + right + root.val);
        return Math.Max(left, right) + root.val;
    }
}
