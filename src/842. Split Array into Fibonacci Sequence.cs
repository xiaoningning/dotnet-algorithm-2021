public class Solution {
    // DFS + prunning along the calucation
    // Time complexity: O(2^n)
    // Space complexity: O(n)
    public IList<int> SplitIntoFibonacci(string num) {
        var ans = new List<int>();
        Func<int, bool> DFS = null;
        // just use ans in DFS to save space
        DFS = (start) =>{
            if (start >= num.Length) return ans.Count >= 3;
            for (int len = 1; len <= num.Length - start; len++) {
                string t = num.Substring(start, len);
                // overflow int32
                if ((t.Length > 1 && t[0] == '0') || t.Length > 10) break;
                long tVal = Int64.Parse(t);
                if (tVal > Int32.MaxValue) break;
                int val = (int)tVal;
                // check Fibonacci along DFS to avoid TLE
                if (ans.Count >= 2) {
                    // int32 overflow
                    long sum = (long)(ans[ans.Count - 2] + ans[ans.Count - 1]);
                    if (sum > val) continue;
                    else if (sum < val) break; // prunning to save time
                    // else sum == val => fibonacci
                }
                ans.Add(val);
                if (DFS(start + len)) return true;
                ans.RemoveAt(ans.Count - 1);
            }
            return false;
        };
        DFS(0);
        return ans;
    }
}
