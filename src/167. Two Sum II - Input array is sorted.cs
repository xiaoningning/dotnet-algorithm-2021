public class Solution {
    // two pointers
    public int[] TwoSum1(int[] numbers, int target) {
        int l = 0, r = numbers.Length - 1;
        while (l < r) {
            int sum = numbers[l] + numbers[r];
            if (sum == target) return new int[]{l+1, r+1};
            else if (sum < target) l++;
            else r--;
        }
        return new int[]{};
    }
    // binary search
    public int[] TwoSum(int[] numbers, int target) {
        int n = numbers.Length;
        for (int i = 0; i< n; i++) {
            int t = target - numbers[i];
            int l = i + 1, r = n;
            while (l < r) {
                int m = l + (r - l) / 2;
                if (numbers[m] == t) return new int[]{i+1, m+1};
                if (numbers[m] < t) l = m + 1;
                else r = m;
            }
        }
        return new int[]{};
    }
}
