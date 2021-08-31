public class Solution {
    // DP => DFS + memo
    // T: O(n^2)
    // S: O(n^2)
    public bool WordBreak1(string s, IList<string> wordDict) {
        var memo = new Dictionary<string, bool>();
        Func<string, bool> DFS = null;
        DFS = (str) => {
            if (str.Length == 0) return true;
            if (memo.ContainsKey(str)) return memo[str];
            if (wordDict.Contains(str)) return memo[str] = true;
            for (int len = 1; len <= str.Length; len++) {
                if (wordDict.Contains(str.Substring(0,len)) 
                    && DFS(str.Substring(len))) return memo[str] = true;
            }
            return memo[str] = false;
        };
        return DFS(s);
    }
    // BFS no memo
    public bool WordBreak(string s, IList<string> wordDict) {
        var dict = new HashSet<string>(wordDict);
        var visited = new int[s.Length];
        var q = new Queue<int>();
        q.Enqueue(0);
        while (q.Any()) {
            int start = q.Dequeue();
            if (visited[start] == 1) continue;
            for (int i = start + 1; i <= s.Length; i++) {
                if (dict.Contains(s.Substring(start, i - start))) {
                    q.Enqueue(i);
                    if (i == s.Length) return true;
                }
            }
            visited[start] = 1;
        }
        return false;
    }
}
