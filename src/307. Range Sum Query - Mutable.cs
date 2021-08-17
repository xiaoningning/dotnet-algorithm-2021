// Segment tree (binary tree with sum val)
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
 * Your NumArray object will be instantiated and called as such:
 * NumArray obj = new NumArray(nums);
 * obj.Update(index,val);
 * int param_2 = obj.SumRange(left,right);
 */
