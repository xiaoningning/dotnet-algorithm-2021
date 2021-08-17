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
    public int KthSmallest1(TreeNode root, int k) {
        if (root == null) return -1;
        int cnt = GetCnt(root.left);
        if (k <= cnt) return KthSmallest(root.left, k);
        else if (k > cnt + 1) return KthSmallest(root.right, k - cnt - 1);
        return root.val;
    }
    int GetCnt(TreeNode root) {
        if (root == null) return 0;
        return GetCnt(root.left) + 1 + GetCnt(root.right);
    }
    // if tree is updated so ofen, keep cnt in the node.  it is faster
    // T: O(logn)
    public int KthSmallest(TreeNode root, int k) {
        if (root == null) return -1;
        var myTree = BuildTree(root);
        return getSmallest(myTree, k);
    }
    int getSmallest(Node root, int k) {
        if (root.left != null) {
           int cnt = root.left.cnt;
            if (k <= cnt) return getSmallest(root.left, k);
            else if (k > cnt + 1) return getSmallest(root.right, k - cnt - 1);
            return root.val; 
        }
        else return k == 1 ? root.val : getSmallest(root.right, k - 1);
        
    }
    Node BuildTree(TreeNode root) {
        if (root == null) return null;
        var n = new Node(root.val);
        n.left = BuildTree(root.left);
        n.right = BuildTree(root.right);
        if (n.left != null) n.cnt += n.left.cnt;
        if (n.right != null) n.cnt += n.right.cnt;
        return n;
    }
}
// Binary tree with cnt to avoid repeated counting
public class Node {
    public int val, cnt; // cnt = cnt(left) + cnt(right) + 1
    public Node left, right;
    public Node(int v, int c = 1, Node l = null, Node r = null) {
        this.val = v;  this.cnt = c;
        left = l; right = r;
    }
}
