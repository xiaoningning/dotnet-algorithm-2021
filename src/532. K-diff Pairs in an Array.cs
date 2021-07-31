public class Solution {
    public int FindPairs(int[] nums, int k) {
        int ans = 0;
        var cnt = new Dictionary<int,int>();
        foreach(int n in nums){
            if (!cnt.ContainsKey(n)) cnt[n] = 0;
            cnt[n]++;
        }
        foreach (int i in cnt.Keys) {
            // only unique pairs
            if ( (k > 0 && cnt.ContainsKey(i + k)) || (k == 0 && cnt[i] > 1)) ans++;
        }
        return ans;
    }
}
