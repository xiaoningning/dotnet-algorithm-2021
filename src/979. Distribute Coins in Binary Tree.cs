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
    // Total n nodes, total n coins
    // all moves count
    int ans = 0;
    public int DistributeCoins(TreeNode root) {
        GetMoves(root);
        return ans;
    }
    int GetMoves(TreeNode root) {
        if (root == null) return 0;
        
        int left = GetMoves(root.left);
        int right = GetMoves(root.right);
        // balance left and right coins
        ans += Math.Abs(left) + Math.Abs(right);
        // - move in, + move out
        // moves of subtree
        return left + right + root.val - 1;
    }
}
