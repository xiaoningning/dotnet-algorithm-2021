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
    List<IList<int>> res = new List<IList<int>>();
    public IList<IList<int>> LevelOrderBottom(TreeNode root) {
        LevelOrderBottom(root, 0);
        res.Reverse();
        return res;
    }
    void LevelOrderBottom(TreeNode root, int level) {
        if (root == null) return;
        if (res.Count < level + 1) res.Add(new List<int>());
        res[level].Add(root.val);
        LevelOrderBottom(root.left, level + 1);
        LevelOrderBottom(root.right, level + 1);
    }
    
    Stack<List<TreeNode>> st = new Stack<List<TreeNode>>();
    public IList<IList<int>> LevelOrderBottom1(TreeNode root) {
        var ans = new List<IList<int>>();
        if (root == null) return ans;
        st.Push(new List<TreeNode>());
        st.Peek().Add(root);
        var ptr = root;
        while (ptr != null) {
            var t = st.Peek();
            st.Push(new List<TreeNode>());
            foreach (var n in t) {
                if (n.left != null) st.Peek().Add(n.left);
                if (n.right != null) st.Peek().Add(n.right);
            }
            // last level
            if (!st.Peek().Any())  {
                st.Pop();
                ptr = null;
            }
            else ptr = st.Peek().Last();
        }
        while (st.Any()) {
            var l = st.Pop();
            ans.Add(new List<int>());
            foreach (var n in l) ans.Last().Add(n.val);
        }
        return ans;
    }
}
