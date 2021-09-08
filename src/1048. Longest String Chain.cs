public class Solution {
    // DP
    // Time complexity: O(n^2*l)
    // Space complexity: O(n)
    public int LongestStrChain1(string[] words) {
        int n = words.Length;
        var t = new List<string>(words);
        t.Sort((x,y) => { 
            if (x.Length == y.Length) return x.CompareTo(y);
            else return x.Length - y.Length;
        });
        Func<string,string,bool> isPred = null;
        isPred = (x,y) => {
            if (y.Length != x.Length + 1) return false;
            int i = 0;
            for (; i < x.Length; i++) if (x[i] != y[i]) break;
            return x.Substring(i) == y.Substring(i+1);
        };
        int[] dp = new int[n];
        Array.Fill(dp, 1);
        for (int i = 0; i < n; i++) {
            for(int j = 0; j < i; j++) {
                if (!isPred(t[j], t[i])) continue;
                dp[i] = Math.Max(dp[i], dp[j] + 1);
            }
        }
        return dp.Max();
    }
    // DP
    // T: O(n*l)
    public int LongestStrChain(string[] words) {
        int n = words.Length, ans = 1;
        var t = new List<string>(words).OrderBy(x => x.Length);
        var dp = new Dictionary<string,int>();
        foreach (string w in t) {
            dp[w] = 1;
            for (int i = 0; i < w.Length; i++) {
                string pre = w.Substring(0,i) + w.Substring(i+1);
                if (!dp.ContainsKey(pre)) continue;
                dp[w] = Math.Max(dp[w], dp[pre] + 1);
                ans = Math.Max(ans, dp[w]);
            }
        }
        return ans;
    }
}
