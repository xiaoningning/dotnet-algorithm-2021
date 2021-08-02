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
    // memo to optimize speed
    Dictionary<TreeNode,int> d = new Dictionary<TreeNode,int>();
    // recursion
    public int DiameterOfBinaryTree(TreeNode root) {
        int ans = 0;
        GetHeightOfTree(root, ref ans);
        return ans;
    }   
    int GetHeightOfTree(TreeNode root, ref int ans) {
        if (root == null) return 0;
        if (d.ContainsKey(root)) return d[root];
        int left = GetHeightOfTree(root.left, ref ans);
        int right = GetHeightOfTree(root.right, ref ans);
        // diameter = height of left + right
        ans = Math.Max(ans, left + right);
        return d[root] = Math.Max(left, right) + 1;
    }
    // DFS + stack
    public int DiameterOfBinaryTree1(TreeNode root) {
        int ans = 0;
        var st = new Stack<TreeNode>();
        // c# dict does not support null, runtime error
        d.Add(null, -1);
        st.Push(root);
        while (st.Any()) {
            var t = st.Peek();
            if (d.ContainsKey(t.left) && d.ContainsKey(t.right)) {
                int l = d[t.left];
                int r = d[t.right];
                // null: -1, diameter =  heights of l + r
                ans = Math.Max(ans, r + l);
                d[t] = Math.Max(l, r) + 1;
                if (t.left != null) d.Remove(t.left);
                if (t.right != null) d.Remove(t.right);
                st.Pop(); 
            }
            else {
                st.Push(t.left);
                st.Push(t.right);
            }
        }
        return ans;
    }
}
