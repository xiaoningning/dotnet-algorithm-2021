/*
// Definition for a Node.
public class Node {
    public int val;
    public Node next;
    public Node random;
    
    public Node(int _val) {
        val = _val;
        next = null;
        random = null;
    }
}
*/

public class Solution {
    Dictionary<Node, Node> m = new Dictionary<Node, Node>();
    public Node CopyRandomList(Node head) {
        if (head == null) return null;
        if (m.ContainsKey(head)) return m[head];
        m[head] = new Node(head.val);
        m[head].next = CopyRandomList(head.next);
        m[head].random = CopyRandomList(head.random);
        return m[head];
    }
}
