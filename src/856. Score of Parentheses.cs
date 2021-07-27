public class Solution {
    public int ScoreOfParentheses(string s) {
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
    
    public int ScoreOfParentheses3(string s) {
        int res = 0, cnt = 0;
        for (int i = 0; i < s.Length; i++) {
            cnt += s[i] == '(' ? 1 : -1;
            // () => 2^0, (()) => 2^1, ((())) => 2^2
            if (s[i] == ')' && s[i-1] == '(') res += 1 << cnt;
        }
        // O(n)
        return res;
    }
    
    public int ScoreOfParentheses2(string s) {
        if (s == "") return 0;
        if (s == "()") return 1;
        int cnt = 0;
        for (int i = 0; i < s.Length - 1; i++) {
            cnt += (s[i] == '(') ? 1 : -1;
            if (cnt == 0) // balance case (A)(B) 
                return ScoreOfParentheses(s.Substring(0, i - 0 + 1)) + ScoreOfParentheses(s.Substring(i+1));
        }
        // O(n^2)
        // (A) case => A*2
        return 2 * ScoreOfParentheses(s.Substring(1, s.Length - 1));
    }
    
    public int ScoreOfParentheses1(string s) {
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
}
