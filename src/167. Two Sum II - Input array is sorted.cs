public class Solution {
    public int[] TwoSum(int[] numbers, int target) {
        int l = 0, r = numbers.Length - 1;
        while (l < r) {
            int sum = numbers[l] + numbers[r];
            if (sum == target) return new int[]{l+1, r+1};
            else if (sum < target) l++;
            else r--;
        }
        return new int[]{};
    }
}
