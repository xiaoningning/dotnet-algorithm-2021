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
    // init 0 in sums for root.val = targetSum
    Dictionary<int, int> sums = new Dictionary<int, int>(){[0] = 1};
    int ans = 0;
    public int PathSum(TreeNode root, int targetSum) {
        PathSum(root, 0, targetSum);
        return ans;
    }
    void PathSum(TreeNode node, int cur, int target) {
        if (node == null) return;
        cur += node.val;
        ans += sums.ContainsKey(cur - target) ? sums[cur - target] : 0;
        if (!sums.ContainsKey(cur)) sums[cur] = 0;
        sums[cur]++;
        PathSum(node.left, cur, target);
        PathSum(node.right, cur, target);
        // remove cur sum for a different path
        sums[cur]--;
    }
}
