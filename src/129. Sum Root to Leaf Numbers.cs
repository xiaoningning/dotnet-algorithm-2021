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
    public int SumNumbers(TreeNode root) {
        SumNums(root, 0);
        return ans;
    }
    void SumNums(TreeNode root, int cnt) {
        if (root == null) return;
        if (root.left == null && root.right == null) { 
            ans += cnt * 10 + root.val;
            return;
        }
        cnt = cnt * 10 + root.val;
        SumNums(root.left, cnt);
        SumNums(root.right, cnt);
        cnt %= 10;
    }
}
