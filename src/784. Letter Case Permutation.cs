public class Solution {
    // DFS
    // Time complexity: O(n*2^s.Length)
    // Space complexity: O(n) + O(n*2^s.Length)
    public IList<string> LetterCasePermutation1(string s) {
        int n = s.Length;
        int[] used = new int[n];
        var ans = new List<string>();
        Action<int, List<string>> DFS = null;
        DFS = (start, cur) => {
            if (start == n ) {
                foreach (var s in cur) ans.Add(new string(s));
                return;
            }
            for (int i = start ; i < n; i++) {
                if (used[i] == 1) continue;
                var nx = new List<string>();
                if (char.IsLetter(s[i])) { 
                    foreach (var str in cur) { nx.Add(str + char.ToLower(s[i])); nx.Add(str + char.ToUpper(s[i])); }
                }
                else foreach (var str in cur) nx.Add(str + s[i]);
                used[i] = 1;
                DFS(start + 1, nx);
            }
        };
        DFS(0, new List<string>{""});
        return ans;
    }
    // DFS v2
    public IList<string> LetterCasePermutation2(string s) {
        int n = s.Length;
        var ans = new List<string>();
        Action<int, List<string>> DFS = null;
        DFS = (i, cur) => {
            if (i == n ) {
                foreach (var s in cur) ans.Add(new string(s));
                return;
            }
            var nx = new List<string>();
            if (char.IsLetter(s[i])) { 
                foreach (var str in cur) { nx.Add(str + char.ToLower(s[i])); nx.Add(str + char.ToUpper(s[i])); }
            }
            else foreach (var str in cur) nx.Add(str + s[i]);
            DFS(i + 1, nx);
        };
        DFS(0, new List<string>{""});
        return ans;
    }
    // DFS v3
    public IList<string> LetterCasePermutation3(string s) {
        int n = s.Length;
        var ans = new List<string>();
        Action<int, char[]> DFS = null;
        DFS = (i, cur) => {
            if (i == n ) { ans.Add(new string(cur)); return; }
            DFS(i + 1, cur);
            if (char.IsLower(cur[i])) { 
                cur[i] = char.ToUpper(cur[i]); 
                DFS(i + 1, cur);
            }
            else if (char.IsUpper(cur[i])) { 
                cur[i] = char.ToLower(cur[i]); 
                DFS(i + 1,  cur);
            }
        };
        DFS(0, s.ToCharArray());
        return ans;
    }
    // bit mask
    public IList<string> LetterCasePermutation(string s) {
        int cnt = 0;
        foreach (var c in s) if (c > '9') cnt++;
        var ans = new List<string>();
        // state: j of letters used or not
        for (int state = 0; state < 1 << cnt; state++) {
            int j = 0;
            string cur = "";
            foreach (char c in s) {
                if (c >= '0' && c <= '9') cur += c;
                else {
                    // i of letter is used, then lower, otherwise always Upper
                    if ((state & 1 << j++) > 0) cur += char.ToLower(c);
                    else cur += char.ToUpper(c);
                }
            }
            ans.Add(cur);
        }
        return ans;
    }
}
