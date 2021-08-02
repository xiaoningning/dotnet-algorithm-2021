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
    // BFS
    public IList<IList<int>> LevelOrder(TreeNode root) {
        if (root == null) return ans;
        var cur = new Queue<TreeNode>();
        var next = new Queue<TreeNode>();
        cur.Enqueue(root);
        while (cur.Any()) {
            ans.Add(new List<int>());
            foreach (var t in cur) {
                ans.Last().Add(t.val);
                if (t.left != null) next.Enqueue(t.left);
                if (t.right != null) next.Enqueue(t.right);
            }
            cur = next;
            next = new Queue<TreeNode>();
        }
        return ans;
    }
    // DFS
    public IList<IList<int>> LevelOrder1(TreeNode root) {
        DFS(root, 0);
        return ans;
    }
    void DFS(TreeNode node, int level) {
        if (node == null) return;
        if (ans.Count < level + 1) ans.Add(new List<int>());
        ans[level].Add(node.val);
        DFS(node.left, level+1);
        DFS(node.right, level+1);
    }
}
