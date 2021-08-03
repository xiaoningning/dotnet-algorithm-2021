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
    public bool LeafSimilar(TreeNode root1, TreeNode root2) {
        var l1 = new List<int>();
        var l2 = new List<int>();
        GetLeafs(root1, ref l1);
        GetLeafs(root2, ref l2);
        return l1.SequenceEqual(l2);
    }
    void GetLeafs(TreeNode r, ref List<int> res) {
        if (r == null) return;
        if (r.left == null && r.right == null) res.Add(r.val);
        GetLeafs(r.left, ref res);
        GetLeafs(r.right, ref res);
    }
}
