public class Solution {
    public int MinEatingSpeed(int[] piles, int h) {
        if (piles.Length == 0) return 0;
        // h >= piles.length
        // speed can be 1  if h is big 
        // or max itself if h is small
        int l = 1, r = piles.Max() + 1;
        while (l < r) {
            int m = l + (r - l) / 2;   
            int cnt = 0;
            foreach (var n in piles) cnt += (n + m - 1) / m;
            if (cnt <= h) r = m;
            else l = m + 1;
        }
        // T: O(nlogh)
        return r;
    }
}
