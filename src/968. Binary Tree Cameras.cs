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
    // Greedy algorithm
    int ans = 0;
    public int MinCameraCover(TreeNode root) {
        var st = DFS(root);
        return st == State.None ? ++ans : ans;
    }
    // DFS to check leave node first
    // root of leave nodes is cam => min # of cam
    State DFS(TreeNode root) {
        if (root == null) return State.Covered;
        var left = DFS(root.left);
        var right = DFS(root.right);
        if (left == State.None || right == State.None) {
            ans++;
            return State.Cam;
        }
        return (left == State.Cam || right == State.Cam) ? State.Covered : State.None;
    }
    enum State {None, Cam, Covered}
}
