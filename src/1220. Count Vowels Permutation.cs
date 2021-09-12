public class Solution {
    // DP v1
    // T: O(n) S: O(n) -> O(1)
    public int CountVowelPermutation1(int n) {
        int kM = (int)Math.Pow(10,9)+7;
        // only calculate count, not string
        string[] vowel = new string[]{"a","e","i","o","u"};
        // # of strings ends with j at permutation i
        // long to avoid overflow int32
        long[,] dp = new long[n,5];
        for (int j = 0; j < 5; j++) dp[0,j] = 1;
        for (int i = 1; i < n; i++) {
            dp[i,0] = (dp[i-1,1] + dp[i-1,2] + dp[i-1,4]) % kM;
            dp[i,1] = (dp[i-1,0] + dp[i-1,2]) % kM;
            dp[i,2] = (dp[i-1,1] + dp[i-1,3]) % kM;
            dp[i,3] = dp[i-1,2] % kM;
            dp[i,4] = (dp[i-1,2] + dp[i-1,3]) % kM;
        }
        int ans = 0;
        for (int j = 0; j < 5; j++) ans = (ans + (int)dp[n-1,j]) % kM;
        return ans;
    }
    // DP v2 save space
    public int CountVowelPermutation(int n) {
        int kM = (int)Math.Pow(10,9)+7;
        // long to avoid overflow int32
        long a= 1, e = 1, i = 1, o = 1,u = 1;
        for (int k = 1; k < n; k++) {
            long aa = (e + i + u) % kM;
            long ee = (a + i) % kM;
            long ii = (e + o) % kM;
            long oo = i % kM;
            long uu = (i + o) % kM;
            a = aa; e = ee; i = ii; o = oo; u = uu;
        }
        long ans = (a + e + i + o + u) % kM;
        return (int)ans;
    }
}
