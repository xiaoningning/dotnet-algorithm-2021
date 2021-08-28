public class Solution {
    // DFS
    // Time complexity: O(4^n) <= +/-/*/no-op
    // Space complexity: O(n)
    public IList<string> AddOperators(string num, int target) {
        var ans = new List<string>();
        Action<int, int, int, string> DFS = null;
        // need prev for * case
        DFS = (start, prev, val, cur) => {
            if (start == num.Length) {
                if (val == target) ans.Add(new string(cur));
                return;
            }
            for (int i = 1; i <= num.Length - start; i++) {
                string t = num.Substring(start, i);
                // skip "05"
                if (t.Length > 1 && t[0] == '0') return;
                long tVal = Int64.Parse(t);
                // if num string is very long, it could be overflow int32
                if (tVal > Int32.MaxValue) break;
                int curVal = (int)tVal;
                if (cur.Length != 0) {
                    DFS(start + i, curVal, val + curVal, cur + "+" + t);
                    DFS(start + i, -curVal, val - curVal, cur + "-" + t);
                    DFS(start + i, prev * curVal, val - prev + prev * curVal, cur + "*" + t);
                }
                else DFS(start + i, curVal, curVal, t);
            }
        };
        DFS(0,0,0,"");
        return ans;
    }
}
