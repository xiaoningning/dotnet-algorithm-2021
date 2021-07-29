/**
 * SkipNode
 * l  4  30  -------------------->  nil
 * e  3  30  ---> 50  ----------->  nil
 * v  2  30  ---> 50  ---->70 ----> nil
 * e  1  30 ->40->50 ->60->70 ->90->nil
 * l
 * skipList: each level is sorted. 
 * one ptr to next level of its own
 * ont ptr to the same level of next node
 * T: O(logn) -> worst O(n)
 * S: O(n)
 **/
public class Skiplist {

    public Skiplist() {
        // head ptr point to the first node on the top level
        headPtr = new Node();
    }
    
    public bool Search(int target) {
        for (var ptr = headPtr; ptr != null; ptr = ptr.down) {
            while (ptr.next != null && ptr.next.val < target) ptr = ptr.next;
            if (ptr.next != null && ptr.next.val == target) return true;
        }
        return false;
    }
    
    public void Add(int num) {
        var prevNodes = new Stack<Node>();
        for (var ptr = headPtr; ptr != null; ptr = ptr.down) {
            while (ptr.next != null && ptr.next.val < num) ptr = ptr.next;
            prevNodes.Push(ptr);
        }
        
        Node downPtr = null;
        while (prevNodes.Any()) {
            var cur = prevNodes.Pop();
            cur.next = new Node(num, cur.next, downPtr);
            downPtr = cur.next;
        }
        // new node is the first one of level
        // create new level
        var newLevelPtr = headPtr.next;
        if (num == newLevelPtr.val && newLevelPtr.next != null) {
            while (newLevelPtr.next.down != null) {
                newLevelPtr.down = new Node(num, newLevelPtr.next.down, null);
                newLevelPtr = newLevelPtr.down;
            }
        }
    }
    
    public bool Erase(int num) {
        bool found = false;
        for (var ptr = headPtr; ptr != null; ptr = ptr.down) {
            while (ptr.next != null && ptr.next.val < num) ptr = ptr.next;
            if (ptr.next != null && ptr.next.val == num) { 
                // delete on the current level
                ptr.next = ptr.next.next;
                found = true;
            }
            // go on to delete next level
        }
        return found;
    }
    
    Node headPtr;
}

public class Node {
    public int val;
    public Node down, next;
    public Node(int v = -1, Node nptr = null, Node dptr = null) {
        val = v; next = nptr; down = dptr; 
    }
}

/**
 * Your Skiplist object will be instantiated and called as such:
 * Skiplist obj = new Skiplist();
 * bool param_1 = obj.Search(target);
 * obj.Add(num);
 * bool param_3 = obj.Erase(num);
 */
