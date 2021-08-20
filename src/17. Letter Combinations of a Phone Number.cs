public class Solution {
    string[] d = new string[10]{" ", "", "abc", 
                                "def", "ghi", "jkl", "mno",
                                "pqrs", "tuv", "wxyz"};
    // DFS
    public IList<string> LetterCombinations1(string digits) {
        var ans = new List<string>();
        if (string.IsNullOrEmpty(digits)) return ans;
        Action<int, string> DFS = null;
        DFS = (i, tmp) => {
            if (i == digits.Length) { ans.Add(new string(tmp));  return; }
            foreach (char c in d[digits[i] - '0']) DFS(i + 1, tmp + c);
        };
        DFS(0, "");
        return ans;
    }
    // BFS
    public IList<string> LetterCombinations(string digits) {
        var ans = new List<string>();
        if (string.IsNullOrEmpty(digits)) return ans;
        ans.Add(""); // base
        for (int i = 0; i < digits.Length; i++) {
            var tmp = new List<string>();
            foreach (char c in d[digits[i] - '0'])
                foreach (var s in ans) tmp.Add(s + c);
            ans = new List<string>(tmp);
        }
        return ans;
    }
}
