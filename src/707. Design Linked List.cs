public class MyLinkedList {

    /** Initialize your data structure here. */
    public MyLinkedList() {
        head = null; tail = null;
        size = 0;
    }
    
    /** Get the value of the index-th node in the linked list. If the index is invalid, return -1. */
    public int Get(int index) {
        if (index < 0 || index >= size || head == null) return -1;
        var ptr = head;
        while (--index >= 0) ptr = ptr.next;
        return ptr.val;
    }
    
    /** Add a node of value val before the first element of the linked list. After the insertion, the new node will be the first node of the linked list. */
    public void AddAtHead(int val) {
        var t = new Node(val, head);
        head = t;
        if (size == 0) tail = t;
        size++;
    }
    
    /** Append a node of value val to the last element of the linked list. */
    public void AddAtTail(int val) {
        var t = new Node(val, null);
        if (size == 0) {
            tail = t;
            head = t;
            size++;
            return;
        }
        tail.next = t;
        tail = t;
        size++;
    }
    
    /** Add a node of value val before the index-th node in the linked list. If index equals to the length of linked list, the node will be appended to the end of linked list. If index is greater than the length, the node will not be inserted. */
    public void AddAtIndex(int index, int val) {
        if (index > size) return;
        if (index <= 0) { AddAtHead(val); return;}
        if (index == size) {AddAtTail(val);  return;}
        var ptr = head;
        for (int i = 0; i < index - 1; i++) ptr = ptr.next; 
        var t = new Node(val, ptr.next);
        ptr.next = t;
        size++;
    }
    
    /** Delete the index-th node in the linked list, if the index is valid. */
    public void DeleteAtIndex(int index) {
        if (index >= size || index < 0) return;
        if (index == 0) { 
            head = head.next;
            size--;
            return;
        }
        var ptr = head;
        for (int i = 0; i < index - 1; i++) ptr = ptr.next;
        ptr.next = ptr.next.next;
        if (index == size - 1) tail = ptr;
        size--;
    }
    
    Node head;
    Node tail;
    int size;
}

public class Node {
    public int val;
    public Node next;
    public Node(int v, Node node) {
        this.val = v;
        this.next = node;
    }
}

/**
 * Your MyLinkedList object will be instantiated and called as such:
 * MyLinkedList obj = new MyLinkedList();
 * int param_1 = obj.Get(index);
 * obj.AddAtHead(val);
 * obj.AddAtTail(val);
 * obj.AddAtIndex(index,val);
 * obj.DeleteAtIndex(index);
 */
