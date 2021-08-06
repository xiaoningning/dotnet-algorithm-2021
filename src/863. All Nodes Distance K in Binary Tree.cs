/**
 * Definition for a binary tree node.
 * public class TreeNode {
 *     public int val;
 *     public TreeNode left;
 *     public TreeNode right;
 *     public TreeNode(int x) { val = x; }
 * }
 */
public class Solution {
    List<int> ans = new List<int>();
    // DFS + BFS
    Dictionary<TreeNode, HashSet<TreeNode>> m = new Dictionary<TreeNode, HashSet<TreeNode>>();
    public IList<int> DistanceK(TreeNode root, TreeNode target, int k) {
        FindParentChildren(root, null);
        var q = new Queue<TreeNode>();
        var seen = new HashSet<TreeNode>();
        seen.Add(target);
        q.Enqueue(target);
        int d = 0;
        // DFS
        while (q.Any() && d <= k) {
            int size = q.Count;
            // BFS
            while (--size >= 0) {
                var t = q.Dequeue();
                if (d == k) ans.Add(t.val);
                foreach (var c in m[t]) {
                    if (seen.Contains(c)) continue;
                    q.Enqueue(c); seen.Add(c);
                }
            }
            d++;
        }
        return ans;
    }
    void FindParentChildren(TreeNode child, TreeNode parent) {
        if (child == null) return;
        if (!m.ContainsKey(child)) m[child] = new HashSet<TreeNode>();
        if (parent != null) {
            if (!m.ContainsKey(parent)) m[parent] = new HashSet<TreeNode>();
            m[child].Add(parent);
            m[parent].Add(child);
        }
        FindParentChildren(child.left, child);
        FindParentChildren(child.right, child);
    }
    
    // recursion: calculate k along the path
    public IList<int> DistanceK1(TreeNode root, TreeNode target, int k) {
        GetDistance(root, target, k);
        return ans;
    }
    int GetDistance(TreeNode root, TreeNode target, int k) {
        // -1 not found;
        if (root == null) return -1;
        if (root.val == target.val) {
            GetAns(root, k);
            return 0;
        }
        int l = GetDistance(root.left, target, k);
        int r = GetDistance(root.right, target, k);
        if (l >= 0) {
            if (l + 1 == k) ans.Add(root.val);
            GetAns(root.right, k - l - 1 -1);
            return l + 1;
        }
        if (r >= 0) {
            if (r + 1 == k) ans.Add(root.val);
            GetAns(root.left, k - r - 1 -1);
            return r + 1;
        }
        // no found
        return -1;
    }
    void GetAns(TreeNode root, int k) {
        if (root == null || k < 0) return;
        if (k == 0) ans.Add(root.val);
        GetAns(root.left, k-1);
        GetAns(root.right, k-1);
    }
}
