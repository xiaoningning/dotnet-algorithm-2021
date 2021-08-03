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
    // Iterative
    public int DeepestLeavesSum1(TreeNode root) {
        if (root == null) return 0;
        var prev = new List<int>();
        var q = new Queue<TreeNode>();
        q.Enqueue(root);
        while (q.Any()) {
            int size = q.Count;
            var cur = new List<int>();
            for (int i = 0; i < size; i++) {
                var n = q.Dequeue();
                cur.Add(n.val);
                if (n.left != null) q.Enqueue(n.left);
                if (n.right != null) q.Enqueue(n.right);
            }
            prev = new List<int>(cur);
        }
        // T: O(n) S: O(n)
        return prev.Sum();
    }
    
    // Recursion
    int ans = 0;
    int mxLevel = 0;
    public int DeepestLeavesSum(TreeNode root) {
        DFS(root, 0);
        // T: O(n) S: O(n)
        return ans;
    }
    void DFS(TreeNode node, int level) {
        if (node == null) return;
        if (level > mxLevel) {
            mxLevel = level;
            ans = 0;
        }
        // only sum at max level
        if (level == mxLevel) ans += node.val;
        DFS(node.left, level + 1);
        DFS(node.right, level + 1);
    }
}
