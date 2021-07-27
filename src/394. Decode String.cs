public class Solution {
    public string DecodeString(string s) {
        int i = 0;
        return DecodeString(s, ref i);
    }
    // each level [ ], should a string
    string DecodeString(string s, ref int i) {
        var ans = "";
        var cnt = 1;
        // when ], return the recursive call to upper level
        while (i < s.Length && s[i] != ']') {
            if (s[i] == '[') {
                i++;
                var tmp = DecodeString(s, ref i);
                i++;
                // before [, it should be cnt
                while (cnt-- > 0) ans += tmp;
            }
            else if (char.IsNumber(s[i]))  cnt = GetCnt(s, ref i);
            else ans += GetStr(s, ref i);   
        }
        return ans;
    }
    string GetStr(string s, ref int i) {
        var ans = "";
        while (i < s.Length && char.IsLetter(s[i]))
            ans += s[i++];
        return ans;
    }
    int GetCnt(string s, ref int i) {
        int cnt = 0;
        while (i < s.Length && char.IsNumber(s[i]))
            cnt = cnt * 10 + (s[i++] - '0');
        return cnt == 0 ? 1 : cnt; 
    }
}
