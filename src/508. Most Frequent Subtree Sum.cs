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
    Dictionary<int,int> d = new Dictionary<int,int>();
    List<int> ans = new List<int>();
    int mx = 0;
    public int[] FindFrequentTreeSum(TreeNode root) {
        TreeSum(root);
        return ans.ToArray();
    }
    int TreeSum(TreeNode root) {
        if (root == null) return 0;
        int sum = root.val + TreeSum(root.left) + TreeSum(root.right);
        if (!d.ContainsKey(sum)) d[sum] = 0;
        int freq = ++d[sum];
        if (freq > mx) {
            mx = freq;
            ans.Clear();
        }
        if (freq == mx) ans.Add(sum);
        return sum;
    }
}
