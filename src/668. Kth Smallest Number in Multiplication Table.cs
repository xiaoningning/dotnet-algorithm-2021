public class Solution {
    public int FindKthNumber(int m, int n, int k) {
        int l = 1, r = m * n + 1;
        while ( l < r) {
            int x = l + (r - l) / 2;
            int cnt = 0, i = m, j = 1;
            while (i >= 1 && j <= n) {
                if (i * j <= x) {
                    cnt += i;
                    j++;   
                }
                else i--;
            }
            if (cnt < k) l = x + 1;
            else r = x;
        }
        return l;
    }
    
    public int FindKthNumber1(int m, int n, int k) {
        int l = 1, r = m * n + 1;
        while ( l < r) {
            int x = l + (r - l) / 2;
            int cnt = 0;
            for (int i = 1; i <= m; i++) cnt += x > i * n ? n : x / i;
            if (cnt < k) l = x + 1;
            else r = x;
        }
        return l;
    }
}
