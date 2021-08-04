public class Solution {
    // x >= 0
    public int MySqrt1(int x) {
        if (x <= 1) return x;
        // s => long, since x can int32.maxvalue
        for (long s = 1; s <= x; s++) {
            if ( s * s > (long)x) return (int)(s - 1);
        }
        // T: O(sqrt(x))
        return -1;
    }
    // Binary search
    public int MySqrt(int x) {
        long l = 1;
        // handle x = 1 => r = x + 1
        long r = (long)x + 1;
        while (l < r) {
            long m = l + (r - l) / 2;
            if (m * m > (long)x) r = m;
            else l = m + 1;
        }
        return (int)l - 1;
    }
}
