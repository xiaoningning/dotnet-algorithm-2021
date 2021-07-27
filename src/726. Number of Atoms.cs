public class Solution {
    public string CountOfAtoms(string formula) {
        int i = 0;
        string res = "";
        var d = CountOfAtoms(formula, ref i);
        var ks = d.Keys.ToList().OrderBy(k => k);
        Console.WriteLine(string.Join(',', ks));
        foreach (var k in ks) {
            res += k + (d[k] > 1 ? d[k].ToString() : ""); 
        }
        return res;
    }
    Dictionary<string, int> CountOfAtoms(string s, ref int i) {
        var res = new Dictionary<string, int>();
        while (i < s.Length) {
            if (s[i] == '(') {
                i++;
                var tmpD = CountOfAtoms(s, ref i);
                foreach (var kv in tmpD) {
                    res[kv.Key] = kv.Value + (res.ContainsKey(kv.Key) ? res[kv.Key] : 0);
                }   
            }
            else if (s[i] == ')') {
                i++;
                var cnt = GetCnt(s, ref i);
                var ks = res.Keys.ToList();
                foreach (var k in ks) res[k] *= cnt;
                return res;
            }
            else {
                var atom = GetAtom(s, ref i);
                var cnt = GetCnt(s, ref i);
                res[atom] = cnt + (res.ContainsKey(atom) ? res[atom] : 0);
            }
        }
        return res;
    }
    string GetAtom(string s, ref int i) {
        string res = "" + s[i++]; // first is UpperCase
        while (i < s.Length && (s[i] >= 'a' && s[i] <= 'z')) res += s[i++];
        return res;
    }
    int GetCnt(string s, ref int i) {
        int cnt = 0;
        while (i < s.Length && s[i] >= '0' && s[i] <= '9') {
            cnt = cnt * 10 + (s[i++] - '0');
        }
        return cnt == 0 ? 1 : cnt;
    }
}
