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
    List<string> ans = new List<string>();
    public IList<string> BinaryTreePaths1(TreeNode root) {
        if (root == null) return ans;
        DFS(root, "");
        return ans;
    }
    void DFS(TreeNode root, string s) {
        if (root == null) return;
        s += s == "" ? root.val.ToString() : "->" + root.val;
        if (root.left == null && root.right == null) { 
            ans.Add(new string(s)); 
            return;
        }
        DFS(root.left, s);
        DFS(root.right, s);
        int idx = s.LastIndexOf("->");
        if (idx >= 0) s = s.Substring(0, idx);
    }
    public IList<string> BinaryTreePaths(TreeNode root) {
        List<string> res = new List<string>();
        if (root == null) return res;
        if (root.left == null && root.right == null) { 
            res.Add(root.val.ToString());
        }
        foreach (var str in BinaryTreePaths(root.left))
            res.Add(root.val + "->" + str);
        foreach (var str in BinaryTreePaths(root.right))
            res.Add(root.val + "->" + str);
        return res;
    }
}
