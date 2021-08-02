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
    public int MinCameraCover1(TreeNode root) {
        var st = DFS(root);
        return st == State.None ? ++ans : ans;
    }
    // DFS to check leave node first
    // parent of leave nodes is cam => it covers itself, children, its parent
    // this option to min # of cams
    State DFS(TreeNode root) {
        if (root == null) return State.Monitored;
        var left = DFS(root.left);
        var right = DFS(root.right);
        if (left == State.None || right == State.None) {
            ans++;
            return State.Cam;
        }
        return (left == State.Cam || right == State.Cam) ? State.Monitored : State.None;
    }
    
    enum State {None, Cam, Monitored}
    
    // Top-Down + Memo in Node value
    public int MinCameraCover(TreeNode root) {
        return MinCam(root, false, false);
    }
    int MN = 1000;
    int MinCam(TreeNode root, bool cam, bool monitored) {
        if (root == null) return 0;
        if (cam) return 1 + MinCam(root.left, false, true) + MinCam(root.right, false, true);
        if (monitored) {
            var noCam = MinCam(root.left, false, false) + MinCam(root.right, false, false);
            var addCam = 1 + MinCam(root.left, false, true) + MinCam(root.right, false, true);
            return Math.Min(noCam, addCam);
        }
        
        if (root.val != 0) return root.val; // default val: 0
        var rootCam = 1 + MinCam(root.left, false, true) + MinCam(root.right, false, true);
        var leftCam = root.left == null ? MN : MinCam(root.left, true, false) + MinCam(root.right, false, false);
        var rightCam = root.right == null ? MN : MinCam(root.left, false, false) + MinCam(root.right, true, false);
        // memo min # of cam in the node val
        return root.val = new int[]{rootCam, leftCam, rightCam}.Min();
    }
}
