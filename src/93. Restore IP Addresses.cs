public class Solution {
    // T: O(3^4)
    public IList<string> RestoreIpAddresses(string s) {
        var ans = new List<string>();
        Action<string, int, string> DFS = null;
        DFS = (str, level, ip) => {
            int len = str.Length;
            if (level == 4) {
                if (len == 0) ans.Add(ip);
                return;
            }
            for (int i = 1; i <= Math.Min(3, s[0] == '0' ? 1 : len); i++) {
                string t = str.Substring(0, i);
                if (Int32.Parse(t) > 255) break;
                DFS(str.Substring(i), level + 1, ip + (level == 0 ? "" : ".") + t);
            }
        };
        DFS(s, 0, "");
        return ans;
    }
    public IList<string> RestoreIpAddresses1(string s) {
        var ans = new List<string>();
        Func<string, bool> isValid = (s) => {
            if (s == "" || (s.Length > 1 && s[0] == '0') || s.Length > 3) return false;
            int a = Int32.Parse(s); 
            return a >= 0 && a <= 255;
        };
        Func<string, int, List<string>> DFS = null;
        DFS = (str, level) => {
            var ans = new List<string>();
            if (level == 1) {
                if (isValid(str)) ans.Add(str);
                return ans;
            }
            for (int i = 1; i <= 3 && i <= str.Length; i++) {
                var ip = str.Substring(0,i);
                if (isValid(ip)) {
                    var t = DFS(str.Substring(i), level - 1);
                    foreach (var s in t) ans.Add(ip + "." + s);
                }
            }
            return ans;
        };
        return DFS(s, 4);
    }
}
