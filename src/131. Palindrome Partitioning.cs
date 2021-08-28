public class Solution {
    // Recursion (DFS) T: O(2^n)
    public IList<IList<string>> Partition(string s) {
        Func<string, bool> isPalindrome = (str) => {
            for (int l = 0, r = str.Length - 1; l < r; l++, r--) if (str[l] != str[r]) return false;
            return true;
        };
        var ans = new List<IList<string>>();
        if (s == "") { ans.Add(new List<string>()); return ans;}
        for (int len = 1; len <= s.Length; len++) {
            string t = s.Substring(0,len);
            if (!isPalindrome(t)) continue;
            var tmp = Partition(s.Substring(len));
            foreach (var lst in tmp) {
                lst.Insert(0, t);
                ans.Add(lst);
            }
        }
        return ans;
    }
    // DFS T: O(2^n)
    public IList<IList<string>> Partition1(string s) {
        Func<string, bool> isPalindrome = (str) => {
            for (int l = 0, r = str.Length - 1; l < r; l++, r--) if (str[l] != str[r]) return false;
            return true;
        };
        var ans = new List<IList<string>>();
        Action<int, List<string>> DFS = null;
        DFS = (start, cur) => {
            if (start == s.Length) ans.Add(new List<string>(cur));
            for (int i = start; i < s.Length; i++) {
                var t = s.Substring(start, i - start + 1);
                if (!isPalindrome(t)) continue;
                cur.Add(t);
                DFS(i+1, cur);
                cur.RemoveAt(cur.Count - 1);
            }
        };
        DFS(0, new List<string>());
        return ans;
    }
}
