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
    public int ClosestValue(TreeNode root, double target) {
        int ans = Int32.MaxValue;
        while (root != null) {
            if (Math.Abs(ans - target) >= Math.Abs(root.val - target)) ans = root.val;
            root = target < root.val ? root.left : root.right;
        }
        return ans;
    }
    public int ClosestValue1(TreeNode root, double target) {
        if (root == null) return Int32.MaxValue;
        if (target < root.val) {
            int prev = ClosestValue(root.left, target);
            return Math.Abs(target - prev) < Math.Abs(target - root.val) ? prev : root.val;
        }
        else {
            int next = ClosestValue(root.right, target);
            return Math.Abs(target - next) < Math.Abs(target - root.val) ? next : root.val;
        }
    }
}
