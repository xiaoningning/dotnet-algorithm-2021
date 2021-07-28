public class Solution {
    // moore voting if  # n / 3, then could be 2 results.
    public IList<int> MajorityElement(int[] nums) {
        // m1 and m2 should be different
        int n = nums.Length, cnt1 = 0, cnt2 = 0, m1 = 0, m2 = 1;
        foreach (int x in nums) {
            if (x == m1) cnt1++;
            else if (x == m2) cnt2++;
            else if (cnt1 == 0){
                m1 = x; cnt1++;
            }
            else if (cnt2 == 0) {
                m2 = x; cnt2++;
            }
            else {
                cnt1--; cnt2--;
            }
        }
        var ans = new List<int>();
        if (Array.FindAll(nums, x => x == m1).Length > (n / 3)) ans.Add(m1);
        if (Array.FindAll(nums, x => x == m2).Length > (n / 3)) ans.Add(m2);
        return ans;
    }
}
