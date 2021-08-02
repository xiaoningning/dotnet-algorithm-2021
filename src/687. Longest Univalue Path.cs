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
    int ans = 0;
    public int LongestUnivaluePath(TreeNode root) {
        // -1000 <= Node.val <= 1000
        UnivaluePath(root);
        return ans;
    }
    
    int UnivaluePath(TreeNode root) {
        if (root == null) return 0;
        int left = UnivaluePath(root.left);
        int right = UnivaluePath(root.right);
        int l = 0, r = 0;
        if (root.left != null && root.val == root.left.val) l = left + 1;
        if (root.right != null && root.val == root.right.val) r = right + 1;
        ans = Math.Max(ans, l + r);
        // Path, not a subtree => just return one side
        return Math.Max(l, r);
    }
}
