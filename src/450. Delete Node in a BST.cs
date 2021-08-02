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
    // Find, then delete
    public TreeNode DeleteNode1(TreeNode root, int key) {
        if (root == null) return null;
        var ptr = root;
        TreeNode prev = null;
        while (ptr != null) {
            if (ptr.val == key) break;
            prev = ptr;
            ptr = ptr.val > key ? ptr.left : ptr.right;
        }
        if (prev == null) return Del(ptr);
        if (prev.left != null && prev.left.val == key) prev.left = Del(ptr);
        else prev.right = Del(ptr);
        return root;
    }
    TreeNode Del(TreeNode n) {
        if (n == null) return null;
        else if (n.right == null) return n.left;
        else {
            var ptr = n.right;
            while (ptr.left != null) ptr = ptr.left;
            ptr.left = n.left;
            return n.right;
        }
    }
    // Binary search BST
    public TreeNode DeleteNode(TreeNode root, int key) {
        if (root == null) return null;
        if (root.val == key) {
            if (root.right == null) return root.left;
            else {
                var ptr = root.right;
                while (ptr.left != null) ptr = ptr.left;
                ptr.left = root.left;
                return root.right;
            }
        }
        // subtree cases, but still return root
        // just update left/right as the root of subtree
        else if (root.val > key) root.left = DeleteNode(root.left, key);
        else root.right = DeleteNode(root.right, key);
        return root;
    }
    // Binary search BST with recursion on root.right
    public TreeNode DeleteNode2(TreeNode root, int key) {
        if (root == null) return null;
        if (root.val == key) {
            if (root.right == null) return root.left;
            else {
                var ptr = root.right;
                while (ptr.left != null) ptr = ptr.left;
                root.val = ptr.val;
                // after delete root val, ptr should be null
                root.right = DeleteNode(root.right, ptr.val);
            }
        }
        // subtree cases, but still return root
        // just update left/right as the root of subtree
        else if (root.val > key) root.left = DeleteNode(root.left, key);
        else root.right = DeleteNode(root.right, key);
        return root;
    }
}
