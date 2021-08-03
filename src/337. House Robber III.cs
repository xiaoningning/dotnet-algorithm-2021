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
    // Memo to avoid TLE
    Dictionary<TreeNode, int> d = new Dictionary<TreeNode, int>();
    public int Rob(TreeNode root) {
        return DFS(root);
    }
    int DFS(TreeNode root) {
        if (root == null) return 0;
        if (d.ContainsKey(root)) return d[root];
        // no rob at root
        int c1 = DFS(root.left) + DFS(root.right);
        // rob at root, no rob at left/right
        int c2 = root.left == null ? 0 : DFS(root.left.left) + DFS(root.left.right);
        c2 += root.right == null ? 0 : DFS(root.right.left) + DFS(root.right.right);
        c2 += root.val;
        return d[root] = Math.Max(c1, c2);
    }
    public int Rob1(TreeNode root) {
        var ans = DFST(root);
        return Math.Max(ans.noRob, ans.rob);
    }
    (int noRob, int rob) DFST(TreeNode root) {
        if (root == null) return (0,0);
        var left = DFST(root.left);
        var right = DFST(root.right);
        int rob = root.val + left.noRob + right.noRob;
        int noRob = Math.Max(left.rob, left.noRob) + Math.Max(right.rob, right.noRob);
        return (noRob, rob);
    }
    // TLE
    public int Rob3(TreeNode root) {
        return Math.Max(DFSF(root, false), DFSF(root, true));
    }
    int DFSF(TreeNode root, bool rob) {
        if (root == null) return 0;
        if (rob) return root.val + DFSF(root.left, false) + DFSF(root.right, false);
        else return Rob(root.left) + Rob(root.right);
    }
}
