public class Solution {
    public int[] SortedSquares(int[] nums) {
        int n = nums.Length, s = 0, e = n - 1, k = n - 1;
        int[] res = new int[n];
        // find the largest one to avoid the find smallest one
        // since the smalleest could be in the middle.
        // the largest one must be one of ends.
        while (k >= 0) {
            if (Math.Abs(nums[e]) >= Math.Abs(nums[s])) {
                res[k--] = (int) Math.Pow(nums[e--], 2);
            }
            else {
                res[k--] = (int) Math.Pow(nums[s++], 2);
            }
        }
        return res;
    }
    
    public int[] SortedSquares1(int[] nums) {
        int n = nums.Length , j = 0, k = 0;
        int[] res = new int[n];
        foreach (int x in nums) if (x < 0) j++;
        int i = j - 1;
        while (i >= 0 && j < n) {
            if (Math.Abs(nums[i]) >= Math.Abs(nums[j])) {
                res[k++] = nums[j] * nums[j];
                j++;
            }
            else {
                res[k++] = nums[i] * nums[i];
                i--;
            }
        }
        while (j < n) {
            res[k] = nums[j] * nums[j];
            k++; j++;
        }
        while (i >= 0) {
            res[k] = nums[i] * nums[i];
            k++; i--;
        }
        return res;
    }
}
