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
    Dictionary<int, Dictionary<int, List<int>>> d = new Dictionary<int, Dictionary<int, List<int>>>();
    List<IList<int>> ans = new List<IList<int>>();
    public IList<IList<int>> VerticalTraversal(TreeNode root) {
        if (root == null) return ans;
        VerticalTraversal(root, 0, 0);
        var ks = d.Keys.ToList();
        ks.Sort();
        foreach(var k in ks) {
            var m = d[k];
            var t = new List<int>();
            var ks2 = m.Keys.ToList();
            ks2.Sort();
            foreach (var k2 in ks2) {
                m[k2].Sort();
                t.AddRange(m[k2]);
            }
            ans.Add(t);
        }
        return ans;
    }
    void VerticalTraversal(TreeNode root, int x, int y) {
        if (root == null) return;
        if (!d.ContainsKey(y)) d[y] = new Dictionary<int, List<int>>();
        if (!d[y].ContainsKey(x)) d[y][x] = new List<int>();
        d[y][x].Add(root.val);
        VerticalTraversal(root.left, x + 1, y - 1);
        VerticalTraversal(root.right, x + 1, y + 1);
    }
}
