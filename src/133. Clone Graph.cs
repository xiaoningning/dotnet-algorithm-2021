/*
// Definition for a Node.
public class Node {
    public int val;
    public IList<Node> neighbors;

    public Node() {
        val = 0;
        neighbors = new List<Node>();
    }

    public Node(int _val) {
        val = _val;
        neighbors = new List<Node>();
    }

    public Node(int _val, List<Node> _neighbors) {
        val = _val;
        neighbors = _neighbors;
    }
}
*/

public class Solution {
    // map to its clone
    Dictionary<Node, Node> m = new Dictionary<Node, Node>();
    // DFS
    public Node CloneGraph1(Node node) {
        if (node == null) return null;
        if (m.ContainsKey(node)) return m[node];
        var n = new Node(node.val);
        m[node] = n;
        foreach (var c in node.neighbors) { 
            // clone edge even if the node is already clone
            n.neighbors.Add(CloneGraph(c));
        }
        // T: O(V+E)
        return n;
    }
    // BFS
    public Node CloneGraph(Node node) {
        if (node == null) return null;
        var q = new Queue<Node>();
        q.Enqueue(node);
        m[node] = new Node(node.val);
        while (q.Any()) {
            var t = q.Dequeue();
            foreach (var c in t.neighbors) {
                if (!m.ContainsKey(c)) {
                    m[c] = new Node(c.val);
                    // c is not cloned yet, put it into queue
                    q.Enqueue(c);
                }
                m[t].neighbors.Add(m[c]);
            }
        }
        // T: O(V+E)
        return m[node];
    }
}
