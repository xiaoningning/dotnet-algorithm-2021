public class Solution {
    // Moore voting 
    // if # > n / 2, only one result
    // if # > n / 3, only two results
    public int MajorityElement(int[] nums) {
        int n = nums.Length, cnt = 0, ans = nums[0];
        foreach (int x in nums) {
            if (x == ans) cnt++;
            else if (--cnt == 0) {
                ans = x; cnt = 1;
            }
        }
        // Majority cnt >= 1
        // T: O(n), S: O(1)
        return ans;
    }
    
    // bit cnt
    public int MajorityElement1(int[] nums) {
        int n = nums.Length, ans = 0;
        for (int i = 0; i < 32; i++) {
            int cnt = 0;
            int mask = 1 << i;
            // x < 0 or x >= 0
            foreach (int x in nums) {
                // if count 1 of each bit > n / 2, keep 1 of this bit to ans
                if ((x & mask) != 0 && ++cnt > n / 2) {
                    // Console.WriteLine(x + " : "+ Convert.ToString(x, 2) + " m: " + Convert.ToString(mask, 2));
                    ans |= mask;
                    // Console.WriteLine("ans: " + Convert.ToString(ans, 2));
                    break;
                }
            }
        }
        // T: O(n), S: O(1)
        return ans;
    }
    
    // Divide and conquer 
    // TLE
    public int MajorityElement2(int[] nums) {
        // T: O(nlogn) S: O(logn)
        return GetMajority(nums, 0, nums.Length - 1);
    }
    int GetMajority(int[] nums, int l, int r) {
        if (l == r) return nums[l];
        int mid = l + (r - l) / 2;
        int m1 = GetMajority(nums, l, mid);
        int m2 = GetMajority(nums, mid + 1, r);
        if (m1 == m2) return m1;
        return GetCnt(nums, m1) > GetCnt(nums, m2) ? m1 : m2;
    }
    // TLE
    int GetCnt(int[] nums, int x) {
        // return Array.FindAll(nums, n => n == x).Length;
        // return nums.ToList().Count(n => n == x);
        int cnt = 0;
        foreach (var n in nums) if (n == x) cnt++;
        return cnt;
    }
}
