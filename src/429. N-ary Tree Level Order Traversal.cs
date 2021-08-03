/*
// Definition for a Node.
public class Node {
    public int val;
    public IList<Node> children;

    public Node() {}

    public Node(int _val) {
        val = _val;
    }

    public Node(int _val, IList<Node> _children) {
        val = _val;
        children = _children;
    }
}
*/

public class Solution {
    List<IList<int>> ans = new List<IList<int>>();
    // recursion
    public IList<IList<int>> LevelOrder1(Node root) {
        LevelOrder(root, 0);
        return ans;
    }
    void LevelOrder(Node root, int level) {
        if (root == null) return;
        if (ans.Count < level + 1) ans.Add(new List<int>());
        ans[level].Add(root.val);
        foreach (var c in root.children) {
            LevelOrder(c, level + 1);
        }
    }
    
    // Iterative
    public IList<IList<int>> LevelOrder(Node root) {
        if (root == null) return ans;
        var q = new Queue<Node>();
        q.Enqueue(root);
        while (q.Any()) {
            int size = q.Count;
            var t = new List<int>();
            for (int i = 0; i < size; i++) {
                var n = q.Dequeue();
                t.Add(n.val);
                foreach (var c in n.children) q.Enqueue(c);
            }
            ans.Add(t);
        }
        return ans;
    }
}
