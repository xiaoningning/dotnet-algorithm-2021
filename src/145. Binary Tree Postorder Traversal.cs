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
    public IList<int> PostorderTraversal(TreeNode root) {
        if (root == null) return ans;
        PostorderTraversal(root.left);
        PostorderTraversal(root.right);
        ans.Add(root.val);
        return ans;
    }
    
    public IList<int> PostorderTraversal1(TreeNode root) {
        if (root == null) return ans;
        // PostOder uses stack
        var q = new Stack<TreeNode>();
        q.Push(root);
        while (q.Any()) {
            var n = q.Pop();
            // PostOrder insert at the front
            ans.Insert(0, n.val);
            if (n.left != null) q.Push(n.left);
            if (n.right != null) q.Push(n.right);
        }
        return ans;
    }
}
