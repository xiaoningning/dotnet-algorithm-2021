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
    public TreeNode SortedArrayToBST(int[] nums) {
        if (nums.Length == 0) return null;
        return ArrayToBST(nums, 0, nums.Length - 1);
    }
    TreeNode ArrayToBST(int[] nums, int l, int r) {
        if (l > r) return null;
        int m = l + (r - l) / 2;
        var node = new TreeNode(nums[m]);
        node.left = ArrayToBST(nums, l, m - 1);
        node.right = ArrayToBST(nums, m + 1, r);
        return node;
    }
}
