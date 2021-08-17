// brute force => TLE
public class NumArray2 {
    int[] data;
    public NumArray2(int[] nums) {
        data = (int[]) nums.Clone();
    }
    public void Update(int index, int val) {
        data[index] = val;
    }
    // inclusive on left
    public int SumRange(int left, int right) {
        int sum = 0;
        for (int i = left; i <= right; i++) sum += data[i];
        return sum;
    }
}

// Segment tree (Binary tree with sum val)
// T: O(logn)
public class NumArray {
    SegmentTreeNode root;
    public NumArray(int[] nums) {
        root = BuildTree(0, nums.Length - 1, nums);
    }
    public void Update(int index, int val) {
        UpdateTree(root, index, val);
    }
    // inclusive on left
    public int SumRange(int left, int right) {
        return GetSum(root, left, right);
    }
    int GetSum(SegmentTreeNode root, int i, int j) {
        if (root.start == i && root.end == j) return root.sum;
        int mid = root.start + (root.end - root.start) / 2;
        if (j <= mid) return GetSum(root.left, i, j);
        else if (i > mid) return GetSum(root.right, i, j);
        else return GetSum(root.left, i, mid) + GetSum(root.right, mid + 1, j);
    }
    void UpdateTree(SegmentTreeNode root, int idx, int val) {
        if (root.start == idx && root.end == idx) { root.sum = val; return; }
        int mid = root.start + (root.end - root.start) / 2;
        if (idx <= mid) UpdateTree(root.left, idx, val);
        if (idx > mid) UpdateTree(root.right, idx, val);
        root.sum = root.left.sum + root.right.sum;
    }
    SegmentTreeNode BuildTree(int s, int e, int[] nums) {
        if (s == e) return new SegmentTreeNode(s, e, nums[s]);
        int mid = s + (e - s) / 2;
        var left = BuildTree(s, mid, nums);
        var right = BuildTree(mid+1, e, nums);
        return root = new SegmentTreeNode(s, e, left.sum + right.sum, left, right);
    }
}
public class SegmentTreeNode {
    public int start, end, sum;
    public SegmentTreeNode left, right;
    public SegmentTreeNode(int s, 
                           int e, 
                           int val, 
                           SegmentTreeNode l = null, 
                           SegmentTreeNode r = null) {
        start = s; end = e; sum = val;
        left = l; right = r;
    }
}
// Fenwick Tree (Binary Index Tree)
// T: O(logn)
public class NumArray1 {
    FenwickTree tree;
    int[] data;
    public NumArray1(int[] nums) {
        data = (int[]) nums.Clone();
        tree = new FenwickTree(nums.Length);
        for (int i = 0; i < nums.Length; i++) 
            tree.Update(i + 1, nums[i]);
    }
    public void Update(int index, int val) {
        int diff = val - data[index];
        tree.Update(index + 1, diff);
        data[index] = val;
    }
    // inclusive on left
    public int SumRange(int left, int right) {
        return tree.GetSum(right + 1) - tree.GetSum(left);
    }
}
public class FenwickTree {
    int[] bitSums;
    // low bit of index => how many nums in this index
    // x & (x ^ (x-1))  == x & -x
    int lowBit(int x) {
        return x & (x ^ (x-1));
        // return x & (-x);
    }
    public FenwickTree (int n) {
        // append extra 0 for index as [1,n] of nums
        bitSums = new int[n+1];
    }
    public void Update(int i, int val) {
        for (int j = i; j < bitSums.Length; j += lowBit(j))
            bitSums[j] += val;
    }
    public int GetSum(int i) {
        int sum = 0;
        for (int j = i; j > 0; j -= lowBit(j))
            sum += bitSums[j];
        return sum;
    }
}
/**
Fenwick Tree
C1 = A1
C2 = A1 + A2
C3 = A3
C4 = A1 + A2 + A3 + A4
C5 = A5
C6 = A5 + A6
C7 = A7
C8 = A1 + A2 + A3 + A4 + A5 + A6 + A7 + A8
idx             binary        low bit
1               0001          1
2               0010          2
3               0011          1
4               0100          4
5               0101          1
6               0110          2
7               0111          1
8               1000          8
*/

/**
 * Your NumArray object will be instantiated and called as such:
 * NumArray obj = new NumArray(nums);
 * obj.Update(index,val);
 * int param_2 = obj.SumRange(left,right);
 */
