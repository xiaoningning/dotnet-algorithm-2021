public class Solution {
    public int Evaluate(string expression) {
        var d = new Stack<Dictionary<string, int>>();
        return Parse(expression, d);
    }
    // each layer needs a dictionary
    int Parse(string s, Stack<Dictionary<string, int>> d) {
        // only number case
        if (s[0] == '-' || (s[0] >= '0' && s[0] <= '9')) return Int32.Parse(s);
        // none expr without "()"
        else if (char.IsLetter(s[0])) return d.Peek()[s];
        // expr case with cmd
        else {
            // expr always starts "()"
            s = s.Substring(1, s.Length - 2);
            int cur = 0;
            var cmd = GetToken(s, ref cur);
            if (cmd == "let") {
                var map = d.Any() ? new Dictionary<string, int>(d.Peek()) : new Dictionary<string, int>();
                d.Push(map);
                while (true) {
                    var token = GetToken(s, ref cur);
                    if (cur > s.Length) {
                        var val = Parse(token, d);
                        // Console.WriteLine("p: " + token + " v: " + val);
                        // reach the end, remove one level dict.
                        d.Pop(); 
                        return val;
                    } 
                    var v = Parse(GetToken(s, ref cur), d);
                    d.Peek()[token] = v;
                }
            }
            else if (cmd == "add") return Parse(GetToken(s, ref cur), d) + Parse(GetToken(s, ref cur), d);
            else if (cmd == "mult") return Parse(GetToken(s, ref cur), d) * Parse(GetToken(s, ref cur), d);
            else return 0; // should not be hit
        }
    }
    string GetToken(string s, ref int i) {
        int start = i, end = i + 1, cnt = 1;
        // full expr case "(expr)"
        if (s[i] == '(') {
            while (cnt != 0) {
                if (s[end] == '(') cnt++;
                if (s[end] == ')') cnt--;
                end++;
            }
        }
        // none expr case
        else while (end < s.Length && s[end] != ' ') end++;
        i = end + 1;
        return s.Substring(start, end - start);
    }
}
