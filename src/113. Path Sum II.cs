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
    List<IList<int>> ans = new List<IList<int>>();
    public IList<IList<int>> PathSum(TreeNode root, int targetSum) {
        var cur = new List<int>();
        PathSum(root, cur, targetSum);
        return ans;
    }
    // DFS
    void PathSum(TreeNode node, List<int> cur, int target) {
        if (node == null) return;
        if (node.left == null && node.right == null) {
            if (node.val == target) {
                cur.Add(node.val);
                // create a new object into ans
                // otherwise recursion on the same object
                ans.Add(new List<int>(cur));
                // remove the last one before return the prev level
                cur.RemoveAt(cur.Count - 1);
            }
            return; // reach the leaf, always return the prev level
        }
        cur.Add(node.val);
        PathSum(node.left, cur, target - node.val);
        PathSum(node.right, cur, target - node.val);
        cur.RemoveAt(cur.Count - 1);
    }
}
