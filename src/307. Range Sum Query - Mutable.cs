// Segment tree (Binary tree with sum val)
// Fenwick Tree (Binary Index Tree)
// T: O(logn)
public class NumArray {
    FenwickTree tree;
    int[] data;
    public NumArray(int[] nums) {
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
