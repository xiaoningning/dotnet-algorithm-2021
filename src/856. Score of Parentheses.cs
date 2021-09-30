public class Solution {
    // monotonic stack
    public int ScoreOfParentheses4(string s) {
        int res = 0;
        var st = new Stack<int>();
        foreach (var c in s) {
            if (c == '(') {
                st.Push(res);
                res = 0;
            }
            else res = st.Pop() + Math.Max(1, res * 2); // '()' => 1
        }
        // O(n)
        return res;
    }
    // array linear count
    public int ScoreOfParentheses3(string s) {
        int res = 0, cnt = 0;
        for (int i = 0; i < s.Length; i++) {
            cnt += s[i] == '(' ? 1 : -1;
            // () => 2^0, (()) => 2^1, ((())) => 2^2
            // 1 << x = x * 2 if x >= 1; 1 << x = 1 if x == 0
            if (s[i] == ')' && s[i-1] == '(') res += 1 << cnt;
        }
        // O(n)
        return res;
    }
    // recursion v1
    public int ScoreOfParentheses2(string s) {
        // base case
        if (s == "") return 0;
        if (s == "()") return 1;
        int cnt = 0;
        for (int i = 0; i < s.Length - 1; i++) {
            cnt += (s[i] == '(') ? 1 : -1;
            // balance case (A)(B)
            if (cnt == 0) return ScoreOfParentheses(s.Substring(0, i - 0 + 1)) + ScoreOfParentheses(s.Substring(i+1));
        }
        // O(n^2)
        // (A) case => A*2
        return 2 * ScoreOfParentheses(s.Substring(1, s.Length - 1));
    }
    // recursion v2 is the same as loop
    public int ScoreOfParentheses111(string s) {
        int i = 0;
        return GetScore(s, ref i);
    }
    int GetScore(string s, ref int i) {
        int res = 0;
        while (i < s.Length) {
            if (s[i] == '(') {
                i++;
                res += GetScore(s, ref i);
            }
            else { // s[i] == ')'
                i++;
                res = res == 0 ? 1: res * 2;
                break;
            }
        }
        return res;
    }
    // recursion v3 is the same as loop
    public int ScoreOfParentheses(string s) {
        int ans = 0;
        Action<int,int> GetScore = null;
        GetScore = (i,cnt) => {
            if (i == s.Length) return;
            cnt += s[i] == '(' ? 1 : -1;
            if (s[i] == ')' && s[i-1] == '(')  ans += 1 << cnt; 
            GetScore(i+1, cnt);
        };
        GetScore(0,0);
        return ans;
    }
}
