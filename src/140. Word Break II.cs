public class Solution {
    // DFS + memo
    // Time complexity: O(2^n)
    // Space complexity: O(2^n)
    public IList<string> WordBreak(string s, IList<string> wordDict) {
        var memo = new Dictionary<string, List<string>>();
        var ans = new List<IList<string>>();
        Func<string, List<string>> DFS = null;
        DFS = (str) => {
            if (memo.ContainsKey(str)) return memo[str];
            var res = new List<string>();
            // need to add empty str as result
            // since empty return list => not breakable
            if (str == "") { res.Add(str); return res; }
            // dict word can be further splitted
            for (int len = 1; len <= str.Length; len++) {
                string t = str.Substring(0,len);
                if (!wordDict.Contains(t)) continue;
                var rem = DFS(str.Substring(len));
                foreach (string a in rem) res.Add(t + (a == "" ? "" : " ") + a);
            }
            return memo[str] = res;
        };
        return DFS(s);
    }
}
