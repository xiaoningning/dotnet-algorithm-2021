public class Solution {
    // DFS
    public IList<string> GenerateParenthesis1(int n) {
        var ans = new List<string>();
        Action<int, int, string> DFS = null;
        DFS = (left, right, tmp) => {
            // "(" must be in front of ")" => left must less than right
            if (left < 0 || right < 0 || left > right) return;
            if (left == 0 && right == 0) { ans.Add(new string(tmp)); return;}
            DFS(left - 1, right, tmp + "(");
            DFS(left, right - 1, tmp + ")");
        };
        DFS(n, n, "");
        return ans;
    }
    // Recursion
    public IList<string> GenerateParenthesis(int n) {
        var st = new HashSet<string>();
        // base case for recursion
        if (n == 0) st.Add("");
        else {
            var res = GenerateParenthesis(n - 1);
            for (int k = 0; k < res.Count; k++) {
                string s = res[k];
                for (int i = 0; i < s.Length; i++) {
                    // '(' must be first
                    if (s[i] == '(') {
                        s = s.Insert(i + 1, "()");
                        st.Add(new string(s));
                        s = s.Remove(i + 1, 2);
                    }
                }
                // the case of at index 0
                st.Add("()" + s);
            }
        }
        return new List<string>(st);
    }
}
