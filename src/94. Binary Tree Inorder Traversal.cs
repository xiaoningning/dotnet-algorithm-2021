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
    List<int> ans = new List<int>();
    public IList<int> InorderTraversal1(TreeNode root) {
        InOrder(root);
        return ans;
    }
    void InOrder(TreeNode root) {
        if (root == null) return;
        InOrder(root.left);
        ans.Add(root.val);
        InOrder(root.right);
    }
    public IList<int> InorderTraversal(TreeNode root) {
        if (root == null) return ans;
        var s = new Stack<TreeNode>();
        var ptr = root;
        // go all the way to left;
        // check right at the level
        while (ptr != null || s.Any()) {
            while (ptr != null) {
                s.Push(ptr);
                ptr = ptr.left;
            }
            var t = s.Pop();
            ans.Add(t.val);
            ptr = t.right;
        }
        return ans;
    }
}
