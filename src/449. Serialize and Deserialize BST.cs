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
    string ans = "";
    public string serialize(TreeNode root) {
        if (root == null) return  ans += "# ";
        ans += root.val + " ";
        serialize(root.left);
        serialize(root.right);
        return ans;
    }

    // Decodes your encoded data to tree.
    int i = 0;
    public TreeNode deserialize(string data) {
        return deserialize(data, ref i);
    }
    TreeNode deserialize(string s, ref int i) {
        var t = "";
        while (i < s.Length && s[i] != ' ') t += s[i++];
        i++; // skip ' '
        if (t == "#") return null;
        Console.WriteLine(t);
        var node = new TreeNode(Int32.Parse(t));
        node.left = deserialize(s, ref i);
        node.right = deserialize(s, ref i);
        return node;
    }
}

// Your Codec object will be instantiated and called as such:
// Codec ser = new Codec();
// Codec deser = new Codec();
// String tree = ser.serialize(root);
// TreeNode ans = deser.deserialize(tree);
// return ans;
