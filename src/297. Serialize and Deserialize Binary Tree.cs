/**
 * Definition for a binary tree node.
 * public class TreeNode {
 *     public int val;
 *     public TreeNode left;
 *     public TreeNode right;
 *     public TreeNode(int x) { val = x; }
 * }
 */
public class Codec {

    // Encodes a tree to a single string.
    public string serialize(TreeNode root) {
        string ans = "";
        serialize(root, ref ans);
        return ans;
    }
    void serialize(TreeNode root, ref string ans) {
        if (root == null)  { 
            ans += "# ";
            return;
        }
        ans += root.val + " ";
        serialize(root.left, ref ans);
        serialize(root.right, ref ans);
    }
    // Decodes your encoded data to tree.
    public TreeNode deserialize(string data) {
        int i = 0;
        return deserialize(data, ref i);
    }
    TreeNode deserialize(string s, ref int i) { 
        var t = GetToken(s, ref i);
        if (t == "#" || i >= s.Length) return null;
        
        var root = new TreeNode(Int32.Parse(t));
        root.left = deserialize(s, ref i);
        root.right = deserialize(s, ref i);
        return root;
    }
    string GetToken(string s, ref int i) {
        string t = "";
        while (i < s.Length && s[i] != ' ') t += s[i++];
        i++; // skip " "
        return t;
    }
}

// Your Codec object will be instantiated and called as such:
// Codec ser = new Codec();
// Codec deser = new Codec();
// TreeNode ans = deser.deserialize(ser.serialize(root));
