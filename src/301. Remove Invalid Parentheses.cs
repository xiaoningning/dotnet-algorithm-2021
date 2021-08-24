public class Solution {
    // DFS
    // T: O(2^(left + right))
    // O: (n^2) n: string.length 
    public IList<string> RemoveInvalidParentheses1(string s) {
        var ans = new List<string>();
        int cnt1 = 0, cnt2 = 0;
        foreach (char c in s) {
            if (c == '(') cnt1++;
            else if (cnt1 > 0 && c == ')') cnt1--;
            else if (c == ')') cnt2++;
        }
        Action<int, int, int, string> DFS = null;
        DFS = (start, left, right, str) => {
            if (left == 0 && right == 0) {
                if (isValid(str)) ans.Add(str);
                return;
            }
            // use start from where invalid char is removed
            // avoid to duplicated scannings
            for (int i = start; i < str.Length; i++) {
                // if the same, such as "())", remove 1st or 2nd is the same string
                // avoid the duplicated results!!!
                if (i != start && str[i] == str[i - 1]) continue;
                if (str[i] == '(' && left > 0) DFS(i, left - 1, right, str.Remove(i, 1));
                if (str[i] == ')' && right > 0) DFS(i, left, right - 1, str.Substring(0, i) + str.Substring(i + 1));
            }
        };
        DFS(0, cnt1, cnt2, s);
        // if no valid result, it should have "" string.
        return ans;
    }
    bool isValid(string s) {
        int cnt = 0;
        foreach (char c in s) {
            if (c == '(') cnt++;
            if (c == ')') cnt--;
            if (cnt < 0) return false;
        }
        return cnt == 0;
    }
    // BFS brute force scan
    public IList<string> RemoveInvalidParentheses(string s) {
        var ans = new List<string>();
        // Set to avoid duplicated results!!!
        var st = new HashSet<string>(){s};
        // each leve of BFS, remove 1 char
        while (st.Any()) {
            var nx = new HashSet<string>();
            foreach (string str in st) {
                if (isValid(str)) ans.Add(str);
                // check all str from this level
                if (ans.Any()) continue; 
                for (int i = 0; i < str.Length; i++) {
                    if (str[i] != '(' && str[i] != ')') continue;
                    nx.Add(str.Remove(i, 1));
                }
            }
            st = nx;
        }
        return ans;
    }
}
